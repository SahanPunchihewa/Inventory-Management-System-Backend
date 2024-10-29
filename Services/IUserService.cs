using InventoryManagementSystemAPI.Models;

namespace InventoryManagementSystemAPI.Services
{
    public interface IUserService
    {
        User Create(User user);

        User GetByUsername(string username);

        User GetByEmail(string email);

        List<User> GetAll();
    }
}
