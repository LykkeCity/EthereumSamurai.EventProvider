﻿namespace EthereumSamurai.EventProvider.Service.Actors.Messages
{
    using System.Collections.Generic;
    using System.Collections.Immutable;



    internal sealed class IndexerNotificationReceived
    {
        internal IndexerNotificationReceived(IEnumerable<byte> notificationBody)
        {
            NotificationBody = notificationBody.ToImmutableArray();
        }

        public ImmutableArray<byte> NotificationBody { get; }
    }
}