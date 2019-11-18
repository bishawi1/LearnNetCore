using MSIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSIS.Models
{
    public class SQLSettingsRepository
    {
        public AppDBContext context { get; }
        public SQLSettingsRepository(AppDBContext context)
        {
            this.context = context;
        }
        //----------------------- Currency
        public Currency Add(Currency currency)
        {
            context.Currency.Add(currency);
            context.SaveChanges();
            return currency;
        }

        public Currency Delete(int id)
        {
            Currency currency = context.Currency.Find(id);
            if (currency != null)
            {
                context.Currency.Remove(currency);
                context.SaveChanges();
            }
            return currency;
        }

        public IEnumerable<Currency> GetCurrencyList()
        {
            return context.Currency;
        }

        public Currency GetCurrency(int Id)
        {
            return context.Currency.Find(Id);
        }

        public Currency Update(Currency currencyChanges)
        {
            var currency = context.Currency.Attach( currencyChanges);
            currency.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return currencyChanges;
        }
        //--------------End Currency


        //----------------------- Units
        public ItemUnit AddItemUnit(ItemUnit itemUnit)
        {
            context.ItemUnits.Add(itemUnit);
            context.SaveChanges();
            return itemUnit;
        }

        public ItemUnit DeleteItemUnit(int id)
        {
            ItemUnit itemUnit = context.ItemUnits.Find(id);
            if (itemUnit != null)
            {
                context.ItemUnits.Remove(itemUnit);
                context.SaveChanges();
            }
            return itemUnit;
        }

        public IEnumerable<ItemUnit> GetItemUnitList()
        {
            return context.ItemUnits;
        }

        public ItemUnit GetItemUnit(int Id)
        {
            return context.ItemUnits.Find(Id);
        }

        public ItemUnit UpdateItemUnit(ItemUnit itemUnitChanges)
        {
            var currency = context.ItemUnits.Attach(itemUnitChanges);
            currency.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return itemUnitChanges;
        }
        //--------------End Currency

        //----------------------- Item Category
        public ItemCategory AddItemCat(ItemCategory itemCat)
        {
            context.ItemCategories.Add(itemCat);
            context.SaveChanges();
            return itemCat;
        }

        public ItemCategory DeleteItemCat(int id)
        {
            ItemCategory itemCat = context.ItemCategories.Find(id);
            if (itemCat != null)
            {
                context.ItemCategories.Remove(itemCat);
                context.SaveChanges();
            }
            return itemCat;
        }

        public IEnumerable<ItemCategory> GetItemCategoryList()
        {
            return context.ItemCategories;
        }

        public ItemCategory GetItemCategory(int Id)
        {
            return context.ItemCategories.Find(Id);
        }

        public ItemCategory UpdateItemCategory(ItemCategory itemCategoryChanges)
        {
            var itemCategory = context.ItemCategories.Attach(itemCategoryChanges);
            itemCategory.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return itemCategoryChanges;
        }
        //--------------End Category
        //----------------------- Main Item 
        public MainItem AddMainItem(MainItem mainItem)
        {
            context.MainItems.Add(mainItem);
            context.SaveChanges();
            return mainItem;
        }

        public MainItem DeleteMainItem(int id)
        {
            MainItem mainItem = context.MainItems.Find(id);
            if (mainItem != null)
            {
                context.MainItems.Remove(mainItem);
                context.SaveChanges();
            }
            return mainItem;
        }

        public IEnumerable<ViewModels.MainItemViewModel> GetMainItemList()
        {
           var model = (from mainItem in context.MainItems
                                                  join itemCategory in context.ItemCategories
                                                  on mainItem.ItemCategoryId equals itemCategory.Id
                                                  select new ViewModels.MainItemViewModel()
                                                  {
                                                      CategoryName = itemCategory.CategoryName,
                                                      Id = mainItem.Id,
                                                      ItemCategoryId = mainItem.ItemCategoryId,
                                                      MainItemName = mainItem.MainItemName,
                                                      OtherInformation = mainItem.OtherInformation
                                                  }).ToList();
            return model;
        }

        public MainItem GetMainItem(int Id)
        {
            return context.MainItems.Find(Id);
        }
        public ViewModels.MainItemViewModel GetMainItemDetails(int Id)
        {
            var model = (from mainItem in context.MainItems
                         join itemCategory in context.ItemCategories
                         on mainItem.ItemCategoryId equals itemCategory.Id
                         where mainItem.Id==Id
                         select new ViewModels.MainItemViewModel()
                         {
                             CategoryName = itemCategory.CategoryName,
                             Id = mainItem.Id,
                             ItemCategoryId = mainItem.ItemCategoryId,
                             MainItemName = mainItem.MainItemName,
                             OtherInformation = mainItem.OtherInformation
                         }).ToList();
            return model[0];
        }
        public MainItem UpdateMainItem(MainItem mainItemChanges)
        {
            var mainItem = context.MainItems.Attach(mainItemChanges);
            mainItem.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return mainItemChanges;
        }
        //--------------End Main Item


        //-----------------------  Item 
        public Item AddItem(Item item)
        {
            context.Items.Add(item);
            context.SaveChanges();
            return item;
        }

        public Item DeleteItem(int id)
        {
            Item Item = context.Items.Find(id);
            if (Item != null)
            {
                context.Items.Remove(Item);
                context.SaveChanges();
            }
            return Item;
        }

        public IEnumerable<ViewModels.ItemDetailsViewModels> GetItemList()
        {
            var model = (from item in context.Items
                         join mainItem in context.MainItems
                         on item.MainItemId equals mainItem.Id
                         join itemUnit in context.ItemUnits
                         on item.ItemUnitId equals itemUnit.Id
                         join currency in context.Currency
                         on item.CurrencyId equals currency.Id
                         select new ViewModels.ItemDetailsViewModels()
                         {
                             CurrencyName = currency.CurrencyName,
                             CurrencyCode = currency.CurrencyCode,
                             Id = mainItem.Id,
                             ItemUnitName = itemUnit.ItemUnitName,
                             CurrencyId = item.CurrencyId,
                             MainItemName = mainItem.MainItemName,
                             ItemName = item.ItemName,
                             ItemUnitId = item.ItemUnitId,
                             MainItemId = item.MainItemId,
                             PhotoPath = item.PhotoPath,
                             UnitPrice = item.UnitPrice,
                             OtherInformation = item.OtherInformation
                         }).ToList();
            return model;
        }

        public Item GetItem(int Id)
        {
            return context.Items.Find(Id);
        }
        public ViewModels.ItemDetailsViewModels GetItemDetails(int Id)
        {
            var model = (from item in context.Items
                         join mainItem in context.MainItems
                         on item.MainItemId equals mainItem.Id
                         join itemUnit in context.ItemUnits
                         on item.ItemUnitId equals itemUnit.Id
                         join currency in context.Currency
                         on item.CurrencyId equals currency.Id
                         where item.Id == Id
                         select new ViewModels.ItemDetailsViewModels()
                         {
                             CurrencyName = currency.CurrencyName,
                             CurrencyCode=currency.CurrencyCode,
                             Id = mainItem.Id,
                             ItemUnitName=itemUnit.ItemUnitName,
                             CurrencyId=item.CurrencyId,                            
                             MainItemName = mainItem.MainItemName,
                             ItemName=item.ItemName,
                             ItemUnitId=item.ItemUnitId,
                             MainItemId=item.MainItemId,
                             PhotoPath=item.PhotoPath,
                             UnitPrice=item.UnitPrice,                             
                             OtherInformation = item.OtherInformation
                         }).ToList();
            return model[0];
        }
        public Item UpdateItem(Item itemChanges)
        {
            var item = context.Items.Attach(itemChanges);
            item.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return itemChanges;
        }

        //--------------End Main Item

    }
}
