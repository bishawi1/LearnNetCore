using Microsoft.EntityFrameworkCore;
using MSIS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSIS.Models
{
    public class SQLSupplierRepository
    {
        private readonly AppDBContext context;

        public SQLSupplierRepository(AppDBContext Context)
        {
            context = Context;
        }
        public UserPermissionsViewModel GetUserParentMenuPermission(string UserId, string PageName)
        {
            UserPermissionsViewModel model = new UserPermissionsViewModel();
            var result = context.SQLUserAllowedParentMenuesViewModel.FromSql("SELECT * FROM dbo.UserAllowedParentMenu Where ParentName = 'Settings' And UserId = '" + UserId + "' And PageName ='" + PageName + "'").ToList();
            var Menues = result.Select(x => x.ParentName).Distinct().ToList();
            model.ParentMenus = Menues;
            model.UserPermissions = result;
            return model;
        }
        public string ValidateDeletSupplier(int Id)
        {
            string ErrorMessage = "";

                var purchaseOrder = context.PurchaseOrders.Where(x => x.SupplierId == Id).ToList();
                if (purchaseOrder.Count > 0)
                {
                    ErrorMessage = "cannot delete Supplier, Supplier has Purchase Order or more";
                }

            return ErrorMessage;
        }


        public Supplier Add(Supplier supplier)
        {
            context.Suppliers.Add(supplier);
            context.SaveChanges();
            return supplier;
        }

        public Supplier Delete(int id)
        {
            Supplier supplier = context.Suppliers.Find(id);
            if (supplier != null)
            {
                context.Suppliers.Remove(supplier);
                context.SaveChanges();
            }
            return supplier;
        }
        public ListSupplierViewModel ListSuppliers()
        {
            ListSupplierViewModel model = new ListSupplierViewModel();
            model.Suppliers = context.Suppliers.ToList();
            return model;
        }
        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return context.Suppliers;
        }

        public Supplier GetSupplier(int Id)
        {
            return context.Suppliers.Find(Id);
        }

        public Supplier Update(Supplier supplierChanges)
        {
            var supplier = context.Suppliers.Attach(supplierChanges);
            supplier.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return supplierChanges;
        }
    }
}
