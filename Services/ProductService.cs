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

        // Get all product details
        public List<Product> Get()
        {
            return _product.Find(product => true).ToList();
        }

        // Get product by ID
        public Product GetById(string id)
        {
            return _product.Find(product => product.Id == id).FirstOrDefault();
        }

        // Delete product
        public void DeleteProduct(string id)
        {
          _product.DeleteOne(product => product.Id == id);  
        }

        // Update Product
        public void UpdateProduct(string id, Product product)
        {

           _product.ReplaceOne(product => product.Id == id, product);
        }

        // Get low stock product
        public List<Product> GetLowOfStockProduct()
        {

            return _product.Find(product => product.QuantityInStock < product.MininumStockLevel).ToList();
        }

        // Get out of stock product
        public List<Product> GetOutOfStockProduct()
        {
            return _product.Find(product => product.QuantityInStock == 0).ToList();
        }

        // Get inventory summary
        public object GetInventorySummary()
        {
            var totalProducts = _product.CountDocuments(FilterDefinition<Product>.Empty);

            var lowStockCount = _product.CountDocuments(product => product.QuantityInStock < product.MininumStockLevel);

            var outOfStockCount = _product.CountDocuments(product => product.QuantityInStock == 0);

            return (object)new InventorySummary
            {
                TotalProducts = totalProducts,
                LowStockProduct = lowStockCount,
                OutOfStockProduct = outOfStockCount
            };

        }
    }
}
