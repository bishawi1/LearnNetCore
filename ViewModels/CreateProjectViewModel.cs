using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSIS.Models;
namespace MSIS.ViewModels
{
    public class CreateProjectViewModel:Project
    {
            public List<Customer> Customers { get; set; }
    }
}
