namespace EthereumSamurai.EventProvider.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///    Erc20 balance changes replay request.
    /// </summary>
    public class Erc20BalanceChangesReplayRequest
    {
        /// <summary>
        ///    Get balances for this asset holder address.
        /// </summary>
        [Required]
        public string AssetHolder { get; set; }

        /// <summary>
        ///    Send balances to this exchange.
        /// </summary>
        [Required]
        public string Exchange { get; set; }

        /// <summary>
        ///    Send a Erc20BalanceChangeReplayEndNotification message with this request id when finished request.
        /// </summary>
        [Required]
        public int ReplayId { get; set; }

        /// <summary>
        ///    Send balances with this routing key.
        /// </summary>
        [Required]
        public string RoutingKey { get; set; }
    }
}