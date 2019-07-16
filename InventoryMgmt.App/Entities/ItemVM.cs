using InventoryMgmt.Persistence.Entities;
using System.Collections.Generic;

namespace InventoryMgmt.App.Entities
{
    public class ItemVM
    {
        public IEnumerable<ItemData> Items { get; set; }

        public decimal TotalValue { get; set; }

        public decimal ProfitAmount { get; set; }
    }
}
