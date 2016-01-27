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
    public class SubjectCategoriesController : ApiController
    {
        private SperienceEntities db = new SperienceEntities();

        // GET: api/SubjectCategories
        public IQueryable<SubjectCategory> GetSubjectCategories()
        {
            return db.SubjectCategories;
        }

        // GET: api/SubjectCategories/5
        [ResponseType(typeof(SubjectCategory))]
        public IHttpActionResult GetSubjectCategory(int id)
        {
            SubjectCategory subjectCategory = db.SubjectCategories.Find(id);
            if (subjectCategory == null)
            {
                return NotFound();
            }

            return Ok(subjectCategory);
        }

        // PUT: api/SubjectCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubjectCategory(int id, SubjectCategory subjectCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subjectCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(subjectCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectCategoryExists(id))
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

        // POST: api/SubjectCategories
        [ResponseType(typeof(SubjectCategory))]
        public IHttpActionResult PostSubjectCategory(SubjectCategory subjectCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SubjectCategories.Add(subjectCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = subjectCategory.Id }, subjectCategory);
        }

        // DELETE: api/SubjectCategories/5
        [ResponseType(typeof(SubjectCategory))]
        public IHttpActionResult DeleteSubjectCategory(int id)
        {
            SubjectCategory subjectCategory = db.SubjectCategories.Find(id);
            if (subjectCategory == null)
            {
                return NotFound();
            }

            db.SubjectCategories.Remove(subjectCategory);
            db.SaveChanges();

            return Ok(subjectCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubjectCategoryExists(int id)
        {
            return db.SubjectCategories.Count(e => e.Id == id) > 0;
        }
    }
}