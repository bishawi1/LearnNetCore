using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnNetCore.Models;
using System.Net.Http;
using LearnNetCore.ViewModels;

namespace LearnNetCore.wwwroot.Controllers
{
    public class HomeController:Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository EmployeeRepository)
        {
            _employeeRepository = EmployeeRepository;
        }
        public JsonResult Index()
        {
            //MockEmployeeRepository employeeRepository  = new MockEmployeeRepository();
            return Json(new {IsSucceeded="true", ErrorText="",Employee= _employeeRepository.GetEmployee(1)});
            //return Json(new { id=1,Name="John"});
        }
        public ViewResult Details() {
            Employee model = _employeeRepository.GetEmployee(1);
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
        public ViewResult EmployeeList()
        {
            IEnumerable<Employee> model=_employeeRepository.GetAllEmployees();
            return View(model);
        }
    }
}
