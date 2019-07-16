using System;

namespace InventoryMgmt.Persistence.Interfaces
{
    public interface IItemRepository<T> : IRepository<T>
    {
        decimal GetProfitWithInTimePeriod(DateTime timeFrom);
    }
}
