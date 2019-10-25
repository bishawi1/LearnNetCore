using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSIS.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Claims = new List<string>();
        }
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required][EmailAddress]
        public string Email { get; set; }
        public string City { get; set; }
        public List<string> Claims { get; set; }
        public IList<string> Roles { get; set; }
        public int EmployeeId { get; set; }
        public IList<Models.Employee> Employees { get; set; }


    }
}
