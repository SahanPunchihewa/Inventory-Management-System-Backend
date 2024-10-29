using InventoryManagementSystemAPI.Models;

namespace InventoryManagementSystemAPI.Services
{
    public interface IUserService
    {
        User Create(User user);

        User GetByUsername(string username);

        User GetByEmail(string email);

        List<User> GetAll();

        void UpdateUser(string id, User user);

        void DeleteUser(string id);

        User GetById(string id);

    }
}
