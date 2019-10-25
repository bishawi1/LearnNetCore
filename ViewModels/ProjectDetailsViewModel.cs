using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSIS.ViewModels
{
    public class ProjectDetailViewModel
    {
        [Key]
        public int Id { get; set; }

        public int? ProjectYear { get; set; }
        public int? ProjectSerial { get; set; }
        public string ProjectName { get; set; }
        public int? ProjectOwner { get; set; }
        public string CustomerName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string OtheInformation { get; set; }
        public string Code { get; set; }

    }
}
