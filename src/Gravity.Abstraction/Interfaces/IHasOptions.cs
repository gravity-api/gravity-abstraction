/*
 * CHANGE LOG
 * 
 * 2019-01-06
 *    -    add: create IHasOptions interface
 *    - modify: add xml comments
 */
namespace Gravity.Abstraction.Interfaces
{
    /// <summary>
    /// describes a contract which can get & set options object
    /// </summary>
    /// <typeparam name="T">options type</typeparam>
    public interface IHasOptions<T>
    {
        /// <summary>
        /// gets or sets the options
        /// </summary>
        T Options { get; set; }
    }
}
