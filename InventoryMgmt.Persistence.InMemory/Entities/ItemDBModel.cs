using InventoryMgmt.Persistence.Entities;

namespace InventoryMgmt.Persistence.InMemory.Entities
{
    internal class ItemDBModel : ItemData
    {
        public bool IsDeleted { get; set; } = false;
        public ItemDBModel(string name, decimal costPrice, decimal sellPrice) : base(name, costPrice, sellPrice)
        {
            
        }
    }
}
