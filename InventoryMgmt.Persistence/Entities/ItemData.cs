using InventoryMgmt.Domain.Entities;
using InventoryMgmt.Domain.Enums;
using System;
using System.Collections.Generic;

namespace InventoryMgmt.Persistence.Entities
{
    public class ItemData : Item
    {
        public IList<ItemTransactionDeatil> ItemTransactions { get;  }
        public ItemData (string name, decimal costPrice, decimal sellPrice) : base( name,  costPrice,  sellPrice)
        {
            ItemTransactions = new List<ItemTransactionDeatil>();
        }
    }

    public class ItemTransactionDeatil
    {
        public int Quantity { get;  }

        public InventoryType ActionType { get; }

        public decimal BuyUnitPrice { get; }

        public decimal  SellUnitPrice { get; }

        public decimal TotalAmount { get; }

        public decimal ProfitAmount { get; }

        public DateTime LastChangedTimeStamp { get; set; }

        public ItemTransactionDeatil(int quantity, InventoryType actionType,
                                        decimal buyUnitPrice, decimal sellUnitPrice)
        {
            Quantity = quantity;
            ActionType = actionType;
            BuyUnitPrice = buyUnitPrice;
            SellUnitPrice = sellUnitPrice;
            TotalAmount = actionType == InventoryType.Buy ? quantity * buyUnitPrice : quantity * sellUnitPrice;
            ProfitAmount = actionType == InventoryType.Sell ? (SellUnitPrice - BuyUnitPrice) * quantity : 0;
            LastChangedTimeStamp = DateTime.UtcNow;

        }
    }
}
