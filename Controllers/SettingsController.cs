using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MSIS.Models;
using MSIS.ViewModels;

namespace MultiSolution.Controllers
{
    public class SettingsController : Controller
    {
        public SQLSettingsRepository SettingsRepository { get; }
        public IHostingEnvironment HostingEnvironment { get; }
        public UserManager<ApplicationUser> userManager { get; }

        public SettingsController(SQLSettingsRepository settingsRepository, IHostingEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager)
        {
            SettingsRepository = settingsRepository;
            HostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
        }
        public IActionResult testSendMail()
        {
            //SettingsRepository.testSendMail();
            try
            {
            string Message = "نظام ادارة المهام";
            var Employees = new List<int>() { 34 };
            SettingsRepository.SendEmail(Employees, Message);
            return new JsonResult(true);

            }
            catch(Exception ex)
            {
             return new JsonResult(ex.Message.ToString());
               
            }
        }
        public IActionResult SendMail(List<int> Employees, string Message)
        {
            SettingsRepository.SendEmail(Employees, Message);
            return new JsonResult(true);
        }

        //--------------------------- Currency
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            string errorMessage = "";
            errorMessage = SettingsRepository.ValidateDeletCurrency(Id);
            //PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
            if (errorMessage == "")
            {
                Currency currency = SettingsRepository.Delete(Id);
                if (currency == null)
                {
                    return Redirect("NotFound");
                }
                else
                {
                    //return new JsonResult("{Deleted:true,ErrorText:''}");
                    List<Currency> model = SettingsRepository.GetCurrencyList().ToList();
                    return new JsonResult(model);

                    //return PartialView("_PurchaseOrderItems", model);

                }

            }
            else
            {
                return new JsonResult(errorMessage);
            }
        }

        [HttpPost]
        public IActionResult ValidateDelete(int Id)
        {
            string errorMessage = "";
            errorMessage = SettingsRepository.ValidateDeletCurrency(Id);
            //PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
            return new JsonResult(errorMessage);
        }

        [HttpGet]
        public IActionResult ListCurrency()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MSIS.ViewModels.UserPermissionsViewModel permission = SettingsRepository.GetSettingsUserParentMenuPermission(userId, "Currency");

            ListCurrencyViewModel model = SettingsRepository.ListCurrencies();
            model.userPermission = permission.UserPermissions[0];

            return View(model);

