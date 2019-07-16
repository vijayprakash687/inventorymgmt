using System;

using InventoryMgmt.Domain.Enums;
using InventoryMgmt.Persistence.Entities;
using InventoryMgmt.Persistence.Interfaces;


namespace InventoryMgmt.App.Items
{
    public class UpdateSellItemCommand : BaseUpdateItemCommand
    {
        public UpdateSellItemCommand(IRepository<ItemData> itemRepository,string name, int quantity) 
                                        : base(itemRepository, name, quantity)
        {

        }
        protected override void Process()
        {
            var item = ItemRepository.Get(Name);

            if (item == null)
            {
                throw new Exception($"Item {Name} not found in repository");
            }

            if (item.Quantity < Quantity)
            {
                throw new Exception($"Available items should be greater than or equal to the sell items.");
            }

            item.ItemTransactions.Add(new ItemTransactionDeatil(Quantity, InventoryType.Sell, item.CostPrice,item.SellPrice));

            item.Quantity -= Quantity;

            ItemRepository.Update(item);
        }
    }
}
