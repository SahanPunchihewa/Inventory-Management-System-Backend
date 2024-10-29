using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InventoryManagementSystemAPI.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        [BsonElement("username")]
        public string Username { get; set; } = String.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = String.Empty;

        [BsonElement("contact")]
        public string Contact {  get; set; } = String.Empty;

        [BsonElement("password")]
        public string Password { get; set; } = String.Empty;

        [BsonElement("role")]
        public string Role { get; set; } = String.Empty;
    }
}
