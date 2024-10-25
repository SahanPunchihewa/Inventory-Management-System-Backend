using MongoDB.Driver.Core.Configuration;

namespace InventoryManagementSystemAPI.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; } = string.Empty;

        public string DatabaseName { get; set; } = string.Empty;
    }
}
