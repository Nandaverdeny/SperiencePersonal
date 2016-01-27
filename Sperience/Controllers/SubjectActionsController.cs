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
    public class SubjectActionsController : ApiController
    {
        private SperienceEntities db = new SperienceEntities();

        // GET: api/SubjectActions
        public IQueryable<SubjectAction> GetSubjectActions()
        {
            return db.SubjectActions;
        }

        // GET: api/SubjectActions/5
        [ResponseType(typeof(SubjectAction))]
        public IHttpActionResult GetSubjectAction(int id)
        {
            SubjectAction subjectAction = db.SubjectActions.Find(id);
            if (subjectAction == null)
            {
                return NotFound();
            }

            return Ok(subjectAction);
        }

        // PUT: api/SubjectActions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubjectAction(int id, SubjectAction subjectAction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subjectAction.Id)
            {
                return BadRequest();
            }

            db.Entry(subjectAction).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectActionExists(id))
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

        // POST: api/SubjectActions
        [ResponseType(typeof(SubjectAction))]
        public IHttpActionResult PostSubjectAction(SubjectAction subjectAction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SubjectActions.Add(subjectAction);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = subjectAction.Id }, subjectAction);
        }

        // DELETE: api/SubjectActions/5
        [ResponseType(typeof(SubjectAction))]
        public IHttpActionResult DeleteSubjectAction(int id)
        {
            SubjectAction subjectAction = db.SubjectActions.Find(id);
            if (subjectAction == null)
            {
                return NotFound();
            }

            db.SubjectActions.Remove(subjectAction);
            db.SaveChanges();

            return Ok(subjectAction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubjectActionExists(int id)
        {
            return db.SubjectActions.Count(e => e.Id == id) > 0;
        }
    }
}