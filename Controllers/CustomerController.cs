using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MSIS.Models;
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
        public IActionResult ListCustomers()
        {
            var model = customersRepository.GetAllCustomers().ToList();
            return View(model);
        }

        public IActionResult Details(int Id)
        {
            var customer = customersRepository.GetCustomer(Id);
            return View(customer);
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