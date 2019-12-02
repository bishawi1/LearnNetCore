using MSIS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSIS.Models
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int Id);
        IEnumerable<Employee> GetAllEmployees();
        Employee Add(Employee employee);
        Employee Update(Employee employeeChanges);
        Employee Delete(int id);
        string ValidateDeletEmployee(int Id);
        UserPermissionsViewModel GetUserParentMenuPermission(string userId, string PageName);
        ListEmployeesViewModel ListEmployees();

    }
}
