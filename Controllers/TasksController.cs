using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MSIS.Models;
using MSIS.ViewModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Itenso.TimePeriod;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MSIS.Controllers
{
    public class TasksController : Controller
    {
        private readonly SQLTasksRepository tasksRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmployeeRepository employeeRepository;

        public SQLSettingsRepository SettingsRepository { get; }

        public TasksController(SQLTasksRepository TasksRepository, IHostingEnvironment hostingEnvironment,
                                 UserManager<ApplicationUser> userManager,
                                 IEmployeeRepository employeeRepository,
                                 SQLSettingsRepository settingsRepository)
        {
            tasksRepository = TasksRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
            this.employeeRepository = employeeRepository;
            SettingsRepository = settingsRepository;
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
            model.Customers.Insert(0,new Customer() { 
                Id=-1,
                CustomerName="Select ..."
            });
            model.Projects = projectRepository.GetAllProjects().ToList();
            model.Projects.Insert(0, new Project()
            {
                Id = -1,
                ProjectName = "Select ..."
            });
            model.Employees = employeeRepository.GetAllEmployees().Where(x=>x.Active==true).ToList();
            model.Employees.Insert(0, new Employee()
            {
                Id = -1,
                Name = "Select ..."
            });
            model.Branches = branchRepository.GetAllBranches().ToList();
            model.Branches.Insert(0, new Branch()
            {
                Id = -1,
                Name = "Select ..."
            });
            model.TaskStatusList = tasksRepository.GetAllTaskStatus().ToList();
            model.TaskStatusId = 1;
            model.TaskDate = DateTime.Today;
            model.TaskStartTime = DateTime.Now.ToString("hh:mm:ss tt");
            model.TaskStartDate = DateTime.Today;
            model.TaskEndDate = DateTime.Today.AddDays(1);
            model.TaskEndTime = DateTime.Now.ToString("hh:mm:ss tt");
            model.TaskOwnerId= userManager.Users.Where(x => x.UserName == User.Identity.Name).Select(x => x.EmployeeId).FirstOrDefault();
            model.ContinuousTask = false;
            model.ContinueFromDate = DateTime.Today;
            model.ContinueToDate = DateTime.Today;
            try
            {
            model.PeriodTypes = SettingsRepository.PeriodTypeList().ToList();

            }catch(Exception ex)
            {

            }
            model.PeriodId = 1;
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(CreateTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserName = User.Identity.Name;
                model.Time_Stamp = DateTime.Now;
                model.TaskOwnerId = userManager.Users.Where(x => x.UserName == User.Identity.Name).Select(x => x.EmployeeId).FirstOrDefault();
                if (model.ContinuousTask)
                {
                    model.TaskStatusId = 7;//cont
                }
                    var addedTask =tasksRepository.Add(model);
                if (model.ContinuousTask)
                {
                    ContinuousTask continuousTask = new ContinuousTask();
                    continuousTask.PeriodId = model.PeriodId;
                    continuousTask.TaskId = addedTask.Id;
                    continuousTask.ContinueFromDate = model.ContinueFromDate;
                    continuousTask.ContinueToDate = model.ContinueToDate;
                    if ((model.ContinueFromDate<DateTime.Today) && (model.ContinueToDate >= DateTime.Today))
                    {
                        continuousTask.NextTaskDate = DateTime.Today;
                    }
                    else
                    {
                        continuousTask.NextTaskDate = model.ContinueFromDate;
                    }
                    var addedContinuousTask = tasksRepository.AddContinuousTask(continuousTask);
                    var xx = 0;
                }

                if (SettingsRepository.SendMailOnCreateTask())
                {
                    string link = UriHelper.GetDisplayUrl(Request).Replace("Create", "") + "Details/" + addedTask.Id.ToString();
                    //string strMyUrl = Context.Request.Scheme + Request.Host.Value +  Url.Action("Details", "Tasks", new { Id = addedTask.Id }).ToString();
                    //Uri myUri = new Uri("https://localhost:44309/Tasks/Details/" + );
                    string strMessage = "تم اضافة مهمة جديدة، للتفاصيل الرجاء الضغط على الرابط  " + System.Environment.NewLine + link.ToString();
                    List<int> Employees = new List<int>();
                    Employees.Add(model.TaskOwnerId);
                    if (model.TaskResponsibleId != model.TaskOwnerId) { 
                        Employees.Add(model.TaskResponsibleId);
                    }
                    SettingsRepository.SendEmail(Employees, strMessage);
                }
                return RedirectToAction("Details", "Tasks", new { Id=addedTask.Id});
            }
            else
            {
                foreach (var modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        ModelState.AddModelError("",error.ErrorMessage);
                    }
                }
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
                model.Employees = employeeRepository.GetAllEmployees().Where(x=>x.Active==true).ToList();
                model.Branches = branchRepository.GetAllBranches().ToList();
                model.TaskStatusList = tasksRepository.GetAllTaskStatus().ToList();
                model.TaskStatusId = task.TaskStatusId;
                model.TaskDate = task.TaskDate;
                model.TaskStartTime = task.TaskStartTime;
                model.TaskEndTime = task.TaskEndTime;
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
                model.ContinuousTask = task.ContinuousTask;
                if (task.ContinuousTask) {
                    var continuousTask = tasksRepository.GetContinuousTaskByTaskId(Id);
                    model.ContinueFromDate = continuousTask.ContinueFromDate;
                    model.ContinueToDate = continuousTask.ContinueToDate;
                    model.PeriodId = continuousTask.PeriodId;
                    model.PeriodTypes = context.PeriodTypes.ToList();
                };
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
                ContinuousTask continousTask = null;
                if (task.ContinuousTask)
                {
                    continousTask = tasksRepository.GetContinuousTaskByTaskId(tasksChanges.Id);
                    continousTask.ContinueFromDate = tasksChanges.ContinueFromDate;
                    continousTask.ContinueToDate = tasksChanges.ContinueToDate;
                    continousTask.PeriodId = tasksChanges.PeriodId;
                    if (!tasksChanges.ContinuousTask)
                    {
                        tasksChanges.TaskStatusId = 8;
                    }
               }
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
                task.TaskStartTime = tasksChanges.TaskStartTime;
                task.TaskEndTime = tasksChanges.TaskEndTime;
                task.Time_Stamp = tasksChanges.Time_Stamp;
                task.UserName = tasksChanges.UserName;
                task = tasksRepository.Update(task);
                if(continousTask != null)
                {
                    continousTask = tasksRepository.UpdateContinuousTask(continousTask);
                }
                return RedirectToAction("ListTasks","Tasks");
            }
        }
        private int DateDiff(DateTime fromDate, DateTime toDate)
        {
            DateTime myDate = fromDate;
            int Days = 0;
            while (myDate<toDate)
            {
                Days += 1;
                myDate= myDate.AddDays(1);

            }
            return Days;
        }
        private bool CreateTasks(List<SQLActiveContinuousTasks> ActiveContinuousTask)
        {
            ContinuousTask contTask = null;
            Tasks newTask = null;
            foreach(var task in ActiveContinuousTask)
            {
                int days=DateDiff(task.TaskStartDate,task.TaskEndDate);
                //if (days == 0)
                //{
                //    days = 1;
                //}
                contTask = tasksRepository.GetContinuousTaskByTaskId(task.TaskId);
                newTask = new Tasks();
                newTask.BranchId = task.BranchId;
                newTask.Description = task.Description;
                newTask.OtherInformation = task.OtherInformation;
                newTask.ProjectId = task.ProjectId;
                newTask.TaskDate = DateTime.Today;
                newTask.TaskEndDate = task.NextTaskDate.AddDays(days);
                newTask.TaskEndTime = task.TaskEndTime;
                newTask.TaskOwnerId = task.TaskOwnerId;
                newTask.TaskResponsibleId = task.TaskResponsibleId;
                newTask.TaskStartDate = task.NextTaskDate;
                newTask.TaskStartTime = task.TaskStartTime;
                newTask.TaskStatusId = 1;//waiting
                newTask.TaskSubject = task.TaskSubject;
                newTask.Time_Stamp = DateTime.Now;
                newTask.UserName = User.Identity.Name;
                tasksRepository.Add(newTask);
                if (task.PeriodId == 1)//daily
                {
                    contTask.NextTaskDate = contTask.NextTaskDate.AddDays(1);
                }else if (task.PeriodId == 2)//weekly
                {
                    contTask.NextTaskDate = contTask.NextTaskDate.AddDays(7);
                }else if (task.PeriodId == 3)//monthly
                {
                    contTask.NextTaskDate = contTask.NextTaskDate.AddMonths(1);
                }
                tasksRepository.UpdateContinuousTask(contTask);
            }
            return true;
        }


        [HttpGet]
        public async Task <IActionResult> ListTasks()
        {
            bool hasCriteria = true;
            SearchTaskViewModel criteria = null;

            if (HttpContext.Session.GetString("searchCriteria") == null)
            {
                hasCriteria = false;
            }else
            {
                criteria = JsonConvert.DeserializeObject<SearchTaskViewModel>(HttpContext.Session.GetString("searchCriteria"));
            }
            

            TaskDetailsListViewModel model = new TaskDetailsListViewModel();
            var ActiveContinuousTask = tasksRepository.getActiveContinuousTask();
            CreateTasks(ActiveContinuousTask);
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                if (hasCriteria)
                {
                    model.TaskDetails = tasksRepository.getAllTaskDetails(criteria,true).ToList();
                    model.CountByStatus = tasksRepository.getTaskCountByStatus(model.TaskDetails);
                    model.criteria = retCriteria(criteria);
                }
                else
                {
                    model.criteria = retCriteria(criteria);
                    model.TaskDetails = tasksRepository.getAllTaskDetails(model.criteria,true).ToList();
                    model.CountByStatus = tasksRepository.getTaskCountByStatus(model.TaskDetails);
                }
                //return PartialView("ListTasks", model);
                return View(model);
                 
            }
            else
            {
                model.criteria = retCriteria(criteria);
                model.TaskDetails = tasksRepository.getEmployeeTaskDetails(model.criteria, user.EmployeeId, "").ToList();
                //model.TaskDetails = tasksRepository.getEmployeeTaskDetails(user.EmployeeId,"").ToList();
                model.CountByStatus = tasksRepository.getTaskCountByStatus(user.EmployeeId);
                return View(model);
            }

            ////var model = tasksRepository.GetAllTasks().ToList();
            //var model = tasksRepository.getAllTaskDetails().ToList();
            //return View( model);
        }
        [HttpGet]
        public async Task<IActionResult> TaskReport()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                var model = tasksRepository.getAllTaskDetails().ToList();
                return View(model);
            }
            else
            {
                var model = tasksRepository.getEmployeeTaskDetails(user.EmployeeId,"").ToList();
                return View(model);
            }

            ////var model = tasksRepository.GetAllTasks().ToList();
            //var model = tasksRepository.getAllTaskDetails().ToList();
            //return View( model);
        }
        [HttpGet]
        public async Task<IActionResult> ListTasksByProject()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                var model = tasksRepository.getAllTaskDetails().ToList();
                return View(model);
            }
            else
            {
                var model = tasksRepository.getEmployeeTaskDetails(user.EmployeeId,"").ToList();
                return View(model);
            }

            ////var model = tasksRepository.GetAllTasks().ToList();
            //var model = tasksRepository.getAllTaskDetails().ToList();
            //return View( model);
        }
        [HttpGet]
        public async Task<IActionResult> ListTasksByTaskOwner()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                var model = tasksRepository.getAllTaskDetails().ToList();
                return View(model);
            }
            else
            {
                var model = tasksRepository.getEmployeeTaskDetails(user.EmployeeId,"").ToList();
                return View(model);
            }

            ////var model = tasksRepository.GetAllTasks().ToList();
            //var model = tasksRepository.getAllTaskDetails().ToList();
            //return View( model);
        }
        [HttpGet]
        public async Task<IActionResult> ListTasksByResponsible()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                var model = tasksRepository.getAllTaskDetails().ToList();
                return View(model);
            }
            else
            {
                var model = tasksRepository.getEmployeeTaskDetails(user.EmployeeId,"").ToList();
                return View(model);
            }

            ////var model = tasksRepository.GetAllTasks().ToList();
            //var model = tasksRepository.getAllTaskDetails().ToList();
            //return View( model);
        }
        [HttpGet]
        public async Task<IActionResult> ListTasksByStatus()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                var model = tasksRepository.getAllTaskDetails().ToList();
                return View(model);
            }
            else
            {
                var model = tasksRepository.getEmployeeTaskDetails(user.EmployeeId,"").ToList();
                return View(model);
            }

            ////var model = tasksRepository.GetAllTasks().ToList();
            //var model = tasksRepository.getAllTaskDetails().ToList();
            //return View( model);
        }

        [HttpGet]
        public IActionResult ListTasksAjax()
        {
            //var model = tasksRepository.GetAllTasks().ToList();
            var model = tasksRepository.getAllTaskDetails().ToList();
            return new JsonResult(model);
        }
        [HttpGet]
        public async Task <IActionResult> ListTasksPartial(int TaskOwnerId, int TaskResponsibleId, int ProjectId, int TaskStatusId, int BranchId, DateTime FromTaskDate, DateTime ToTaskDate, string strGroupBy)
        {
            MSIS.ViewModels.SearchTaskViewModel criteria = new SearchTaskViewModel();
            criteria.ProjectId = ProjectId;
            criteria.TaskOwnerId = TaskOwnerId;
            criteria.TaskResponsibleId = TaskResponsibleId;
            criteria.FromTaskDate = FromTaskDate;
            criteria.ToTaskDate = ToTaskDate;
            criteria.strGroupBy = strGroupBy;
                        criteria.BranchId = BranchId;

            TaskDetailsListViewModel model = new TaskDetailsListViewModel();

            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                model.TaskDetails = tasksRepository.getAllTaskDetails(criteria,true).ToList();
                model.CountByStatus = await getTaskStatusCount(TaskOwnerId, TaskResponsibleId, ProjectId,BranchId, FromTaskDate, ToTaskDate, strGroupBy);
                return PartialView("_ListTasks", model);
            }
            else
            {
                model.TaskDetails = tasksRepository.getEmployeeTaskDetails(user.EmployeeId,"").ToList();
                model.CountByStatus = await getTaskStatusCount(TaskOwnerId, TaskResponsibleId, ProjectId, BranchId, FromTaskDate, ToTaskDate, strGroupBy);
                return PartialView("_ListTasks", model);
            }

            //var model = tasksRepository.GetAllTasks().ToList();
        }
        [HttpGet]
        public async Task <IActionResult> ListWaitingTasks(int TaskOwnerId, int TaskResponsibleId, int ProjectId, DateTime FromTaskDate, DateTime ToTaskDate, int BranchId, bool ContinuousTask, string strGroupBy)
        {
                                                                                                       
            MSIS.ViewModels.SearchTaskViewModel criteria = new SearchTaskViewModel();
            criteria.ProjectId = ProjectId;
            criteria.TaskOwnerId = TaskOwnerId;
            criteria.TaskResponsibleId = TaskResponsibleId;
            criteria.FromTaskDate = FromTaskDate;
            criteria.ToTaskDate = ToTaskDate;
            criteria.strGroupBy = strGroupBy;
            criteria.ContinuousTask = ContinuousTask;
            criteria.BranchId = BranchId;
            TaskDetailsListViewModel model = new TaskDetailsListViewModel();

            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                model.TaskDetails = tasksRepository.getTaskDetailsByStatusId(1,criteria).ToList();
                model.CountByStatus = await getTaskStatusCount(TaskOwnerId, TaskResponsibleId, ProjectId, BranchId, FromTaskDate, ToTaskDate, strGroupBy);
                return PartialView("_ListTasks", model);
            }
            else
            {
                model.TaskDetails = tasksRepository.getTaskDetailsByStatusId( user.EmployeeId, 1,criteria).ToList();
                model.CountByStatus = await getTaskStatusCount(TaskOwnerId, TaskResponsibleId, ProjectId, BranchId, FromTaskDate, ToTaskDate, strGroupBy);
                return PartialView("_ListTasks", model);
            }
            ////var model = tasksRepository.GetAllTasks().ToList();
            //var model = tasksRepository.getTaskDetailsByStatusId(1).ToList();
            //return PartialView("_ListTasks", model);
        }
        [HttpGet]
        public async Task <IActionResult> ListInProgressTasks(int TaskOwnerId, int TaskResponsibleId, int ProjectId, DateTime FromTaskDate, DateTime ToTaskDate, int BranchId, bool ContinuousTask, string strGroupBy)
        {
            MSIS.ViewModels.SearchTaskViewModel criteria = new SearchTaskViewModel();
            criteria.ProjectId = ProjectId;
            criteria.TaskOwnerId = TaskOwnerId;
            criteria.TaskResponsibleId = TaskResponsibleId;
            criteria.FromTaskDate = FromTaskDate;
            criteria.ToTaskDate = ToTaskDate;
            criteria.ContinuousTask = ContinuousTask;
            criteria.BranchId = BranchId;
            criteria.strGroupBy = strGroupBy;
            TaskDetailsListViewModel model = new TaskDetailsListViewModel();
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                model.TaskDetails = tasksRepository.getTaskDetailsByStatusId(2,criteria).ToList();
                model.CountByStatus = await getTaskStatusCount(TaskOwnerId, TaskResponsibleId, ProjectId, BranchId, FromTaskDate, ToTaskDate, strGroupBy);
                return PartialView("_ListTasks", model);
            }
            else
            {
                model.TaskDetails = tasksRepository.getTaskDetailsByStatusId(user.EmployeeId, 2, criteria).ToList();
                model.CountByStatus = await getTaskStatusCount(TaskOwnerId, TaskResponsibleId, ProjectId, BranchId, FromTaskDate, ToTaskDate, strGroupBy);
                return PartialView("_ListTasks", model);
            }
            ////var model = tasksRepository.GetAllTasks().ToList();
            //var model = tasksRepository.getTaskDetailsByStatusId(2).ToList();
            //return PartialView("_ListTasks", model);
        }
        [HttpGet]
        public async Task <IActionResult> ListRejectedTasks(int TaskOwnerId, int TaskResponsibleId, int ProjectId, DateTime FromTaskDate, DateTime ToTaskDate, int BranchId, bool ContinuousTask, string strGroupBy)
        {
            MSIS.ViewModels.SearchTaskViewModel criteria = new SearchTaskViewModel();
            criteria.ProjectId = ProjectId;
            criteria.TaskOwnerId = TaskOwnerId;
            criteria.TaskResponsibleId = TaskResponsibleId;
            criteria.FromTaskDate = FromTaskDate;
            criteria.ToTaskDate = ToTaskDate;
            criteria.strGroupBy = strGroupBy;
            criteria.BranchId = BranchId;
            criteria.ContinuousTask = ContinuousTask;
            TaskDetailsListViewModel model = new TaskDetailsListViewModel();
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                model.TaskDetails = tasksRepository.getTaskDetailsByStatusId(3,criteria).ToList();
                model.CountByStatus = await getTaskStatusCount(TaskOwnerId, TaskResponsibleId, ProjectId, BranchId, FromTaskDate, ToTaskDate, strGroupBy);
                return PartialView("_ListTasks", model);
            }
            else
            {
                model.TaskDetails = tasksRepository.getTaskDetailsByStatusId(user.EmployeeId, 3, criteria).ToList();
                model.CountByStatus = await getTaskStatusCount(TaskOwnerId, TaskResponsibleId, ProjectId, BranchId, FromTaskDate, ToTaskDate, strGroupBy);
                return PartialView("_ListTasks", model);
            }
            ////var model = tasksRepository.GetAllTasks().ToList();
            //var model = tasksRepository.getTaskDetailsByStatusId(3).ToList();
            //return PartialView("_ListTasks", model);
        }
        [HttpGet]
        public async Task <IActionResult> ListDoneTasks(int TaskOwnerId, int TaskResponsibleId, int ProjectId, DateTime FromTaskDate, DateTime ToTaskDate, int BranchId, bool ContinuousTask, string strGroupBy)
        {
            MSIS.ViewModels.SearchTaskViewModel criteria = new SearchTaskViewModel();
            criteria.ProjectId = ProjectId;
            criteria.TaskOwnerId = TaskOwnerId;
            criteria.TaskResponsibleId = TaskResponsibleId;
            criteria.FromTaskDate = FromTaskDate;
            criteria.ToTaskDate = ToTaskDate;
            criteria.strGroupBy = strGroupBy;
            criteria.ContinuousTask = ContinuousTask;
            criteria.BranchId = BranchId;
            TaskDetailsListViewModel model = new TaskDetailsListViewModel();
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                model.TaskDetails = tasksRepository.getTaskDetailsByStatusId(4,criteria).ToList();
                model.CountByStatus = await getTaskStatusCount(TaskOwnerId, TaskResponsibleId, ProjectId, BranchId, FromTaskDate, ToTaskDate, strGroupBy);
                return PartialView("_ListTasks", model);
            }
            else
            {
                model.TaskDetails = tasksRepository.getTaskDetailsByStatusId(user.EmployeeId, 4, criteria).ToList();
                model.CountByStatus = await getTaskStatusCount(TaskOwnerId, TaskResponsibleId, ProjectId, BranchId, FromTaskDate, ToTaskDate, strGroupBy);
                return PartialView("_ListTasks", model);
            }
            ////var model = tasksRepository.GetAllTasks().ToList();
            //var model = tasksRepository.getTaskDetailsByStatusId(4).ToList();
            //return PartialView("_ListTasks", model);
        }
        [HttpGet]
        public async Task <IActionResult> ListApprovedTasks(int TaskOwnerId, int TaskResponsibleId, int ProjectId, DateTime FromTaskDate, DateTime ToTaskDate, int BranchId, bool ContinuousTask, string strGroupBy)
        {
            MSIS.ViewModels.SearchTaskViewModel criteria = new SearchTaskViewModel();
            criteria.ProjectId = ProjectId;
            criteria.TaskOwnerId = TaskOwnerId;
            criteria.TaskResponsibleId = TaskResponsibleId;
            criteria.FromTaskDate = FromTaskDate;
            criteria.ToTaskDate = ToTaskDate;
            criteria.strGroupBy = strGroupBy;
            criteria.BranchId = BranchId;
            criteria.ContinuousTask = ContinuousTask;
            TaskDetailsListViewModel model = new TaskDetailsListViewModel();
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                model.TaskDetails = tasksRepository.getTaskDetailsByStatusId(5,criteria).ToList();
                model.CountByStatus = await getTaskStatusCount(TaskOwnerId, TaskResponsibleId, ProjectId, BranchId, FromTaskDate, ToTaskDate, strGroupBy);
                return PartialView("_ListTasks", model);
            }
            else
            {
                model.TaskDetails = tasksRepository.getTaskDetailsByStatusId(user.EmployeeId, 5, criteria).ToList();
                model.CountByStatus = await getTaskStatusCount(TaskOwnerId, TaskResponsibleId, ProjectId, BranchId, FromTaskDate, ToTaskDate, strGroupBy);
                return PartialView("_ListTasks", model);
            }


            //var model = tasksRepository.GetAllTasks().ToList();
        }
        [HttpGet]
        public async Task <IActionResult> TaskCountByStatus()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                return new JsonResult(tasksRepository.getTaskCountByStatus());
            }
            else
            {
                return new JsonResult(tasksRepository.getTaskCountByStatus(user.EmployeeId));
            }

            
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
        [HttpGet]
        public IActionResult PrintTaskDetails(int Id)
        {
            var model = tasksRepository.getTaskDetails(Id);
            return View(model);
        }

        [HttpPost]
        public IActionResult AddComment(int TaskId ,string Description,int TaskStatusId)
        {
            //if (ModelState.IsValid)
            //{
            TaskAction model = new TaskAction();
            model.TaskStatusId = TaskStatusId;
            model.Description = Description;
            model.TaskId = TaskId;
            model.UserName = User.Identity.Name;
            model.CurrentTaskStatusId = TaskStatusId;
            if(tasksRepository.AddTaskComment(model))
            {
                var TaskActionList = tasksRepository.getTaskActionList(TaskId);
                return PartialView("_TaskActionList", TaskActionList);
            }
            return new JsonResult ("true");
            //}
            //else
            //{
            //    return View(false);
            //}
        }


        [HttpPost]
        public IActionResult StartTask(EditTaskStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.TaskStatusID =2;
                model.UserName = User.Identity.Name;
                model.CurrentTaskStatusId = 1;
                tasksRepository.EditTaskStatus(model);
                if (SettingsRepository.SendMailOnCreateTask())
                {
                    var task = tasksRepository.getTaskDetails(model.Id);
                    string link = UriHelper.GetDisplayUrl(Request).Replace("StartTask", "") + "Details/" + task.Id.ToString();
                    //string strMyUrl = Context.Request.Scheme + Request.Host.Value +  Url.Action("Details", "Tasks", new { Id = addedTask.Id }).ToString();
                    //Uri myUri = new Uri("https://localhost:44309/Tasks/Details/" + );
                    string strMessage = "task " + link.ToString() + " is Started";
                    List<int> Employees = new List<int>();
                    Employees.Add(task.TaskOwnerId);
                    if (task.TaskResponsibleId != task.TaskOwnerId)
                    {
                        Employees.Add(task.TaskResponsibleId);
                    }
                    SettingsRepository.SendEmail(Employees, strMessage);
                }
                return RedirectToAction("Details", "Tasks", new { Id=model.Id});
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
                model.CurrentTaskStatusId =2;
                tasksRepository.EditTaskStatus(model);
                if (SettingsRepository.SendMailOnCreateTask())
                {
                    var task = tasksRepository.getTaskDetails(model.Id);
                    string link = UriHelper.GetDisplayUrl(Request).Replace("CompleteTask", "") + "Details/" + task.Id.ToString();
                    //string strMyUrl = Context.Request.Scheme + Request.Host.Value +  Url.Action("Details", "Tasks", new { Id = addedTask.Id }).ToString();
                    //Uri myUri = new Uri("https://localhost:44309/Tasks/Details/" + );
                    string strMessage = "task " + link.ToString() + " is Done";
                    List<int> Employees = new List<int>();
                    Employees.Add(task.TaskOwnerId);
                    if (task.TaskResponsibleId != task.TaskOwnerId)
                    {
                        Employees.Add(task.TaskResponsibleId);
                    }
                    SettingsRepository.SendEmail(Employees, strMessage);
                }
                return RedirectToAction("Details", "Tasks",new { Id=model.Id});
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

                }else if (model.TaskOperation == "2")
                {
                    model.TaskStatusID = 2;
                }
                    model.UserName = User.Identity.Name;
                model.CurrentTaskStatusId = 4;
                tasksRepository.EditTaskStatus(model);
                if (SettingsRepository.SendMailOnCreateTask())
                {
                    var task = tasksRepository.getTaskDetails(model.Id);
                    string link = UriHelper.GetDisplayUrl(Request).Replace("VerifyTaskExecution", "") + "Details/" + task.Id.ToString();
                    //string strMyUrl = Context.Request.Scheme + Request.Host.Value +  Url.Action("Details", "Tasks", new { Id = addedTask.Id }).ToString();
                    //Uri myUri = new Uri("https://localhost:44309/Tasks/Details/" + );
                    string strOperation;
                    if (model.TaskOperation == "0")
                    {
                        strOperation = " Approved";
                    }
                    else {
                        strOperation = " Rejected";
                    }                    
                    string strMessage = "task " + link.ToString() + strOperation;
                    List<int> Employees = new List<int>();
                    Employees.Add(task.TaskOwnerId);
                    if (task.TaskResponsibleId != task.TaskOwnerId)
                    {
                        Employees.Add(task.TaskResponsibleId);
                    }
                    SettingsRepository.SendEmail(Employees, strMessage);
                }
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
        //    return View(model);
        }

        private List<Project> retProjectsList()
        {
            AppDBContext context = tasksRepository.getContext();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = userManager.Users.ToList().Where(x => x.Id == userId).FirstOrDefault();
            List<Project> ProjectList=null;
            if (user.Category.ToLower() == "0")//employee
            {
                ProjectList = context.Projects.ToList();
                ProjectList.Insert(0, new Project
                {
                    Id = -1,
                    ProjectName = "Select Project"
                });
                return ProjectList;
            }
            else
            {
                var result = (from project in context.Projects
                              join userProjects in context.UserProjects
                              on project.Id equals userProjects.ProjectId
                              where userProjects.UserId == userId
                              select new Project()
                              {
                                  Address = project.Address,
                                  Customer = project.Customer,
                                  Email = project.Email,
                                  EndDate = project.EndDate,
                                  Fax = project.Fax,
                                  Id = project.Id,
                                  MobileNo = project.MobileNo,
                                  OtheInformation = project.OtheInformation,
                                  ProjectName = project.ProjectName,
                                  ProjectOwner = project.ProjectOwner,
                                  ProjectSerial = project.ProjectSerial,
                                  ProjectYear = project.ProjectYear,
                                  StartDate = project.StartDate
                              }).ToList();
                return result;
            }

        }
        private List<Branch> retBranchesList()
        {
            AppDBContext context = tasksRepository.getContext();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = userManager.Users.ToList().Where(x => x.Id == userId).FirstOrDefault();
            List<Branch> BranchList=null;
            if (user.Category.ToLower() == "0")
            {
                BranchList = context.Branches.ToList();
                BranchList.Insert(0, new Branch
                {
                    Id = -1,
                    Name = "Select Branch"
                });
                return BranchList;
            }
            else
            {
                var result = (from branch in context.Branches
                              join userBranches in context.UserBranches
                              on branch.Id equals userBranches.BranchId
                              where userBranches.UserId == userId
                              select new Branch()
                              {
                                  Address = branch.Address,
                                  Code=branch.Code,
                                  Id=branch.Id,
                                  Mobile=branch.Mobile,
                                  Name=branch.Name,
                                  Phone=branch.Phone
                              }).ToList();
                return result;
            }

        }
        private List<Employee> retEmployeesList()
        {
            AppDBContext context = tasksRepository.getContext();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = userManager.Users.ToList().Where(x => x.Id == userId).FirstOrDefault();
            List<Employee> employeeList = null;
            if (user.Category.ToLower() == "0")
            {
                employeeList = context.Employees.ToList();
                employeeList.Insert(0, new Employee
                {
                    Id = -1,
                    Name = "Select ...s"
                });
                return employeeList;
            }
            else
            {
                var result = (from employee in context.Employees
                              join userEmployees in context.UserEmployees
                              on employee.Id equals userEmployees.EmployeeId
                              where userEmployees.UserId == userId
                              select new Employee()
                              {
                                  Id=employee.Id,
                                  Name= employee.Name,
                                  Active= employee.Active,
                                  Address= employee.Address,
                                  Department= employee.Department,
                                  EducationDegree= employee.EducationDegree,
                                  Email= employee.Email,
                                  EmployeeNo= employee.EmployeeNo,
                                  IdentityNo= employee.IdentityNo,
                                  JobDescription= employee.JobDescription,
                                  MobileNo= employee.MobileNo,
                                  OtherInformation= employee.OtherInformation,
                                  PhoneNo= employee.PhoneNo,
                                  PhotoPath= employee.PhotoPath,
                                  Specialization= employee.Specialization,
                                  WorkMobileNo= employee.WorkMobileNo
                              }).ToList();
                return result;
            }

        }

        private SearchTaskViewModel retCriteria(SearchTaskViewModel criteria)
        {
            AppDBContext context = tasksRepository.getContext();
            MSIS.ViewModels.SearchTaskViewModel model = new ViewModels.SearchTaskViewModel();
            SQLCustomerRepository customerRepository = new SQLCustomerRepository(context);
            SQLProjectRepository projectRepository = new SQLProjectRepository(context);
            SQLEmployeeRepository employeeRepository = new SQLEmployeeRepository(context);
            SQLBranchRepository branchRepository = new SQLBranchRepository(context);
            if (criteria != null)
            {
                model.Projects = retProjectsList();
                model.Branches = retBranchesList();
                model.Employees = retEmployeesList();
                model.FromTaskDate = new DateTime(2019, 1, 1);
                model.ContinuousTask = criteria.ContinuousTask;
                if (criteria.ProjectId > 0)
                {
                    model.ProjectId = criteria.ProjectId;
                }
                if (criteria.TaskOwnerId > 0)
                {
                    model.TaskOwnerId = criteria.TaskOwnerId;
                }
                if (criteria.TaskResponsibleId > 0)
                {
                    model.TaskResponsibleId = criteria.TaskResponsibleId;
                }
                if (criteria.BranchId > 0)
                {
                    model.BranchId = criteria.BranchId;
                }
                model.TaskStatusId = criteria.TaskStatusId;
                if (criteria.strGroupBy == null)
                {
                    model.strGroupBy = "";
                }
                else
                {
                    model.strGroupBy = criteria.strGroupBy;
                }
                if (criteria.FromTaskDate.Year == 1)
                {
                    model.FromTaskDate = new DateTime(2019, 1, 1);
                }
                if (criteria.ToTaskDate.Year == 1)
                {
                    model.ToTaskDate = DateTime.Today;
                }
                model.ToTaskDate = DateTime.Today;
                return model;
            }
            else
            {
                if (criteria == null)
                {
                    criteria = new SearchTaskViewModel();
                }
                criteria.Projects = retProjectsList();
                criteria.Branches = retBranchesList();
                criteria.Employees = retEmployeesList();
                criteria.FromTaskDate = new DateTime(2019, 1, 1);
                if (criteria.Projects.Count > 0)
                {
                    criteria.ProjectId = criteria.Projects[0].Id;
                }
                if (criteria.Branches.Count > 0)
                {
                    criteria.BranchId = criteria.Branches[0].Id;
                }
                if (criteria.Employees.Count > 0)
                {
                    criteria.TaskOwnerId = criteria.Employees[0].Id;
                    criteria.TaskResponsibleId = criteria.Employees[0].Id;
                }
                criteria.FromTaskDate=new DateTime(2019, 1, 1);
                criteria.ToTaskDate = DateTime.Today;
                return criteria;
            }
        }

        [HttpGet]
        public IActionResult TaskSearch(SearchTaskViewModel criteria)
        {
            AppDBContext context = tasksRepository.getContext();
            MSIS.ViewModels.SearchTaskViewModel model = new ViewModels.SearchTaskViewModel();
            SQLCustomerRepository customerRepository = new SQLCustomerRepository(context);
            SQLProjectRepository projectRepository = new SQLProjectRepository(context);
            SQLEmployeeRepository employeeRepository = new SQLEmployeeRepository(context);
            SQLBranchRepository branchRepository = new SQLBranchRepository(context);
            
            model.ContinuousTask = criteria.ContinuousTask;
            if (criteria.ProjectId > 0)
            {
              model.ProjectId= criteria.ProjectId;
            }
            if (criteria.TaskOwnerId > 0)
            {
                model.TaskOwnerId = criteria.TaskOwnerId;
            }
            if (criteria.TaskResponsibleId > 0)
            {
                model.TaskResponsibleId = criteria.TaskResponsibleId;
            }
            if (criteria.BranchId > 0)
            {
                model.BranchId = criteria.BranchId;
            }
            model.TaskStatusId = criteria.TaskStatusId;
            model.Projects = projectRepository.GetAllProjects().ToList();
            model.Projects.Insert(0, new Project
            {
                Id = -1,
                ProjectName = "Select Project"
            });
            model.Branches = branchRepository.GetAllBranches().ToList();
            model.Branches.Insert(0, new Branch
            {
                Id = -1,
                Name = "Select Branch"
            });
            model.Employees = employeeRepository.GetAllEmployees().ToList();
            model.Employees.Insert(0, new Employee
            {
                Id = -1,
                Name = "Select ..."
            });
            if (criteria.FromTaskDate.Year == 1)
            {
                model.FromTaskDate = new DateTime(2019, 1, 1);
            }
            if (criteria.ToTaskDate.Year == 1)
            {
                model.ToTaskDate = DateTime.Today;
            }

            if (criteria.strGroupBy == null)
            {
                model.strGroupBy= "";
            }
            else
            {
                model.strGroupBy =criteria.strGroupBy;
            }

            return PartialView("_TaskSearch", model);

        }
        [HttpGet]
        public IActionResult TaskReportsSearch(string strGroupBy)
        {
            AppDBContext context = tasksRepository.getContext();
            MSIS.ViewModels.SearchTaskReportsViewModel model = new ViewModels.SearchTaskReportsViewModel();
            SQLCustomerRepository customerRepository = new SQLCustomerRepository(context);
            SQLProjectRepository projectRepository = new SQLProjectRepository(context);
            SQLEmployeeRepository employeeRepository = new SQLEmployeeRepository(context);
            SQLBranchRepository branchRepository = new SQLBranchRepository(context);
            model.Projects = projectRepository.GetAllProjects().ToList();
            model.Branches = branchRepository.GetAllBranches().ToList();
            model.Branches.Insert(0, new Branch
            {
                Id = -1,
                Name = "Select Branch..."
            });

            model.TaskStatsus = tasksRepository.GetAllTaskStatus().ToList();
            model.TaskStatsus.Insert(0, new Models.TaskStatus
            {
                Id = -1,
                StatusName = "Select Status..."
            });

            model.Projects.Insert(0, new Project
            {
                Id = -1,
                ProjectName = "Select Project..."
            });
            model.Employees = employeeRepository.GetAllEmployees().ToList();
            model.Employees.Insert(0, new Employee
            {
                Id = -1,
                Name = "Select ..."
            });

            model.FromTaskDate = new DateTime(2019, 1, 1);
            model.ToTaskDate = DateTime.Today;
            if (strGroupBy == null)
            {
                model.strGroupBy = "";
            }
            else
            {
                model.strGroupBy = strGroupBy;
            }

            return PartialView("_TaskReportsSearch", model);

        }
       
        private async Task<TaskCountByStatusViewModel> getTaskStatusCount(int TaskOwnerId,int TaskResponsibleId, int ProjectId,int BranchId, DateTime FromTaskDate, DateTime ToTaskDate, string strGroupBy)
        {
            MSIS.ViewModels.SearchTaskViewModel criteria = new SearchTaskViewModel();
            criteria.ProjectId = ProjectId;
            criteria.TaskOwnerId = TaskOwnerId;
            criteria.TaskResponsibleId = TaskResponsibleId;
            criteria.FromTaskDate = FromTaskDate;
            criteria.ToTaskDate = ToTaskDate;
            criteria.BranchId = BranchId;
            criteria.strGroupBy = strGroupBy;
            TaskDetailsListViewModel model = new TaskDetailsListViewModel();
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                model.TaskDetails = tasksRepository.getAllTaskDetails(criteria,false).ToList();
                model.CountByStatus = tasksRepository.getTaskCountByStatus(model.TaskDetails);
                return model.CountByStatus;
            }
            else
            {
                model.TaskDetails = tasksRepository.getEmployeeTaskDetails(criteria, user.EmployeeId, strGroupBy).ToList();
                model.CountByStatus = tasksRepository.getTaskCountByStatus(model.TaskDetails);
                return model.CountByStatus;
            }
        }
        [HttpPost]
        public async Task<IActionResult> TaskSearchAsync(int TaskOwnerId,int TaskResponsibleId, int ProjectId, DateTime FromTaskDate, DateTime ToTaskDate, int BranchId, bool ContinuousTask, string strGroupBy)
        {
            MSIS.ViewModels.SearchTaskViewModel criteria = new SearchTaskViewModel();
            criteria.ProjectId = ProjectId;
            criteria.BranchId = BranchId;
            criteria.TaskOwnerId = TaskOwnerId;
            criteria.TaskResponsibleId = TaskResponsibleId;
            criteria.FromTaskDate = FromTaskDate;
            criteria.ToTaskDate = ToTaskDate;
            criteria.ContinuousTask = ContinuousTask;
            criteria.strGroupBy = strGroupBy;

            HttpContext.Session.SetString("searchCriteria",JsonConvert.SerializeObject( criteria)); 

            TaskDetailsListViewModel model = new TaskDetailsListViewModel();
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                model.TaskDetails = tasksRepository.getAllTaskDetails(criteria,true).ToList();
                model.CountByStatus = model.CountByStatus = await getTaskStatusCount(TaskOwnerId, TaskResponsibleId, ProjectId, BranchId, FromTaskDate, ToTaskDate, strGroupBy);
                return PartialView("_ListTasks", model);
            }
            else
            {
                model.TaskDetails = tasksRepository.getEmployeeTaskDetails(criteria, user.EmployeeId, strGroupBy).ToList();
                model.CountByStatus = tasksRepository.getTaskCountByStatus(model.TaskDetails);
                return PartialView("_ListTasks",  model);
            }

        }

        [HttpPost]
        public async Task<IActionResult> TaskReportsSearchAsync(int TaskOwnerId, int TaskResponsibleId, int ProjectId, int TaskStatusId, int BranchId, DateTime FromTaskDate, DateTime ToTaskDate, string strGroupBy)
        {
            MSIS.ViewModels.SearchTaskReportsViewModel criteria = new SearchTaskReportsViewModel();
            criteria.ProjectId = ProjectId;
            criteria.TaskOwnerId = TaskOwnerId;
            criteria.TaskResponsibleId = TaskResponsibleId;
            criteria.FromTaskDate = FromTaskDate;
            criteria.ToTaskDate = ToTaskDate;
            criteria.BranchId = BranchId;
            criteria.TaskStatusId = TaskStatusId;
            criteria.strGroupBy = strGroupBy;
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                var model = tasksRepository.getAllTaskDetails(criteria).ToList();
                return PartialView("_ListTasksReport", model);
            }
            else
            {
                var model = tasksRepository.getEmployeeTaskDetails(user.EmployeeId,criteria).ToList();
                return PartialView("_ListTasksReport", model);
            }

        }

        [HttpPost]
        public async Task<IActionResult> DeleteTaskAsync(int Id)
        {
            //PurchaseOrderDetails purchaseOrder = purchaseOrderRepository.DeletePurchaseOrderItem(Id);
            Tasks purchase = tasksRepository.Delete(Id);
            if (purchase == null)
            {
                return Redirect("NotFound");
            }
            else
            {
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                if (await userManager.IsInRoleAsync(user, "Admin"))
                {
                    var model = tasksRepository.getAllTaskDetails().ToList();
                    return RedirectToAction("ListTasks", model);
                }
                else
                {
                    var model = tasksRepository.getEmployeeTaskDetails(user.EmployeeId, "").ToList();
                    return RedirectToAction("ListTasks", model);
                }

            }
        }

    }
}