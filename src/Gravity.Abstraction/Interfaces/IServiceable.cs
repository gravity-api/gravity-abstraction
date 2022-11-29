/*
 * CHANGE LOG
 * 
 * 2019-01-10
 *    -    fix: all warnings
 */
namespace Gravity.Abstraction.Interfaces
{
    /// <summary>
    /// provides mechanism to transform ServiceParams into DriverService
    /// </summary>
    /// /// <typeparam name="T">The DriverService type</typeparam>
    public interface IServiceable<out T>
    {
        /// <summary>
        /// Generate DriverService for the current driver based of the params object
        /// </summary>
        /// <param name="driverPath">The full path to the directory containing the executable providing the service
        /// to drive the browser.</param>
        /// <returns>DriverService</returns>
        T ToDriverService(string driverPath);
    }
}
