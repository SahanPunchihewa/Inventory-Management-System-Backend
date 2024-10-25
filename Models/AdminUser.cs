using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InventoryManagementSystemAPI.Models
{
    [BsonIgnoreExtraElements]
    public class AdminUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; } = string.Empty;

        [BsonElement("username")]
        public string Username { get; set; } = String.Empty;

        [BsonElement("password")]
        public string Password { get; set; } = String.Empty;


    }
}
