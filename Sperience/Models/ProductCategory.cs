using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sperience.Models
{
    public class ProductCategory
    {
        public int id { get; set; }
        public string CategoryName { get; set; }
        public decimal RevenueEstimate { get; set; }
    }
}