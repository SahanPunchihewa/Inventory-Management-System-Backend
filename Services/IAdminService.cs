using InventoryManagementSystemAPI.Models;

namespace InventoryManagementSystemAPI.Services
{
    public interface IAdminService
    {
        AdminUser Create(AdminUser adminUser);

        AdminUser GetByUsername(string username);
    }
}
