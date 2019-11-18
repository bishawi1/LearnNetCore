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
