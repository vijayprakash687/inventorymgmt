using System;

using InventoryMgmt.Core.Interfaces;
using InventoryMgmt.Persistence.Entities;
using InventoryMgmt.Persistence.Interfaces;

namespace InventoryMgmt.App.Items
{
    public class CreateItemCommand : IInventoryCommand
    {
        readonly IRepository<ItemData> _repository;
        readonly ItemData _item;
        public bool IsCompleted { get; set; }
        public CreateItemCommand(IRepository<ItemData> repository,ItemData item)
        {
            _repository = repository;
            _item = item;
            IsCompleted = false;
        }

        public void Execute()
        {
            if (_item == null || string.IsNullOrWhiteSpace(_item.Name))
            {
                throw new Exception("Item or name of the item should not be blank");
            }

            _repository.Add(_item);
        }
    }
}
