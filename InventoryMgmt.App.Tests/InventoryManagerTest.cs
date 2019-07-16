using InventoryMgmt.Persistence.Entities;
using InventoryMgmt.Domain.Enums;
using Xunit;
using System;
using InventoryMgmt.Persistence.InMemory;
using InventoryMgmt.Persistence.Interfaces;
using InventoryMgmt.App.Manager;
using InventoryMgmt.App.Items;

namespace InventoryMgmt.App.Tests
{
    public class InventoryManagerTest
    {
        readonly IItemRepository<ItemData> _itemRepository;
        readonly InventoryManager _inventoryManager;

        public InventoryManagerTest()
        {
            _itemRepository = new InMemoryItemRepository();
            _inventoryManager = new InventoryManager();


        }

        [Fact]
        public void Test_InventoryMgr()
        {
            var timestampBeforReportRun = DateTime.UtcNow;
            _inventoryManager.AddInventoryCommand(new CreateItemCommand(_itemRepository, new ItemData("Book01", 10.50M, 13.79M)));
            Assert.True(_inventoryManager.HasPendingCommands);
            _inventoryManager.AddInventoryCommand(new CreateItemCommand(_itemRepository, new ItemData("Food01", 1.47M, 3.98M)));
            var foodItem = _itemRepository.Get("Food01");
            Assert.True(_inventoryManager.HasPendingCommands);
            _inventoryManager.AddInventoryCommand(new CreateItemCommand(_itemRepository, new ItemData("Med01", 30.63M, 34.29M)));

            var medItem = _itemRepository.Get("Med01");
            Assert.True(_inventoryManager.HasPendingCommands);
            _inventoryManager.AddInventoryCommand(new CreateItemCommand(_itemRepository, new ItemData("Tab01", 57.00M, 84.98M)));
            var tabItem = _itemRepository.Get("Tab01");
            Assert.True(_inventoryManager.HasPendingCommands);

            _inventoryManager.AddInventoryCommand(new UpdateBuyItemCommand(_itemRepository, "Tab01", 100));
            _inventoryManager.AddInventoryCommand(new UpdateSellItemCommand(_itemRepository, "Tab01", 2));
            _inventoryManager.AddInventoryCommand(new UpdateBuyItemCommand(_itemRepository, "Food01", 500));
            _inventoryManager.AddInventoryCommand(new UpdateBuyItemCommand(_itemRepository, "Book01", 100));

            _inventoryManager.AddInventoryCommand(new UpdateBuyItemCommand(_itemRepository, "Med01", 100));
            _inventoryManager.AddInventoryCommand(new UpdateSellItemCommand(_itemRepository, "Food01", 1));
            _inventoryManager.AddInventoryCommand(new UpdateSellItemCommand(_itemRepository, "Food01", 1));
            _inventoryManager.AddInventoryCommand(new UpdateSellItemCommand(_itemRepository, "Tab01", 2));

            _inventoryManager.AddInventoryCommand(new ReportItemCommand(_itemRepository));
            _inventoryManager.ProcessInventoryCommands();

            Assert.False(_inventoryManager.HasPendingCommands);
            Assert.Equal(116.94M, _itemRepository.GetProfitWithInTimePeriod(timestampBeforReportRun));
            var timestampAfterReportRun = DateTime.UtcNow;
            _inventoryManager.AddInventoryCommand(new DeleteItemCommand(_itemRepository, "Book01"));
            _inventoryManager.AddInventoryCommand(new UpdateSellItemCommand(_itemRepository, "Tab01", 5));
            _inventoryManager.AddInventoryCommand(new CreateItemCommand(_itemRepository, new ItemData("Mobile01", 10.51M, 44.56M)));
            _inventoryManager.AddInventoryCommand(new UpdateBuyItemCommand(_itemRepository, "Mobile01", 250));
            _inventoryManager.AddInventoryCommand(new UpdateSellItemCommand(_itemRepository, "Food01", 5));
            _inventoryManager.AddInventoryCommand(new UpdateSellItemCommand(_itemRepository, "Mobile01", 4));
            _inventoryManager.AddInventoryCommand(new UpdateSellItemCommand(_itemRepository, "Med01", 10));            
            _inventoryManager.AddInventoryCommand(new ReportItemCommand(_itemRepository));
            Assert.True(_inventoryManager.HasPendingCommands);

            _inventoryManager.ProcessInventoryCommands();
            Assert.False(_inventoryManager.HasPendingCommands);
            Assert.Equal(-724.75M, _itemRepository.GetProfitWithInTimePeriod(timestampAfterReportRun));
        }
    }
}
