using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSIS.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
            try
            {
            context.PurchaseOrders.Add(purchaseOrder);
            context.SaveChanges();
            return purchaseOrder;
            }catch (Exception ex)
            {
                return null;
            }

        }

        public UserPermissionsViewModel GetUserParentMenuPermission(string UserId, string PageName)
        {            
            UserPermissionsViewModel model = new UserPermissionsViewModel();
            var result = context.SQLUserAllowedParentMenuesViewModel.FromSql("SELECT * FROM dbo.UserAllowedParentMenu Where ParentName = 'PurchaseOrder' And UserId = '" + UserId + "' And PageName ='" + PageName + "'").ToList();
            var Menues = result.Select(x => x.ParentName).Distinct().ToList();
            model.ParentMenus = Menues;
            model.UserPermissions = result;
            return model;
        }
        public MSIS.ViewModels.ListPurchaseOrdersViewModel getPurchaseOrderList(int StateId,int BranchId)
        {
            var strWhere = "";
            if (StateId!=0)
            {
                if (strWhere != "")
                {
                    strWhere = strWhere + " And ";
                }
                strWhere = " StateId = " + StateId.ToString();
            }
            if (BranchId != 0)
            {
                if (strWhere != "")
                {
                    strWhere = strWhere + " And ";
                }
                strWhere = strWhere + " BranchId = " + BranchId.ToString();
            }
            if (strWhere != "")
            {
                strWhere = " Where " + strWhere;
            }
            var result =context.SQLListPurchaseOrderDetailsViewModel.FromSql("SELECT * FROM dbo.vPurchaseOrders " + strWhere).ToList();
            ListPurchaseOrdersViewModel model = new ListPurchaseOrdersViewModel();
            model.CountByStatus = new PurchaseOrdersCountByStatusViewModel();
            var Totals = context.PurchaseOrdersTotals.FromSql("SELECT CurrencyId, CurrencyCode, CurrencyName, SUM(TotalPrice) AS TotalAmount FROM dbo.vPurchaseOrders " + strWhere + " GROUP BY CurrencyId, CurrencyCode, CurrencyName").ToList();
            
            var CountByStatus = result.Where(x => x.StateId == 1)
                                     .GroupBy(x => new { OrderStatusId = x.StateId })
                                     .Select(x => new { OrderCount = x.Count() }).FirstOrDefault();
            if(CountByStatus != null)
            {
                model.CountByStatus.NewOrdersCount = CountByStatus.OrderCount;
            }
            CountByStatus = result.Where(x => x.StateId == 2)
                                     .GroupBy(x => new { OrderStatusId = x.StateId })
                                     .Select(x => new { OrderCount = x.Count() }).FirstOrDefault();
            if (CountByStatus != null)
            {
                model.CountByStatus.ConfirmedOrdersCount = CountByStatus.OrderCount;
            }
            CountByStatus = result.Where(x => x.StateId == 3)
                                     .GroupBy(x => new { OrderStatusId = x.StateId })
                                     .Select(x => new { OrderCount = x.Count() }).FirstOrDefault();
            if (CountByStatus != null)
            {
                model.CountByStatus.RejectedOrdersCount = CountByStatus.OrderCount;
            }
            CountByStatus = result.Where(x => x.StateId == 4)
                                     .GroupBy(x => new { OrderStatusId = x.StateId })
                                     .Select(x => new { OrderCount = x.Count() }).FirstOrDefault();
            if (CountByStatus != null)
            {
                model.CountByStatus.ApprovedOrdersCount = CountByStatus.OrderCount;
            }
            CountByStatus = result.Where(x => x.StateId == 5)
                                     .GroupBy(x => new { OrderStatusId = x.StateId })
                                     .Select(x => new { OrderCount = x.Count() }).FirstOrDefault();
            if (CountByStatus != null)
            {
                model.CountByStatus.WaitForDeliveryCount = CountByStatus.OrderCount;
            }
            CountByStatus = result.Where(x => x.StateId == 6)
                                     .GroupBy(x => new { OrderStatusId = x.StateId })
                                     .Select(x => new { OrderCount = x.Count() }).FirstOrDefault();
            if (CountByStatus != null)
            {
                model.CountByStatus.DeliveredCount = CountByStatus.OrderCount;
            }
            CountByStatus = result.Where(x => x.StateId == 7)
                                     .GroupBy(x => new { OrderStatusId = x.StateId })
                                     .Select(x => new { OrderCount = x.Count() }).FirstOrDefault();
            if (CountByStatus != null)
            {
                model.CountByStatus.DeliveredPartialyCount = CountByStatus.OrderCount;
            }
            model.ListPurchaseOrders = result;
            model.PurchaseOrderTotals = Totals;
            return model;// projectDetailViewModel;

        }
        public int retPurchaseOrderNo()
        {
            int OrderNo = context.PurchaseOrders.Where(c => c.PurchaseOrderYear == DateTime.Today.Year).Select(x => x.PurchaseOrderNo).DefaultIfEmpty(0).Max();
            OrderNo = OrderNo + 1;
            return OrderNo;
        }
        public PurchaseOrder ConfirmPurchaseOrder(PurchaseOrder purchaseOrderChanges)
        {
                purchaseOrderChanges.StateId = 2;
                var purchase = context.PurchaseOrders.Attach(purchaseOrderChanges);
                purchase.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return purchaseOrderChanges;
        }
        public PurchaseOrder waitPurchaseOrderForDelivery(PurchaseOrder purchaseOrderChanges)
        {
            try
            {
                purchaseOrderChanges.StateId = 5;
                var purchase = context.PurchaseOrders.Attach(purchaseOrderChanges);
                purchase.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return purchaseOrderChanges;
            }
            catch(Exception ex) {
                return null;
            }
        }
        public PurchaseOrder DeleverPurchaseOrder(PurchaseOrder purchaseOrderChanges)
        {
            try
            {
                purchaseOrderChanges.StateId = 6;
                var purchase = context.PurchaseOrders.Attach(purchaseOrderChanges);
                purchase.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return purchaseOrderChanges;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public PurchaseOrder DeleverPartialPurchaseOrder(PurchaseOrder purchaseOrderChanges)
        {
            try
            {
                purchaseOrderChanges.StateId = 7;
                var purchase = context.PurchaseOrders.Attach(purchaseOrderChanges);
                purchase.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return purchaseOrderChanges;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public PurchaseOrder DeleverCancelPurchaseOrder(PurchaseOrder purchaseOrderChanges)
        {
            try
            {
                purchaseOrderChanges.StateId = 8;
                var purchase = context.PurchaseOrders.Attach(purchaseOrderChanges);
                purchase.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return purchaseOrderChanges;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public PurchaseOrder ApprovePurchaseOrder(PurchaseOrder purchaseOrderChanges)
        {
            purchaseOrderChanges.StateId = 4;
            var purchase = context.PurchaseOrders.Attach(purchaseOrderChanges);
            purchase.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return purchaseOrderChanges;
        }
        public PurchaseOrder RejectPurchaseOrder(PurchaseOrder purchaseOrderChanges)
        {
            purchaseOrderChanges.StateId = 3;
            var purchase = context.PurchaseOrders.Attach(purchaseOrderChanges);
            purchase.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return purchaseOrderChanges;
        }
        public MSIS.ViewModels.PurchaseOrderDetailsViewModel getPurchaseOrderDetails(int Id)
        {
            try
            {
            var result = context.SQLPurchaseOrderDetailsViewModel.FromSql("SELECT * FROM dbo.vPurchaseOrders Where Id = " + Id.ToString()).ToList();
            var Details = context.SQLPurchaseOrderItemsViewModel.FromSql("SELECT * FROM dbo.vPurchaseOrdersDetails Where PuchaseOrderId = " + Id.ToString()).ToList();

            result[0].OrderItems = Details;
            return result[0];// projectDetailViewModel;
            }catch(Exception ex)
            {
                return null;
            }

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
            model.Projects = context.Projects.ToList();
            model.Employees = context.Employees.Where(x=>x.Active==true).ToList();
            model.Branches = context.Branches.ToList();

            return model;// projectDetailViewModel;
        }
        public AppDBContext getContext()
        {
            return this.context;
        }
        public List<MSIS.ViewModels.PurchaseOrderDetailsViewModel> getAllPurchaseOrderDetails(ViewModels.PurchaseOrderSearchViewModel Criteria)
        {
            try
            {
                string strWhere = "";
                if (Criteria.BranchId > 0)
                {
                    strWhere = strWhere + " BranchId = " + Criteria.BranchId.ToString();
                }
                if (Criteria.StateId > 0)
                {
                    strWhere = strWhere + " StateId = " + Criteria.StateId.ToString();
                }
                if (Criteria.CurrencyId > 0)
                {
                    if (strWhere != "")
                    {
                        strWhere = strWhere + " And ";
                    }
                    strWhere = strWhere + " CurrencyId = " + Criteria.CurrencyId.ToString();
                }
                if (Criteria.EmployeeId > 0)
                {
                    if (strWhere != "")
                    {
                        strWhere = strWhere + " And ";
                    }
                    strWhere = strWhere + " EmployeeId = " + Criteria.EmployeeId.ToString();
                }
                if (Criteria.OrderNo > 0)
                {
                    if (strWhere != "")
                    {
                        strWhere = strWhere + " And ";
                    }
                    strWhere = strWhere + " OrderNo = " + Criteria.OrderNo.ToString();
                }
                if (Criteria.OrderYear > 0)
                {
                    if (strWhere != "")
                    {
                        strWhere = strWhere + " And ";
                    }
                    strWhere = strWhere + " OrderYear = " + Criteria.OrderYear.ToString();
                }
                if (Criteria.ProjectId > 0)
                {
                    if (strWhere != "")
                    {
                        strWhere = strWhere + " And ";
                    }
                    strWhere = strWhere + " ProjectId = " + Criteria.ProjectId.ToString();
                }
                if (Criteria.SupplierId > 0)
                {
                    if (strWhere != "")
                    {
                        strWhere = strWhere + " And ";
                    }
                    strWhere = strWhere + " SupplierId = " + Criteria.SupplierId.ToString();
                }

                if (Criteria.FromDate.Year > 1)
                {
                    if (strWhere != "")
                    {
                        strWhere = strWhere + " And ";
                    }
                    strWhere = strWhere + " PurchaseOrderDate >= '" + Criteria.FromDate.ToString() + "'";
                }
                if (Criteria.ToDate.Year > 1)
                {
                    if (strWhere != "")
                    {
                        strWhere = strWhere + " And ";
                    }
                    strWhere = strWhere + " PurchaseOrderDate <= '" + Criteria.ToDate.ToString() + "'";
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
                var result = context.SQLPurchaseOrderDetailsViewModel.FromSql("SELECT *,'" + Criteria.strGroupBy + "' As strGroupBy FROM dbo.vPurchaseOrders " + strWhere).ToList();

                return result.ToList();// projectDetailViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }
        public List<PurchaseOrderState> getPurchaseOrderStates()
        {
            return context.PurchaseOrderStates.ToList();
        }
        public MSIS.ViewModels.CreatePurchaseOrderViewModel getCreatePurchaseOrderDetails()
        {
            PurchaseOrderDetailsViewModel result = new PurchaseOrderDetailsViewModel();

            CreatePurchaseOrderViewModel model = new CreatePurchaseOrderViewModel();
            result.PurchaseOrderDate = DateTime.Today;
            result.DeliveryDate = DateTime.Today;
            model.PurchaseOrderDetails = result;
            model.suppliers = context.Suppliers.ToList();
            model.suppliers.Insert(0,new Models.Supplier()
            {
                Id = -1,
                SupplierName = "Select Supplier ..."
            });
            model.CurrencyList = context.Currency.ToList();
            model.Projects = context.Projects.ToList();
            model.Projects.Insert(0,new Models.Project()
            {
                Id = -1,
                ProjectName = "Select Project ..."
            });

            model.Employees = context.Employees.Where(x=>x.Active==true).ToList();
            model.Employees.Insert(0, new Models.Employee()
            {
                Id = -1,
                Name = "Select Employee ..."
            });

            model.Branches = context.Branches.ToList();
            model.Branches.Insert(0, new Models.Branch()
            {
                Id = -1,
                Name = "Select Branch ..."
            });
            model.PurchaseOrderStates = context.PurchaseOrderStates.ToList();
            model.PurchaseOrderStates.Insert(0, new Models.PurchaseOrderState() { 
            Id=-1,
            StateName="Select ..."
            });
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
        public Boolean DeletePurchaseOrder(int Id)
        {
            context.Database.BeginTransaction();
            try{
                PurchaseOrder purchaseOrder = context.PurchaseOrders.Find(Id);
                if (purchaseOrder != null)
                {
                    context.PurchaseOrdersDetails.RemoveRange(context.PurchaseOrdersDetails.Where(x=>x.PuchaseOrderId==Id).ToList());
                    context.SaveChanges();
                    context.PurchaseOrders.Remove(purchaseOrder);
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
        public PurchaseOrder Update(PurchaseOrder purchaseOrderChanges)
        {
            var Customer = context.PurchaseOrders.Attach(purchaseOrderChanges);
            Customer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return purchaseOrderChanges;
        }

        //-------------------------   Purchase Order Attachments
        public PurchaseOrderAttachment AddAttachment(PurchaseOrderAttachment attachment)
        {
            context.PurchaseOrderAttachments.Add(attachment);
            context.SaveChanges();
            return attachment;
        }
    }
}
