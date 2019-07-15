using InventoryMgmt.Core.Interfaces;
using InventoryMgmt.Domain.Entities;
using InventoryMgmt.Domain.Enums;
using InventoryMgmt.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMgmt.App.Items
{
    public class DeleteItemCommand : IInventoryCommand
    {
        protected readonly IRepository<Item> _itemRepository;
        protected readonly string _name;
        public DeleteItemCommand(IRepository<Item> itemRepository,string name) 
        {
            _itemRepository = itemRepository;            
            _name = name;
        }

       
        public void Execute()
        {
            if (string.IsNullOrWhiteSpace(_name))
            {
                throw new Exception("Item name is a required field");
            }

            var item = _itemRepository.Get(_name);
            if (item == null)
                throw new Exception($"Item {_name} not found in repository");            
            
            _itemRepository.Delete(item);
            
        }

        
    }
}
