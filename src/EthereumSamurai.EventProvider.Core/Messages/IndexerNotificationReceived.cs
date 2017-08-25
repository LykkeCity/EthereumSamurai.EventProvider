namespace EthereumSamurai.EventProvider.Core.Messages
{
    using System.Collections.Generic;
    using System.Collections.Immutable;

    public sealed class IndexerNotificationReceived
    {
        internal IndexerNotificationReceived(IEnumerable<byte> notificationBody)
        {
            NotificationBody = notificationBody.ToImmutableArray();
        }

        public ImmutableArray<byte> NotificationBody { get; }
    }
}