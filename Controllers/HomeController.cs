using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSIS.Models;
using System.Net.Http;
using MSIS.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MSIS.wwwroot.Controllers
{
    public class HomeController:Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public HomeController(IEmployeeRepository EmployeeRepository, IHostingEnvironment hostingEnvironment)
        {
            _employeeRepository = EmployeeRepository;
            this.hostingEnvironment = hostingEnvironment;
        }


        [AllowAnonymous]
        public ViewResult Index()
        {

            //MockEmployeeRepository employeeRepository  = new MockEmployeeRepository();
            //return Json(new {IsSucceeded="true", ErrorText="",Employee= _employeeRepository.GetEmployee(1)});
            //return Json(new { id=1,Name="John"});
            return View();
        }
        [AllowAnonymous]
        public ViewResult PortalIndex()
        {

            //MockEmployeeRepository employeeRepository  = new MockEmployeeRepository();
            //return Json(new {IsSucceeded="true", ErrorText="",Employee= _employeeRepository.GetEmployee(1)});
            //return Json(new { id=1,Name="John"});
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            Employee employee = _employeeRepository.GetEmployee(Id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel {
                Department = employee.Department,
                Email = employee.Email,
                Id = employee.Id,
                Name = employee.Name,
                EducationDegree = employee.EducationDegree,
                IdentityNo = employee.IdentityNo,
                MobileNo = employee.MobileNo,
                Specialization = employee.Specialization,
                WorkMobileNo = employee.WorkMobileNo,
                OtherInformation = employee.OtherInformation,
                Address = employee.Address,
                ExistingPhotoPath = employee.PhotoPath,
                PhoneNo = employee.PhoneNo,
                JobDescription = employee.JobDescription
            };
            return View(employeeEditViewModel);
        }


        [HttpPost]
        public ActionResult Edit(EmployeeEditViewModel model)
        {
            Employee employee = _employeeRepository.GetEmployee(model.Id);
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photos != null)
                {
                    foreach (IFormFile photo in model.Photos)
                    {
                        string UploadFolders = Path.Combine(hostingEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(photo.FileName);// model.Photo.Name;
                        string filePath = Path.Combine(UploadFolders, uniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                };
                employee.Address = model.Address;
                employee.Department = model.Department;
                employee.EducationDegree = model.EducationDegree;
                employee.Email = model.Email;
                employee.IdentityNo = model.IdentityNo;
                employee.MobileNo = model.MobileNo;
                employee.Name = model.Name;
                employee.OtherInformation = model.OtherInformation;
                employee.Specialization = model.Specialization;
                employee.WorkMobileNo = model.WorkMobileNo;
                employee.JobDescription = model.JobDescription;
                employee.PhoneNo = model.PhoneNo;
                if (model.Photos != null)
                {
                    employee.PhotoPath = uniqueFileName;
                }
                _employeeRepository.Update(employee);
                return RedirectToAction("EmployeeList", "home");
            }
            else
            {
                return View(model);
            }
        }


        [HttpPost]
        public IActionResult Delete(int Id)
        {
            string errorMessage = "";
            errorMessage = _employeeRepository.ValidateDeletEmployee(Id);
            //PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
            if (errorMessage == "")
            {
                Employee employee = _employeeRepository.Delete(Id);
                if (employee == null)
                {
                    return Redirect("NotFound");
                }
                else
                {
                    //return new JsonResult("{Deleted:true,ErrorText:''}");
                    List<Employee> model = _employeeRepository.GetAllEmployees().ToList();
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
            errorMessage = _employeeRepository.ValidateDeletEmployee(Id);
            //PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
            return new JsonResult(errorMessage);
        }

        [HttpGet]
        //[Route("Home/Details/{Id?}")]
        public ViewResult Details(int? Id) {
            EmployeeDetailsViewModel model = new EmployeeDetailsViewModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MSIS.ViewModels.UserPermissionsViewModel permission = _employeeRepository.GetUserParentMenuPermission(userId, "Employees");
            if (permission.UserPermissions.Count > 0)
            {
                model.Permission = permission.UserPermissions[0];
            }
            model.Employee = _employeeRepository.GetEmployee(Id??1);
            //ViewData["Employee"] = model;
            //ViewData["PageTitle"] = "Employee Details";
            //ViewBag.Employee = model;

            //HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            //{
            //    Employee = model,
            //    PageTitle = "Employee Details"
            //};
            //ViewBag.PageTitle = "Employee Details";
            return View(model);
            //return View(model);
            //return _employeeRepository.GetEmployee(1).Name;
        }
        [HttpGet]
        public IActionResult EmployeeList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MSIS.ViewModels.UserPermissionsViewModel permission = _employeeRepository.GetUserParentMenuPermission(userId, "Employees");

            ListEmployeesViewModel model = _employeeRepository.ListEmployees();
            model.userPermission = permission.UserPermissions[0];

            return View(model);
            //IEnumerable<Employee> model=_employeeRepository.GetAllEmployees();
            ////return PartialView("EmployeeList", model);
            //return View(model);
        }
        [HttpGet]
        public IActionResult EmployeeListAjax()
        {
            IEnumerable<Employee> model = _employeeRepository.GetAllEmployees();
            return new JsonResult(model);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if(model.Photos != null && model.Photos.Count > 0)
                {
                    foreach(IFormFile photo in model.Photos){ 
                    string UploadFolders= Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(photo.FileName);// model.Photo.Name;
                    string filePath= Path.Combine( UploadFolders, uniqueFileName);
                    photo.CopyTo(new FileStream(filePath,FileMode.Create));
                    }
                }

                Employee NewEmplyee = new Employee()
                {
                    Name = model.Name,
                    Email=model.Email,
                    Department=model.Department,
                    EducationDegree=model.EducationDegree,
                    IdentityNo=model.IdentityNo,
                    MobileNo=model.MobileNo,
                    Specialization=model.Specialization,
                    OtherInformation=model.Specialization,
                    WorkMobileNo=model.WorkMobileNo,
                    Address=model.Address,
                    PhotoPath=uniqueFileName
                };            
                    _employeeRepository.Add(NewEmplyee);
                    return RedirectToAction("Details", new { Id = NewEmplyee.Id });
            }
            return View();
        }
    }



}
