using MSIS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

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
                        TimeStamp = DateTime.Now,
                        TaskStatusId = model.TaskStatusID,
                        CurrentTaskStatusId = model.CurrentTaskStatusId,
                        UserName = model.UserName
                    }) ;
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
        public ViewModels.TaskCountByStatusViewModel getTaskCountByStatus(int EmployeeId)
        {
            TaskCountByStatusViewModel result = new TaskCountByStatusViewModel();
            if (context.Tasks != null)
            {
                var model = context.Tasks
                       .Where(x => x.TaskStatusId == 5 && (x.TaskOwnerId==EmployeeId || x.TaskResponsibleId == EmployeeId))
                       .GroupBy(x => new { TaskStatusId = x.TaskStatusId })
                       .Select(x => new { TaskCount = x.Count() }).FirstOrDefault();
                if (model != null)
                {
                    result.ApprovedCount = model.TaskCount;
                }

                model = context.Tasks
                       .Where(x => x.TaskStatusId == 2 && (x.TaskOwnerId == EmployeeId || x.TaskResponsibleId == EmployeeId))
                       .GroupBy(x => new { TaskStatusId = x.TaskStatusId })
                       .Select(x => new { TaskCount = x.Count() }).FirstOrDefault();
                if (model != null)
                {
                    result.InProgressCount = model.TaskCount;
                }
                model = context.Tasks
                    .Where(x => x.TaskStatusId == 3 && (x.TaskOwnerId == EmployeeId || x.TaskResponsibleId == EmployeeId))
                    .GroupBy(x => new { TaskStatusId = x.TaskStatusId })
                    .Select(x => new { TaskCount = x.Count() }).FirstOrDefault();
                if (model != null)
                {
                    result.RejectedCount = model.TaskCount;
                }
                model = context.Tasks
                      .Where(x => x.TaskStatusId == 4 && (x.TaskOwnerId == EmployeeId || x.TaskResponsibleId == EmployeeId))
                      .GroupBy(x => new { TaskStatusId = x.TaskStatusId })
                      .Select(x => new { TaskCount = x.Count() }).FirstOrDefault();

                if (model != null)
                {
                    result.DoneCount = model.TaskCount;
                }
                model = context.Tasks
                   .Where(x => x.TaskStatusId == 1 && (x.TaskOwnerId == EmployeeId || x.TaskResponsibleId == EmployeeId))
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
                          join ownerEmployee in context.Employees
                          on task.TaskOwnerId equals ownerEmployee.Id
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
                              StatusName = taskStatus.StatusName,
                              ProjectName = project.ProjectName,
                              TaskStatusId = task.TaskStatusId,
                              TaskOwnerName=ownerEmployee.Name,
                              BranchName = branch.Name


                          }).ToList();

            return result;// projectDetailViewModel;
        }

        public List<MSIS.ViewModels.TaskDetailsViewModel> getTaskDetailsByStatusId(int EmployeeId, int status)
        {
            MSIS.ViewModels.TaskDetailsViewModel taskDetailViewModel = null;
            var result = (from task in context.Tasks
                          join employee in context.Employees
                          on task.TaskResponsibleId equals employee.Id
                          join ownerEmployee in context.Employees
                          on task.TaskOwnerId equals ownerEmployee.Id
                          join taskStatus in context.TaskStatus
                          on task.TaskStatusId equals taskStatus.Id
                          join project in context.Projects
                          on task.ProjectId equals project.Id
                          join branch in context.Branches
                          on task.BranchId equals branch.Id
                          where task.TaskStatusId==status &&(task.TaskOwnerId==EmployeeId || task.TaskResponsibleId ==EmployeeId)
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
                              StatusName = taskStatus.StatusName,
                              ProjectName = project.ProjectName,
                              TaskStatusId = task.TaskStatusId,
                              TaskOwnerName=ownerEmployee.Name,
                              BranchName = branch.Name


                          }).ToList();

            return result;// projectDetailViewModel;
        }

        //private static Func<Employee, bool> GetDynamicQueryWithExpresionTrees(string propertyName, string val)
        //{
        //    var param = Expression.Parameter(typeof(Employee), "x");

        //    #region Convert to specific data type
        //    MemberExpression member = Expression.Property(param, propertyName);
        //    UnaryExpression valueExpression = GetValueExpression(propertyName, val, param);
        //    #endregion

        //    Expression body = Expression.Equal(member, valueExpression);
        //    var final = Expression.Lambda<Func<User, bool>>(body: body, parameters: param);
        //    return final.Compile();
        //}

        //private static UnaryExpression GetValueExpression(string propertyName, string val, ParameterExpression param)
        //{
        //    var member = Expression.Property(param, propertyName);
        //    var propertyType = ((PropertyInfo)member.Member).PropertyType;
        //    var converter = TypeDescriptor.GetConverter(propertyType);

        //    if (!converter.CanConvertFrom(typeof(string)))
        //        throw new NotSupportedException();

        //    var propertyValue = converter.ConvertFromInvariantString(val);
        //    var constant = Expression.Constant(propertyValue);
        //    return Expression.Convert(constant, propertyType);
        //}
        public List<MSIS.ViewModels.TaskDetailsViewModel> getAllTaskDetails(ViewModels.SearchTaskViewModel Criteria)
        {
            try
            {
                string strWhere = "";
                if (Criteria.TaskOwnerId > 0) {
                    strWhere = strWhere + " TaskOwnerId = " + Criteria.TaskOwnerId.ToString(); 
                }
                if (Criteria.TaskResponsibleId > 0)
                {
                    if (strWhere != "")
                    {
                        strWhere =strWhere + " And ";
                    }
                    strWhere = strWhere + " TaskResponsibleId = " + Criteria.TaskResponsibleId.ToString();
                }
                if (Criteria.FromTaskDate.Year > 1)
                {
                    if (strWhere != "")
                    {
                        strWhere =strWhere + " And ";
                    }
                    strWhere = strWhere + " TaskDate >= '" + Criteria.FromTaskDate.ToString() + "'";
                }
                if (Criteria.ToTaskDate.Year > 1)
                {
                    if (strWhere != "")
                    {
                        strWhere = strWhere + " And ";
                    }
                    strWhere = strWhere + " TaskDate <= '" + Criteria.ToTaskDate.ToString() + "'";
                }
                if (strWhere != "")
                {
                    strWhere = " Where " + strWhere;
                }
                if (Criteria.strGroupBy != null)
                {
                    if (Criteria.strGroupBy.ToLower() != "all") { 
                        strWhere = strWhere + " Order By " + Criteria.strGroupBy;
                    }
                }
                var result = context.SQLTaskDetails.FromSql("SELECT *,'" + Criteria.strGroupBy + "' As strGroupBy FROM dbo.vTaskDetails " + strWhere).ToList();
            
            return result.ToList();// projectDetailViewModel;
            }catch(Exception ex)
            {
                throw ex;
            };
        }
       public List<MSIS.ViewModels.TaskDetailsViewModel> getAllTaskDetails(ViewModels.SearchTaskReportsViewModel Criteria)
        {
            try
            {
                string strWhere = "";
                if (Criteria.TaskOwnerId > 0) {
                    strWhere = strWhere + " TaskOwnerId = " + Criteria.TaskOwnerId.ToString(); 
                }
                if (Criteria.TaskResponsibleId > 0)
                {
                    if (strWhere != "")
                    {
                        strWhere =strWhere + " And ";
                    }
                    strWhere = strWhere + " TaskResponsibleId = " + Criteria.TaskResponsibleId.ToString();
                }
                if (Criteria.ProjectId > 0)
                {
                    if (strWhere != "")
                    {
                        strWhere = strWhere + " And ";
                    }
                    strWhere = strWhere + " ProjectId = " + Criteria.ProjectId.ToString();
                }
                if (Criteria.BranchId > 0)
                {
                    if (strWhere != "")
                    {
                        strWhere = strWhere + " And ";
                    }
                    strWhere = strWhere + " BranchId = " + Criteria.BranchId.ToString();
                }
                if (Criteria.TaskStatusId > 0)
                {
                    if (strWhere != "")
                    {
                        strWhere = strWhere + " And ";
                    }
                    strWhere = strWhere + " TaskStatusId = " + Criteria.TaskStatusId.ToString();
                }

                if (Criteria.FromTaskDate.Year > 1)
                {
                    if (strWhere != "")
                    {
                        strWhere =strWhere + " And ";
                    }
                    strWhere = strWhere + " TaskDate >= '" + Criteria.FromTaskDate.ToString() + "'";
                }
                if (Criteria.ToTaskDate.Year > 1)
                {
                    if (strWhere != "")
                    {
                        strWhere = strWhere + " And ";
                    }
                    strWhere = strWhere + " TaskDate <= '" + Criteria.ToTaskDate.ToString() + "'";
                }
                if (strWhere != "")
                {
                    strWhere = " Where " + strWhere;
                }
                if (Criteria.strGroupBy != null)
                {
                    if (Criteria.strGroupBy.ToLower() != "all") { 
                        strWhere = strWhere + " Order By " + Criteria.strGroupBy;
                    }
                }
                var result = context.SQLTaskDetails.FromSql("SELECT *,'" + Criteria.strGroupBy + "' As strGroupBy FROM dbo.vTaskDetails " + strWhere).ToList();            
            return result.ToList();// projectDetailViewModel;
            }catch(Exception ex)
            {
                throw ex;
            };
        }

        public List<MSIS.ViewModels.TaskDetailsViewModel> getAllTaskDetails()
        {

            MSIS.ViewModels.TaskDetailsViewModel taskDetailViewModel = null;
            var result = (from task in context.Tasks
                          join employee in context.Employees
                          on task.TaskResponsibleId equals employee.Id
                          join ownerEmployee in context.Employees
                          on task.TaskOwnerId equals ownerEmployee.Id
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
                              StatusName = taskStatus.StatusName,
                              ProjectName=project.ProjectName,
                              TaskStatusId=task.TaskStatusId,
                              TaskOwnerName=ownerEmployee.Name,
                              BranchName=branch.Name
                             
                                    
                          }).ToList();
         
            return result;// projectDetailViewModel;
        }
        public List<MSIS.ViewModels.TaskDetailsViewModel> getEmployeeTaskDetails(int EmployeeId,string strGroupBy)
        {
            string strWhere = " Where (TaskOwnerId = " + EmployeeId + " or TaskResponsibleId = " + EmployeeId + ")";
            try { 
            var result = context.SQLTaskDetails.FromSql("SELECT *,'"+ strGroupBy + "' As strGroupBy FROM dbo.vTaskDetails " + strWhere).ToList();
            return result.ToList();// projectDetailViewModel;
            }catch(Exception ex)
            {
                throw ex;
            };

    //MSIS.ViewModels.TaskDetailsViewModel taskDetailViewModel = null;
    //        var result = (from task in context.Tasks
    //                      join employee in context.Employees
    //                      on task.TaskResponsibleId equals employee.Id
    //                      join ownerEmployee in context.Employees
    //                      on task.TaskOwnerId equals ownerEmployee.Id
    //                      join taskStatus in context.TaskStatus
    //                      on task.TaskStatusId equals taskStatus.Id
    //                      join project in context.Projects
    //                      on task.ProjectId equals project.Id
    //                      join branch in context.Branches
    //                      on task.BranchId equals branch.Id
    //                      where (task.TaskOwnerId==EmployeeId || task.TaskResponsibleId==EmployeeId )
    //                      select new MSIS.ViewModels.TaskDetailsViewModel()
    //                      {
    //                          Id = task.Id,
    //                          Description = task.Description,
    //                          TaskDate = task.TaskDate,
    //                          OtherInformation = task.OtherInformation,
    //                          TaskEndDate = task.TaskEndDate,
    //                          TaskResponsibleName = employee.Name,
    //                          TaskResultDescription = task.TaskResultDescription,
    //                          TaskStartDate = task.TaskStartDate,
    //                          TaskSubject = task.TaskSubject,
    //                          StatusName = taskStatus.StatusName,
    //                          ProjectName = project.ProjectName,
    //                          TaskStatusId = task.TaskStatusId,
    //                          TaskOwnerName=ownerEmployee.Name,
    //                          BranchName = branch.Name


    //                      }).ToList();

    //        return result;// projectDetailViewModel;
        }

        public List<MSIS.ViewModels.TaskDetailsViewModel> getEmployeeTaskDetails(int EmployeeId, ViewModels.SearchTaskReportsViewModel Criteria)
        {
            try
            {
                string strWhere = " (TaskResponsibleId = " + EmployeeId + " Or TaskOwnerId = " + EmployeeId + ")";
                if (Criteria.TaskOwnerId > 0)
                {
                    strWhere = strWhere + " And TaskOwnerId = " + Criteria.TaskOwnerId.ToString();
                }
                if (Criteria.TaskResponsibleId > 0)
                {
                    if (strWhere != "")
                    {
                        strWhere = strWhere + " And ";
                    }
                    strWhere = strWhere + " TaskResponsibleId = " + Criteria.TaskResponsibleId.ToString();
                }
                if (Criteria.ProjectId > 0)
                {
                    if (strWhere != "")
                    {
                        strWhere = strWhere + " And ";
                    }
                    strWhere = strWhere + " ProjectId = " + Criteria.ProjectId.ToString();
                }
                if (Criteria.BranchId > 0)
                {
                    if (strWhere != "")
                    {
                        strWhere = strWhere + " And ";
                    }
                    strWhere = strWhere + " BranchId = " + Criteria.BranchId.ToString();
                }
                if (Criteria.TaskStatusId > 0)
                {
                    if (strWhere != "")
                    {
                        strWhere = strWhere + " And ";
                    }
                    strWhere = strWhere + " TaskStatusId = " + Criteria.TaskStatusId.ToString();
                }

                if (Criteria.FromTaskDate.Year > 1)
                {
                    if (strWhere != "")
                    {
                        strWhere = strWhere + " And ";
                    }
                    strWhere = strWhere + " TaskDate >= '" + Criteria.FromTaskDate.ToString() + "'";
                }
                if (Criteria.ToTaskDate.Year > 1)
                {
                    if (strWhere != "")
                    {
                        strWhere = strWhere + " And ";
                    }
                    strWhere = strWhere + " TaskDate <= '" + Criteria.ToTaskDate.ToString() + "'";
                }
                if (strWhere != "")
                {
                    strWhere = " Where " + strWhere;
                }
                if (Criteria.strGroupBy != null)
                {
                    if (Criteria.strGroupBy.ToLower() != "all")
                    {
                        strWhere = strWhere + " Order By " + Criteria.strGroupBy;
                    }
                }
                var result = context.SQLTaskDetails.FromSql("SELECT *,'" + Criteria.strGroupBy + "' As strGroupBy FROM dbo.vTaskDetails " + strWhere).ToList();
                return result.ToList();// projectDetailViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            };


        }

        public MSIS.ViewModels.TaskDetailsViewModel getTaskDetails(int Id)
        {
            MSIS.ViewModels.TaskDetailsViewModel taskDetailViewModel = null;
            var taskTeam = GetAllTaskAssignedEmployees(Id).ToList();
            var result = (from task in context.Tasks
                          join employee in context.Employees
                          on task.TaskResponsibleId equals employee.Id
                          join ownerEmployee in context.Employees
                          on task.TaskOwnerId equals ownerEmployee.Id
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
                              StatusName = taskStatus.StatusName,
                              ProjectName = project.ProjectName,
                              BranchName = branch.Name,
                              TaskStatusId=task.TaskStatusId,
                              TaskOwnerName=ownerEmployee.Name,
                              TaskOwnerId=task.TaskOwnerId,
                              TaskTeam=taskTeam
                          }).ToList();

            var owner = context.Users.Where(x => x.EmployeeId == result[0].TaskOwnerId).FirstOrDefault();
            if (owner != null)
            {
                result[0].TaskOwnerUserName = owner.UserName;
            }

            var TaskActions = (from taskAction in context.TaskActions
                               join taskStatus in context.TaskStatus
                               on taskAction.TaskStatusId equals taskStatus.Id
                               join currentTaskStatus in context.TaskStatus
                               on taskAction.CurrentTaskStatusId equals currentTaskStatus.Id
                               where taskAction.TaskId == Id
                               select new ViewModels.TaskActionDetailsViewModel
                               {
                                   Id = taskAction.Id,
                                   TaskId = taskAction.TaskId,
                                   ActionDate = taskAction.ActionDate,
                                   CurrentTaskStatusId = taskAction.CurrentTaskStatusId,
                                   CurrentTaskStatusName = currentTaskStatus.StatusName,
                                   Description = taskAction.Description,
                                   TaskStatusId = taskAction.TaskStatusId,
                                   TaskStatusName = taskStatus.StatusName,
                                   TimeStamp = taskAction.TimeStamp,
                                   UserName = taskAction.UserName
                               }).ToList();
            result[0].TaskActions = TaskActions;
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
