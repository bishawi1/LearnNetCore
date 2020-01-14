using MSIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSIS.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MSIS.Models
{
    public class SQLProjectRepository
    {
        private readonly AppDBContext context;

        public SQLProjectRepository(AppDBContext Context)
        {
            context = Context;
        }
        public UserPermissionsViewModel GetUserParentMenuPermission(string UserId, string PageName)
        {
            UserPermissionsViewModel model = new UserPermissionsViewModel();
            var result = context.SQLUserAllowedParentMenuesViewModel.FromSql("SELECT * FROM dbo.UserAllowedParentMenu Where ParentName = 'Settings' And UserId = '" + UserId + "' And PageName ='" + PageName + "'").ToList();
            var Menues = result.Select(x => x.ParentName).Distinct().ToList();
            model.ParentMenus = Menues;
            model.UserPermissions = result;
            return model;
        }
        public string ValidateDeletProject(int Id)
        {
            string ErrorMessage = "";
            var result = context.Tasks.Where(x => x.ProjectId == Id ).ToList();
            if (result.Count > 0)
            {
                ErrorMessage = "cannot delete Project, there is Tasks for this Project";
            }
            else
            {
                var purchaseOrder = context.PurchaseOrders.Where(x => x.ProjectId == Id).ToList();
                if (purchaseOrder.Count > 0)
                {
                    ErrorMessage = "cannot delete Project, Project has Purchase Order or More";
                }
            }
            return ErrorMessage;
        }

        public Boolean IsProjectExists(int ProjectId, int ProjectYear, int ProjectSerial)
        {
            var result = context.Projects.FromSql("Select * from dbo.Projects Where Id <> " + ProjectId.ToString() + " And ProjectYear = " + ProjectYear + " And ProjectSerial = " + ProjectSerial).ToList();
            Boolean value = true;
            if (result.Count > 0)
            {
                value = false;
            }
            return value;
        }
        public Project Add(Project project)
        {
// for auto Project no
            //var intSerial=0;
            //if (context.Projects.Count() == 0)
            //{
            //    intSerial = 0;
            //}
            //else
            //{
            //    intSerial = context.Projects.Where(o => o.ProjectYear == DateTime.Today.Year).Max(x => x.ProjectSerial);
            //}
            //project.ProjectYear = DateTime.Today.Year;
            //project.ProjectSerial = intSerial+1;            
            context.Projects.Add(project);
            context.SaveChanges();
            return project;
        }

        public Project Delete(int id)
        {
            Project project = context.Projects.Find(id);
            if (project != null)
            {
                context.Projects.Remove(project);
                context.SaveChanges();
            }
            return project;
        }
        public ListProjectsViewModels ListProjects()
        {
            ListProjectsViewModels model = new ListProjectsViewModels();
            model.Projects = getAllProjectDetails().ToList();
            return model;
        }
        public IEnumerable<Project> GetAllProjects()
        {
            return context.Projects;
        }

        public Project GetProject(int Id)
        {
            return context.Projects.Find(Id);
        }

        public Project Update(Project projectChanges)
        {           
            var project = context.Projects.Attach(projectChanges);
            project.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return projectChanges;
        }

        public ProjectDetailViewModel getProjectDetails(int projectId)
        {
            ProjectDetailViewModel projectDetailViewModel=null;
            var result = (from project in context.Projects
                                                             join customer in context.Customers
                                                             on project.ProjectOwner equals customer.Id
                                                             where project.Id == projectId
                                          select new ProjectDetailViewModel()
                                                             {
                                                                 Id = project.Id,
                                                                 Address = project.Address,
                                                                 Code = project.ProjectSerial.ToString() + project.ProjectYear.ToString(),
                                                                 CustomerName = customer.CustomerName,
                                                                 Email = customer.Email,
                                                                 ProjectOwner=project.ProjectOwner,
                                                                 EndDate=project.EndDate,
                                                                 MobileNo=customer.MobileNo,
                                                                 OtheInformation=project.OtheInformation,
                                                                 ProjectName=project.ProjectName,
                                                                 ProjectSerial=project.ProjectSerial,
                                                                 ProjectYear=project.ProjectYear,
                                                                 StartDate=project.StartDate                                                                 
                                                             }).ToList();
            return result[0];// projectDetailViewModel;
        }
        public List<ProjectDetailViewModel> getAllProjectDetails()
        {
            ProjectDetailViewModel projectDetailViewModel = null;
            var result = (from project in context.Projects
                          join customer in context.Customers
                          on project.ProjectOwner equals customer.Id
                          select new ProjectDetailViewModel()
                          {
                              Id = project.Id,
                              Address = project.Address,
                              Code = project.ProjectSerial.ToString() + project.ProjectYear.ToString(),
                              CustomerName = customer.CustomerName,
                              Email = customer.Email,
                              ProjectOwner = project.ProjectOwner,
                              EndDate = project.EndDate,
                              MobileNo = customer.MobileNo,
                              OtheInformation = project.OtheInformation,
                              ProjectName = project.ProjectName,
                              ProjectSerial = project.ProjectSerial,
                              ProjectYear = project.ProjectYear,
                              StartDate = project.StartDate
                          }).ToList();
            return result;// projectDetailViewModel;
        }
    }
}
