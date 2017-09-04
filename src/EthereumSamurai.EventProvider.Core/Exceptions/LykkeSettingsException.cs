namespace EthereumSamurai.EventProvider.Core.Exceptions
{
    using System;
    using System.Runtime.Serialization;


    public class LykkeSettingsException : Exception
    {
        public LykkeSettingsException()
        {
        }

        public LykkeSettingsException(string message) 
            : base(message)
        {
        }

        public LykkeSettingsException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        protected LykkeSettingsException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}