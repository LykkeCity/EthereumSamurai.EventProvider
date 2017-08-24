namespace EthereumSamurai.EventProvider.Core.Repositories.Entities
{
    using MongoDB.Bson.Serialization.Attributes;

    [BsonIgnoreExtraElements]
    public class Erc20SubscriptionEntity
    {
        [BsonElement]
        public string AssetHolder { get; set; }

        [BsonElement]
        public string Contract { get; set; }

        [BsonElement]
        public string Subscriber { get; set; }
    }
}