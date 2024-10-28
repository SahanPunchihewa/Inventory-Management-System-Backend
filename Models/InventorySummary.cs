namespace InventoryManagementSystemAPI.Models
{
    public class InventorySummary
    {
        public long TotalProducts { get; set; }

        public long LowStockProduct {  get; set; }

        public long OutOfStockProduct { get; set; }

        public long TotalValue { get; set; }
    }
}
