using System;

using InventoryMgmt.Core.Interfaces;
using InventoryMgmt.Persistence.Entities;
using InventoryMgmt.Persistence.Interfaces;

namespace InventoryMgmt.App.Items
{
    public abstract class BaseUpdateItemCommand : IInventoryCommand
    {
        protected readonly IRepository<ItemData> ItemRepository;        
        protected readonly string Name;
        protected readonly int Quantity;

        public bool IsCompleted { get; set; }

        public BaseUpdateItemCommand(IRepository<ItemData> itemRepository, 
                                string name, int quantity)
        {
            ItemRepository = itemRepository;
            Name = name;
            Quantity = quantity;
            IsCompleted = false;
        }

        protected virtual void Validate()
        {
            if(string.IsNullOrWhiteSpace(Name))
            {
                throw new Exception("Item name is a required field");
            }

            if (Quantity <= 0)
            {
                throw new Exception("Quantity amount should be greater than 0");
            }
        }

        protected abstract void  Process();

        public void Execute()
        {
            Validate();
            Process();
        }
    }
}
