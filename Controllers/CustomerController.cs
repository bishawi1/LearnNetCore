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
    public class CustomerController : Controller
    {
        private readonly SQLCustomerRepository customersRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public CustomerController(SQLCustomerRepository CustomersRepository, IHostingEnvironment hostingEnvironment)
        {
            customersRepository = CustomersRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            string errorMessage = "";
            errorMessage = customersRepository.ValidateDeletCustomer(Id);
            //PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
            if (errorMessage == "")
            {
                Customer customer = customersRepository.Delete(Id);
                if (customer == null)
                {
                    return Redirect("NotFound");
                }
                else
                {
                    //return new JsonResult("{Deleted:true,ErrorText:''}");
                    List<Customer> model = customersRepository.GetAllCustomers().ToList();
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
            errorMessage = customersRepository.ValidateDeletCustomer(Id);
            //PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
            return new JsonResult(errorMessage);
        }


        public IActionResult ListCustomers()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MSIS.ViewModels.UserPermissionsViewModel permission = customersRepository.GetUserParentMenuPermission(userId, "Customers");

            ListCustomerViewModel model = customersRepository.ListCustomers();
            model.userPermission = permission.UserPermissions[0];

            return View(model);
        }

        public IActionResult Details(int Id)
        {
            CustomerDetailsViewModel model = new CustomerDetailsViewModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MSIS.ViewModels.UserPermissionsViewModel permission = customersRepository.GetUserParentMenuPermission(userId, "Customers");

            if (permission.UserPermissions.Count > 0)
            {
                model.Permission = permission.UserPermissions[0];
            }


            var customer = customersRepository.GetCustomer(Id);
            model.Customer = customer;
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Customer customer = customersRepository.GetCustomer(Id);
            if (customer == null)
            {
                return Redirect("NotFound");
            }
            else
            {
                return View(customer);
            }
        }
        [HttpPost]
        public IActionResult Edit(Customer customerChanges)
        {
            if (ModelState.IsValid)
            {
                Customer customer = customersRepository.GetCustomer(customerChanges.Id);
                if (customer == null)
                {
                    return Redirect("NotFound");
                }
                else
                {
                    customer.OtherInformation = customerChanges.OtherInformation;
                    customer.Address = customerChanges.Address;
                    customer.MobileNo = customerChanges.MobileNo;
                    customer.CustomerName = customerChanges.CustomerName;
                    customer.Email = customerChanges.Email;
                    customersRepository.Update(customer);
                    return RedirectToAction("ListCustomers", "Customer");
                }
            }
            return View(customerChanges);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customersRepository.Add(customer);
                return RedirectToAction("ListCustomers","Customer");
            }
            return View();
        }

    }
}