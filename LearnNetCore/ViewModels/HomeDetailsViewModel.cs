using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnNetCore.Models;
namespace LearnNetCore.ViewModels
{
    public class HomeDetailsViewModel
    {
        public  Employee Employee { get; set; }
        public String PageTitle { get; set; }
    }
}
