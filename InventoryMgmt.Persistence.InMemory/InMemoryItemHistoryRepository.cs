using InventoryMgmt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace InventoryMgmt.Persistence.InMemory
{
    public class InMemoryItemHistoryRepository //: IRepository<ItemHistory>
    {
        //readonly Dictionary<int, ItemHistory> _itemHistoryCollection;
        //static int _ID_Counter = 1;

        //public InMemoryItemHistoryRepository()
        //{
        //    _itemHistoryCollection = new Dictionary<int, ItemHistory>();
        //}
        //public void Add(ItemHistory item)
        //{
        //    _itemHistoryCollection.Add(_ID_Counter, item);
        //}

        //public bool Delete(ItemHistory item)
        //{
        //    _itemHistoryCollection.Select(x => x.Value.Name == item.Name).ToList().RemoveAll(x=>x ==true);
        //    return true;
        //}

        //public ItemHistory Get(string name)
        //{
        //    return _itemHistoryCollection.FirstOrDefault(x => x.Value.Name == name).Value;
        //}

        //public IEnumerable<ItemHistory> GetAll()
        //{
        //    return _itemHistoryCollection.Values.ToList();
        //}

        //public void Update(ItemHistory item)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
