using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TMS.Models
{
    public class MainItem
    {
        [Key]
        public int Id { get; set; }
        public int ItemCategoryId { get; set; }
        public string MainItemName { get; set; }
        public string OtherInformation { get; set; }


    }
}
