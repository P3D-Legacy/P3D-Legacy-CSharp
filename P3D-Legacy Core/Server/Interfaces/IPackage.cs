using System.Collections.Generic;

namespace P3D.Legacy.Core.Server
{
    public interface IPackage
    {
        /// <summary>
        /// The PackageType of this Package.
        /// </summary>
        PackageTypes PackageType { get; }

        /// <summary>
        /// The Origin ID of this Package.
        /// </summary>
        int Origin { get; }

        /// <summary>
        /// The DataItems of this Package.
        /// </summary>
        List<string> DataItems { get; }

        /// <summary>
        /// Returns if the data used to create this Package was valid.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// The protocol version of this package.
        /// </summary>
        string ProtocolVersion { get; }

        /// <summary>
        /// The protocol type (TCP or UDP) this package is using when sending data.
        /// </summary>
        ProtocolTypes ProtocolType { get; set; }

        /// <summary>
        /// Returns the raw Package data from the members of this instance.
        /// </summary>
        string ToString();

        /// <summary>
        /// Gives this package to the PackageHandler.
        /// </summary>
        void Handle();

        /// <summary>
        /// Returns a byte array of the data of this package.
        /// </summary>
        byte[] GetByteArray();
    }
}
