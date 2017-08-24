﻿namespace EthereumSamurai.EventProvider.Api.Models
{
    public class Erc20TransferCommitsReplayRequest
    {
        public string AssetHolder { get; set; }

        public string Exchange { get; set; }

        public int ReplayNumber { get; set; }

        public string RoutingKey { get; set; }
    }
}