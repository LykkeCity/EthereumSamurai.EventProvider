namespace EthereumSamurai.EventProvider.Api.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Validation;


    /// <summary>
    ///    Erc20 transfer commits replay request
    /// </summary>
    public class Erc20TransferCommitsReplayRequest
    {
        /// <summary>
        ///    Get transfer commits for this asset holder address
        /// </summary>
        [Required, EthereumAddress]
        public string AssetHolder { get; set; }

        /// <summary>
        ///    Get transfer commits for these contracts (leave null or empty, to get transfer commits for all contracts)
        /// </summary>
        [EthereumAddressList]
        public IEnumerable<string> Contracts { get; set; }

        /// <summary>
        ///    Send transfer commits to this exchange
        /// </summary>
        [Required]
        public string Exchange { get; set; }

        /// <summary>
        ///    Send a Erc20TransferCommitsReplayEndNotification message with this request id when finished request
        /// </summary>
        [Required]
        public int ReplayId { get; set; }

        /// <summary>
        ///    Send transfer commits with this routing key
        /// </summary>
        [Required]
        public string RoutingKey { get; set; }
    }
}