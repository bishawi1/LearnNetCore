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
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            Employee employee = _employeeRepository.GetEmployee(Id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel {
                Department=employee.Department,
                Email=employee.Email,
                Id=employee.Id,
                Name=employee.Name,
                EducationDegree=employee.EducationDegree,
                IdentityNo=employee.IdentityNo,
                MobileNo=employee.MobileNo,
                Specialization=employee.Specialization,
                WorkMobileNo=employee.WorkMobileNo,
                OtherInformation=employee.OtherInformation,
                Address=employee.Address,
                ExistingPhotoPath=employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }


        [HttpPost]
        public ActionResult Edit(EmployeeEditViewModel model)
        {
            Employee employee = _employeeRepository.GetEmployee(model.Id);
            if (ModelState.IsValid)
            {
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
                _employeeRepository.Update(employee);
                return RedirectToAction("EmployeeList", "home");
            }
            else
            {
                return View(model);
            }
        }




        [HttpGet]
        //[Route("Home/Details/{Id?}")]
        public ViewResult Details(int? Id) {
            Employee model = _employeeRepository.GetEmployee(Id??1);
            //ViewData["Employee"] = model;
            //ViewData["PageTitle"] = "Employee Details";
            //ViewBag.Employee = model;

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = model,
                PageTitle = "Employee Details"
            };
            //ViewBag.PageTitle = "Employee Details";
            return View(homeDetailsViewModel);
            //return View(model);
            //return _employeeRepository.GetEmployee(1).Name;
        }
        [HttpGet]
        public ViewResult EmployeeList()
        {
            IEnumerable<Employee> model=_employeeRepository.GetAllEmployees();
            return View(model);
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
