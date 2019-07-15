using InventoryMgmt.Core.Interfaces;
using InventoryMgmt.Domain.Entities;
using InventoryMgmt.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMgmt.App.Items
{
    public class CreateItemCommand : IInventoryCommand
    {
        readonly IRepository<Item> _repository;
        readonly Item _item;
        public CreateItemCommand(IRepository<Item> repository,Item item)
        {
            _repository = repository;
            _item = item;
        }
        public void Execute()
        {
            if (_item == null || string.IsNullOrWhiteSpace(_item.Name))
                throw new Exception("Item or name of the item should not be blank");
            _repository.Add(_item);
        }
    }
}
