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
    /// describes a contract which can get & set service object
    /// </summary>
    /// <typeparam name="T">service type</typeparam>
    public interface IHasService<T>
    {
        /// <summary>
        /// gets or sets the service configurations
        /// </summary>
        T Service { get; set; }
    }
}
