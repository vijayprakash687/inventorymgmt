using InventoryMgmt.Domain.Entities;
using InventoryMgmt.Persistence.Entities;
using InventoryMgmt.Persistence.InMemory.Entities;
using InventoryMgmt.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
namespace InventoryMgmt.Persistence.InMemory
{
    public class InMemoryItemRepository : IItemRepository<ItemData>
    {
        readonly Dictionary<string, ItemDBModel> _map;

        public InMemoryItemRepository()
        {
            _map = new Dictionary<string, ItemDBModel>();
        }
       
        public void Add(ItemData item)
        {
            if(!_map.ContainsKey(item.Name))
            {
                _map.Add(item.Name, new ItemDBModel(item.Name, item.CostPrice, item.SellPrice));
            }            
        }

        public bool Delete(ItemData item)
        {
            if (_map.ContainsKey(item.Name) && !_map[item.Name].IsDeleted)
            {
                _map[item.Name].IsDeleted = true;
                _map[item.Name].LastChangedTimeStamp = DateTime.UtcNow;

                return true;
            }

            return false;
        }

        public ItemData Get(string name)
        {
            if (_map.ContainsKey(name) && !_map[name].IsDeleted)
            {
                return _map[name];
            }

            return null;
        }

        public IEnumerable<ItemData> GetAll()
        {
            return _map.Values.Where(x => x.IsDeleted == false).ToList();
        }

        public decimal GetProfitWithInTimePeriod(DateTime timeFrom)
        {
            decimal _profitSinceLasTReport = 0;
            decimal _lostAmountOnDelete = 0;

            foreach(var item in _map.Values)
            {
                _profitSinceLasTReport += item.ItemTransactions
                                              .Where(x => x.LastChangedTimeStamp >= timeFrom)
                                              .Sum(x => x.ProfitAmount);
                if(item.IsDeleted && item.LastChangedTimeStamp >= timeFrom)
                {
                    _lostAmountOnDelete += item.Quantity * item.CostPrice;
                }
            }

            return _profitSinceLasTReport - _lostAmountOnDelete;
        }        

        public void Update(ItemData item)
        {
            if (_map.ContainsKey(item.Name) && !_map[item.Name].IsDeleted)
            {
                item.LastChangedTimeStamp = DateTime.UtcNow;
                _map[item.Name] = (ItemDBModel)item;
            }
        }       
    }
}
