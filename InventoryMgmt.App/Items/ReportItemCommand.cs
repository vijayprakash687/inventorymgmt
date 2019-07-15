using InventoryMgmt.Core.Interfaces;
using InventoryMgmt.Domain.Entities;
using InventoryMgmt.Domain.Enums;
using InventoryMgmt.Persistence;
using InventoryMgmt.App.Entities;
using InventoryMgmt.App.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryMgmt.App.Items
{
    public class ReportItemCommand : IInventoryCommand
    {
        readonly IRepository<Item> _itemRepository;
        static decimal lastReportProfit = 0;
        static IList<Report> reports;
        static int _counter = 1;
        decimal latestProfit = 0;
        static ReportItemCommand()
        {
            reports = new List<Report>();
        }
        public ReportItemCommand(IRepository<Item> itemRepository)
        {
            _itemRepository = itemRepository;            
        }
        public void Execute()
        {
            var items = ToCollection(_itemRepository.GetAll());
            UpdateReportCollection(items);
            var header = String.Format("|{0,10}|{1,10}|{2,10}|{3,10}|", "Item Name", "Bought At", "Sold At", "Value");
            Console.WriteLine(header);
            var header1 = string.Format("|{0,10}|{1,10}|{2,10}|{3,10}|", "--------", "-------", "-------", "-------");
            Console.WriteLine(header1);
            foreach(var item in items.Items)
            {
                var row = string.Format("|{0,10}|{1,10}|{2,10}|{3,10}|", item.Name, item.CostPrice, item.Quantity, (item.Quantity*item.CostPrice));
                Console.WriteLine(row);
            }
            Console.WriteLine("------------------------------------------------------------------------");
            var footer = String.Format("|{0,30}|{1,10}|", "Total Value", items.TotalValue);
            var footer1 = String.Format("|{0,30}|{1,10}|", "Profit since previous report", latestProfit);
            Console.WriteLine(footer);
            Console.WriteLine(footer1);
        }

        private void UpdateReportCollection(ItemCollection items)
        {
            var lastReportProfit = reports.Count > 0 ? reports.OrderBy(x => x.ReportId).Last().Profit : 0;
            var lastReportTotalValue = reports.Count > 0 ? reports.OrderBy(x => x.ReportId).Last().TotalValue : 0;
            latestProfit = reports.Count == 0? items.Profit :(items.Profit - lastReportProfit + (lastReportTotalValue - items.TotalValue));
            reports.Add(new Report
            {
                ReportId = _counter,
                Profit = latestProfit,
                TotalValue = items.TotalValue
            });

            _counter += 1;
        }
        private ItemCollection ToCollection(IEnumerable<Item> items)
        {            
            var itemCollection = new ItemCollection
            {
                Items = items,
                TotalValue = items.TotalValue(),
                Profit = items.Profit()
            };
            return itemCollection;
        }
    }
}
