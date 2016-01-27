using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sperience.Models
{
    public class ActionDetailsModel
    {
        public int Id { get; set; }
        public string ActionTitle { get; set;}
         public  string ActionDescription { get; set;}
        public DateTime  ExpiryDate { get; set;}
        public  DateTime  StartDateTime{ get; set;}
       public  DateTime EndDateTime { get; set;}
       public  string OutcomeDescription { get; set;}
       public string Outcome { get; set; }
        public  int    Assignedto { get; set;}
        public string Code { get; set; }
    }
}