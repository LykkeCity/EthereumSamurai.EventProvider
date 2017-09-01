namespace EthereumSamurai.EventProvider.Service.Repositories.Entities
{
    using Interfaces;
    using MongoDB.Bson.Serialization.Attributes;


    [BsonIgnoreExtraElements]
    public sealed class Erc20TransferCommitsSubscriptionEntity : IErc20SubscriptionEntity
    {
        [BsonElement]
        public string AssetHolder { get; set; }

        [BsonElement]
        public string Contract { get; set; }

        [BsonElement]
        public string Subscriber { get; set; }
    }
}