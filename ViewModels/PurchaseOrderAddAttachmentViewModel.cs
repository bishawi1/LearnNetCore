using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MSIS.Models;
using Microsoft.AspNetCore.Http;

namespace MSIS.ViewModels
{
    public class PurchaseOrderAddAttachmentViewModel:PurchaseOrderAttachment
    {
        [NotMapped]
        public List<IFormFile> Photos { get; set; }

    }
}
