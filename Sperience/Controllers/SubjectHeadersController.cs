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
    public class SubjectHeadersController : ApiController
    {
        private SperienceEntities db = new SperienceEntities();

        // GET: api/SubjectHeaders
        public IQueryable<SubjectHeader> GetSubjectHeaders()
        {
            return db.SubjectHeaders;
        }

        // GET: api/SubjectHeaders/5
        [ResponseType(typeof(SubjectHeader))]
        public IHttpActionResult GetSubjectHeader(int id)
        {
            SubjectHeader subjectHeader = db.SubjectHeaders.Find(id);
            if (subjectHeader == null)
            {
                return NotFound();
            }

            return Ok(subjectHeader);
        }

        // PUT: api/SubjectHeaders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubjectHeader(int id, SubjectHeader subjectHeader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subjectHeader.Id)
            {
                return BadRequest();
            }

            db.Entry(subjectHeader).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectHeaderExists(id))
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

        // POST: api/SubjectHeaders
        [ResponseType(typeof(SubjectHeader))]
        public IHttpActionResult PostSubjectHeader(SubjectHeader subjectHeader)
        {
            subjectHeader.StageId = 2;
            subjectHeader.Createdby = 1;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SubjectHeaders.Add(subjectHeader);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = subjectHeader.Id }, subjectHeader);
        }

        // DELETE: api/SubjectHeaders/5
        [ResponseType(typeof(SubjectHeader))]
        public IHttpActionResult DeleteSubjectHeader(int id)
        {
            SubjectHeader subjectHeader = db.SubjectHeaders.Find(id);
            if (subjectHeader == null)
            {
                return NotFound();
            }

            db.SubjectHeaders.Remove(subjectHeader);
            db.SaveChanges();

            return Ok(subjectHeader);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubjectHeaderExists(int id)
        {
            return db.SubjectHeaders.Count(e => e.Id == id) > 0;
        }
    }
}