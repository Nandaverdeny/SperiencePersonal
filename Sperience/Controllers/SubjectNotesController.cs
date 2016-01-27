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
    public class SubjectNotesController : ApiController
    {
        private SperienceEntities db = new SperienceEntities();

        // GET: api/SubjectNotes
        public IQueryable<SubjectNote> GetSubjectNotes()
        {
            return db.SubjectNotes;
        }

        // GET: api/SubjectNotes/5
        [ResponseType(typeof(SubjectNote))]
        public IHttpActionResult GetSubjectNote(int id)
        {
            SubjectNote subjectNote = db.SubjectNotes.Find(id);
            if (subjectNote == null)
            {
                return NotFound();
            }

            return Ok(subjectNote);
        }

        // PUT: api/SubjectNotes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubjectNote(int id, SubjectNote subjectNote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subjectNote.Id)
            {
                return BadRequest();
            }

            db.Entry(subjectNote).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectNoteExists(id))
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

        // POST: api/SubjectNotes
        [ResponseType(typeof(SubjectNote))]
        public IHttpActionResult PostSubjectNote(SubjectNote subjectNote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SubjectNotes.Add(subjectNote);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = subjectNote.Id }, subjectNote);
        }

        // DELETE: api/SubjectNotes/5
        [ResponseType(typeof(String))]
        public IHttpActionResult DeleteSubjectNote(int id)
        {
            SubjectNote subjectNote = db.SubjectNotes.Find(id);
            if (subjectNote == null)
            {
                return NotFound();
            }

            db.SubjectNotes.Remove(subjectNote);
            db.SaveChanges();

            return Ok("Note is deleted");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubjectNoteExists(int id)
        {
            return db.SubjectNotes.Count(e => e.Id == id) > 0;
        }
    }
}