            //List<Currency> model = new List<Currency>();
            //model = SettingsRepository.GetCurrencyList().ToList();
            //return View(model);
        }
        [HttpGet]
        public IActionResult CurrencyDetails(int Id)
        {
            CurrencyDetailsViewModel model = new CurrencyDetailsViewModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MSIS.ViewModels.UserPermissionsViewModel permission = SettingsRepository.GetSettingsUserParentMenuPermission(userId, "Currency");

            if (permission.UserPermissions.Count > 0)
            {
                model.Permission = permission.UserPermissions[0];
            }


            var currency = SettingsRepository.GetCurrency(Id);
            model.Currency = currency;
            return View(model);
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
        //--------------------------- PurchaseOrderPermission
        [HttpPost]
        public IActionResult DeletePurchaseOrderPermission(int Id)
        {
            PurchaseOrderPermission purchaseOrderPermission = SettingsRepository.DeletePurchaseOrderPermission(Id);
                if (purchaseOrderPermission == null)
                {
                    return Redirect("NotFound");
                }
                else
                {
                    //return new JsonResult("{Deleted:true,ErrorText:''}");
                    List<PurchaseOrderPermission> model = SettingsRepository.GetPurchaseOrderPermissionList().ToList();
                    return new JsonResult(model);

                    //return PartialView("_PurchaseOrderItems", model);

                }

        }


        [HttpGet]
        public IActionResult ListPurchaseOrderPermission()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MSIS.ViewModels.UserPermissionsViewModel permission = SettingsRepository.GetSettingsUserParentMenuPermission(userId, "PurchaseOrderPermission");

            ListPurchaseOrderPermissionViewModel model = SettingsRepository.ListPurchaseOrderPermissions();
            if (permission.UserPermissions.Count() > 0)
            {
                model.userPermission = permission.UserPermissions[0];
            }
            else
            {
                model.userPermission = new UserPermissionDetailsViewModel();
            }
            return View(model);

            //List<Currency> model = new List<Currency>();
            //model = SettingsRepository.GetCurrencyList().ToList();
            //return View(model);
        }
        [HttpGet]
        public IActionResult PurchaseOrderPermissionDetails(int Id)
        {
            PurchaseOrderPermissionDetailsViewModels model = new PurchaseOrderPermissionDetailsViewModels();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MSIS.ViewModels.UserPermissionsViewModel permission = SettingsRepository.GetSettingsUserParentMenuPermission(userId, "PurchaseOrderPermission");
            if (permission.UserPermissions.Count > 0)
            {
                model.Permission = permission.UserPermissions[0];
            }


            var purchaseOrderPermission = SettingsRepository.GetPurchaseOrderPermission(Id);
            var user = userManager.Users.Where(x=>x.Id == purchaseOrderPermission.UserId).FirstOrDefault();
            if (user!= null)
            {
                model.UserName = user.UserName;
            }
            if (purchaseOrderPermission.BranchId == 0)
            {
                model.BranchName = "All";
            }
            else
            {
                var branch = SettingsRepository.context.Branches.ToList().Where(x => x.Id == purchaseOrderPermission.BranchId).FirstOrDefault();
                if (branch != null)
                {
                    model.BranchName = branch.Name;
                }

            }
            model.purchaseOrderPermission = purchaseOrderPermission;
            return View(model);
        }
        [HttpGet]
        public IActionResult EditPurchaseOrderPermission(int Id)
        {
            CreatePurchaseOrderPermissionViewModel model = new CreatePurchaseOrderPermissionViewModel();
            model.Users = userManager.Users.ToList();

            PurchaseOrderPermission purchaseOrderPermission = SettingsRepository.GetPurchaseOrderPermission(Id);
            if (purchaseOrderPermission == null)
            {
                return Redirect("NotFound");
            }
            else
            {
                model.AllowConfirm = purchaseOrderPermission.AllowConfirm;
                model.AllowDelivery = purchaseOrderPermission.AllowDelivery;
                model.AllowPrint = purchaseOrderPermission.AllowPrint;
                model.AllowVerify = purchaseOrderPermission.AllowVerify;
                model.Id = purchaseOrderPermission.Id;
                model.Notes = purchaseOrderPermission.Notes;
                model.UserId = purchaseOrderPermission.UserId;
                model.BranchId = purchaseOrderPermission.BranchId;
                model.Branches = SettingsRepository.context.Branches.ToList();
                model.Branches.Insert(0,new Branch() { 
                        Id=0,
                        Name="All"
                });
                return View(model);
            }
        }
        [HttpPost]
        public IActionResult EditPurchaseOrderPermission(PurchaseOrderPermission purchaseOrderPermissionChanges)
        {
            if (ModelState.IsValid)
            {
                PurchaseOrderPermission purchaseOrderPermission = SettingsRepository.GetPurchaseOrderPermission(purchaseOrderPermissionChanges.Id);
                if (purchaseOrderPermission == null)
                {
                    return Redirect("NotFound");
                }
                else
                {
                    purchaseOrderPermission.AllowConfirm = purchaseOrderPermissionChanges.AllowConfirm;
                    purchaseOrderPermission.AllowDelivery = purchaseOrderPermissionChanges.AllowDelivery;
                    purchaseOrderPermission.AllowPrint= purchaseOrderPermissionChanges.AllowDelivery;
                    purchaseOrderPermission.AllowVerify= purchaseOrderPermissionChanges.AllowVerify;
                    purchaseOrderPermission.Notes= purchaseOrderPermissionChanges.Notes;
                    purchaseOrderPermission.UserId= purchaseOrderPermissionChanges.UserId;
                    purchaseOrderPermission.BranchId = purchaseOrderPermissionChanges.BranchId;
                    SettingsRepository.UpdatePurchaseOrderPermission(purchaseOrderPermission);
                    return RedirectToAction("ListPurchaseOrderPermission", "Settings");
                }
            }
            return View(purchaseOrderPermissionChanges);
        }
        [HttpGet]
        public IActionResult CreatePurchaseOrderPermission()
        {
            CreatePurchaseOrderPermissionViewModel model = new CreatePurchaseOrderPermissionViewModel();
            model.Users = userManager.Users.ToList();
            model.Branches = SettingsRepository.context.Branches.ToList();
            model.Branches.Insert(0, new Branch()
            {
                Id = 0,
                Name = "All"
            });
            return View(model);
        }
        [HttpPost]
        public IActionResult CreatePurchaseOrderPermission(CreatePurchaseOrderPermissionViewModel purchaseOrderPermission)
        {
            if (ModelState.IsValid)
            {
                SettingsRepository.AddPurchaseOrderPermission(purchaseOrderPermission);
                return RedirectToAction("ListPurchaseOrderPermission", "Settings"); 
            }
            return View();
        }
        //--------------------------  end Currency

        //--------------------------  Item Unit

        [HttpPost]
        public IActionResult DeleteItemUnit(int Id)
        {
            string errorMessage = "";
            errorMessage = SettingsRepository.ValidateDeletItemUnit(Id);
            //PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
            if (errorMessage == "")
            {
                ItemUnit itemUnit = SettingsRepository.DeleteItemUnit(Id);
                if (itemUnit == null)
                {
                    return Redirect("NotFound");
                }
                else
                {
                    //return new JsonResult("{Deleted:true,ErrorText:''}");
                    List<ItemUnit> model = SettingsRepository.GetItemUnitList().ToList();
                    return new JsonResult(model);

                    //return PartialView("_PurchaseOrderItems", model);

                }

            }
            else
            {
                return new JsonResult(errorMessage);
            }
        }

        [HttpPost]
        public IActionResult ValidateDeleteItemUnit(int Id)
        {
            string errorMessage = "";
            errorMessage = SettingsRepository.ValidateDeletItemUnit(Id);
            //PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
            return new JsonResult(errorMessage);
        }

        [HttpGet]
        public IActionResult ListItemUnit()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MSIS.ViewModels.UserPermissionsViewModel permission = SettingsRepository.GetSettingsUserParentMenuPermission(userId, "Item Units");

            ListItemUnitsViewModel model = SettingsRepository.ListItemUnits();
            model.userPermission = permission.UserPermissions[0];

            return View(model);


            //List<ItemUnit> model = new List<ItemUnit>();
            //model = SettingsRepository.GetItemUnitList().ToList();
            //return View(model);
        }
        [HttpGet]
        public IActionResult ItemUnitDetails(int Id)
        {
            ItemUnitsDetailsViewModel model = new ItemUnitsDetailsViewModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MSIS.ViewModels.UserPermissionsViewModel permission = SettingsRepository.GetSettingsUserParentMenuPermission(userId, "Item Units");

            if (permission.UserPermissions.Count > 0)
            {
                model.Permission = permission.UserPermissions[0];
            }
            var itemUnit = SettingsRepository.GetItemUnit(Id);
            model.ItemUnit = itemUnit;
            return View(model);
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
        [HttpPost]
        public IActionResult DeleteItemCategory(int Id)
        {
            string errorMessage = "";
            errorMessage = SettingsRepository.ValidateDeletItemCategory(Id);
            //PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
            if (errorMessage == "")
            {
                ItemCategory itemCategory = SettingsRepository.DeleteItemCat(Id);
                if (itemCategory == null)
                {
                    return Redirect("NotFound");
                }
                else
                {
                    //return new JsonResult("{Deleted:true,ErrorText:''}");
                    List<ItemCategory> model = SettingsRepository.GetItemCategoryList().ToList();
                    return new JsonResult(model);

                    //return PartialView("_PurchaseOrderItems", model);

                }

            }
            else
            {
                return new JsonResult(errorMessage);
            }
        }

        [HttpPost]
        public IActionResult ValidateDeleteItemCategory(int Id)
        {
            string errorMessage = "";
            errorMessage = SettingsRepository.ValidateDeletItemCategory(Id);
            //PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
            return new JsonResult(errorMessage);
        }

        [HttpGet]
        public IActionResult ListItemCategory()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MSIS.ViewModels.UserPermissionsViewModel permission = SettingsRepository.GetSettingsUserParentMenuPermission(userId, "Item Categories");

            ListItemCategoriesViewModel model = SettingsRepository.ListItemCategories();
            model.userPermission = permission.UserPermissions[0];

            return View(model);


            //List<ItemCategory> model = new List<ItemCategory>();
            //model = SettingsRepository.GetItemCategoryList().ToList();
            //return View(model);
        }
        [HttpGet]
        public IActionResult ItemCategoryDetails(int Id)
        {
            ItemCategoryDetailsViewModel model = new ItemCategoryDetailsViewModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MSIS.ViewModels.UserPermissionsViewModel permission = SettingsRepository.GetSettingsUserParentMenuPermission(userId, "Item Categories");

            if (permission.UserPermissions.Count > 0)
            {
                model.Permission = permission.UserPermissions[0];
            }

            var itemCategory = SettingsRepository.GetItemCategory(Id);
            model.ItemCategory = itemCategory;
            return View(model);
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
        [HttpPost]
        public IActionResult DeleteMainItem(int Id)
        {
            string errorMessage = "";
            errorMessage = SettingsRepository.ValidateDeletMainItem(Id);
            //PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
            if (errorMessage == "")
            {
                MainItem mainItem = SettingsRepository.DeleteMainItem(Id);
                if (mainItem == null)
                {
                    return Redirect("NotFound");
                }
                else
                {
                    //return new JsonResult("{Deleted:true,ErrorText:''}");
                    List<MainItemViewModel> model = SettingsRepository.GetMainItemList().ToList();
                    return new JsonResult(model);

                    //return PartialView("_PurchaseOrderItems", model);

                }

            }
            else
            {
                return new JsonResult(errorMessage);
            }
        }

        [HttpPost]
        public IActionResult ValidateDeleteMainItem(int Id)
        {
            string errorMessage = "";
            errorMessage = SettingsRepository.ValidateDeletMainItem(Id);
            //PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
            return new JsonResult(errorMessage);
        }

        [HttpGet]
        public IActionResult ListMainItems()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MSIS.ViewModels.UserPermissionsViewModel permission = SettingsRepository.GetSettingsUserParentMenuPermission(userId, "Main Items");

            ListMainItemViewModel model = SettingsRepository.ListMainItems();
            model.userPermission = permission.UserPermissions[0];

            return View(model);


            //List<MainItemViewModel> model = new List<MainItemViewModel>();
            //model = SettingsRepository.GetMainItemList().ToList();
            //return View(model);
        }
        [HttpGet]
        public IActionResult MainItemDetails(int Id)
        {
            MainItemsDetailViewModel model = new MainItemsDetailViewModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MSIS.ViewModels.UserPermissionsViewModel permission = SettingsRepository.GetSettingsUserParentMenuPermission(userId, "Main Items");

            if (permission.UserPermissions.Count > 0)
            {
                model.Permission = permission.UserPermissions[0];
            }

            var mainItem = SettingsRepository.GetMainItemDetails(Id);
            model.MainItem = mainItem;
            return View(model);
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
        [HttpPost]
        public IActionResult DeleteItem(int Id)
        {
            string errorMessage = "";
            errorMessage = SettingsRepository.ValidateDeletItem(Id);
            //PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
            if (errorMessage == "")
            {
                Item item = SettingsRepository.DeleteItem(Id);
                if (item == null)
                {
                    return Redirect("NotFound");
                }
                else
                {
                    //return new JsonResult("{Deleted:true,ErrorText:''}");
                    List<ItemDetailsViewModels> model = SettingsRepository.GetItemList().ToList();
                    return new JsonResult(model);

                    //return PartialView("_PurchaseOrderItems", model);

                }

            }
            else
            {
                return new JsonResult(errorMessage);
            }
        }

        [HttpPost]
        public IActionResult ValidateDeleteItem(int Id)
        {
            string errorMessage = "";
            errorMessage = SettingsRepository.ValidateDeletItem(Id);
            //PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
            return new JsonResult(errorMessage);
        }

        [HttpGet]
        public IActionResult ListItems()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MSIS.ViewModels.UserPermissionsViewModel permission = SettingsRepository.GetSettingsUserParentMenuPermission(userId, "Items");

            ListItemsViewModel model = SettingsRepository.ListItems();
            model.userPermission = permission.UserPermissions[0];

            return View(model);



            //List<ItemDetailsViewModels> model = new List<ItemDetailsViewModels>();
            //model = SettingsRepository.GetItemList().ToList();
            //return View(model);
        }
        [HttpGet]
        public IActionResult ItemDetails(int Id)
        {
            ItemsDetailsViewModel model = new ItemsDetailsViewModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MSIS.ViewModels.UserPermissionsViewModel permission = SettingsRepository.GetSettingsUserParentMenuPermission(userId, "Items");

            if (permission.UserPermissions.Count > 0)
            {
                model.Permission = permission.UserPermissions[0];
            }


            var item = SettingsRepository.GetItemDetails(Id);
            model.ItemDetails = item;
            return View(model);
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
                    string uniqueFileName = null;
                    if (itemChanges.Photos != null && itemChanges.Photos.Count > 0)
                    {
                        foreach (IFormFile photo in itemChanges.Photos)
                        {
                            string UploadFolders = Path.Combine(HostingEnvironment.WebRootPath, "images");
                            uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(photo.FileName);// model.Photo.Name;
                            string filePath = Path.Combine(UploadFolders, uniqueFileName);
                            photo.CopyTo(new FileStream(filePath, FileMode.Create));
                        }
                    }

                    item.ItemName = itemChanges.ItemName;
                    item.MainItemId = itemChanges.MainItemId;
                    item.ItemUnitId = itemChanges.ItemUnitId;
                    item.UnitPrice = itemChanges.UnitPrice;
                    item.CurrencyId = itemChanges.CurrencyId;
                    item.OtherInformation = itemChanges.OtherInformation;
                    if (uniqueFileName != null)
                    {
                        item.PhotoPath = uniqueFileName;
                    };
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
        public IActionResult CreateItem(ItemDetailsViewModels model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photos != null && model.Photos.Count > 0)
                {
                    foreach (IFormFile photo in model.Photos)
                    {
                        string UploadFolders = Path.Combine(HostingEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(photo.FileName);// model.Photo.Name;
                        string filePath = Path.Combine(UploadFolders, uniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                }
                Item item = new Item();
                item.ItemName = model.ItemName;
                item.ItemUnitId = model.ItemUnitId;
                item.CurrencyId = model.CurrencyId;
                item.MainItemId = model.MainItemId;
                item.OtherInformation = model.OtherInformation;
                item.UnitPrice = model.UnitPrice;
                if (uniqueFileName != null)
                {
                    item.PhotoPath = uniqueFileName;
                };
                SettingsRepository.AddItem(item);
                return RedirectToAction("ListItems", "Settings");
            }
            return View();
        }
        //--------------------------  end Item 
        [HttpGet]
        public IActionResult getMainMenuPermission(string UserId)
        {
            UserPermissionsViewModel Permission= SettingsRepository.GetUserParentMenuPermission(UserId);
            return new JsonResult(Permission); 
            //return PartialView("~/Views/Shared/_SideBar.cshtml", Permission); 
        }

        //--------------------------  User ProjectsList 
        [HttpGet]
        public IActionResult ListUserProjects(string UserId)
        {
            try
            {
                UserProjectListViewModel model = new UserProjectListViewModel();
                MSIS.ViewModels.UserPermissionsViewModel permission = SettingsRepository.GetUserParentMenuPermission(UserId, "UserTaskPermissions");             
                model.UserProjects = SettingsRepository.GetUserProjects(UserId);            
                model.userPermission = permission.UserPermissions[0];
                model.UserId = UserId;
                model.Projects = SettingsRepository.context.Projects.ToList();
                return View(model);

            }catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View("NotFound");
            }
            //return PartialView("~/Views/Shared/_SideBar.cshtml", Permission); 
        }

        [HttpPost]
        public IActionResult AddUserProject(String UserId, int ProjectId)
        {
            UserProject userProject = new UserProject();
            userProject.UserId = UserId;
            userProject.ProjectId = ProjectId;
            SettingsRepository.AddUserProject(userProject);


            List<SQLUserProjectsViewModel> model = SettingsRepository.GetUserProjects(UserId);
            return new JsonResult(model);
            //return PartialView("_PurchaseOrderItems", model);
        }

        [HttpPost]
        public IActionResult DeleteUserProject(int Id)
        {
            //string errorMessage = "";
            //            errorMessage = SettingsRepository.ValidateDeletItem(Id);
            //PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
            //if (errorMessage == "")
            //{
            UserProject userProject = SettingsRepository.DeleteUserProject(Id);
                if (userProject == null)
                {
                    return Redirect("NotFound");
                }
                else
                {
                //return new JsonResult("{Deleted:true,ErrorText:''}");
                List<SQLUserProjectsViewModel> model = SettingsRepository.GetUserProjects(userProject.UserId);
                return new JsonResult(model);

                //return PartialView("_PurchaseOrderItems", model);

            }

            //}
            //else
            //{
            //    return new JsonResult(errorMessage);
            //}
        }
        [HttpGet]
        public IActionResult EditUserProject(int Id)
        {
            UserProjectCreateViewModel model = new UserProjectCreateViewModel();
            UserProject userProject = SettingsRepository.GetUserProject(Id);
            if (userProject == null)
            {
                return Redirect("NotFound");
            }
            else
            {

                model.ProjectId = userProject.Id;
                model.ProjectId = userProject.ProjectId;
                model.UserId = userProject.UserId;
                model.Projects = SettingsRepository.context.Projects.ToList();
                model.Users = userManager.Users.ToList();
                return View(userProject);
            }
        }

        [HttpGet]
        public IActionResult GetUserProject(int Id)
        {
            UserProject userProject = SettingsRepository.GetUserProject(Id);
            return new JsonResult(userProject);
        }
        [HttpPost]
        public IActionResult EditUserProject(int Id, int ProjectId)
        {
                UserProject userProject = SettingsRepository.GetUserProject(Id);
                if (userProject == null)
                {
                    return Redirect("NotFound");
                }
                else
                {
                    userProject.ProjectId = ProjectId;
                    SettingsRepository.UpdateUserProject(userProject);


                    List<SQLUserProjectsViewModel> model = SettingsRepository.GetUserProjects(userProject.UserId);
                    return new JsonResult(model);
                    //return PartialView("_PurchaseOrderItems", model);

                }

            return new JsonResult(false);
        }

        //--------------------------  User Branches List 
        [HttpGet]
        public IActionResult ListUserBranches(string UserId)
        {
            try
            {
                UserBranchListViewModel model = new UserBranchListViewModel();
                MSIS.ViewModels.UserPermissionsViewModel permission = SettingsRepository.GetUserParentMenuPermission(UserId, "UserTaskPermissions");
                model.UserBranches = SettingsRepository.GetUserBranches(UserId);
                model.userPermission = permission.UserPermissions[0];
                model.UserId = UserId;
                model.Branches = SettingsRepository.context.Branches.ToList();
                return View(model);

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View("NotFound");
            }
            //return PartialView("~/Views/Shared/_SideBar.cshtml", Permission); 
        }

        [HttpPost]
        public IActionResult AddUserBranch(String UserId, int BranchId)
        {
            UserBranch userBranch = new UserBranch();
            userBranch.UserId = UserId;
            userBranch.BranchId = BranchId;
            SettingsRepository.AddUserBranch(userBranch);


            List<SQLUserBranchesViewModel> model = SettingsRepository.GetUserBranches(UserId);
            return new JsonResult(model);
            //return PartialView("_PurchaseOrderItems", model);
        }

        [HttpPost]
        public IActionResult DeleteUserBranch(int Id)
        {
            //string errorMessage = "";
            //            errorMessage = SettingsRepository.ValidateDeletItem(Id);
            //PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
            //if (errorMessage == "")
            //{
            UserBranch userBranch = SettingsRepository.DeleteUserBranch(Id);
            if (userBranch == null)
            {
                return Redirect("NotFound");
            }
            else
            {
                //return new JsonResult("{Deleted:true,ErrorText:''}");
                List<SQLUserBranchesViewModel> model = SettingsRepository.GetUserBranches(userBranch.UserId);
                return new JsonResult(model);

                //return PartialView("_PurchaseOrderItems", model);

            }

            //}
            //else
            //{
            //    return new JsonResult(errorMessage);
            //}
        }
        [HttpGet]
        public IActionResult EditUserBranch(int Id)
        {
            UserProjectCreateViewModel model = new UserProjectCreateViewModel();
            UserProject userProject = SettingsRepository.GetUserProject(Id);
            if (userProject == null)
            {
                return Redirect("NotFound");
            }
            else
            {

                model.Id = userProject.Id;
                model.ProjectId = userProject.ProjectId;
                model.UserId = userProject.UserId;
                model.Projects = SettingsRepository.context.Projects.ToList();
                model.Users = userManager.Users.ToList();
                return View(userProject);
            }
        }

        [HttpGet]
        public IActionResult GetUserBranch(int Id)
        {
            UserBranch userBranch = SettingsRepository.GetUserBranch(Id);
            return new JsonResult(userBranch);
        }
        [HttpPost]
        public IActionResult EditUserBranch(int Id, int BranchId)
        {
            UserBranch userBranch = SettingsRepository.GetUserBranch(Id);
            if (userBranch == null)
            {
                return Redirect("NotFound");
            }
            else
            {
                userBranch.BranchId = BranchId;
                SettingsRepository.UpdateUserBranch(userBranch);


                List<SQLUserBranchesViewModel> model = SettingsRepository.GetUserBranches(userBranch.UserId);
                return new JsonResult(model);
                //return PartialView("_PurchaseOrderItems", model);
            }
        }
        //--------------------------  User Employees List 
        [HttpGet]
        public IActionResult ListUserEmployees(string UserId)
        {
            try
            {
                UserEmployeesListViewModel model = new UserEmployeesListViewModel();
                MSIS.ViewModels.UserPermissionsViewModel permission = SettingsRepository.GetUserParentMenuPermission(UserId, "UserTaskPermissions");
                model.UserEmployees = SettingsRepository.GetUserEmployees(UserId);
                model.userPermission = permission.UserPermissions[0];
                model.UserId = UserId;
                model.Employees = SettingsRepository.context.Employees.ToList();
                return View(model);

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View("NotFound");
            }
            //return PartialView("~/Views/Shared/_SideBar.cshtml", Permission); 
        }

        [HttpPost]
        public IActionResult AddUserEmployee(String UserId, int EmployeeId)
        {
            UserEmployee userEmployee = new UserEmployee();
            userEmployee.UserId = UserId;
            userEmployee.EmployeeId = EmployeeId;
            SettingsRepository.AddUserEmployee(userEmployee);


            List<SQLUserEmployeesViewModel> model = SettingsRepository.GetUserEmployees(UserId);
            return new JsonResult(model);
            //return PartialView("_PurchaseOrderItems", model);
        }

        [HttpPost]
        public IActionResult DeleteUserEmployee(int Id)
        {
            //string errorMessage = "";
            //            errorMessage = SettingsRepository.ValidateDeletItem(Id);
            //PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
            //if (errorMessage == "")
            //{
            UserEmployee userEmployee = SettingsRepository.DeleteUserEmployee(Id);
            if (userEmployee == null)
            {
                return Redirect("NotFound");
            }
            else
            {
                //return new JsonResult("{Deleted:true,ErrorText:''}");
                List<SQLUserEmployeesViewModel> model = SettingsRepository.GetUserEmployees(userEmployee.UserId);
                return new JsonResult(model);

                //return PartialView("_PurchaseOrderItems", model);

            }

            //}
            //else
            //{
            //    return new JsonResult(errorMessage);
            //}
        }
        [HttpGet]
        public IActionResult EditUserEmployee(int Id)
        {
            UserEmployeeCreateViewModel model = new UserEmployeeCreateViewModel();
            UserEmployee userEmployee = SettingsRepository.GetUserEmployee(Id);
            if (userEmployee == null)
            {
                return Redirect("NotFound");
            }
            else
            {

                model.Id = userEmployee.Id;
                model.EmployeeId = userEmployee.EmployeeId;
                model.UserId = userEmployee.UserId;
                model.Employees = SettingsRepository.context.Employees.ToList();
                model.Users = userManager.Users.ToList();
                return View(userEmployee);
            }
        }

        [HttpGet]
        public IActionResult GetUserEmployee(int Id)
        {
            UserEmployee userEmployee = SettingsRepository.GetUserEmployee(Id);
            return new JsonResult(userEmployee);
        }
        [HttpPost]
        public IActionResult EditUserEmployee(int Id, int EmployeeId)
        {
            UserEmployee userEmployee = SettingsRepository.GetUserEmployee(Id);
            if (userEmployee == null)
            {
                return Redirect("NotFound");
            }
            else
            {
                userEmployee.EmployeeId = EmployeeId;
                SettingsRepository.UpdateUserEmployee(userEmployee);


                List<SQLUserEmployeesViewModel> model = SettingsRepository.GetUserEmployees(userEmployee.UserId);
                return new JsonResult(model);
                //return PartialView("_PurchaseOrderItems", model);
            }
        }
    }
}