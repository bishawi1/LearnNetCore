using Microsoft.EntityFrameworkCore;
using MSIS.Models;
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
