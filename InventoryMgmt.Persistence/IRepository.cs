using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMgmt.Persistence
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
