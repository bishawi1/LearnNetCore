using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MSIS.Models
{
    [NotMapped]
    public class Setting
    {
        [Key]
        public int Id { get; set; }
        public string SenderEmail { get; set; }
        public string SenderMailPassword { get; set; }
        public string SMTPServer { get; set; }
        public int Port { get; set; }
        public bool SendMailOnCreateTask { get; set; }


    }
}
