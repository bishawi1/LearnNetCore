using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Column(TypeName ="nvarchar(50)")]        
        public string City { get; set; }
        public int EmployeeId { get; set; }
        public string Category { get; set; }

        //public string AppRoleId { get; set; }
        //public string AppRoleName { get; set; }


    }
}
