using MSIS.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MSIS.ViewModels
{
    public class EmployeeCreateViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot Exceed 50 characters")]
        public string Name { get; set; }

        public string IdentityNo { get; set; }
        public string MobileNo { get; set; }
        public string WorkMobileNo { get; set; }
        public string EducationDegree { get; set; }
        public string Specialization { get; set; }
        public string OtherInformation { get; set; }
        public string Address { get; set; }


        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid eMial")]
        [Display(Name = "Office Email")]
        public string Email { get; set; }
        //[Required] no need to be required because of its datatype is enum and it is by default required
        // to make property optional use ?, Adding requuired here make the requered attribute meaningful 
        public Dept? Department { get; set; }
        public List<IFormFile> Photos { get; set; }

    }
}
