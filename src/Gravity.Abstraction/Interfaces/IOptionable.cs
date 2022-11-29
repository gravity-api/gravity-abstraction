using OpenQA.Selenium;

namespace Gravity.Abstraction.Interfaces
{
    /// <summary>
    /// Provides mechanism to transform <see cref="Contracts.DriverOptionsParams"/> into <see cref="DriverOptions"/>.
    /// </summary>
    /// <typeparam name="T">The <see cref="DriverOptions"/> type</typeparam>
    public interface IOptionable<out T> where T : DriverOptions
    {
        /// <summary>
        /// Generate <see cref="DriverOptions"/> for the current driver based on the parameters object.
        /// </summary>
        /// <returns>The generated <see cref="DriverOptions"/>.</returns>
        T ToDriverOptions();
    }
}