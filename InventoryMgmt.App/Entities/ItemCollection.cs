using InventoryMgmt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMgmt.App.Entities
{
    public class ItemCollection
    {
        public IEnumerable<Item> Items { get; set; }
        public decimal TotalValue { get; set; }

        public decimal Profit { get; set; }
    }
}
