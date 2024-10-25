using MongoDB.Driver;
using InventoryManagementSystemAPI.Models;

namespace InventoryManagementSystemAPI.Services
{
    public class AdminService : IAdminService
    {
        private readonly IMongoCollection<AdminUser> _adminUser;

        // Constructor
        public AdminService(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _adminUser = database.GetCollection<AdminUser>("admin");
        }

        // Create new admin user
        public AdminUser Create(AdminUser adminUser)
        {
            _adminUser.InsertOne(adminUser);
            return adminUser;
        }

        public AdminUser GetByUsername(string username) 
        {
            return _adminUser.Find(adminUser => adminUser.Username == username).FirstOrDefault();
        }

    }
}
