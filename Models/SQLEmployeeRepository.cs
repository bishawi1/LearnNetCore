using Microsoft.EntityFrameworkCore;
using MSIS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSIS.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDBContext context;
        public Boolean IsEmployeeExists(int EmployeeId, string EmployeeNo)
        {
            var result = context.Employees.FromSql("Select * from dbo.Employees Where Id <> " + EmployeeId.ToString() + " And EmployeeNo = '" + EmployeeNo + "'").ToList();
            Boolean value = false;
            if (result.Count > 0)
            {
                value = true;
            }
            return value;
        }
        public AppDBContext getContext()
        {
            return context;
        }


        public Boolean IsEmployeeHasUser(string UserName, int EmployeeId)
        {
            var result = context.Users.FromSql("Select * from dbo.AspNetUsers Where UserName <> '" + UserName + "' And EmployeeId = " + EmployeeId).ToList();
            Boolean value = true;
            if (result.Count > 0)
            {
                value = false;
            }
            return value;
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
        public string ValidateDeletEmployee(int Id)
        {
            string ErrorMessage = "";
            var PhysicalDelete = true;
            var result = context.Tasks.Where(x => x.TaskOwnerId == Id || x.TaskResponsibleId == Id).ToList();
            if (result.Count > 0)
            {
                result = result.Where(x => x.TaskStatusId != 3 && x.TaskStatusId != 5).ToList();
                if (result.Count > 0) { 
                    ErrorMessage = "cannot delete employee, employee has Tasks";
                }
                else
                {
                    PhysicalDelete = false;
                }
            }
            if(PhysicalDelete==false && ErrorMessage=="")
            {
                var purchaseOrder = context.PurchaseOrders.Where(x => x.EmployeeId == Id).ToList();
                if (purchaseOrder.Count > 0)
                {
                    ErrorMessage = "cannot delete employee, employee has Purchase Order";
                }
            }
            if(!PhysicalDelete && ErrorMessage == "")
            {
                ErrorMessage = "Deactivate";
            }
            return ErrorMessage;
        }
        public SQLEmployeeRepository(AppDBContext context)
        {
            this.context = context;
        }
        public Employee Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = context.Employees.Find(id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
            return employee;
        }

        public ListEmployeesViewModel ListEmployees ()
        {
            ListEmployeesViewModel model = new ListEmployeesViewModel();
            model.Employees = context.Employees.ToList();
            return model;
        }
        public ListEmployeesViewModel ListActiveEmployees()
        {
            ListEmployeesViewModel model = new ListEmployeesViewModel();
            model.Employees = context.Employees.Where(x=>x.Active==true).ToList();
            return model;
        }
        public ListEmployeesViewModel ListInActiveEmployees()
        {
            ListEmployeesViewModel model = new ListEmployeesViewModel();
            model.Employees = context.Employees.Where(x => x.Active == false).ToList();
            return model;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return context.Employees;
        }

        public Employee GetEmployee(int Id)
        {
            return  context.Employees.Find(Id);
        }

        public Employee Update(Employee employeeChanges)
        {            
            var employee= context.Employees.Attach(employeeChanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return employeeChanges;
        }


    }
}
