using InventoryMgmt.Domain.Entities;
using InventoryMgmt.Domain.Enums;
using InventoryMgmt.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMgmt.App.Items
{
    public class UpdateSellItemCommand : BaseUpdateItemCommand
    {
        public UpdateSellItemCommand(IRepository<Item> itemRepository,string name, int quantity) 
                                        : base(itemRepository, name, quantity)
        {

        }
        protected override void Process()
        {
            var item = ItemRepository.Get(Name);
            if (item == null)
                throw new Exception($"Item {Name} not found in repository");
            if(item.Quantity < Quantity)
                throw new Exception($"Available items should be greater than or equal to the sell items.");
            item.ItemHistory.Add(new ItemUpdateDetail(Quantity, InventoryUpdates.Sell, item.CostPrice,item.SellPrice));
            item.Quantity -= Quantity;
            ItemRepository.Update(item);
        }
    }
}
