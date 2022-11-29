using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Gravity.Abstraction.Extensions
{
    internal static class JTokenExtensions
    {
        /// <summary>
        /// Gets a child <see cref="JToken"/> by name comparison.
        /// </summary>
        /// <param name="driverParamsToken">This <see cref="JToken"/> instance.</param>
        /// <param name="name">name by which to find a child token</param>
        /// <param name="stringComparison">comparison method</param>
        /// <returns>token instance or null if not found</returns>
        public static JToken ByName(this JToken driverParamsToken, string name, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            if (driverParamsToken == null)
            {
                throw new ArgumentNullException(nameof(driverParamsToken));
            }

            // convert into string/object collection
            var collection = driverParamsToken.ToObject<Dictionary<string, object>>();
            if (collection == null)
            {
                return driverParamsToken;
            }

            // extract pair and validate
            var pair = collection.FirstOrDefault(i => i.Key.Equals(name, stringComparison));
            if (EqualityComparer<KeyValuePair<string, object>>.Default.Equals(pair, default))
            {
                return default;
            }

            var resultToken = new Dictionary<string, object> { [pair.Key] = pair.Value };
            var json = JsonConvert.SerializeObject(resultToken, Formatting.None, GetJsonSettings());

            // return as token
            return JToken.Parse(json);
        }

        /// <summary>
        /// Gets a value for a dictionary, or default value if not found.
        /// </summary>
        /// <typeparam name="T">The type of the returned value.</typeparam>
        /// <param name="dictionary">The dictionary to get from.</param>
        /// <param name="path">JSON path to find by.</param>
        /// <param name="defaultValue">The default value to return.</param>
        /// <returns>A value from the dictionary, or default value if not found.</returns>
        public static T Find<T>(this IDictionary<string, object> dictionary, string path, T defaultValue)
        {
            try
            {
                // setup
                var json = System.Text.Json.JsonSerializer.Serialize(dictionary, GetJsonOptions());
                var token = JToken.Parse(json);

                // build
                var value = token.SelectToken(path);

                // exit conditions
                if (value == null)
                {
                    return defaultValue;
                }

                // setup
                var isJson = IsJson($"{value}");

                // get
                if (isJson)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<T>($"{value}");
                }
                return value.ToObject<T>();
            }
            catch (Exception e) when (e != null)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Transforms a dictionary into an object.
        /// </summary>
        /// <typeparam name="T">The type to transform into.</typeparam>
        /// <param name="dictionary">The dictionary to transform.</param>
        /// <returns>A new object type.</returns>
        public static T Transform<T>(this IDictionary<string, object> dictionary)
        {
            // setup
            var json = System.Text.Json.JsonSerializer.Serialize(dictionary, GetJsonOptions());

            // get
            return System.Text.Json.JsonSerializer.Deserialize<T>(json, GetJsonOptions());
        }

        // gets the JSON response settings and formatting
        private static JsonSerializerSettings GetJsonSettings() => new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        // gets the JSON response options and formatting
        private static System.Text.Json.JsonSerializerOptions GetJsonOptions()
        {
            return new System.Text.Json.JsonSerializerOptions
            {
                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
                DictionaryKeyPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
                WriteIndented = false
            };
        }

        // gets the JSON response options and formatting
        private static bool IsJson(string json)
        {
            try
            {
                System.Text.Json.JsonDocument.Parse(json);
                return true;
            }
            catch (Exception e) when (e != null)
            {
                return false;
            }
        }
    }
}