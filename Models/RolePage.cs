using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MSIS.Models
{
    [NotMapped]
    public class RolePage
    {
        [Key]
        public int Id { get; set; }
        public string RoleId { get; set; }
        public int PageId { get; set; }
        public Boolean CanAdd { get; set; }
        public Boolean CanView { get; set; }
        public Boolean CanEdit { get; set; }
        public Boolean CanDelete { get; set; }

    }
}
