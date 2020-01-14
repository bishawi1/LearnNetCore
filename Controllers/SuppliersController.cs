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
    public class SuppliersController : Controller
    {
        public SQLSupplierRepository suppliersRepository { get; }
        public IHostingEnvironment hostingEnvironment { get; }
        public SuppliersController(SQLSupplierRepository suppliersRepository, IHostingEnvironment hostingEnvironment)
        {
            this.suppliersRepository = suppliersRepository;
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            string errorMessage = "";
            errorMessage = suppliersRepository.ValidateDeletSupplier(Id);
            //PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
            if (errorMessage == "")
            {
                Supplier supplier = suppliersRepository.Delete(Id);
                if (supplier == null)
                {
                    return Redirect("NotFound");
                }
                else
                {
                    //return new JsonResult("{Deleted:true,ErrorText:''}");
                    List<Supplier> model = suppliersRepository.GetAllSuppliers().ToList();
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
            errorMessage = suppliersRepository.ValidateDeletSupplier(Id);
            //PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
            return new JsonResult(errorMessage);
        }


        [HttpGet]
        public IActionResult ListSuppliers()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MSIS.ViewModels.UserPermissionsViewModel permission = suppliersRepository.GetUserParentMenuPermission(userId, "Suppliers");

            ListSupplierViewModel model = suppliersRepository.ListSuppliers();
            model.userPermission = permission.UserPermissions[0];
            return View(model);

            //var model = suppliersRepository.GetAllSuppliers().ToList();
            //return View(model);
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            SupplierDetailsViewModel model = new SupplierDetailsViewModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MSIS.ViewModels.UserPermissionsViewModel permission = suppliersRepository.GetUserParentMenuPermission(userId, "Suppliers");

            if (permission.UserPermissions.Count > 0)
            {
                model.Permission = permission.UserPermissions[0];
            }


            var supplier = suppliersRepository.GetSupplier(Id);
            model.Supplier = supplier;
            return View(model);
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
                    if (suppliersRepository.IsSupplierExists(supplierChanges.Id, supplierChanges.SupplierCode))
                    {
                        ModelState.AddModelError("SupplierCode", "Supplier Code Already Used.");
                        return View(supplier);
                    }
                    supplier.OtherInformation = supplierChanges.OtherInformation;
                    supplier.Address = supplierChanges.Address;
                    supplier.MobileNo = supplierChanges.MobileNo;
                    supplier.SupplierCode = supplierChanges.SupplierCode;
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
                if (suppliersRepository.IsSupplierExists(supplier.Id, supplier.SupplierCode))
                {
                    ModelState.AddModelError("SupplierCode", "Supplier Code Already Used.");
                    return View(supplier);
                }
                suppliersRepository.Add(supplier);
                return RedirectToAction("ListSuppliers", "Suppliers");
            }
            return View();
        }

    }
}