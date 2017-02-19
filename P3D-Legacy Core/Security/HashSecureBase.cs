using System;
using System.Collections.Generic;

namespace P3D.Legacy.Core.Security
{
    /// <summary>
    /// Represents a base class that adds hash checked properties to other classes.
    /// </summary>
    /// <remarks>To use the features of this class, call UpdatePropertyHash after changing the value of a Property. This class does not work with properties that have parameters.</remarks>
    public abstract class HashSecureBase
    {
        /// <summary>
        /// The exception that gets thrown when the system detects mismatching hash values.
        /// </summary>
        private sealed class MismatchingHashValuesException : Exception
        {
            public MismatchingHashValuesException(string propertyName) : base("The internal Hash Validation System has detected external changes to secure properties.")
            {
                Data.Add("PropertyName", propertyName);
            }

        }


        /// <summary>
        /// List of Properties to keep track of
        /// </summary>
        private Dictionary<string, int> Properties { get; } = new Dictionary<string, int>();

        /// <summary>
        /// Runs a test check on a secure property.
        /// </summary>
        /// <param name="valueName">The name of the value.</param>
        /// <param name="currentValue">The value that SHOULD be stored in the property.</param>
        protected void Assert(string valueName, object currentValue) => Assert(valueName, currentValue, currentValue);

        /// <summary>
        /// Runs a test check on a secure property.
        /// </summary>
        /// <param name="valueName">The name of the value.</param>
        /// <param name="currentValue">The value that SHOULD be stored in the property.</param>
        /// <param name="newValue">The value that will be stored in the value when the test is over.</param>
        protected void Assert(string valueName, object currentValue, object newValue)
        {
            var hash = currentValue.GetHashCode();
            var listName = valueName.ToLower();

            if (Properties.ContainsKey(listName))
            {
                if (Properties[listName] == hash)
                    Properties[listName] = newValue.GetHashCode();
                else
                    throw new MismatchingHashValuesException(valueName);
            }
            else
                Properties.Add(listName, newValue.GetHashCode());
        }
    }
}
