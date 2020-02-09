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

        // gets the JSON response settings and formatting
        private static JsonSerializerSettings GetJsonSettings() => new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
    }
}