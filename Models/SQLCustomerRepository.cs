using Microsoft.EntityFrameworkCore;
using MSIS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSIS.Models
{
    public class SQLCustomerRepository
    {
        private readonly AppDBContext context;

        public SQLCustomerRepository(AppDBContext Context)
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
        public string ValidateDeletCustomer(int Id)
        {
            string ErrorMessage = "";
            var result = context.Offers.Where(x => x.CustomerId == Id ).ToList();
            if (result.Count > 0)
            {
                ErrorMessage = "cannot delete Customer, there is Offers for this Customer";
            }
            else
            {
                var purchaseOrder = context.Projects.Where(x => x.ProjectOwner == Id).ToList();
                if (purchaseOrder.Count > 0)
                {
                    ErrorMessage = "cannot delete Customer, Customer has Purchase Project or More";
                }
            }
            return ErrorMessage;
        }


        public Customer Add(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
            return customer;
        }

        public Customer Delete(int id)
        {
            Customer customer = context.Customers.Find(id);
            if (customer != null)
            {
                context.Customers.Remove(customer);
                context.SaveChanges();
            }
            return customer;
        }
        public ListCustomerViewModel ListCustomers()
        {
            ListCustomerViewModel model = new ListCustomerViewModel();
            model.Customers = context.Customers.ToList();
            return model;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return context.Customers;
        }

        public Customer GetCustomer(int Id)
        {
            return context.Customers.Find(Id);
        }

        public Customer Update(Customer customerChanges)
        {
            var Customer = context.Customers.Attach(customerChanges);
            Customer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return customerChanges;
        }


    }
}
