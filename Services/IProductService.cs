using InventoryManagementSystemAPI.Models;

namespace InventoryManagementSystemAPI.Services
{
    public interface IProductService
    {
        Product Create(Product product);

        List<Product> Get();

        Product GetById(string id);

        void UpdateProduct(string id, Product product);

        void DeleteProduct(string id);
    }
}
