using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sperience.Models
{
    public class Guarantee
    {
        public int Id { get; set; }
        public int ClosingDateTypeId { get; set; }
        public string Code { get; set; }
    }
}