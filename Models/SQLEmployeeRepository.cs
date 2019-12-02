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
            var result = context.Tasks.Where(x => x.TaskOwnerId == Id || x.TaskResponsibleId == Id).ToList();
            if (result.Count > 0)
            {
                ErrorMessage = "cannot delete employee, employee has Tasks";
            }
            else
            {
                var purchaseOrder = context.PurchaseOrders.Where(x => x.EmployeeId == Id).ToList();
                if (purchaseOrder.Count > 0)
                {
                    ErrorMessage = "cannot delete employee, employee has Purchase Order";
                }
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
