using InventoryMgmt.App.Items;
using InventoryMgmt.Core.Interfaces;
using InventoryMgmt.Domain.Entities;
using InventoryMgmt.Persistence;
using System;
using InventoryMgmt.Persistence.InMemory;
using InventoryMgmt.App.Manager;

namespace InventoryMgmt.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IRepository<Item> itemRepository = new InMemoryItemRepository();
            var inventoryManager = new InventoryManager();

            inventoryManager.AddInventoryCommand( new CreateItemCommand(itemRepository, new Item("Book01", 10.50M, 13.79M)));
            inventoryManager.AddInventoryCommand(new CreateItemCommand(itemRepository, new Item("Food01", 1.47M, 3.98M)));
            inventoryManager.AddInventoryCommand(new CreateItemCommand(itemRepository, new Item("Med01", 30.63M, 34.29M)));
            inventoryManager.AddInventoryCommand(new CreateItemCommand(itemRepository, new Item("Tab01", 57.00M, 84.98M)));

            inventoryManager.AddInventoryCommand(new UpdateBuyItemCommand(itemRepository, "Tab01", 100));
            inventoryManager.AddInventoryCommand(new UpdateSellItemCommand(itemRepository, "Tab01", 2));
            inventoryManager.AddInventoryCommand(new UpdateBuyItemCommand(itemRepository, "Food01", 500));
            inventoryManager.AddInventoryCommand(new UpdateBuyItemCommand(itemRepository, "Book01", 100));

            inventoryManager.AddInventoryCommand(new UpdateBuyItemCommand(itemRepository, "Med01", 100));
            inventoryManager.AddInventoryCommand(new UpdateSellItemCommand(itemRepository, "Food01", 1));
            inventoryManager.AddInventoryCommand(new UpdateSellItemCommand(itemRepository, "Food01", 1));
            inventoryManager.AddInventoryCommand(new UpdateSellItemCommand(itemRepository, "Tab01", 2));

            inventoryManager.AddInventoryCommand(new ReportItemCommand(itemRepository));

            inventoryManager.AddInventoryCommand(new DeleteItemCommand(itemRepository, "Book01"));
            inventoryManager.AddInventoryCommand(new UpdateSellItemCommand(itemRepository, "Tab01", 5));
            inventoryManager.AddInventoryCommand(new CreateItemCommand(itemRepository, new Item("Mobile01", 10.51M, 44.56M)));
            inventoryManager.AddInventoryCommand(new UpdateBuyItemCommand(itemRepository, "Mobile01", 250));
            inventoryManager.AddInventoryCommand(new UpdateSellItemCommand(itemRepository, "Food01", 5));
            inventoryManager.AddInventoryCommand(new UpdateSellItemCommand(itemRepository, "Mobile01", 4));
            inventoryManager.AddInventoryCommand(new UpdateSellItemCommand(itemRepository, "Med01", 10));

            inventoryManager.AddInventoryCommand(new ReportItemCommand(itemRepository));

            inventoryManager.ProcessInventoryCommands();

            System.Console.Read();

        }
    }
}

