namespace EthereumSamurai.EventProvider.Api.Models.Validation
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;


    public class EthereumAddressAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value == null
                || value is string valueString
                && Regex.IsMatch(valueString, @"0x[a-zA-Z0-9]{40}");
        }
    }
}