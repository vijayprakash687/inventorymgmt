using InventoryMgmt.Core.Interfaces;
using InventoryMgmt.Domain.Entities;
using InventoryMgmt.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMgmt.App.Items
{
    public abstract class BaseUpdateItemCommand : IInventoryCommand
    {
        protected readonly IRepository<Item> ItemRepository;        
        protected readonly string Name;
        protected readonly int Quantity;
        
        public BaseUpdateItemCommand(IRepository<Item> itemRepository, 
                                string name, int quantity)
        {
            ItemRepository = itemRepository;
            Name = name;
            Quantity = quantity;
        }
        protected virtual void Validate()
        {
            if(string.IsNullOrWhiteSpace(Name))
            {
                throw new Exception("Item name is a required field");
            }

            if(Quantity <= 0)
                throw new Exception("Quantity amount should be greater than 0");
        }
        protected abstract void  Process();
        public void Execute()
        {
            Validate();
            Process();
        }
    }
}
