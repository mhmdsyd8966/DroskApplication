using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Education.System.Core.Application
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public string StudentName { get; set; }
        public string StudentId { get; set; }
        public Guid PackageId { get; set; }
        public string PackageName { get; set; }
        public double PackagePrice { get; set; }
        public string DiscountCode { get; set; }
        public double DiscountPercentage { get; set; }
        public double FinalPrice { get; set; }
        public string Status { get; set; }
    }
}