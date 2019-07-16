using System;

using InventoryMgmt.Core.Interfaces;
using InventoryMgmt.Persistence.Entities;
using InventoryMgmt.Persistence.Interfaces;

namespace InventoryMgmt.App.Items
{
    public class DeleteItemCommand : IInventoryCommand
    {
        protected readonly IRepository<ItemData> _itemRepository;
        protected readonly string _name;
        public bool IsCompleted { get; set; }
        public DeleteItemCommand(IRepository<ItemData> itemRepository,string name) 
        {
            _itemRepository = itemRepository;            
            _name = name;
            IsCompleted = false;
        }

       
        public void Execute()
        {
            if (string.IsNullOrWhiteSpace(_name))
            {
                throw new Exception("Item name is a required field");
            }

            var item = _itemRepository.Get(_name);
            if (item == null)
            {
                throw new Exception($"Item {_name} not found in repository");
            }

            _itemRepository.Delete(item);            
        }        
    }
}
