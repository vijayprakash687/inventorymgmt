using System;

using InventoryMgmt.Domain.Enums;
using InventoryMgmt.Persistence.Entities;
using InventoryMgmt.Persistence.Interfaces;


namespace InventoryMgmt.App.Items
{
    public class UpdateBuyItemCommand : BaseUpdateItemCommand
    {
        public UpdateBuyItemCommand(IRepository<ItemData> itemRepository,
                                 string name, int quantity) :base(itemRepository,name, quantity)
        {

        }

        protected override void Process()
        {
            var item = ItemRepository.Get(Name);

            if (item  == null)
            {
                throw new Exception($"Item {Name} not found in repository");
            }

            item.ItemTransactions.Add( new ItemTransactionDeatil(Quantity, InventoryType.Buy, item.CostPrice,0));
            item.Quantity += Quantity;
            ItemRepository.Update(item);
        }
    }
}
