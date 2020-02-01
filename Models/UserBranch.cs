using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.Models
{
    public class UserBranch
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BranchId { get; set; }

    }
}
