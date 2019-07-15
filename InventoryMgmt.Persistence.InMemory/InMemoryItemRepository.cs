using InventoryMgmt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
namespace InventoryMgmt.Persistence.InMemory
{
    public class InMemoryItemRepository : IRepository<Item>
    {
        readonly Dictionary<string, Item> _map;

        public InMemoryItemRepository()
        {
            _map = new Dictionary<string, Item>();
        }
        public void Add(Item item)
        {
            _map.Add(item.Name, item);
        }

        public bool Delete(Item item)
        {
            if(_map.ContainsKey(item.Name))
            {
                _map.Remove(item.Name);
                return true;
            }
            return false;
               
        }

        public Item Get(string name)
        {
            if (_map.ContainsKey(name))
            {
                return _map[name];
            }
            return null;
        }
        public IEnumerable<Item> GetAll()
        {
            return _map.Values.ToList();
        }

        public void Update(Item item)
        {
            if (_map.ContainsKey(item.Name))
            {
                _map[item.Name] = item;
            }
        }
    }
}
