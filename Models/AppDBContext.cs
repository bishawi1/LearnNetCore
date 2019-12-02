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
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<ItemUnit> ItemUnits { get; set; }
        public DbSet<PurchaseOrderDetails> PurchaseOrdersDetails { get; set; }
        public DbSet<TaskDetailsViewModel> SQLTaskDetails { get; set; }
        public DbSet<ListPurchaseOrderDetailsViewModel> SQLListPurchaseOrderDetailsViewModel { get; set; }
        public DbSet<PurchaseOrderDetailsViewModel> SQLPurchaseOrderDetailsViewModel { get; set; }

        public DbSet<PurchaseOrderItemsViewModel> SQLPurchaseOrderItemsViewModel { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<MainItem> MainItems { get; set; }

        public DbSet<Item> Items { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<OfferDetail> offerDetails { get; set; }

        public DbSet<Page> Pages { get; set; }
        public DbSet<RolePage> RolePages { get; set; }
        public DbSet<UserPermissionDetailsViewModel> SQLUserAllowedParentMenuesViewModel { get; set; }

    }
}
