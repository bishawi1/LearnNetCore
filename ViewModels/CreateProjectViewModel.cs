using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TMS.Models;
namespace TMS.ViewModels
{
    public class CreateProjectViewModel:Project
    {
        public List<Customer> Customers { get; set; }
        public Boolean AddProjectNoManually { get; set; }

    }
}
