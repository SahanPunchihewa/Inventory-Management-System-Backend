using MongoDB.Driver;
using InventoryManagementSystemAPI.Models;

namespace InventoryManagementSystemAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _product;

        // Constructor
        public ProductService(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _product = database.GetCollection<Product>("product");
        }
        // Create new product
        public Product Create(Product product)
        {
            _product.InsertOne(product);
            return product;
        
        }
    }
}
