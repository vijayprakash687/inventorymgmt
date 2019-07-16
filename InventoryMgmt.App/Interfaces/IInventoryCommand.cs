namespace InventoryMgmt.Core.Interfaces
{
    public interface IInventoryCommand
    {
        bool IsCompleted { get; set; }
        void Execute();
    }

    
}
