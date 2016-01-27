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
    public class StagesController : ApiController
    {
        private SperienceEntities db = new SperienceEntities();

        // GET: api/Stages
        public IQueryable<Stage> GetStages()
        {
            return db.Stages;
        }

        // GET: api/Stages/5
        [ResponseType(typeof(Stage))]
        public IHttpActionResult GetStage(int id)
        {
            Stage stage = db.Stages.Find(id);
            if (stage == null)
            {
                return NotFound();
            }

            return Ok(stage);
        }

        // PUT: api/Stages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStage(int id, Stage stage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stage.Id)
            {
                return BadRequest();
            }

            db.Entry(stage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StageExists(id))
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

        // POST: api/Stages
        [ResponseType(typeof(Stage))]
        public IHttpActionResult PostStage(Stage stage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stages.Add(stage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = stage.Id }, stage);
        }

        // DELETE: api/Stages/5
        [ResponseType(typeof(Stage))]
        public IHttpActionResult DeleteStage(int id)
        {
            Stage stage = db.Stages.Find(id);
            if (stage == null)
            {
                return NotFound();
            }

            db.Stages.Remove(stage);
            db.SaveChanges();

            return Ok(stage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StageExists(int id)
        {
            return db.Stages.Count(e => e.Id == id) > 0;
        }
    }
}