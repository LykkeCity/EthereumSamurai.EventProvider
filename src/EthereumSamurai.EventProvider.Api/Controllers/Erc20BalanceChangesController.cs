namespace EthereumSamurai.EventProvider.Api.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Service.Actors.Proxies;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Service.Actors.Messages;


    [Route("api/erc20BalanceChanges")]
    public sealed class Erc20BalanceChangesController : Controller
    {
        private readonly IErc20BalanceChangesReplayManagerProxy       _replayManager;
        private readonly IErc20BalanceChangesSubscribtionManagerProxy _subscribtionManager;

        public Erc20BalanceChangesController(
            IErc20BalanceChangesReplayManagerProxy       replayManager,
            IErc20BalanceChangesSubscribtionManagerProxy subscribtionManager)
        {
            _replayManager       = replayManager;
            _subscribtionManager = subscribtionManager;
        }

        /// <summary>
        ///    Adds specified replay request to the queue.
        /// </summary>
        /// <param name="request">
        ///    Erc20 balance changes replay request.
        /// </param>
        /// <returns></returns>
        [HttpPost("replay-requests")]
        public IActionResult Replay([FromBody, Required] Erc20BalanceChangesReplayRequest request)
        {
            _replayManager.Tell(new ReplayErc20BalanceChanges(
                exchange:     request.Exchange,
                routingKey:   request.RoutingKey,
                replayId: request.ReplayId,
                assetHolder:  request.AssetHolder.ToLowerInvariant()
            ));

            return Ok();
        }

        /// <summary>
        ///    Adds specified subscription .
        /// </summary>
        /// <param name="subscription">
        ///    New subscription model.
        /// </param>
        /// <returns></returns>
        [HttpPost("subscriptions")]
        public IActionResult Subscribe([FromBody, Required] Erc20BalanceChangesSubscription subscription)
        {
            _subscribtionManager.Tell(new SubscribeToErc20BalanceChanges
            (
                exchange:    subscription.Exchange,
                routingKey:  subscription.RoutingKey,
                assetHolder: subscription.AssetHolder.ToLowerInvariant(),
                contracts:   subscription.Contracts.Select(x => x.ToLowerInvariant())
            ));

            return Ok();
        }

        [HttpDelete("subscriptions")]
        public IActionResult Unsubscribe([FromBody, Required] Erc20BalanceChangesSubscription subscription)
        {
            _subscribtionManager.Tell(new UnsubscribeFromErc20BalanceChanges
            (
                exchange:    subscription.Exchange,
                routingKey:  subscription.RoutingKey,
                assetHolder: subscription.AssetHolder.ToLowerInvariant(),
                contracts:   subscription.Contracts.Select(x => x.ToLowerInvariant())
            ));
            
            return Ok();
        }
    }
}