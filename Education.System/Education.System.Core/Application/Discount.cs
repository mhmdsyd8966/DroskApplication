using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Education.System.Core.Application
{
    public class Discount
    {
        [Key]
        public Guid Id { get; set; }
        public double Pencentege { get; set; }
    }
}