using System.Collections.Generic;

namespace InventoryMgmt.Persistence.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        T Get(string name);
        void Add(T item);

        void Update(T item);       

        bool Delete(T item);
    }
}
