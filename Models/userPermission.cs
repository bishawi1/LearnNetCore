using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSIS.Models
{
    public class userPermission
    {
        public userPermission(AppDBContext context)
        {
            Context = context;
        }

        public AppDBContext Context { get; }
    }
}
