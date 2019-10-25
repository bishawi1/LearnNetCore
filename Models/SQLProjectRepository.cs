using MSIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSIS.ViewModels;
namespace MSIS.Models
{
    public class SQLProjectRepository
    {
        private readonly AppDBContext context;

        public SQLProjectRepository(AppDBContext Context)
        {
            context = Context;
        }
        public Project Add(Project project)
        {
            var intSerial = context.Projects.Where(o => o.ProjectYear == DateTime.Today.Year).Max(x => x.ProjectSerial);
            project.ProjectYear = DateTime.Today.Year;
            project.ProjectSerial = intSerial+1;            
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
