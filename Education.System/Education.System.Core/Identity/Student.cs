using Education.System.Core.Application;
using Education.System.Core.Identity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.System.Core.Identity
{
    public class Student : Consumer
    {
        public List<Package> Packages { get; set; }
    }
}