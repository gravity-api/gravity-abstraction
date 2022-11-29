/*
 * CHANGE LOG
 * 
 * 2019-01-06
 *    - modify: remove interface
 *    - modify: add xml comments
 *    - modify: driver type is not string
 */
using System.Runtime.Serialization;
using static Gravity.Abstraction.Contracts.Driver;

namespace Gravity.Abstraction.Contracts
{
    /// <summary>
    /// describes a contract for sending gravity mock-driver-params to gravity-service
    /// </summary>
    public class MockDriverParams
    {
        /// <summary>
        /// gets the driver type
        /// </summary>
        [DataMember]
        public string Driver => Mock;
    }
}