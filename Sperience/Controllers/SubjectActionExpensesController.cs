using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Sperience;

namespace Sperience.Controllers
{
    public class SubjectActionExpensesController : ApiController
    {
        private SperienceEntities db = new SperienceEntities();

        // GET: api/SubjectActionExpenses
        public IQueryable<SubjectActionExpense> GetSubjectActionExpenses()
        {
            return db.SubjectActionExpenses;
        }

        // GET: api/SubjectActionExpenses/5
        [ResponseType(typeof(SubjectActionExpense))]
        public IHttpActionResult GetSubjectActionExpense(int id)
        {
            SubjectActionExpense subjectActionExpense = db.SubjectActionExpenses.Find(id);
            if (subjectActionExpense == null)
            {
                return NotFound();
            }

            return Ok(subjectActionExpense);
        }

        // PUT: api/SubjectActionExpenses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubjectActionExpense(int id, SubjectActionExpense subjectActionExpense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subjectActionExpense.Id)
            {
                return BadRequest();
            }

            db.Entry(subjectActionExpense).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectActionExpenseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SubjectActionExpenses
        [ResponseType(typeof(SubjectActionExpense))]
        public IHttpActionResult PostSubjectActionExpense(SubjectActionExpense subjectActionExpense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SubjectActionExpenses.Add(subjectActionExpense);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = subjectActionExpense.Id }, subjectActionExpense);
        }

        // DELETE: api/SubjectActionExpenses/5
        [ResponseType(typeof(SubjectActionExpense))]
        public IHttpActionResult DeleteSubjectActionExpense(int id)
        {
            SubjectActionExpense subjectActionExpense = db.SubjectActionExpenses.Find(id);
            if (subjectActionExpense == null)
            {
                return NotFound();
            }

            db.SubjectActionExpenses.Remove(subjectActionExpense);
            db.SaveChanges();

            return Ok(subjectActionExpense);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubjectActionExpenseExists(int id)
        {
            return db.SubjectActionExpenses.Count(e => e.Id == id) > 0;
        }
    }
}