namespace EthereumSamurai.EventProvider.Api.Controllers
{
    using Service.Actors.Proxies;
    using Service.Messages;
    using Microsoft.AspNetCore.Mvc;
    using Models;


    [Route("api/[controller]")]
    public class Erc20TransferCommitsController : Controller
    {
        private readonly IErc20TransferCommitsReplayManagerProxy       _replayManager;
        private readonly IErc20TransferCommitsSubscriptionManagerProxy _subscriptionManager;

    
        public Erc20TransferCommitsController(
           IErc20TransferCommitsReplayManagerProxy       replayManager,
           IErc20TransferCommitsSubscriptionManagerProxy subscriptionManager)
        {
            _replayManager       = replayManager;
            _subscriptionManager = subscriptionManager;
        }

        [HttpPost("replay-requests")]
        public IActionResult Replay(Erc20TransferCommitsReplayRequest request)
        {
            _replayManager.Tell(new ReplayErc20TransferCommits(
                exchange:    request.Exchange,
                routingKey:  request.RoutingKey,
                replayId:    request.ReplayId,
                assetHolder: request.AssetHolder
            ));

            return Ok();
        }

        [HttpPost("subscriptions")]
        public IActionResult Subscribe(Erc20TransferCommitsSubscription subscription)
        {
            _subscriptionManager.Tell(new SubscribeToErc20TransferCommits(
                exchange:    subscription.Exchange,
                routingKey:  subscription.RoutingKey,
                assetHolder: subscription.AssetHolder,
                contracts:   subscription.Contracts
            ));

            return Ok();
        }

        [HttpDelete("subscriptions")]
        public IActionResult Unsubscribe(Erc20TransferCommitsSubscription subscription)
        {
            _subscriptionManager.Tell(new UnsubscribeFromErc20TransferCommits(
                exchange:    subscription.Exchange,
                routingKey:  subscription.RoutingKey,
                assetHolder: subscription.AssetHolder,
                contracts:   subscription.Contracts
            ));

            return Ok();
        }
    }
}