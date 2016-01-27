using Sperience;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SperienceWeb.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string SubjectTitle { get; set; }
        public string SubjectDescription { get; set; }
        public string Stage { get; set; }
        public Decimal RevenueEstimate { get; set; }
        public DateTime ClosingDate { get; set; }
    }

    public class SubjectModel
    {
        public Subject Subject { get; set; }
        public List<String> Source { get; set; }
    }

    public class StageModel
    {
        public Stage Stage { get; set; }
        public List<SubjectModel> Subjects { get; set; }
    }

    public class AccountsModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public Decimal RevenueEstimate { get; set; }
        public int BudgetaryAllocationId { get; set; }
    }


}