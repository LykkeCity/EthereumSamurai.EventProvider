namespace EthereumSamurai.EventProvider.Api.Controllers
{
    using System.Linq;
    using Service.Actors.Proxies;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Service.Actors.Messages;


    /// <inheritdoc />
    /// <summary>
    ///    The <see cref="Erc20TransferCommitsController"/> class.
    /// </summary>
    [Route("api/[controller]")]
    public class Erc20TransferCommitsController : Controller
    {
        private readonly IErc20TransferCommitsReplayManagerProxy       _replayManager;
        private readonly IErc20TransferCommitsSubscriptionManagerProxy _subscriptionManager;

        /// <summary>
        ///    Initializes a new instance of the <see cref="Erc20TransferCommitsController"/> class.
        /// </summary>
        public Erc20TransferCommitsController(
           IErc20TransferCommitsReplayManagerProxy       replayManager,
           IErc20TransferCommitsSubscriptionManagerProxy subscriptionManager)
        {
            _replayManager       = replayManager;
            _subscriptionManager = subscriptionManager;
        }

        /// <summary>
        ///    Adds specified replay request to the queue
        /// </summary>
        /// <param name="request">
        ///    Erc20 transfer commits replay request
        /// </param>
        [HttpPost("replay-requests")]
        public IActionResult Replay([FromBody] Erc20TransferCommitsReplayRequest request)
        {
            _replayManager.Tell(new ReplayErc20TransferCommits(
                exchange:    request.Exchange,
                routingKey:  request.RoutingKey,
                replayId:    request.ReplayId,
                assetHolder: request.AssetHolder.ToLowerInvariant(),
                contracts:   request.Contracts?.Select(x => x.ToLowerInvariant()) ?? Enumerable.Empty<string>()
            ));

            return Ok();
        }

        /// <summary>
        ///    Adds specified subscription
        /// </summary>
        /// <param name="subscription">
        ///    New subscription model
        /// </param>
        [HttpPost("subscriptions")]
        public IActionResult Subscribe([FromBody] Erc20TransferCommitsSubscription subscription)
        {
            _subscriptionManager.Tell(new SubscribeToErc20TransferCommits(
                exchange:    subscription.Exchange,
                routingKey:  subscription.RoutingKey,
                assetHolder: subscription.AssetHolder.ToLowerInvariant(),
                contracts:   subscription.Contracts?.Select(x => x.ToLowerInvariant()) ?? Enumerable.Empty<string>()
            ));

            return Ok();
        }

        /// <summary>
        ///    Remove specified subscription
        /// </summary>
        /// <param name="subscription">
        ///    Subscription model
        /// </param>
        [HttpDelete("subscriptions")]
        public IActionResult Unsubscribe([FromBody] Erc20TransferCommitsSubscription subscription)
        {
            _subscriptionManager.Tell(new UnsubscribeFromErc20TransferCommits(
                exchange:    subscription.Exchange,
                routingKey:  subscription.RoutingKey,
                assetHolder: subscription.AssetHolder.ToLowerInvariant(),
                contracts:   subscription.Contracts?.Select(x => x.ToLowerInvariant()) ?? Enumerable.Empty<string>()
            ));

            return Ok();
        }
    }
}