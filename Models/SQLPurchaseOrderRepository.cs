using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSIS.ViewModels;
using Microsoft.EntityFrameworkCore;
namespace MSIS.Models
{
    public class SQLPurchaseOrderRepository
    {
        public AppDBContext context { get; }
        public SQLPurchaseOrderRepository(AppDBContext Context)
        {
            this.context = Context;
        }
        public PurchaseOrder Add(PurchaseOrder purchaseOrder)
        {
            context.PurchaseOrders.Add(purchaseOrder);
            context.SaveChanges();
            return purchaseOrder;
        }


        public List<MSIS.ViewModels.ListPurchaseOrderDetailsViewModel> getPurchaseOrderList()
        {
            var result =context.SQLListPurchaseOrderDetailsViewModel.FromSql("SELECT * FROM dbo.vPurchaseOrders ").ToList();

            return result;// projectDetailViewModel;

        }
        public int retPurchaseOrderNo()
        {
            int OrderNo = context.PurchaseOrders.Where(c => c.PurchaseOrderYear == DateTime.Today.Year).Select(x => x.PurchaseOrderNo).DefaultIfEmpty(0).Max();
            OrderNo = OrderNo + 1;
            return OrderNo;
        }
        public MSIS.ViewModels.PurchaseOrderDetailsViewModel getPurchaseOrderDetails(int Id)
        {
            var result = context.SQLPurchaseOrderDetailsViewModel.FromSql("SELECT * FROM dbo.vPurchaseOrders Where Id = " + Id.ToString()).ToList();
            var Details = context.SQLPurchaseOrderItemsViewModel.FromSql("SELECT * FROM dbo.vPurchaseOrdersDetails Where PuchaseOrderId = " + Id.ToString()).ToList();
            result[0].OrderItems = Details;
            return result[0];// projectDetailViewModel;
        }
        public MSIS.ViewModels.EditPurchaseOrderViewModel getEditPurchaseOrderDetails(int Id)
        {
            var result = context.SQLPurchaseOrderDetailsViewModel.FromSql("SELECT * FROM dbo.vPurchaseOrders Where Id = " + Id.ToString()).ToList();
            var Details = context.SQLPurchaseOrderItemsViewModel.FromSql("SELECT * FROM dbo.vPurchaseOrdersDetails Where PuchaseOrderId = " + Id.ToString()).ToList();
            result[0].OrderItems = Details;
            EditPurchaseOrderViewModel model=new EditPurchaseOrderViewModel();
            model.PurchaseOrderDetails = result[0];
            model.suppliers = context.Suppliers.ToList();
            model.CurrencyList = context.Currency.ToList();
            model.Units = context.ItemUnits.ToList();
            return model;// projectDetailViewModel;
        }
        public MSIS.ViewModels.CreatePurchaseOrderViewModel getCreatePurchaseOrderDetails()
        {
            PurchaseOrderDetailsViewModel result = new PurchaseOrderDetailsViewModel();

            CreatePurchaseOrderViewModel model = new CreatePurchaseOrderViewModel();
            model.PurchaseOrderDetails = result;
            model.suppliers = context.Suppliers.ToList();
            model.CurrencyList = context.Currency.ToList();
            return model;// projectDetailViewModel;
        }
        public List<MSIS.ViewModels.PurchaseOrderItemsViewModel> getPurchaseOrderItems(int Id)
        {
            var Details = context.SQLPurchaseOrderItemsViewModel.FromSql("SELECT * FROM dbo.vPurchaseOrdersDetails Where PuchaseOrderId = " + Id.ToString()).ToList();
            return Details;
        }
        public MSIS.ViewModels.PurchaseOrderItemsViewModel getPurchaseOrderItemDetails(int Id)
        {
            var Details = context.SQLPurchaseOrderItemsViewModel.FromSql("SELECT * FROM dbo.vPurchaseOrdersDetails Where Id = " + Id.ToString()).ToList();
            return Details[0];
        }
        public PurchaseOrder GetPurchaseOrder(int Id)
        {
            return context.PurchaseOrders.Find(Id);
        }
        public PurchaseOrderDetails GetPurchaseOrderItem(int Id)
        {
            return context.PurchaseOrdersDetails.Find(Id);
        }
        public PurchaseOrderDetails UpdatePurchaseOrderItem(PurchaseOrderDetails purchaseOrderChanges)
        {
            var Customer = context.PurchaseOrdersDetails.Attach(purchaseOrderChanges);
            Customer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return purchaseOrderChanges;
        }
        public PurchaseOrderDetails AddPurchaseOrderItem(PurchaseOrderDetails purchaseOrderItem)
        {
            context.PurchaseOrdersDetails.Add(purchaseOrderItem);
            context.SaveChanges();
            return purchaseOrderItem;
        }
        public PurchaseOrderDetails DeletePurchaseOrderItem(int id)
        {
            PurchaseOrderDetails Details = context.PurchaseOrdersDetails.Find(id);
            //PurchaseOrderDetails purchaseOrderDetails = context.PurchaseOrdersDetails.Find(id);
            if (Details != null)
            {
                context.PurchaseOrdersDetails.Remove(Details);
                context.SaveChanges();
            }
            return Details;
        }
        public PurchaseOrder Update(PurchaseOrder purchaseOrderChanges)
        {
            var Customer = context.PurchaseOrders.Attach(purchaseOrderChanges);
            Customer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return purchaseOrderChanges;
        }
    }
}
