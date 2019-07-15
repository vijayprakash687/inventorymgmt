using InventoryMgmt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace InventoryMgmt.App.Extensions
{
    internal static class ItemExtensions
    {
        public static decimal TotalValue(this IEnumerable<Item> items)
        {
            return items.Sum(x => x.Quantity * x.CostPrice);
        }

        public static decimal Profit (this IEnumerable<Item> items)
        {
            decimal profit = 0;
            foreach(var item in items)
            {
                profit += item.ItemHistory.Sum(x => x.Profit);
            }
            return profit;
        }

        
    }
}
