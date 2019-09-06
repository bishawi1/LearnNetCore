using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnNetCore.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _EmployeList;
        public  MockEmployeeRepository()
        {
            _EmployeList = new List<Employee>()
            {
                new Employee() { Id = 1, Name ="Mary" ,Department=Dept.HR,Email="Mary@gmail.com"},
                new Employee() { Id = 2, Name = "John", Department = Dept.IT, Email = "John@gmail.com" },
                new Employee() { Id = 3, Name = "Sam" ,Department=Dept.IT,Email="Sam@gmail.com"}
            };
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _EmployeList;
        }

        public Employee GetEmployee(int Id)
        {
            return _EmployeList.FirstOrDefault(e => e.Id == Id);
        }
    }
}
