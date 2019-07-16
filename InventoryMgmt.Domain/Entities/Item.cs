using System;

namespace InventoryMgmt.Domain.Entities
{
    public class Item
    {
        public string Name { get;  }

        public int Quantity { get; set; } = 0;

        public decimal CostPrice { get; set; } 

        public decimal SellPrice { get; set; }
        
        public DateTime? LastChangedTimeStamp { get; set; }

        public Item(string name,decimal costPrice,decimal sellPrice)
        {
            Name = name;
            CostPrice = costPrice;
            SellPrice = sellPrice;
            LastChangedTimeStamp = DateTime.UtcNow;
            
        }

    }    
}
