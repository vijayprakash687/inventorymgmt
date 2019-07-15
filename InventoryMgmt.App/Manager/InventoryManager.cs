using InventoryMgmt.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMgmt.App.Manager
{
    public class InventoryManager
    {
        private readonly List<IInventoryCommand> _inventoryCommands = new List<IInventoryCommand>();
        public void AddInventoryCommand(IInventoryCommand inventoryCommand)
        {
            _inventoryCommands.Add(inventoryCommand);
        }

        public void ProcessInventoryCommands()
        {
            // Apply transactions in the order they were added.
            foreach (IInventoryCommand inventoryCommand in _inventoryCommands)
            {
                inventoryCommand.Execute();
            }
        }
    }
}
