using InventoryMgmt.Domain.Entities;
using InventoryMgmt.Domain.Enums;
using InventoryMgmt.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMgmt.App.Items
{
    public class UpdateBuyItemCommand : BaseUpdateItemCommand
    {
        public UpdateBuyItemCommand(IRepository<Item> itemRepository,
                                 string name, int quantity) :base(itemRepository,name, quantity)
        {

        }
        protected override void Process()
        {
            var item = ItemRepository.Get(Name);
            if (item  == null)
                throw new Exception($"Item {Name} not found in repository");
            item.ItemHistory.Add( new ItemUpdateDetail(Quantity, InventoryUpdates.Buy, item.CostPrice,0));
            item.Quantity += Quantity;
            ItemRepository.Update(item);
        }
    }
}
