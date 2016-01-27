using Sperience;
using Sperience.Models;
using SperienceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SperienceWeb.Controllers
{
    public class HomeController : Controller
    {
        SperienceEntities db = new SperienceEntities();
        
        // GET: Home
        public ActionResult Index()
        {
            IEnumerable<Customer> accounts = db.Database.SqlQuery<Customer>("SELECT * FROM Customer");

            List<StageModel> result = new List<StageModel>();
            
            IEnumerable<Stage> queryStages = db.Database.SqlQuery<Stage>("SELECT * FROM crm.Stage ORDER BY LookupOrder");
            List<Stage> stageResult = queryStages.ToList();
            foreach (var stage in stageResult)
            {
                List<SubjectModel> subjectsModel = new List<SubjectModel>();
                
                string querySubjectHeader = "SELECT crm.SubjectHeader.Id, Customer.CustomerName AS Account, "
                + "crm.SubjectHeader.SubjectTitle, crm.SubjectHeader.SubjectDescription, crm.vwSubjectType.Code AS Stage, "
                + "crm.SubjectHeader.RevenueEstimate, crm.SubjectHeader.ClosingDate FROM crm.SubjectHeader "
                + "JOIN crm.vwSubjectType ON crm.SubjectHeader.SubjectTypeId = crm.vwSubjectType.Id "
                + "JOIN Customer ON crm.SubjectHeader.AccountId = Customer.Id "
                + "WHERE crm.vwSubjectType.Code = '"+ stage.Code +"'";
                IEnumerable<Subject> subjectResult = db.Database.SqlQuery<Subject>(querySubjectHeader);
                List<Subject> subjects = subjectResult.ToList();
                
                foreach (var subject in subjects)
                {
                    IEnumerable<String> marketchannelResult = db.Database.SqlQuery<String>("SELECT vwMarketChannel.Code FROM crm.SubjectMarketChannel JOIN vwMarketChannel ON crm.SubjectMarketChannel.MarketChannelId = vwMarketChannel.Id WHERE crm.SubjectMarketChannel.SubjectId = " + subject.Id );
                    List<String> source = marketchannelResult.ToList();

                    subjectsModel.Add(new SubjectModel() { Subject = subject, Source = source });
                }

                result.Add(new StageModel() { Stage = stage, Subjects = subjectsModel });
            }

            ViewBag.result = result.ToList();
            ViewBag.accounts = accounts.ToList();
            return View();
        }

        public ActionResult Activity()
        {
            IEnumerable<SubjectAction> subjectAction = db.Database.SqlQuery<SubjectAction>("Select * FROM crm.SubjectAction WHERE StageActionId='1'");
            ViewBag.subjectAction = subjectAction.ToList();
            return View();
        }

        public ActionResult Activities()
        {

            IEnumerable<SubjectAction> subjectAction = db.Database.SqlQuery<SubjectAction>("Select * FROM crm.SubjectAction WHERE StageActionId='1'");
            ViewBag.subjectAction = subjectAction.ToList();
            return View();

        }
        public ActionResult Actiondetails(string id)
        {

            IEnumerable<SubjectAction> action = db.Database.SqlQuery<SubjectAction>("SELECT * FROM  crm.SubjectAction  where crm.SubjectAction.Id='" + id + "'");
            ViewBag.action = action.ToList();


            IEnumerable<ActionDetailsModel> result = db.Database.SqlQuery<ActionDetailsModel>("select  crm.SubjectAction.Id  ,crm.SubjectAction.ActionTitle ,crm.SubjectAction.ActionDescription , crm.SubjectAction.ExpiryDate ,crm.SubjectAction.StartDateTime, crm.SubjectAction.EndDateTime,crm.SubjectAction.OutcomeDescription , crm.SubjectAction.Assignedto , vwActionType.Code   , A.Outcome from crm.SubjectAction join vwActionType on crm.SubjectAction.ActionTypeId= vwActionType.Id JOIN ( SELECT SolutionLookup.Id , SolutionLookup.Code  as Outcome FROM SolutionLookup where SolutionLookup.TypeCode='OpportunityActionOutcome') as A ON crm.SubjectAction.OutcomeId = A.Id where crm.SubjectAction.Id='" + id + "'");
            ViewBag.result = result.ToList();


            IEnumerable<Expenses> expenses = db.Database.SqlQuery<Expenses>("Select SolutionLookup.Code ,crm.SubjectActionExpense.Id, crm.SubjectActionExpense.ExpenseDescription , crm.SubjectActionExpense.ExpenseAmount from crm.SubjectActionExpense join SolutionLookup on crm.SubjectActionExpense.ExpenseTypeId = SolutionLookup.Id where crm.SubjectActionExpense.ActionId='" + id + "'");
            ViewBag.expenses = expenses.ToList();

            return PartialView();
        }

        // POST: Jobs/Delete/5
        [HttpPost]

        public ActionResult Delete(int id)
        {
            SubjectActionExpense exp = db.SubjectActionExpenses.Find(id);
            db.SubjectActionExpenses.Remove(exp);
            db.SaveChanges();
            return RedirectToAction("");
        }


        public ActionResult _Lead(int id)
        {
            IEnumerable<SubjectHeader> subjectHeader = db.Database.SqlQuery<SubjectHeader>("select * from crm.SubjectHeader where id = '" + id + "'");
            ViewBag.sHeader = subjectHeader.ToList();

            IEnumerable<Guarantee> lead = db.Database.SqlQuery<Guarantee>("select crm.SubjectHeader.ClosingDateTypeId , SolutionLookup.Code from crm.SubjectHeader join SolutionLookup on crm.SubjectHeader.ClosingDateTypeId = SolutionLookup.Id where crm.SubjectHeader.Id='" + id + "'");
            ViewBag.guarantee = lead.ToList();

            IEnumerable<MonetaryAllocation> monetaryAllocation = db.Database.SqlQuery<MonetaryAllocation>("select crm.SubjectHeader.BudgetaryAllocationId , SolutionLookup.Code from crm.SubjectHeader join SolutionLookup on crm.SubjectHeader.BudgetaryAllocationId = SolutionLookup.Id where crm.SubjectHeader.Id='" + id + "'");
            ViewBag.MonetaryAllocation = monetaryAllocation.ToList();
            ViewBag.subjectId = id;

            IEnumerable<ProductCategory> productCategory = db.Database.SqlQuery<ProductCategory>("select Category.Id , Category.CategoryName, crm.SubjectCategory.RevenueEstimate from Category join crm.SubjectCategory on Category.Id = crm.SubjectCategory.CategoryId join crm.SubjectHeader on crm.SubjectCategory.SubjectId = crm.SubjectHeader.Id where crm.SubjectHeader.Id = '" + id + "'");
            ViewBag.product = productCategory.ToList();

            IEnumerable<SubjectCategories> subjectCategoryID = db.Database.SqlQuery<SubjectCategories>("select crm.SubjectCategory.Id  from Category join crm.SubjectCategory on Category.Id = crm.SubjectCategory.CategoryId join crm.SubjectHeader on crm.SubjectCategory.SubjectId = crm.SubjectHeader.Id where crm.SubjectHeader.Id = '" + id + "'");
            ViewBag.idSubjectCategory = subjectCategoryID.ToList();

            return PartialView();
        }


        public ActionResult Test(string id)
        {
            IEnumerable<SubjectHeader> sheader = db.Database.SqlQuery<SubjectHeader>("SELECT * FROM crm.SubjectHeader Where Id = " + id);
            ViewBag.sHeader = sheader.ToList();
            return PartialView();
        }
    }
}