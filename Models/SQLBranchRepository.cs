using Microsoft.EntityFrameworkCore;
using MSIS.Models;
using MSIS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSIS.Models
{
    public class SQLBranchRepository
    {
        private readonly AppDBContext context;

        public SQLBranchRepository(AppDBContext context)
        {
            this.context = context;
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
        public string ValidateDeletBranch(int Id)
        {
            string ErrorMessage = "";
            var result = context.Tasks.Where(x => x.BranchId == Id || x.BranchId == Id).ToList();
            if (result.Count > 0)
            {
                ErrorMessage = "cannot delete Branch, there is Tasks for this Branch";
            }
            else
            {
                var purchaseOrder = context.PurchaseOrders.Where(x => x.BranchId == Id).ToList();
                if (purchaseOrder.Count > 0)
                {
                    ErrorMessage = "cannot delete Branch, Branch has Purchase Order";
                }
            }
            return ErrorMessage;
        }

        public Branch Add(Branch branch)
        {
            context.Branches.Add(branch);
            context.SaveChanges();
            return branch;
        }

        public Branch Delete(int id)
        {
            Branch branch = context.Branches.Find(id);
            if (branch != null)
            {
                context.Branches.Remove(branch);
                context.SaveChanges();
            }
            return branch;
        }

        public IEnumerable<Branch> GetAllBranches()
        {
            return context.Branches;
        }
        public ListBranchesViewModel ListBranches()
        {
            ListBranchesViewModel model = new ListBranchesViewModel();
            model.Branches= context.Branches.ToList();
            return model;
        }
        public Branch GetBranch(int Id)
        {
            return context.Branches.Find(Id);
        }

        public Branch Update(Branch branchChanges)
        {
            var branch = context.Branches.Attach(branchChanges);
            branch.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return branchChanges;
        }

    }
}
