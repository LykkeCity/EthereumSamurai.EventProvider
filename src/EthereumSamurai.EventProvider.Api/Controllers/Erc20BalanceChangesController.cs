namespace EthereumSamurai.EventProvider.Api.Controllers
{
    using Service.Actors.Proxies;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Service.Actors.Messages;


    [Route("api/[controller]")]
    public sealed class Erc20BalanceChangesController : Controller
    {
        private readonly IErc20BalanceChangesReplayManagerProxy       _replayManager;
        private readonly IErc20BalanceChangesSubscribtionManagerProxy _subscribtionManager;

        public Erc20BalanceChangesController(
            IErc20BalanceChangesReplayManagerProxy replayManager,
            IErc20BalanceChangesSubscribtionManagerProxy subscribtionManager)
        {
            _replayManager       = replayManager;
            _subscribtionManager = subscribtionManager;
        }

        [HttpPost("replay-requests")]
        public IActionResult Replay(Erc20BalanceChangesReplayRequest request)
        {
            _replayManager.Tell(new ReplayErc20BalanceChanges(
                exchange:     request.Exchange,
                routingKey:   request.RoutingKey,
                replayNumber: request.ReplayId,
                assetHolder:  request.AssetHolder
            ));

            return Ok();
        }

        [HttpPost("subscriptions")]
        public IActionResult Subscribe(Erc20BalanceChangesSubscription subscription)
        {
            _subscribtionManager.Tell(new SubscribeToErc20BalanceChanges
            (
                exchange:    subscription.Exchange,
                routingKey:  subscription.RoutingKey,
                assetHolder: subscription.AssetHolder,
                contracts:   subscription.Contracts
            ));

            return Ok();
        }

        [HttpDelete("subscriptions")]
        public IActionResult Unsubscribe(Erc20BalanceChangesSubscription subscription)
        {
            _subscribtionManager.Tell(new UnsubscribeFromErc20BalanceChanges
            (
                exchange:    subscription.Exchange,
                routingKey:  subscription.RoutingKey,
                assetHolder: subscription.AssetHolder,
                contracts:   subscription.Contracts
            ));
            
            return Ok();
        }
    }
}