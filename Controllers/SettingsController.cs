using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MSIS.Models;
using MSIS.ViewModels;

namespace MultiSolution.Controllers
{
    public class SettingsController : Controller
    {
        public SQLSettingsRepository SettingsRepository { get; }
        public IHostingEnvironment HostingEnvironment { get; }
       public SettingsController(SQLSettingsRepository settingsRepository, IHostingEnvironment hostingEnvironment)
        {
            SettingsRepository = settingsRepository;
            HostingEnvironment = hostingEnvironment;
        }
        //--------------------------- Currency
        [HttpGet]
        public IActionResult ListCurrency()
        {
            List<Currency> model = new List<Currency>();
            model = SettingsRepository.GetCurrencyList().ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult CurrencyDetails(int Id)
        {
            var currency = SettingsRepository.GetCurrency(Id);
            return View(currency);
        }
        [HttpGet]
        public IActionResult EditCurrency(int Id)
        {
            Currency currency = SettingsRepository.GetCurrency(Id);
            if (currency == null)
            {
                return Redirect("NotFound");
            }
            else
            {
                return View(currency);
            }
        }
        [HttpPost]
        public IActionResult EditCurrency(Currency currencyChanges)
        {
            if (ModelState.IsValid)
            {
                Currency currency = SettingsRepository.GetCurrency(currencyChanges.Id);
                if (currency == null)
                {
                    return Redirect("NotFound");
                }
                else
                {
                    currency.CurrencyCode = currencyChanges.CurrencyCode;
                    currency.CurrencyName = currencyChanges.CurrencyName;
                    SettingsRepository.Update(currency);
                    return RedirectToAction("ListCurrency", "Settings");
                }
            }
            return View(currencyChanges);
        }
        [HttpGet]
        public IActionResult CreateCurrency()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCurrency(Currency currency)
        {
            if (ModelState.IsValid)
            {
                SettingsRepository.Add(currency);
                return RedirectToAction("ListCurrency", "Settings");
            }
            return View();
        }
        //--------------------------  end Currency
        //--------------------------  Item Unit

        [HttpGet]
        public IActionResult ListItemUnit()
        {
            List<ItemUnit> model = new List<ItemUnit>();
            model = SettingsRepository.GetItemUnitList().ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult ItemUnitDetails(int Id)
        {
            var currency = SettingsRepository.GetItemUnit(Id);
            return View(currency);
        }
        [HttpGet]
        public IActionResult EditItemUnit(int Id)
        {
            ItemUnit itemUnit = SettingsRepository.GetItemUnit(Id);
            if (itemUnit == null)
            {
                return Redirect("NotFound");
            }
            else
            {
                return View(itemUnit);
            }
        }
        [HttpPost]
        public IActionResult EditItemUnit(ItemUnit itemUnitChanges)
        {
            if (ModelState.IsValid)
            {
                ItemUnit itemUnit = SettingsRepository.GetItemUnit(itemUnitChanges.Id);
                if (itemUnit == null)
                {
                    return Redirect("NotFound");
                }
                else
                {
                    itemUnit.ItemUnitName = itemUnitChanges.ItemUnitName;
                    SettingsRepository.UpdateItemUnit(itemUnit);
                    return RedirectToAction("ListItemUnit", "Settings");
                }
            }
            return View(itemUnitChanges);
        }
        [HttpGet]
        public IActionResult CreateItemUnit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateItemUnit(ItemUnit itemUnit)
        {
            if (ModelState.IsValid)
            {
                SettingsRepository.AddItemUnit(itemUnit);
                return RedirectToAction("ListItemUnit", "Settings");
            }
            return View();
        }
        //--------------------------  end Item Unit
        //-------------------------------- Item Category
        [HttpGet]
        public IActionResult ListItemCategory()
        {
            List<ItemCategory> model = new List<ItemCategory>();
            model = SettingsRepository.GetItemCategoryList().ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult ItemCategoryDetails(int Id)
        {
            var itemCategory = SettingsRepository.GetItemCategory(Id);
            return View(itemCategory);
        }
        [HttpGet]
        public IActionResult EditItemCategory(int Id)
        {
            ItemCategory itemCategory = SettingsRepository.GetItemCategory(Id);
            if (itemCategory == null)
            {
                return Redirect("NotFound");
            }
            else
            {
                return View(itemCategory);
            }
        }
        [HttpPost]
        public IActionResult EditItemCategory(ItemCategory itemCategoryChanges)
        {
            if (ModelState.IsValid)
            {
                ItemCategory itemCategory = SettingsRepository.GetItemCategory(itemCategoryChanges.Id);
                if (itemCategory == null)
                {
                    return Redirect("NotFound");
                }
                else
                {
                    itemCategory.CategoryName = itemCategoryChanges.CategoryName;
                    itemCategory.OtherInformation = itemCategoryChanges.OtherInformation;
                    SettingsRepository.UpdateItemCategory(itemCategory);
                    return RedirectToAction("ListItemCategory", "Settings");
                }
            }
            return View(itemCategoryChanges);
        }
        [HttpGet]
        public IActionResult CreateItemCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateItemCategory(ItemCategory itemCategory)
        {
            if (ModelState.IsValid)
            {
                SettingsRepository.AddItemCat(itemCategory);
                return RedirectToAction("ListItemCategory", "Settings");
            }
            return View();
        }
        //--------------------------  end Item Category
        //-------------------------------- Main Item
        [HttpGet]
        public IActionResult ListMainItems()
        {
            List<MainItemViewModel> model = new List<MainItemViewModel>();
            model = SettingsRepository.GetMainItemList().ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult MainItemDetails(int Id)
        {
            var mainItem = SettingsRepository.GetMainItemDetails(Id);
            return View(mainItem);
        }
        [HttpGet]
        public IActionResult EditMainItem(int Id)
        {
            MainItemViewModel mainItem = SettingsRepository.GetMainItemDetails(Id);
            if (mainItem == null)
            {
                return Redirect("NotFound");
            }
            else
            {
                mainItem.ItemCategories = SettingsRepository.GetItemCategoryList().ToList();
                return View(mainItem);
            }
        }
        [HttpPost]
        public IActionResult EditMainItem(MainItemViewModel mainItemChanges)
        {
            if (ModelState.IsValid)
            {
                MainItem mainItem = SettingsRepository.GetMainItem(mainItemChanges.Id);
                if (mainItem == null)
                {
                    return Redirect("NotFound");
                }
                else
                {
                    mainItem.MainItemName = mainItemChanges.MainItemName;
                    mainItem.ItemCategoryId = mainItemChanges.ItemCategoryId;
                    mainItem.OtherInformation = mainItemChanges.OtherInformation;
                    SettingsRepository.UpdateMainItem(mainItem);
                    return RedirectToAction("ListMainItems", "Settings");
                }
            }
            return View(mainItemChanges);
        }
        [HttpGet]
        public IActionResult CreateMainItem()
        {
            MainItemViewModel model = new MainItemViewModel();
            model.ItemCategories = SettingsRepository.GetItemCategoryList().ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateMainItem(MainItem msinItem)
        {
            if (ModelState.IsValid)
            {
                SettingsRepository.AddMainItem(msinItem);
                return RedirectToAction("ListMainItems", "Settings");
            }
            return View();
        }
        //--------------------------  end Item Category

        //--------------------------------  Item
        [HttpGet]
        public IActionResult ListItems()
        {
            List<ItemDetailsViewModels> model = new List<ItemDetailsViewModels>();
            model = SettingsRepository.GetItemList().ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult ItemDetails(int Id)
        {
            var item = SettingsRepository.GetItemDetails(Id);
            return View(item);
        }
        [HttpGet]
        public IActionResult EditItem(int Id)
        {
            ItemDetailsViewModels item = SettingsRepository.GetItemDetails(Id);
            if (item == null)
            {
                return Redirect("NotFound");
            }
            else
            {
                item.ItemUnits = SettingsRepository.GetItemUnitList().ToList();
                item.MainItems = SettingsRepository.GetMainItemList().ToList();
                item.CurrencyList = SettingsRepository.GetCurrencyList().ToList();
                return View(item);
            }
        }
        [HttpPost]
        public IActionResult EditItem(ItemDetailsViewModels itemChanges)
        {
            if (ModelState.IsValid)
            {
                Item item = SettingsRepository.GetItem(itemChanges.Id);
                if (item == null)
                {
                    return Redirect("NotFound");
                }
                else
                {
                    item.ItemName = itemChanges.ItemName;
                    item.MainItemId = itemChanges.MainItemId;
                    item.ItemUnitId = itemChanges.ItemUnitId;
                    item.UnitPrice = itemChanges.UnitPrice;
                    item.CurrencyId = itemChanges.CurrencyId;
                    item.OtherInformation = itemChanges.OtherInformation;
                    item.PhotoPath = itemChanges.PhotoPath;
                    SettingsRepository.UpdateItem(item);
                    return RedirectToAction("ListItems", "Settings");
                }
            }
            return View(itemChanges);
        }
        [HttpGet]
        public IActionResult CreateItem()
        {
            ItemDetailsViewModels model = new ItemDetailsViewModels();
            model.ItemUnits = SettingsRepository.GetItemUnitList().ToList();
            model.MainItems = SettingsRepository.GetMainItemList().ToList();
            model.CurrencyList = SettingsRepository.GetCurrencyList().ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateItem(Item item)
        {
            if (ModelState.IsValid)
            {
                SettingsRepository.AddItem(item);
                return RedirectToAction("ListItems", "Settings");
            }
            return View();
        }
        //--------------------------  end Item 

    }
}