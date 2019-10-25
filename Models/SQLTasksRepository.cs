using MSIS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSIS.Models
{
    public class SQLTasksRepository
    {
        private readonly AppDBContext context;

        public SQLTasksRepository(AppDBContext context)
        {
            this.context = context;
        }
        public AppDBContext getContext()
        {
            return this.context;
        }
        public Tasks Add(Tasks task)
        {
            context.Tasks.Add(task);
            context.SaveChanges();
            return task;
        }

        public Tasks Delete(int id)
        {
            Tasks task = context.Tasks.Find(id);
            if (task != null)
            {
                context.Tasks.Remove(task);
                context.SaveChanges();
            }
            return task;
        }

        public IEnumerable<Tasks> GetAllTasks()
        {
            return context.Tasks;
        }

        public Tasks GetTasks(int Id)
        {
            return context.Tasks.Find(Id);
        }
        public bool EditTaskStatus(ViewModels.EditTaskStatusViewModel model)
        {
            var task = GetTasks(model.Id);
            if (task != null)
            {
                try
                {
                    context.Database.BeginTransaction();
                    task.TaskStatusId = model.TaskStatusID;
                    task.TaskResultDescription = model.Description;
                    Update(task);

                    context.TaskActions.Add(new TaskAction()
                    {
                        TaskId = model.Id,
                        ActionDate = DateTime.Today,
                        Description = model.Description,
                        TimeStamp=DateTime.Now,
                        UserName = model.UserName
                    });
                    context.SaveChanges();
                    context.Database.CommitTransaction();
                    return true;
                }
                catch (Exception ex)
                {
                    context.Database.RollbackTransaction();
                }

            }
            return false;

        }
        public Tasks Update(Tasks TasksChanges)
        {
            var task = context.Tasks.Attach(TasksChanges);
            task.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return TasksChanges;
        }

        public IEnumerable<TaskStatus> GetAllTaskStatus()
        {
            return context.TaskStatus;
        }

        public ViewModels.TaskCountByStatusViewModel getTaskCountByStatus() {
            TaskCountByStatusViewModel result = new TaskCountByStatusViewModel();
           if (context.Tasks != null)
            {
                var model = context.Tasks
                       .Where(x => x.TaskStatusId == 5)
                       .GroupBy(x => new { TaskStatusId= x.TaskStatusId })
                       .Select(x => new {  TaskCount = x.Count() }).FirstOrDefault();                    
                if (model != null) { 
                    result.ApprovedCount = model.TaskCount;
                }

                model = context.Tasks
                       .Where(x => x.TaskStatusId == 2)
                       .GroupBy(x => new { TaskStatusId = x.TaskStatusId })
                       .Select(x => new { TaskCount = x.Count() }).FirstOrDefault();
                if (model != null)
                {
                    result.InProgressCount = model.TaskCount;
                }
                model = context.Tasks
                    .Where(x => x.TaskStatusId == 3)
                    .GroupBy(x => new { TaskStatusId = x.TaskStatusId })
                    .Select(x => new { TaskCount = x.Count() }).FirstOrDefault();
                result.RejectedCount = model.TaskCount;

                model = context.Tasks
                      .Where(x => x.TaskStatusId == 4)
                      .GroupBy(x => new { TaskStatusId = x.TaskStatusId })
                      .Select(x => new { TaskCount = x.Count() }).FirstOrDefault();
                
                if (model != null)
                {
                result.DoneCount = model.TaskCount;
                 }
                   model = context.Tasks
                      .Where(x => x.TaskStatusId == 1)
                      .GroupBy(x => new { TaskStatusId = x.TaskStatusId })
                      .Select(x => new { TaskCount = x.Count() }).FirstOrDefault();
                if (model != null)
                {
                    result.WaitingCount = model.TaskCount;
                }
            }
            return result;
        }

        public List<MSIS.ViewModels.TaskDetailsViewModel> getTaskDetailsByStatusId(int status)
        {
            MSIS.ViewModels.TaskDetailsViewModel taskDetailViewModel = null;
            var result = (from task in context.Tasks
                          join employee in context.Employees
                          on task.TaskResponsibleId equals employee.Id
                          join taskStatus in context.TaskStatus
                          on task.TaskStatusId equals taskStatus.Id
                          join project in context.Projects
                          on task.ProjectId equals project.Id
                          join branch in context.Branches
                          on task.BranchId equals branch.Id
                          where task.TaskStatusId==status
                          select new MSIS.ViewModels.TaskDetailsViewModel()
                          {
                              Id = task.Id,
                              Description = task.Description,
                              TaskDate = task.TaskDate,
                              OtherInformation = task.OtherInformation,
                              TaskEndDate = task.TaskEndDate,
                              TaskResponsibleName = employee.Name,
                              TaskResultDescription = task.TaskResultDescription,
                              TaskStartDate = task.TaskStartDate,
                              TaskSubject = task.TaskSubject,
                              TaskStatus = taskStatus.StatusName,
                              ProjectName = project.ProjectName,
                              TaskStatusId = task.TaskStatusId,
                              BranchName = branch.Name


                          }).ToList();

            return result;// projectDetailViewModel;
        }

        public List<MSIS.ViewModels.TaskDetailsViewModel> getAllTaskDetails()
        {
            MSIS.ViewModels.TaskDetailsViewModel taskDetailViewModel = null;
            var result = (from task in context.Tasks
                          join employee in context.Employees
                          on task.TaskResponsibleId equals employee.Id 
                          join taskStatus in context.TaskStatus
                          on task.TaskStatusId equals taskStatus.Id
                          join project in context.Projects
                          on task.ProjectId equals project.Id
                          join branch in context.Branches
                          on task.BranchId equals branch.Id
                          select new MSIS.ViewModels.TaskDetailsViewModel()
                          {
                              Id = task.Id,
                              Description=task.Description,
                              TaskDate=task.TaskDate,
                              OtherInformation=task.OtherInformation,
                              TaskEndDate=task.TaskEndDate,
                              TaskResponsibleName=employee.Name,
                              TaskResultDescription=task.TaskResultDescription,
                              TaskStartDate=task.TaskStartDate,
                              TaskSubject=task.TaskSubject,
                              TaskStatus= taskStatus.StatusName,
                              ProjectName=project.ProjectName,
                              TaskStatusId=task.TaskStatusId,
                              BranchName=branch.Name
                             
                                    
                          }).ToList();
         
            return result;// projectDetailViewModel;
        }

        public MSIS.ViewModels.TaskDetailsViewModel getTaskDetails(int Id)
        {
            MSIS.ViewModels.TaskDetailsViewModel taskDetailViewModel = null;
            var taskTeam = GetAllTaskAssignedEmployees(Id).ToList();
            var result = (from task in context.Tasks
                          join employee in context.Employees
                          on task.TaskResponsibleId equals employee.Id
                          join taskStatus in context.TaskStatus
                          on task.TaskStatusId equals taskStatus.Id
                          join project in context.Projects
                          on task.ProjectId equals project.Id
                          join branch in context.Branches
                          on task.BranchId equals branch.Id
                          where task.Id == Id
                          select new MSIS.ViewModels.TaskDetailsViewModel()
                          {
                              Id = task.Id,
                              Description = task.Description,
                              TaskDate = task.TaskDate,
                              OtherInformation = task.OtherInformation,
                              TaskEndDate = task.TaskEndDate,
                              TaskResponsibleName = employee.Name,
                              TaskResultDescription = task.TaskResultDescription,
                              TaskStartDate = task.TaskStartDate,
                              TaskSubject = task.TaskSubject,
                              TaskStatus = taskStatus.StatusName,
                              ProjectName = project.ProjectName,
                              BranchName = branch.Name,
                              TaskStatusId=task.TaskStatusId,
                              TaskTeam=taskTeam
                          }).ToList();
            return result[0];// projectDetailViewModel;
        }



        public TaskTeam AssignEmployeeToTask(TaskTeam taskTeam)
        {
            context.TaskTeams.Add(taskTeam);
            context.SaveChanges();
            return taskTeam;
        }

        public TaskTeam ExcludeEmployeeFromTask(int id)
        {
            TaskTeam task = context.TaskTeams.Find(id);
            if (task != null)
            {
                context.TaskTeams.Remove(task);
                context.SaveChanges();
            }
            return task;
        }

        public IEnumerable<ViewModels.EmployeesInTaskViewModel> GetAllTaskAssignedEmployees(int TaskID)
        {
            var Result = (from taskTeam in context.TaskTeams
                           join employee in context.Employees
                           on taskTeam.EmployeeId equals employee.Id
                           where taskTeam.TaskId == TaskID
                           select new ViewModels.EmployeesInTaskViewModel()
                           {
                               EmployeeId = taskTeam.EmployeeId,
                               EmployeeName = employee.Name,
                               Id = taskTeam.Id,
                               TaskId = taskTeam.TaskId,
                               TimeStamp = taskTeam.TimeStamp,
                               UserName = taskTeam.UserName
                           }).ToList();
            return Result;
                           
        }

        public TaskTeam GetTaskAssignedEmployee(int Id)
        {
            return context.TaskTeams.Find(Id);
        }

        public TaskTeam UpdateTaskTeam(TaskTeam TasksChanges)
        {
            var task = context.TaskTeams.Attach(TasksChanges);
            task.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return TasksChanges;
        }


    }
}
