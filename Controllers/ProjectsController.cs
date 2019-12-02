using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MSIS.ViewModels;
using MSIS.Models;
using System.Security.Claims;

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

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            string errorMessage = "";
            errorMessage = projectsRepository.ValidateDeletProject(Id);
            //PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
            if (errorMessage == "")
            {
                Project project = projectsRepository.Delete(Id);
                if (project == null)
                {
                    return Redirect("NotFound");
                }
                else
                {
                    //return new JsonResult("{Deleted:true,ErrorText:''}");
                    List<Project> model = projectsRepository.GetAllProjects().ToList();
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
            errorMessage = projectsRepository.ValidateDeletProject(Id);
            //PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
            return new JsonResult(errorMessage);
        }

        [HttpGet]
        public IActionResult ListProjects()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MSIS.ViewModels.UserPermissionsViewModel permission = projectsRepository.GetUserParentMenuPermission(userId, "projects");

            ListProjectsViewModels model = projectsRepository.ListProjects();
            model.userPermission = permission.UserPermissions[0];

            return View(model);
            //var model = projectsRepository.getAllProjectDetails().ToList();
            //return View(model);
        }
        [HttpGet]
        public IActionResult Details(int Id)
        {
            ProjectDetailsViewModels model = new ProjectDetailsViewModels();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MSIS.ViewModels.UserPermissionsViewModel permission = projectsRepository.GetUserParentMenuPermission(userId, "projects");

            if (permission.UserPermissions.Count > 0)
            {
                model.Permission = permission.UserPermissions[0];
            }

            Project project = projectsRepository.GetProject(Id);
            Customer customer = customersRepository.GetCustomer(project.ProjectOwner);
            MSIS.ViewModels.ProjectDetailViewModel details = new ViewModels.ProjectDetailViewModel() {
                Code = project.ProjectSerial.ToString() + "/" + project.ProjectYear.ToString() ,
                Address=project.Address,
                ProjectSerial=project.ProjectSerial,
                ProjectYear=project.ProjectYear,
                Id=project.Id,
                CustomerName=customer.CustomerName,
                Email=customer.Email,
                MobileNo=customer.MobileNo,
                OtheInformation=project.OtheInformation,
                ProjectName=project.ProjectName,
                ProjectOwner=project.ProjectOwner,
                EndDate=project.EndDate,
                StartDate=project.StartDate
            };
            model.ProjectDetails = details;
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var project = new MSIS.ViewModels.CreateProjectViewModel();
            project.Customers = customersRepository.GetAllCustomers().ToList();
            project.Customers.Insert(0,new Customer() { 
            Id=-1,
            CustomerName="Select ..."
            });
            project.ProjectYear = DateTime.Today.Year;
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
            model.Customers = customersRepository.GetAllCustomers().ToList();
            
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