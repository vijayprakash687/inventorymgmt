using InventoryMgmt.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMgmt.Domain.Entities
{
    public class Item
    {
        public string Name { get;  }
        public int Quantity { get; set; } = 0;

        public decimal CostPrice { get; set; } 

        public decimal SellPrice { get; set; }
        
        public ICollection<ItemUpdateDetail> ItemHistory { get; }
        public Item(string name,decimal costPrice,decimal sellPrice)
        {
            Name = name;
            CostPrice = costPrice;
            SellPrice = sellPrice;
            ItemHistory = new List<ItemUpdateDetail>();
        }

    }

    public class ItemUpdateDetail
    {
        
        public int Quantity { get; }

        public InventoryUpdates UpdateType { get;  }

        public decimal BuyUnitPrice { get;  }

        public decimal SellUnitPrice { get; }

        public decimal TotalAmount { get; }

        public decimal Profit { get; }

        
        public ItemUpdateDetail(int quantity, InventoryUpdates updateType, 
            decimal buyUnitPrice, decimal sellUnitPrice)
        {
            Quantity = quantity;
            UpdateType = updateType;
            BuyUnitPrice = buyUnitPrice;
            SellUnitPrice = sellUnitPrice;
            TotalAmount = updateType== InventoryUpdates.Buy? quantity * buyUnitPrice: quantity * sellUnitPrice;
            Profit = updateType == InventoryUpdates.Sell ? (SellUnitPrice - BuyUnitPrice) * quantity:0;

        }

    }
}
