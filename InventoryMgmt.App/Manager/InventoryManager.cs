using InventoryMgmt.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace InventoryMgmt.App.Manager
{
    public class InventoryManager
    {
        private readonly List<IInventoryCommand> _inventoryCommands = new List<IInventoryCommand>();
        public void AddInventoryCommand(IInventoryCommand inventoryCommand)
        {
            _inventoryCommands.Add(inventoryCommand);
        }

        public bool HasPendingCommands
        {
            get { return _inventoryCommands.Any(x => !x.IsCompleted); }
        }
        public void ProcessInventoryCommands()
        {
            foreach (IInventoryCommand inventoryCommand in _inventoryCommands.Where(x=>!x.IsCompleted))
            {
                inventoryCommand.Execute();
                inventoryCommand.IsCompleted = true;
            }
        }
    }
}
