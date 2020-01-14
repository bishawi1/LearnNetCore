using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MSIS.Models;
using MSIS.ViewModels;
namespace MSIS.Controllers
{
    public class OffersController : Controller
    {
        public SQLOffersRepository OffersRepository { get; }
        public IHostingEnvironment HostingEnvironment { get; }
        public OffersController(SQLOffersRepository offersRepository, IHostingEnvironment hostingEnvironment)
        {
            this.OffersRepository = offersRepository;
            this.HostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult GetMainItems(int ItemCategoryId)
        {
            List<MainItem> model= OffersRepository.getMainItems(ItemCategoryId);
            return new JsonResult(model);
        }
        [HttpGet]
        public IActionResult GetItemCategories()
        {
            List<ItemCategory> model= OffersRepository.getItemCategoriess();
            return new JsonResult(model);
        }
        [HttpGet]
        public IActionResult GetItems(int MainItemId)
        {
            List<Item> model= OffersRepository.getItems(MainItemId);
            return new JsonResult(model);
        }
        [HttpGet]
        public IActionResult getItemDetails(int ItemId)
        {
            List<Item> model= OffersRepository.getItemDetails(ItemId);
            return new JsonResult(model[0]);
        }
        [HttpGet]
        public IActionResult ListOffers()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MSIS.ViewModels.UserPermissionsViewModel permission = OffersRepository.GetUserParentMenuPermission(userId, "All Offers");

            OfferListViewModels model = OffersRepository.getOfferList();
            
            model.userPermission = permission.UserPermissions[0];

            return View(model);
        }
        [HttpGet]
        public IActionResult printOfferForm(int Id)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MSIS.ViewModels.UserPermissionsViewModel permission = OffersRepository.GetUserParentMenuPermission(userId, "All Offers");
            var model = OffersRepository.getOffersDetails(Id);
            if (permission.UserPermissions.Count > 0)
            {
                model.Permission = permission.UserPermissions[0];
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult PrintOffersList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MSIS.ViewModels.UserPermissionsViewModel permission = OffersRepository.GetUserParentMenuPermission(userId, "All Offers");

            OfferListViewModels model = OffersRepository.getOfferList();

            model.userPermission = permission.UserPermissions[0];

            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            CreateOfferViewModel model = OffersRepository.NewOffer();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateOfferViewModel offer)
        {
            if (ModelState.IsValid)
            {
                Offer model = new Offer();
                model.CurrencyId = offer.CurrencyId;
                model.CustomerId = offer.CustomerId;
                model.OfferDate = offer.OfferDate;
                model.OtherInformation = offer.OtherInformation;
                var newModel = OffersRepository.Add(model);
                return RedirectToAction("Edit", "Offers", new { Id = newModel.Id });
            }
            return View();
        }
        [HttpGet]
        public IActionResult OfferSearchAsync(string strGroupBy)
        {
            AppDBContext context = OffersRepository.getContext();
            MSIS.ViewModels.OfferSearchViewModel model = new ViewModels.OfferSearchViewModel();
            SQLSettingsRepository settingRepository = new SQLSettingsRepository(context);
            SQLSupplierRepository supplierRepository = new SQLSupplierRepository(context);
            model.CurrencyList = settingRepository.GetCurrencyList().ToList();
            model.CurrencyList.Insert(0, new Currency
            {
                Id = -1,
                CurrencyName = "Select ..."
            });
            model.Customers = context.Customers.ToList();
            model.Customers.Insert(0, new Customer
            {
                Id = -1,
                CustomerName = "Select ..."
            });
            model.FromDate = new DateTime(2019, 1, 1);
            model.ToDate = DateTime.Today;
            if (strGroupBy == null)
            {
                model.strGroupBy = "";
            }
            else
            {
                model.strGroupBy = strGroupBy;
            }

            return PartialView("_OfferSearch", model);

        }

