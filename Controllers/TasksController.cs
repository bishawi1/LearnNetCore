﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MSIS.Models;
using MSIS.ViewModels;

namespace MSIS.Controllers
{
    public class TasksController : Controller
    {
        private readonly SQLTasksRepository tasksRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public TasksController(SQLTasksRepository TasksRepository, IHostingEnvironment hostingEnvironment)
        {
            tasksRepository = TasksRepository;
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public IActionResult Create()
        {
            AppDBContext context = tasksRepository.getContext();
            MSIS.ViewModels.CreateTaskViewModel model = new ViewModels.CreateTaskViewModel();
            SQLCustomerRepository customerRepository = new SQLCustomerRepository(context);
            SQLProjectRepository projectRepository = new SQLProjectRepository(context);
            SQLEmployeeRepository employeeRepository = new SQLEmployeeRepository(context);
            SQLBranchRepository branchRepository = new SQLBranchRepository(context);
            model.Customers = customerRepository.GetAllCustomers().ToList();
            model.Projects = projectRepository.GetAllProjects().ToList();
            model.Employees = employeeRepository.GetAllEmployees().ToList();
            model.Branches = branchRepository.GetAllBranches().ToList();
            model.TaskStatusList = tasksRepository.GetAllTaskStatus().ToList();
            model.TaskStatusId = 1;
            model.TaskDate = DateTime.Today;
            model.TaskStartDate = DateTime.Today;
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(CreateTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                tasksRepository.Add(model);
                return RedirectToAction("ListTasks", "Tasks");
            }
           return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var task = tasksRepository.GetTasks(Id);
            if (task == null)
            {
                ViewBag.ErrorMessage = "Task not Found";
                return View("NotFound");
            }
            else
            {
                AppDBContext context = tasksRepository.getContext();
                MSIS.ViewModels.CreateTaskViewModel model = new ViewModels.CreateTaskViewModel();
                SQLCustomerRepository customerRepository = new SQLCustomerRepository(context);
                SQLProjectRepository projectRepository = new SQLProjectRepository(context);
                SQLEmployeeRepository employeeRepository = new SQLEmployeeRepository(context);
                SQLBranchRepository branchRepository = new SQLBranchRepository(context);
                model.Customers = customerRepository.GetAllCustomers().ToList();
                model.Projects = projectRepository.GetAllProjects().ToList();
                model.Employees = employeeRepository.GetAllEmployees().ToList();
                model.Branches = branchRepository.GetAllBranches().ToList();
                model.TaskStatusList = tasksRepository.GetAllTaskStatus().ToList();
                model.TaskStatusId = task.TaskStatusId;
                model.TaskDate = task.TaskDate;
                model.TaskStartDate = task.TaskStartDate;
                model.BranchId = task.BranchId;
                model.Description = task.Description;
                model.Id = task.Id;
                model.OtherInformation = task.OtherInformation;
                model.ProjectId = task.ProjectId;
                model.TaskEndDate = task.TaskEndDate;
                model.TaskOwnerId = task.TaskOwnerId;
                model.TaskStatusId = task.TaskStatusId;
                model.TaskResponsibleId = task.TaskResponsibleId;
                model.TaskResultDescription = task.TaskResultDescription;
                model.TaskSubject = task.TaskSubject;
                model.Time_Stamp = task.Time_Stamp;
                model.UserName = task.UserName;
                return View(model);

            }
        }

        [HttpPost]
        public IActionResult Edit(CreateTaskViewModel tasksChanges)
        {
            var task = tasksRepository.GetTasks(tasksChanges.Id);
            if (task == null)
            {
                ViewBag.ErrorMessage = "Task not Found";
                return Redirect("NotFound");
            }
            else
            {
                task.TaskStatusId = tasksChanges.TaskStatusId;
                task.TaskDate = tasksChanges.TaskDate;
                task.TaskStartDate = tasksChanges.TaskStartDate;
                task.BranchId = tasksChanges.BranchId;
                task.Description = tasksChanges.Description;
                task.Id = tasksChanges.Id;
                task.OtherInformation = tasksChanges.OtherInformation;
                task.ProjectId = tasksChanges.ProjectId;
                task.TaskEndDate = tasksChanges.TaskEndDate;
                task.TaskOwnerId = tasksChanges.TaskOwnerId;
                task.TaskResponsibleId = tasksChanges.TaskResponsibleId;
                task.TaskResultDescription = tasksChanges.TaskResultDescription;
                task.TaskSubject = tasksChanges.TaskSubject;
                task.TaskStatusId = tasksChanges.TaskStatusId;
                task.Time_Stamp = tasksChanges.Time_Stamp;
                task.UserName = tasksChanges.UserName;
                task = tasksRepository.Update(task);
                return RedirectToAction("ListTasks","Tasks");

            }
        }



        [HttpGet]
        public IActionResult ListTasks()
        {
            //var model = tasksRepository.GetAllTasks().ToList();
            var model = tasksRepository.getAllTaskDetails().ToList();
            return View( model);
        }

        [HttpGet]
        public IActionResult ListTasksAjax()
        {
            //var model = tasksRepository.GetAllTasks().ToList();
            var model = tasksRepository.getAllTaskDetails().ToList();
            return new JsonResult(model);
        }
        [HttpGet]
        public IActionResult ListTasksPartial()
        {
            //var model = tasksRepository.GetAllTasks().ToList();
            var model = tasksRepository.getAllTaskDetails().ToList();
            return PartialView("_ListTasks", model);
        }
        [HttpGet]
        public IActionResult ListWaitingTasks()
        {
            //var model = tasksRepository.GetAllTasks().ToList();
            var model = tasksRepository.getTaskDetailsByStatusId(1).ToList();
            return PartialView("_ListTasks", model);
        }
        [HttpGet]
        public IActionResult ListInProgressTasks()
        {
            //var model = tasksRepository.GetAllTasks().ToList();
            var model = tasksRepository.getTaskDetailsByStatusId(2).ToList();
            return PartialView("_ListTasks", model);
        }
        [HttpGet]
        public IActionResult ListRejectedTasks()
        {
            //var model = tasksRepository.GetAllTasks().ToList();
            var model = tasksRepository.getTaskDetailsByStatusId(3).ToList();
            return PartialView("_ListTasks", model);
        }
        [HttpGet]
        public IActionResult ListDoneTasks()
        {
            //var model = tasksRepository.GetAllTasks().ToList();
            var model = tasksRepository.getTaskDetailsByStatusId(4).ToList();
            return PartialView("_ListTasks", model);
        }
        [HttpGet]
        public IActionResult ListApprovedTasks()
        {
            //var model = tasksRepository.GetAllTasks().ToList();
            var model = tasksRepository.getTaskDetailsByStatusId(5).ToList();
            return PartialView("_ListTasks", model);
        }
        [HttpGet]
        public IActionResult TaskCountByStatus()
        {
            return new JsonResult( tasksRepository.getTaskCountByStatus());
        }



        [HttpPost]
        [AllowAnonymous]
        [Route("Tasks/SearchListTask/{searchText?}")]
        public IActionResult SearchListTask(string searchText)
        {
            var criteria = searchText.ToLower();

            var result = tasksRepository.getContext().Tasks.Where(p => p.TaskSubject.Contains(criteria)).ToList();
            var aa= new JsonResult(result);

            return (aa);
        }
        [HttpGet]
        public IActionResult Details(int Id)
        {
            var model = tasksRepository.getTaskDetails(Id);
            return View(model);
        }
        [HttpPost]
        public IActionResult StartTask(EditTaskStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.TaskStatusID =2;
                model.UserName = User.Identity.Name;
                tasksRepository.EditTaskStatus(model);
                return RedirectToAction("ListTasks", "Tasks");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult CompleteTask(EditTaskStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.TaskStatusID = 4;
                model.UserName = User.Identity.Name;
                tasksRepository.EditTaskStatus(model);
                return RedirectToAction("ListTasks", "Tasks");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult VerifyTaskExecution(EditTaskStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.TaskOperation == "0")
                {
                    model.TaskStatusID = 5;

                }
                else if (model.TaskOperation == "1")
                {
                    model.TaskStatusID = 3;

                }
                model.UserName = User.Identity.Name;
                tasksRepository.EditTaskStatus(model);
                return RedirectToAction("ListTasks", "Tasks");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult ApproveTask(EditTaskStatusViewModel model,bool Rejected)
        {
            if (ModelState.IsValid)
            {
                model.TaskStatusID = 5;
                model.UserName = User.Identity.Name;
                tasksRepository.EditTaskStatus(model);
                return RedirectToAction("ListTasks", "Tasks");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditEmployeeInTask(int TaskId)
        {
            ViewBag.TaskId = TaskId;
            var context = tasksRepository.getContext();
            var model = new List<EmployeesInTaskViewModel>();
            foreach (var employee in context.Employees)
            {
                var userRoleViewModel = new EmployeesInTaskViewModel()
                {
                    EmployeeId=employee.Id,
                    EmployeeName=employee.Name,
                    TaskId=TaskId
                };
                var ExistInTeam = context.TaskTeams.Where(x => x.TaskId == TaskId && x.EmployeeId == employee.Id).FirstOrDefault();
                if (ExistInTeam==null)
                {
                    userRoleViewModel.IsSelected = false;
                }
                else
                {
                    userRoleViewModel.IsSelected = true;
                    userRoleViewModel.Id = ExistInTeam.Id;
                }

                model.Add(userRoleViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployeeInTask(List<EmployeesInTaskViewModel> model,int TaskID)
        {
            int taskID = 0;
            var context = tasksRepository.getContext();
            for (int i = 0; i < model.Count; i++)
            {
                
                var ExistInTeam = context.TaskTeams.Where(x => x.TaskId == model[i].TaskId && x.EmployeeId == model[i].EmployeeId).FirstOrDefault();

                if (model[i].IsSelected && !(ExistInTeam!=null))
                {
                    TaskTeam AssignEmployee = new TaskTeam() {
                        EmployeeId = model[i].EmployeeId,
                        TaskId =TaskID,
                        UserName=User.Identity.Name
                    };

                    tasksRepository.AssignEmployeeToTask(AssignEmployee);//   context.TaskTeams.Add(ExistInTeam);// await userManager.AddToRoleAsync(user, role.Name);

                }
                else if (!model[i].IsSelected && (ExistInTeam != null))
                {
                    tasksRepository.ExcludeEmployeeFromTask(ExistInTeam.Id);
                }
                else
                {
                    continue;
                }
                //if (result.Succeeded)
                //{
                    if (i < model.Count - 1)
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("Details", "Tasks", new { id = TaskID });
                    };
                //};
            };

            return RedirectToAction("Details", "Tasks", new { id = TaskID });

            //var role = await roleManager.FindByIdAsync(model.Id);
            //if (role == null)
            //{
            //    ViewBag.ErrorMessage = $"Role With Id {model.Id} cannot be found";
            //    return View("notFound");
            //}
            //else
            //{
            //    role.Name = model.RoleName;
            //    IdentityResult result = await roleManager.UpdateAsync(role);
            //    if (result.Succeeded)
            //    {
            //        return RedirectToAction("ListRoles", "Administration");
            //    }
            //    else
            //    {
            //        foreach (var error in result.Errors)
            //        {
            //            ModelState.AddModelError("", error.Description);
            //        }
            //        return View(model);
            //    }
            //}
            return View(model);
        }




    }
}