namespace EthereumSamurai.EventProvider.Api.Options
{
    /// <summary>
    ///    Represents common API options.
    /// </summary>
    public class ApiOptions
    {
        /// <summary>
        ///    Initializes a new instance of the <see cref="ApiOptions"/> class.
        /// </summary>
        public ApiOptions()
        {
            // Place defaults here
        }

        /// <summary>
        ///    The host the API will listen on.
        /// </summary>
        /// <remarks>
        ///    Host can not be defaulted via class constructor.
        /// </remarks>
        public string Host { get; set; }
    }
}