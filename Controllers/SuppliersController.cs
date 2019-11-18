using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MSIS.Models;
namespace MSIS.Controllers
{
    public class SuppliersController : Controller
    {
        public SQLSupplierRepository suppliersRepository { get; }
        public IHostingEnvironment hostingEnvironment { get; }
        public SuppliersController(SQLSupplierRepository suppliersRepository, IHostingEnvironment hostingEnvironment)
        {
            this.suppliersRepository = suppliersRepository;
            this.hostingEnvironment = hostingEnvironment;
        }


        [HttpGet]
        public IActionResult ListSuppliers()
        {
            var model = suppliersRepository.GetAllSuppliers().ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int Id)
        {
            var supplier = suppliersRepository.GetSupplier(Id);
            return View(supplier);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Supplier supplier = suppliersRepository.GetSupplier(Id);
            if (supplier == null)
            {
                return Redirect("NotFound");
            }
            else
            {
                return View(supplier);
            }
        }

        [HttpPost]
        public IActionResult Edit(Supplier supplierChanges)
        {
            if (ModelState.IsValid)
            {
                Supplier supplier = suppliersRepository.GetSupplier(supplierChanges.Id);
                if (supplier == null)
                {
                    return Redirect("NotFound");
                }
                else
                {
                    supplier.OtherInformation = supplierChanges.OtherInformation;
                    supplier.Address = supplierChanges.Address;
                    supplier.MobileNo = supplierChanges.MobileNo;
                    supplier.SupplierName = supplierChanges.SupplierName;
                    supplier.Email = supplierChanges.Email;
                    supplier.Fax = supplierChanges.Fax;
                    supplier.Phone = supplierChanges.Phone;
                    suppliersRepository.Update(supplier);
                    return RedirectToAction("ListSuppliers", "Suppliers");
                }
            }
            return View(supplierChanges);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                suppliersRepository.Add(supplier);
                return RedirectToAction("ListSuppliers", "Suppliers");
            }
            return View();
        }

    }
}