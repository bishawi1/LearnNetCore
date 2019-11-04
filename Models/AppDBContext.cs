using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MSIS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSIS.Models
{
    public class AppDBContext:IdentityDbContext<ApplicationUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            :base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<TaskStatus> TaskStatus { get; set; }
        public DbSet<ProjectDetailViewModel> ProjectDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<TaskTeam> TaskTeams { get; set; }
        public DbSet<TaskAction> TaskActions { get; set; }
        public DbSet<TaskDetailsViewModel> SQLTaskDetails { get; set; }

    }
}
