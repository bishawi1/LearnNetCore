using MSIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSIS.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace MSIS.Models
{
    public class SQLSettingsRepository
    {
        public AppDBContext context { get; }
        public SQLSettingsRepository(AppDBContext context)
        {
            this.context = context;
        }
        public Boolean SendMailOnCreateTask()
        {
            Setting settings= context.Settings.Find(1);
            bool sendMail = settings.SendMailOnCreateTask;
            
            return sendMail;
        }
        public AppDBContext getContext()
        {
            return context;
        }
        public string SendEmail(List<int> Employees, string Message)
        {

            try
            {
                Setting settings = context.Settings.Find(1);
                if (settings != null)
                {
                    // Credentials
                    var credentials = new NetworkCredential(settings.SenderEmail, settings.SenderMailPassword);
                    // Mail message
                    var mail = new MailMessage();
             
                    mail.From = new MailAddress(settings.SenderEmail);
                    mail.Subject = "Email Sender App";
                    mail.Body = Message;
                    mail.IsBodyHtml = true;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                    foreach (int employeeId in Employees)
                    {
                        Employee employee = context.Employees.Find(employeeId);
                        if (employee != null)
                        {
                            mail.To.Add(new MailAddress(employee.Email));
                        }
                    }

                    // Smtp client
                    var client = new SmtpClient()
                    {
                        Port = settings.Port,// 587,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Host = settings.SMTPServer,
                        EnableSsl = true,
                        Credentials = credentials
                    };

                    client.Send(mail);
                    
                    return "Email Sent Successfully!";
                }
                else
                {
                    return "employee not Found!";
                }
            }
            catch (System.Exception e)
            {
                return e.Message;
            }

        }


        public UserPermissionsViewModel GetSettingsUserParentMenuPermission(string UserId, string PageName)
        {
            UserPermissionsViewModel model = new UserPermissionsViewModel();
            var result = context.SQLUserAllowedParentMenuesViewModel.FromSql("SELECT * FROM dbo.UserAllowedParentMenu Where ParentName = 'Settings' And UserId = '" + UserId + "' And PageName ='" + PageName + "'").ToList();
            var Menues = result.Select(x => x.ParentName).Distinct().ToList();
            model.ParentMenus = Menues;
            model.UserPermissions = result;
            return model;
        }

        //----------------------- Currency
        public string ValidateDeletCurrency(int Id)
        {
            string ErrorMessage = "";
            var result = context.Offers.Where(x => x.CurrencyId == Id).ToList();
            if (result.Count > 0)
            {
                ErrorMessage = "cannot delete Currency, there is Offers for this Currency";
            }
            else
            {
                var purchaseOrder = context.PurchaseOrders.Where(x => x.CurrencyId == Id).ToList();
                if (purchaseOrder.Count > 0)
                {
                    ErrorMessage = "cannot delete Currency, Currency has Purchase Order";
                }
            }
            return ErrorMessage;
        }


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
        public ListCurrencyViewModel ListCurrencies()
        {
            ListCurrencyViewModel model = new ListCurrencyViewModel();
            model.Currencies = GetCurrencyList().ToList();
            return model;
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
        public ListItemUnitsViewModel ListItemUnits()
        {
            ListItemUnitsViewModel model = new ListItemUnitsViewModel();
            model.ItemUnits = context.ItemUnits.ToList();
            return model;
        }
        public string ValidateDeletItemUnit(int Id)
        {
            string ErrorMessage = "";
            var result = context.offerDetails.Where(x => x.ItemUnitId == Id).ToList();
            if (result.Count > 0)
            {
                ErrorMessage = "cannot delete Item Unit, there is offers for this Item Unit";
            }
            else
            {
                var purchaseOrder = context.Items.Where(x => x.ItemUnitId == Id).ToList();
                if (purchaseOrder.Count > 0)
                {
                    ErrorMessage = "cannot delete Item Unit, Item Unit Used in offer Details or more";
                }
            }
            return ErrorMessage;
        }


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
        public string ValidateDeletItemCategory(int Id)
        {
            string ErrorMessage = "";
            var result = context.MainItems.Where(x => x.ItemCategoryId == Id ).ToList();
            if (result.Count > 0)
            {
                ErrorMessage = "cannot delete Item Category, there is Main Items for this Item Category";
            }

            return ErrorMessage;
        }


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
        public ListItemCategoriesViewModel ListItemCategories()
        {
            ListItemCategoriesViewModel model = new ListItemCategoriesViewModel();
            model.ItemCategories = GetItemCategoryList().ToList();
            return model;
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
        public string ValidateDeletMainItem(int Id)
        {
            string ErrorMessage = "";
            var result = context.Items.Where(x => x.MainItemId == Id ).ToList();
            if (result.Count > 0)
            {
                ErrorMessage = "cannot delete Main Item, there is Items for this Main Item";
            }
            return ErrorMessage;
        }


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
        public ListMainItemViewModel ListMainItems()
        {
            ListMainItemViewModel model = new ListMainItemViewModel();
            model.MainItems = GetMainItemList().ToList();
            return model;
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
        public string ValidateDeletItem(int Id)
        {
            string ErrorMessage = "";
            var result = context.offerDetails.Where(x => x.ItemId == Id ).ToList();
            if (result.Count > 0)
            {
                ErrorMessage = "cannot delete Item, there is Offers Used this Item";
            }
            return ErrorMessage;
        }


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
        public ListItemsViewModel ListItems()
        {
            ListItemsViewModel model = new ListItemsViewModel();
            model.Items = GetItemList().ToList();
            return model;
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
                             Id = item.Id,
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
                             Id = item.Id,
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

        //--------------End Item
        //-----------------------  Period Type 
        public string ValidateDeletPeriodType(int Id)
        {
            string ErrorMessage = "";
            var result = context.PeriodTypes.Where(x => x.PeriodId == Id).ToList();
            if (result.Count > 0)
            {
                ErrorMessage = "cannot delete Period Type, there is Tasks Used this Period Type";
            }
            return ErrorMessage;
        }


        public PeriodTypeModel AddPeriodType(PeriodTypeModel periodType)
        {
            context.PeriodTypes.Add(periodType);
            context.SaveChanges();
            return periodType;
        }

        public PeriodTypeModel DeletePeriodType(int id)
        {
            PeriodTypeModel periodType = context.PeriodTypes.Find(id);
            if (periodType != null)
            {
                context.PeriodTypes.Remove(periodType);
                context.SaveChanges();
            }
            return periodType;
        }
        public List<PeriodTypeModel> PeriodTypeList()
        {
            List<PeriodTypeModel> model = new List<PeriodTypeModel>();
            model = context.PeriodTypes.ToList();
            return model;
        }

        public PeriodTypeModel GetPeriodType(int Id)
        {
            return context.PeriodTypes.Find(Id);
        }
        public PeriodTypeModel UpdatePeriodType(PeriodTypeModel periodTypeChanges)
        {
            var periodType = context.PeriodTypes.Attach(periodTypeChanges);
            periodType.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return periodTypeChanges;
        }

        //--------------End Period Type
        //----------------------- PurchaseOrderPermission


        public PurchaseOrderPermission AddPurchaseOrderPermission(PurchaseOrderPermission purchaseOrderPermission)
        {
            try
            {
                context.PurchaseOrderPermissions.Add(purchaseOrderPermission);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var x = 0;
            }
            return purchaseOrderPermission;
        }

        public PurchaseOrderPermission DeletePurchaseOrderPermission(int id)
        {
            PurchaseOrderPermission purchaseOrderPermission = context.PurchaseOrderPermissions.Find(id);
            if (purchaseOrderPermission != null)
            {
                context.PurchaseOrderPermissions.Remove(purchaseOrderPermission);
                context.SaveChanges();
            }
            return purchaseOrderPermission;
        }
        public ListPurchaseOrderPermissionViewModel ListPurchaseOrderPermissions()
        {
            
            ListPurchaseOrderPermissionViewModel model = new ListPurchaseOrderPermissionViewModel();
            var result = (from permission in context.PurchaseOrderPermissions
                          join user in context.Users 
                          on permission.UserId equals user.Id
                          select new PurchaseOrderPermissionListViewModel
                          {
                              AllowConfirm = permission.AllowConfirm,
                              AllowDelivery = permission.AllowDelivery,
                              AllowPrint = permission.AllowPrint,
                              AllowVerify =permission.AllowVerify,
                              Id =permission.Id,
                              UserId = permission.UserId,
                              UserName=user.UserName
                          }).ToList();
            model.purchaseOrderPermissions =result.ToList();
            return model;
        }
        public IEnumerable<PurchaseOrderPermission> GetPurchaseOrderPermissionList()
        {
            return context.PurchaseOrderPermissions;
        }

        public PurchaseOrderPermission GetPurchaseOrderPermission(int Id)
        {
            PurchaseOrderPermission purchaseOrderPermission = null;
            try
            {
                purchaseOrderPermission= context.PurchaseOrderPermissions.Find(Id);
            }catch (Exception ex)
            {
                var xx = 0;
            }
            return purchaseOrderPermission;
        }
        public PurchaseOrderPermission GetPurchaseOrderPermissionByUser(string userId)
        {
            return context.PurchaseOrderPermissions.Where(x => x.UserId == userId).FirstOrDefault();
        }  

        public PurchaseOrderPermission UpdatePurchaseOrderPermission(PurchaseOrderPermission purchaseOrderPermissionChanges)
        {
            var purchaseOrderPermission = context.PurchaseOrderPermissions.Attach(purchaseOrderPermissionChanges);
            purchaseOrderPermission.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return purchaseOrderPermissionChanges;
        }
        //--------------End Currency

        //--------------Roles
        public Boolean verifyRolePages(string RoleId)
        {
            var pages = context.Pages.ToList();
            
            foreach (Page page in pages)
            {
                var result = (from rolePage in context.RolePages
                              where rolePage.RoleId == RoleId 
                              && rolePage.PageId==page.Id
                                 select new Models.RolePage
                              {
                                  CanAdd = rolePage.CanAdd,
                                  CanDelete = rolePage.CanDelete,
                                  CanEdit = rolePage.CanEdit,
                                  CanView = rolePage.CanView,
                                  PageId = rolePage.PageId,
                                  RoleId = rolePage.RoleId
                              }).ToList();
                if (result.Count == 0)
                {
                    AddRolePage(new RolePage()
                    {
                        CanAdd = true,
                        CanDelete=true,
                        CanEdit=true,
                        CanView=true,
                        PageId=page.Id,
                        RoleId=RoleId
                    }) ;
                };
            };
            return true;
        }

        public List<RolePagesViewModels> getRolePages(string RoleId)
        {
            var result = (from rolePage in context.RolePages
                          join page in context.Pages
                          on rolePage.PageId equals page.Id
                          where rolePage.RoleId == RoleId
                          select new RolePagesViewModels {
                              RolePageId=rolePage.Id,
                              CanAdd=rolePage.CanAdd,
                              CanDelete=rolePage.CanDelete,
                              CanEdit=rolePage.CanEdit,
                              CanView=rolePage.CanView,
                              PageId=rolePage.PageId,
                              PageName=page.PageName,
                              ParentName=page.ParentName,
                              RoleId=rolePage.RoleId
                          }).ToList();
            return result;
        }

        public RolePagesViewModels getRolePageViewModel(int PageId, string RoleId)
        {
            var result = (from rolePage in context.RolePages
                          join page in context.Pages
                          on rolePage.PageId equals page.Id
                          where rolePage.PageId == PageId
                          && rolePage.RoleId == RoleId
                          select new RolePagesViewModels
                          {
                              RolePageId=rolePage.Id,
                              CanAdd = rolePage.CanAdd,
                              CanDelete = rolePage.CanDelete,
                              CanEdit = rolePage.CanEdit,
                              CanView = rolePage.CanView,
                              PageId = rolePage.PageId,
                              PageName = page.PageName,
                              ParentName = page.ParentName,
                              RoleId = rolePage.RoleId
                          }).ToList();
            return result[0];
        }
        public List<Page> getPages()
        {
            return context.Pages.ToList();
        }

        public RolePage AddRolePage(RolePage rolePage)
        {
            context.RolePages.Add(rolePage);
            context.SaveChanges();
            return rolePage;
        }
        public RolePage GetRolePage(int Id)
        {
            return context.RolePages.Find(Id);
        }
        public RolePage UpdateRolePage(RolePage rolePageChanges)
        {
            var rolePage = context.RolePages.Attach(rolePageChanges);
            rolePage.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return rolePageChanges;
        }
        //public List<UserAllowedParentMenuViewModel> GetUserParentMenuPermission(string UserId)
        public UserPermissionsViewModel GetUserParentMenuPermission(string UserId)
        {
            UserPermissionsViewModel model = new UserPermissionsViewModel();
            var result = context.SQLUserAllowedParentMenuesViewModel.FromSql("SELECT * FROM dbo.UserAllowedParentMenu Where UserId = '" + UserId +"'").ToList();
            var Menues= result.Select(x => x.ParentName).Distinct().ToList();
            model.ParentMenus = Menues;
            model.UserPermissions = result;
            return model;
        }
        public UserPermissionsViewModel GetUserParentMenuPermission(string UserId, string PageName)
        {
            UserPermissionsViewModel model = new UserPermissionsViewModel();
            var result = context.SQLUserAllowedParentMenuesViewModel.FromSql("SELECT * FROM dbo.UserAllowedParentMenu Where UserId = '" + UserId + "' And PageName ='" + PageName + "'").ToList();
            var Menues = result.Select(x => x.ParentName).Distinct().ToList();
            model.ParentMenus = Menues;
            model.UserPermissions = result;
            return model;
        }
    }
}
