using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sperience.Models
{
    public class Expenses
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string ExpenseDescription { get; set; }
        public decimal ExpenseAmount { get; set; }
    }
}