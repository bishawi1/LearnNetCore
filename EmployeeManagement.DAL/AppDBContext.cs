using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
namespace EmployeeManagement.DAL
{
    class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            :base(options)
        {

        }
    }
}
