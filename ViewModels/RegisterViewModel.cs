using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using MSIS.Utilites;
using Microsoft.AspNetCore.Identity;

namespace MSIS.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        [ValidEmailDomain(allowedDomain:"msis.ps", ErrorMessage ="Email domain must be MSIS.PS")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name="Confirm Password")]
        [Compare("Password",ErrorMessage ="Password and Confirmation Password do not match")]
        public string ConfirmPassword { get; set; }

        public string City { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter Employee From List")]
        public int EmployeeId { get; set; }

        public List<Models.Employee> Employees { get; set; }
        [NotMapped]
        public string RoleId { get; set; }
        public List<IdentityRole> RoleList { get; set; }

    }
}
