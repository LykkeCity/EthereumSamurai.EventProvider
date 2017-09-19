namespace EthereumSamurai.EventProvider.Api.Models.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;


    /// <inheritdoc />
    /// <summary>
    ///    Marks property as a list of ethereum addresses
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EthereumAddressListAttribute : EthereumAddressAttribute
    {
        /// <inheritdoc />
        public override bool IsValid(object value)
        {
            return value == null
                || value is IEnumerable<string> values
                && values.All(base.IsValid);
        }
    }
}