using InventoryMgmt.Persistence.Entities;
using InventoryMgmt.Domain.Enums;
using Xunit;
using System;

namespace InventoryMgmt.Persistence.InMemory.Tests
{
    public class RepositoryTest
    {
        readonly InMemoryItemRepository _itemRepository;

        public RepositoryTest()
        {
            _itemRepository = new InMemoryItemRepository();
        }

        [Theory]
        [InlineData("Book01", 10.50, 13.79)]
        [InlineData("Food01", 1.47, 3.98)]
        [InlineData("Med01", 30.63, 34.29)]
        [InlineData("Tab01", 57.00, 84.98)]
        public void Should_Create_Items_In_Memory(string itemName, decimal buyPrice, decimal sellPrice)
        {
            _itemRepository.Add(new ItemData(itemName, buyPrice, sellPrice));
            var addedItem = _itemRepository.Get(itemName);
            Assert.NotNull(addedItem);
            Assert.Equal(addedItem.Name, itemName);
            Assert.Equal(addedItem.CostPrice, buyPrice);
            Assert.Equal(addedItem.SellPrice, sellPrice);
        }

        [Fact]
        public void Should_Update_Quantity_In_Memory()
        {
            var unknownItem = _itemRepository.Get("Book01");
            Assert.Null(unknownItem);
            _itemRepository.Add(new ItemData("Book01", 10.50M, 13.79M));
            var addedItem = _itemRepository.Get("Book01");
            Assert.NotNull(addedItem);
            addedItem.Quantity = 100;
            _itemRepository.Update(addedItem);
            Assert.Equal(100, addedItem.Quantity);
        }

        [Fact]
        public void Should_Delete_Quantity_In_Memory()
        {
            var unknownItem = _itemRepository.Get("Book01");
            Assert.Null(unknownItem);
            _itemRepository.Add(new ItemData("Book01", 10.50M, 13.79M));
            var addedItem = _itemRepository.Get("Book01");
            Assert.NotNull(addedItem);
            addedItem.Quantity = 100;
            _itemRepository.Delete(addedItem);
            addedItem = _itemRepository.Get("Book01");
            Assert.Null(addedItem);
        }

        [Fact]
        public void Get_Items_In_Memory()
        {
            _itemRepository.Add(new ItemData("Book01", 10.50M, 13.79M));
            _itemRepository.Add(new ItemData("Food01", 1.47M, 3.98M));
            _itemRepository.Add(new ItemData("Med01", 30.63M, 34.29M));
            _itemRepository.Add(new ItemData("Tab01", 57.00M, 84.98M));

            var addedItems= _itemRepository.GetAll();
            Assert.NotNull(addedItems);
            int count = 0;
            var enumerator = addedItems.GetEnumerator();
            while(enumerator.MoveNext())
            {
                count++;
            }
            Assert.Equal(4, count);
            var bookItem = _itemRepository.Get("Book01");
            Assert.Equal(10.50M, bookItem.CostPrice);
            Assert.NotEqual(10.50M, bookItem.SellPrice);
        }

        [Fact]
        public void GetProfitWithInTimePeriod_In_Memory()
        {
            _itemRepository.Add(new ItemData("Book01", 10.00M, 15.00M));
            var FirstReportTimePeriod = DateTime.UtcNow;
            var addedItem = _itemRepository.Get("Book01");
            //buy products
            addedItem.Quantity = 100;
            addedItem.ItemTransactions.Add(new ItemTransactionDeatil(100, InventoryType.Buy, addedItem.CostPrice, addedItem.SellPrice));
            //Sell Products
            addedItem.ItemTransactions.Add(new ItemTransactionDeatil(50, InventoryType.Sell, addedItem.CostPrice, addedItem.SellPrice));


            var profitAmt = _itemRepository.GetProfitWithInTimePeriod(FirstReportTimePeriod);
            Assert.Equal(250.00M, profitAmt);
            var SecondReportTimePeriod = DateTime.UtcNow;
            //Sell Products after first report run
            addedItem.ItemTransactions.Add(new ItemTransactionDeatil(10, InventoryType.Sell, addedItem.CostPrice, addedItem.SellPrice));
            profitAmt = _itemRepository.GetProfitWithInTimePeriod(SecondReportTimePeriod);
            Assert.Equal(50.00M, profitAmt);
        }
    }
}

