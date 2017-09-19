namespace EthereumSamurai.EventProvider.Api.Models.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    /// <inheritdoc />
    /// <summary>
    ///    Marks property as ethereum address
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EthereumAddressAttribute : ValidationAttribute
    {
        /// <inheritdoc />
        public override bool IsValid(object value)
        {
            return value == null
                || value is string valueString
                && Regex.IsMatch(valueString, @"0x[a-zA-Z0-9]{40}");
        }
    }
}