namespace EthereumSamurai.EventProvider.Api.Models.Validation
{
    using System.Collections.Generic;
    using System.Linq;


    public class EthereumAddressListAttribute : EthereumAddressAttribute
    {
        public override bool IsValid(object value)
        {
            return value == null
                || value is IEnumerable<string> values
                && values.All(base.IsValid);
        }
    }
}