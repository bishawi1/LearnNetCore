using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnNetCore.Models;
namespace LearnNetCore.wwwroot.Controllers
{
    public class HomeController:Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository EmployeeRepository)
        {
            _employeeRepository = EmployeeRepository;
        }
        public string Index()
        {
           //MockEmployeeRepository employeeRepository  = new MockEmployeeRepository();
            return _employeeRepository.GetEmployee(1).Name;
            //return Json(new { id=1,Name="John"});
        }
        public ViewResult Details() {
            Employee model = _employeeRepository.GetEmployee(1);
            //ViewData["Employee"] = model;
            //ViewData["PageTitle"] = "Employee Details";
            ViewBag.Employee = model;
            ViewBag.PageTitle = "Employee Details";
            return View();
            //return View(model);
            //return _employeeRepository.GetEmployee(1).Name;
        }

    }
}
