using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InventoryManagementSystemAPI.Models
{
    [BsonIgnoreExtraElements]
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("productId")]
        public int ProductId { get; set; } = int.MaxValue;

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;

        [BsonElement("quantityInStock")]
        public int QuantityInStock { get; set; } = int.MaxValue;

        [BsonElement("price")]
        public decimal Price { get; set; } = decimal.MaxValue;

        [BsonElement("mininumStockLevel")]
        [BsonIgnoreIfNull]
        public int MininumStockLevel {  get; set; } = int.MaxValue;
    }
}