        [HttpPost]
        public async Task<IActionResult> OfferSearchAsync(int CurrencyId, int CustomerId, DateTime FromDate, DateTime ToDate, string strGroupBy)
        {
            List<SQLOffersViewModel> model = new List<SQLOffersViewModel>();
            try
            {
                MSIS.ViewModels.OfferSearchViewModel criteria = new OfferSearchViewModel();
                criteria.CustomerId = CustomerId;
                criteria.CurrencyId = CurrencyId;
                criteria.FromDate = FromDate;
                criteria.ToDate = ToDate;
                criteria.strGroupBy = strGroupBy;

                model = OffersRepository.getAllOfferDetails(criteria).ToList();
                //return new JsonResult( model);
                return PartialView("_PrintOfferList", model);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("OfferSearchAsync", ex.Message.ToString());

            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MSIS.ViewModels.UserPermissionsViewModel permission = OffersRepository.GetUserParentMenuPermission(userId, "All Offers");

            OfferDetailsViewModel model = OffersRepository.getOffersDetails(Id);
            if (permission.UserPermissions.Count > 0)
            {
                model.Permission = permission.UserPermissions[0];
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            EditOfferViewModel offer = OffersRepository.getEditOffers(Id);
            if (offer == null)
            {
                return Redirect("NotFound");
            }
            else
            {
                return View(offer);
            }
        }
        [HttpPost]
        public IActionResult EditOffer(int Id, DateTime OfferDate, int CustomerId, int CurrencyId, string Notes)
        {
            if (ModelState.IsValid)
            {
                Offer model = OffersRepository.GetOffer(Id);
                if (model == null)
                {

                }
                else
                {
                    model.CurrencyId = CurrencyId;
                    model.OtherInformation = Notes;
                    model.OfferDate = OfferDate;
                    model.CustomerId = CustomerId;
                    OffersRepository.Update(model);
                }
                return View();
            }
            else
            {
                return View();
            }
        }
       
        [HttpPost]
        public IActionResult AddOfferItem(int Id, string Description, int ItemId, int ItemUnitId, int OfferId, float QNT, float UnitPrice)
        {
            OfferDetail offerDetail = new OfferDetail();
            offerDetail.Description = Description;
            offerDetail.ItemId = ItemId;
            offerDetail.ItemUnitId = ItemUnitId;
            offerDetail.OfferId = OfferId;
            offerDetail.QNT = QNT;
            offerDetail.UnitPrice = UnitPrice;
            OffersRepository.AddOfferItem(offerDetail);


            List<OfferItemsViewModel> model = OffersRepository.getOfferItemsList(OfferId);
            return new JsonResult(model);
            //return PartialView("_PurchaseOrderItems", model);
        }

        [HttpGet]
        public IActionResult getOfferItemDetails(int Id)
        {
            OfferItemsViewModel model = OffersRepository.getOfferItemsDetails(Id);
            return new JsonResult(model);
        }

        [HttpPost]
        public IActionResult EditOfferItemDetails(int Id, string Description, int ItemId, int ItemUnitId, int OfferId, float QNT, float UnitPrice)
        {
            if (ModelState.IsValid)
            {
                OfferDetail offerDetails = OffersRepository.GetOfferItem(Id);
                if (offerDetails == null)
                {
                    return Redirect("NotFound");
                }
                else
                {
                    offerDetails.Description = Description;
                    offerDetails.ItemId = ItemId;
                    offerDetails.ItemUnitId = ItemUnitId;
                    offerDetails.OfferId = OfferId;
                    offerDetails.QNT = QNT;
                    offerDetails.UnitPrice = UnitPrice;
                    OffersRepository.UpdateOfferItem(offerDetails);


                    List<OfferItemsViewModel> model = OffersRepository.getOfferItemsList(OfferId);
                    return new JsonResult(model);

                    //return PartialView("_PurchaseOrderItems", model);

                }
            }
            return new JsonResult(false);
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            OfferListViewModels model = new OfferListViewModels();
            Offer offer = OffersRepository.GetOffer(Id);
            if (offer == null)
            {
                return View("NotFound");
            }
            else
            {
                OffersRepository.DeleteOffer(Id);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                MSIS.ViewModels.UserPermissionsViewModel permission = OffersRepository.GetUserParentMenuPermission(userId, "All Offers");

                model = OffersRepository.getOfferList();
                        model.userPermission = permission.UserPermissions[0];
            }

            return new JsonResult(model);// RedirectToAction("ListPurchaseOrders","PurchaseOrders" );
        }


        [HttpPost]
        public IActionResult DeleteOfferItem(int Id)
        {
            //PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
            OfferDetail offerDetail = OffersRepository.DeleteOfferItem(Id);
            if (offerDetail == null)
            {
                return Redirect("NotFound");
            }
            else
            {
                //return new JsonResult("{Deleted:true,ErrorText:''}");
                List<OfferItemsViewModel> model = OffersRepository.getOfferItemsList(offerDetail.OfferId);
                return new JsonResult(model);
                //return PartialView("_PurchaseOrderItems", model);

            }
        }

    }
}