using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public Offer Add(Offer offer)
        {
            context.Offers.Add(offer);
            context.SaveChanges();
            return offer;
        }


        public List<OfferViewModel> getOfferList()
        {
            var result = (from offer in context.Offers
                          join currency in context.Currency
                          on offer.CurrencyId equals currency.Id
                          join customer in context.Customers
                          on offer.CustomerId equals customer.Id
                          select new OfferViewModel
                          {
                              Address = customer.Address,
                              CurrencyId=offer.CurrencyId,
                              CurrencyName=currency.CurrencyName,
                              CustomerId=offer.CustomerId,
                              CustomerName=customer.CustomerName,
                              Id=offer.Id,
                              OfferDate=offer.OfferDate,
                              OtherInformation=offer.OtherInformation
                          }).ToList();

            return result;// projectDetailViewModel;

        }
        public CreateOfferViewModel NewOffer()
        {
            CreateOfferViewModel model = new CreateOfferViewModel();
            model.Customers = context.Customers.ToList();
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

        //public PurchaseOrderDetails GetOfferItem(int Id)
        //{
        //    return context.PurchaseOrdersDetails.Find(Id);
        //}
        //public PurchaseOrderDetails UpdatePurchaseOrderItem(PurchaseOrderDetails purchaseOrderChanges)
        //{
        //    var Customer = context.PurchaseOrdersDetails.Attach(purchaseOrderChanges);
        //    Customer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //    context.SaveChanges();
        //    return purchaseOrderChanges;
        //}
        //public PurchaseOrderDetails AddPurchaseOrderItem(PurchaseOrderDetails purchaseOrderItem)
        //{
        //    context.PurchaseOrdersDetails.Add(purchaseOrderItem);
        //    context.SaveChanges();
        //    return purchaseOrderItem;
        //}
        //public PurchaseOrderDetails DeletePurchaseOrderItem(int id)
        //{
        //    PurchaseOrderDetails Details = context.PurchaseOrdersDetails.Find(id);
        //    //PurchaseOrderDetails purchaseOrderDetails = context.PurchaseOrdersDetails.Find(id);
        //    if (Details != null)
        //    {
        //        context.PurchaseOrdersDetails.Remove(Details);
        //        context.SaveChanges();
        //    }
        //    return Details;
        //}


    }
}
