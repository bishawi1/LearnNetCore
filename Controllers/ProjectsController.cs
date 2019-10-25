using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MSIS.Models;
namespace MSIS.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly SQLProjectRepository projectsRepository;
        private readonly SQLCustomerRepository customersRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public ProjectsController(SQLProjectRepository ProjectsRepository, SQLCustomerRepository CustomersRepository, IHostingEnvironment hostingEnvironment)
        {
            projectsRepository = ProjectsRepository;
            customersRepository = CustomersRepository;
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public IActionResult ListProjects()
        {
            var model = projectsRepository.getAllProjectDetails().ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int Id)
        {

            Project model = projectsRepository.GetProject(Id);
            Customer customer = customersRepository.GetCustomer(model.ProjectOwner);
            MSIS.ViewModels.ProjectDetailViewModel details = new ViewModels.ProjectDetailViewModel() {
                Code = model.ProjectSerial.ToString() + "/" + model.ProjectYear.ToString() ,
                Address=model.Address,
                ProjectSerial=model.ProjectSerial,
                ProjectYear=model.ProjectYear,
                Id=model.Id,
                CustomerName=customer.CustomerName,
                Email=customer.Email,
                MobileNo=customer.MobileNo,
                OtheInformation=model.OtheInformation,
                ProjectName=model.ProjectName,
                ProjectOwner=model.ProjectOwner,
                EndDate=model.EndDate,
                StartDate=model.StartDate
            };

            return View(details);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var project = new MSIS.ViewModels.CreateProjectViewModel();
            project.Customers = customersRepository.GetAllCustomers();
            return View(project);
        }
        [HttpPost]
        public IActionResult Create(MSIS.ViewModels.CreateProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ProjectYear = DateTime.Today.Year;
                projectsRepository.Add(model);
                return RedirectToAction("ListProjects", "Projects");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var project = projectsRepository.GetProject(Id);
            var model = new MSIS.ViewModels.CreateProjectViewModel();
            model.Customers = customersRepository.GetAllCustomers();
            
            model.Id = project.Id;
            model.MobileNo = project.MobileNo;
            model.OtheInformation = project.OtheInformation;
            model.ProjectName = project.ProjectName;
            model.ProjectOwner = project.ProjectOwner;
            model.ProjectSerial = project.ProjectSerial;
            model.ProjectYear = project.ProjectYear;
            model.StartDate = project.StartDate;
            model.EndDate = project.EndDate;
            model.Address = project.Address;
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(MSIS.ViewModels.CreateProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                Project project = projectsRepository.GetProject(model.Id);
                if (project == null)
                {
                    ViewBag.ErrorMessage = "Project not Found";
                    return Redirect("NotFound");
                }
                else
                {
                    project.ProjectOwner = model.ProjectOwner;
                    project.Address = model.Address;
                    project.OtheInformation = model.OtheInformation;
                    projectsRepository.Update(project);
                    return RedirectToAction("ListProjects", "Projects");
                }
            }
            return View(model);
        }
    }
}