using System.Collections.Generic;
using System.Linq;
using InventoryMgmt.Persistence.Entities;

namespace InventoryMgmt.App.Extensions
{
    internal static class ItemExtensions
    {
        public static decimal TotalValue(this IEnumerable<ItemData> items)
        {
            return items.Sum(x => x.Quantity * x.CostPrice);
        }

        public static decimal Profit (this IEnumerable<ItemData> items)
        {
            decimal profit = 0;

            foreach(var item in items)
            {
                profit += item.ItemTransactions.Sum(x => x.ProfitAmount);
            }

            return profit;
        }        
    }
}
