using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MSIS.ViewModels;
namespace MSIS.Models
{
    public class SQLOffersRepository
    {
        public AppDBContext context { get; }
        public SQLOffersRepository(AppDBContext Context)
        {
            this.context = Context;
        }
        public AppDBContext getContext()
        {
            return this.context;
        }
        public List<MSIS.ViewModels.SQLOffersViewModel> getAllOfferDetails(ViewModels.OfferSearchViewModel Criteria)
        {
            try
            {
                string strWhere = "";
                if (Criteria.CustomerId > 0)
                {
                    strWhere = strWhere + " CustomerId = " + Criteria.CustomerId.ToString();
                }
                if (Criteria.CurrencyId > 0)
                {
                    if (strWhere != "")
                    {
                        strWhere = strWhere + " And ";
                    }
                    strWhere = strWhere + " CurrencyId = " + Criteria.CurrencyId.ToString();
                }

                if (Criteria.FromDate.Year > 1)
                {
                    if (strWhere != "")
                    {
                        strWhere = strWhere + " And ";
                    }
                    strWhere = strWhere + " OfferDate >= '" + Criteria.FromDate.ToString() + "'";
                }
                if (Criteria.ToDate.Year > 1)
                {
                    if (strWhere != "")
                    {
                        strWhere = strWhere + " And ";
                    }
                    strWhere = strWhere + " OfferDate <= '" + Criteria.ToDate.ToString() + "'";
                }
                if (strWhere != "")
                {
                    strWhere = " Where " + strWhere;
                }
                if (Criteria.strGroupBy != null)
                {
                    if (Criteria.strGroupBy.ToLower() != "all")
                    {
                        strWhere = strWhere + " Order By " + Criteria.strGroupBy;
                    }
                }
                var result = context.SQLOfferList.FromSql("SELECT *,'" + Criteria.strGroupBy + "' As strGroupBy FROM dbo.vOffers " + strWhere).ToList();

                return result.ToList();// projectDetailViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }

        public Offer Add(Offer offer)
        {
            context.Offers.Add(offer);
            context.SaveChanges();
            return offer;
        }
        public Boolean DeleteOffer(int Id)
        {
            context.Database.BeginTransaction();
            try
            {
                Offer offer = context.Offers.Find(Id);
                if (offer != null)
                {
                    context.offerDetails.RemoveRange(context.offerDetails.Where(x => x.OfferId == Id).ToList());
                    context.SaveChanges();
                    context.Offers.Remove(offer);
                    context.SaveChanges();
                }
                context.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                throw (ex);
            };
            return true;

            //PurchaseOrder purchaseOrder = context.PurchaseOrders.Find(Id);
            ////PurchaseOrderDetails purchaseOrderDetails = context.PurchaseOrdersDetails.Find(id);
            //if (purchaseOrder != null)
            //{
            //    context.PurchaseOrders.Remove(purchaseOrder);
            //    context.SaveChanges();
            //}
            //return purchaseOrder;
        }

        public UserPermissionsViewModel GetUserParentMenuPermission(string UserId, string PageName)
        {
            UserPermissionsViewModel model = new UserPermissionsViewModel();
            var result = context.SQLUserAllowedParentMenuesViewModel.FromSql("SELECT * FROM dbo.UserAllowedParentMenu Where ParentName = 'Offers' And UserId = '" + UserId + "' And PageName ='" + PageName + "'").ToList();
            var Menues = result.Select(x => x.ParentName).Distinct().ToList();
            model.ParentMenus = Menues;
            model.UserPermissions = result;
            return model;
        }
        public List<MainItem> getMainItems(int ItemCategoryId)
        {
            return context.MainItems.Where(x => x.ItemCategoryId == ItemCategoryId).ToList();
        }
        public List<ItemCategory> getItemCategoriess()
        {
            return context.ItemCategories.ToList();
        }
        public List<Item> getItems(int MainItemId)
        {
            return context.Items.Where(x=>x.MainItemId==MainItemId).ToList();
        }
        public List<Item> getItemDetails(int ItemId)
        {
            List<Item> items=null;
            items= context.Items.Where(x=>x.Id==ItemId ).ToList();
            return items;
        }
        public OfferListViewModels getOfferList()
        {
            var result = context.SQLOfferList.FromSql("SELECT *,'ALL' As strGroupBy FROM dbo.vOffers " ).ToList();
            //return result.ToList();// projectDetailViewModel;

            //var result = (from offer in context.Offers
            //              join currency in context.Currency
            //              on offer.CurrencyId equals currency.Id
            //              join customer in context.Customers
            //              on offer.CustomerId equals customer.Id
            //              select new OfferViewModel
            //              {
            //                  Address = customer.Address,
            //                  CurrencyId=offer.CurrencyId,
            //                  CurrencyName=currency.CurrencyName,
            //                  CustomerId=offer.CustomerId,
            //                  CustomerName=customer.CustomerName,
            //                  Id=offer.Id,
            //                  OfferDate=offer.OfferDate,
            //                  OtherInformation=offer.OtherInformation
            //              }).ToList();
            OfferListViewModels model = new OfferListViewModels();
            model.OfferList = result;
            var Totals = context.OffersTotals.FromSql("Select * From dbo.vOffersTotals").ToList();
            model.OffersTotals = Totals;
            return model;

        }
        public CreateOfferViewModel NewOffer()
        {
            CreateOfferViewModel model = new CreateOfferViewModel();
            model.OfferDate = DateTime.Today;
            model.Customers = context.Customers.ToList();
            model.Customers.Add(new Customer() { 
            Id=-1,
            CustomerName="Select ..."
            });
            model.CurrencyList = context.Currency.ToList();
            return model;
        }
        public int retPurchaseOrderNo()
        {
            int OrderNo = context.PurchaseOrders.Where(c => c.PurchaseOrderYear == DateTime.Today.Year).Select(x => x.PurchaseOrderNo).DefaultIfEmpty(0).Max();
            OrderNo = OrderNo + 1;
            return OrderNo;
        }
        public OfferDetailsViewModel getOffersDetails(int Id)
        {
            Double totalAmount = 0;
            var result = (from offer in context.Offers
                          join currency in context.Currency
                          on offer.CurrencyId equals currency.Id
                          join customer in context.Customers
                          on offer.CustomerId equals customer.Id
                          where offer.Id==Id
                          select new OfferDetailsViewModel
                          {
                              Address = customer.Address,
                              CurrencyId = offer.CurrencyId,
                              CurrencyName = currency.CurrencyName,
                              CustomerId = offer.CustomerId,
                              CustomerName = customer.CustomerName,
                              Id = offer.Id,                             
                              OfferDate = offer.OfferDate,
                              OtherInformation = offer.OtherInformation
                          }).ToList();
            result[0].OfferItems = getOfferItemsList(Id).ToList();
            if (result[0] != null)
            {
                foreach (var item in result[0].OfferItems)
                {
                    totalAmount = item.TotalPrice;
                }
                result[0].TotalAmount = totalAmount;
            }
            return result[0];// projectDetailViewModel;
        }
        public EditOfferViewModel getEditOffers(int Id)
        {
            var result = (from offer in context.Offers
                          join currency in context.Currency
                          on offer.CurrencyId equals currency.Id
                          join customer in context.Customers
                          on offer.CustomerId equals customer.Id
                          where offer.Id == Id
                          select new EditOfferViewModel
                          {
                              Address = customer.Address,
                              CurrencyId = offer.CurrencyId,
                              CurrencyName = currency.CurrencyName,
                              CustomerId = offer.CustomerId,
                              CustomerName = customer.CustomerName,
                              Id = offer.Id,
                              OfferDate = offer.OfferDate,                              
                              OtherInformation = offer.OtherInformation
                          }).ToList();
            result[0].OfferItems = getOfferItemsList(Id).ToList();
            result[0].ItemUnits = context.ItemUnits.ToList();
            result[0].CurrencyList = context.Currency.ToList();
            result[0].Customers = context.Customers.ToList();
            result[0].Items = context.Items.ToList();
            return result[0];// projectDetailViewModel;
        }
        public OfferItemsViewModel getOfferItemsDetails(int Id)
        {
            var model = (from offerDetail in context.offerDetails
                         join itemUnits in context.ItemUnits
                         on offerDetail.ItemUnitId equals itemUnits.Id
                         join item in context.Items
                         on offerDetail.ItemId equals item.Id
                         where offerDetail.Id == Id
                         select new OfferItemsViewModel { 
                         Description=offerDetail.Description,
                         Id= offerDetail.Id,
                         ItemId= offerDetail.ItemId,
                         ItemName=item.ItemName,
                         ItemUnitId= offerDetail.ItemUnitId,
                         ItemUnitName=itemUnits.ItemUnitName,
                         OfferId= offerDetail.OfferId,
                         QNT= offerDetail.QNT,
                         UnitPrice = offerDetail.UnitPrice,
                         TotalPrice = offerDetail.QNT * offerDetail.UnitPrice
                         }).ToList();
            return model[0];
        }

        public List<OfferItemsViewModel> getOfferItemsList(int offerId)
        {
            var model = (from offerDetail in context.offerDetails
                         join itemUnits in context.ItemUnits
                         on offerDetail.ItemUnitId equals itemUnits.Id
                         join item in context.Items
                         on offerDetail.ItemId equals item.Id
                         join mainItem in context.MainItems
                         on item.MainItemId equals mainItem.Id
                         join itemCategory in context.ItemCategories
                         on mainItem.ItemCategoryId equals itemCategory.Id
                         where offerDetail.OfferId == offerId
                         select new OfferItemsViewModel
                         {
                             Description = offerDetail.Description,
                             Id = offerDetail.Id,
                             ItemId = offerDetail.ItemId,
                             ItemName = item.ItemName,
                             ItemUnitId = offerDetail.ItemUnitId,
                             ItemUnitName = itemUnits.ItemUnitName,
                             OfferId = offerDetail.OfferId,
                             QNT = offerDetail.QNT,
                             CategoryName=itemCategory.CategoryName,
                             MainItemName=mainItem.MainItemName,
                             UnitPrice = offerDetail.UnitPrice,
                             TotalPrice =offerDetail.QNT * offerDetail.UnitPrice
                         }).ToList();
            return model;
        }
        public Offer GetOffer(int Id)
        {
            return context.Offers.Find(Id);
        }
        public Offer Update(Offer offerChanges)
        {
            var offer = context.Offers.Attach(offerChanges);
            offer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return offerChanges;
        }
        public OfferDetail AddOfferItem(OfferDetail offerItem)
        {
            context.offerDetails.Add(offerItem);
            context.SaveChanges();
            return offerItem;
        }
        public OfferDetail GetOfferItem(int Id)
        {
            return context.offerDetails.Find(Id);
        }
        public OfferDetail UpdateOfferItem(OfferDetail offerItemChanges)
        {
            var Customer = context.offerDetails.Attach(offerItemChanges);
            Customer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return offerItemChanges;
        }

        public OfferDetail DeleteOfferItem(int id)
        {
            OfferDetail Details = context.offerDetails.Find(id);
            //PurchaseOrderDetails purchaseOrderDetails = context.PurchaseOrdersDetails.Find(id);
            if (Details != null)
            {
                context.offerDetails.Remove(Details);
                context.SaveChanges();
            }
            return Details;
        }

        //public MSIS.ViewModels.EditPurchaseOrderViewModel getEditPurchaseOrderDetails(int Id)
        //{
        //    var result = context.SQLPurchaseOrderDetailsViewModel.FromSql("SELECT * FROM dbo.vPurchaseOrders Where Id = " + Id.ToString()).ToList();
        //    var Details = context.SQLPurchaseOrderItemsViewModel.FromSql("SELECT * FROM dbo.vPurchaseOrdersDetails Where PuchaseOrderId = " + Id.ToString()).ToList();
        //    result[0].OrderItems = Details;
        //    EditPurchaseOrderViewModel model = new EditPurchaseOrderViewModel();
        //    model.PurchaseOrderDetails = result[0];
        //    model.suppliers = context.Suppliers.ToList();
        //    model.CurrencyList = context.Currency.ToList();
        //    model.Units = context.ItemUnits.ToList();
        //    return model;// projectDetailViewModel;
        //}
        //public MSIS.ViewModels.CreatePurchaseOrderViewModel getCreatePurchaseOrderDetails()
        //{
        //    PurchaseOrderDetailsViewModel result = new PurchaseOrderDetailsViewModel();

        //    CreatePurchaseOrderViewModel model = new CreatePurchaseOrderViewModel();
        //    model.PurchaseOrderDetails = result;
        //    model.suppliers = context.Suppliers.ToList();
        //    model.CurrencyList = context.Currency.ToList();
        //    return model;// projectDetailViewModel;
        //}
        //public List<MSIS.ViewModels.PurchaseOrderItemsViewModel> getPurchaseOrderItems(int Id)
        //{
        //    var Details = context.SQLPurchaseOrderItemsViewModel.FromSql("SELECT * FROM dbo.vPurchaseOrdersDetails Where PuchaseOrderId = " + Id.ToString()).ToList();
        //    return Details;
        //}
        //public MSIS.ViewModels.PurchaseOrderItemsViewModel getPurchaseOrderItemDetails(int Id)
        //{
        //    var Details = context.SQLPurchaseOrderItemsViewModel.FromSql("SELECT * FROM dbo.vPurchaseOrdersDetails Where Id = " + Id.ToString()).ToList();
        //    return Details[0];
        //}







    }
}
