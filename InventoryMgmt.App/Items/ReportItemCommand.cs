using System;
using System.Linq;
using System.Collections.Generic;

using InventoryMgmt.Core.Interfaces;
using InventoryMgmt.App.Entities;
using InventoryMgmt.App.Extensions;

using InventoryMgmt.Persistence.Interfaces;
using InventoryMgmt.Persistence.Entities;

namespace InventoryMgmt.App.Items
{
    public class ReportItemCommand : IInventoryCommand
    {
        readonly IItemRepository<ItemData> _itemRepository;
        public bool IsCompleted { get; set; }
        static IList<ReportModel> reports;
        static int _counter = 1;        

        static ReportItemCommand()
        {
            reports = new List<ReportModel>();
            
        }

        public ReportItemCommand(IItemRepository<ItemData> itemRepository)
        {
            _itemRepository = itemRepository;
            IsCompleted = false;
        }

        public void Execute()
        {
            var items = ToCollection(_itemRepository.GetAll().OrderBy(x=>x.Name));
            UpdateReportCollection(items);

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("                             Inventory Report                             ");
            Console.WriteLine("");
            var header = String.Format("|{0,15}|{1,15}|{2,15}|{3,15}|", "Item Name", "Bought At", "Sold At", "Value");
            Console.WriteLine(header);
            var header1 = string.Format("|{0,15}|{1,15}|{2,15}|{3,15}|", "--------", "-------", "-------", "-------");
            Console.WriteLine(header1);

            foreach(var item in items.Items)
            {
                var row = string.Format("|{0,15}|{1,15}|{2,15}|{3,15}|", item.Name, item.CostPrice, item.Quantity, (item.Quantity*item.CostPrice));
                Console.WriteLine(row);
            }

            Console.WriteLine("--------------------------------------------------------------------------------------------");

            var footer = String.Format("|{0,15} {1,15} {2,15} {3,15}|", "Total Value","","", items.TotalValue);
            var footer1 = String.Format("|{0,15} {1,15} {2,15} |", "Profit since previous report","", items.ProfitAmount);
            Console.WriteLine(footer);
            Console.WriteLine(footer1);
        }

        #region Private Methods
        private void UpdateReportCollection(ItemVM items)
        {
            var lastReport = reports.Count > 0 ? reports.OrderBy(x => x.ReportId).Last() : null;
            var profitAmount = reports.Count > 0 ? _itemRepository.GetProfitWithInTimePeriod(lastReport.CreatedTime) :
                                                                                                items.ProfitAmount;
            reports.Add(new ReportModel
            {
                ReportId = _counter,
                ProfitAmount = profitAmount,
                TotalValue = items.TotalValue
            });

            items.ProfitAmount = profitAmount;
            _counter += 1;
        }

        private ItemVM ToCollection(IEnumerable<ItemData> items)
        {            
            var itemVM = new ItemVM
            {
                Items = items,
                TotalValue = items.TotalValue(),
                ProfitAmount = items.Profit()
            };

            return itemVM;
        }

        #endregion
    }
}
