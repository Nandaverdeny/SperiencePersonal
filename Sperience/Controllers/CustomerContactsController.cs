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
    public class CustomerContactsController : ApiController
    {
        private SperienceEntities db = new SperienceEntities();

        // GET: api/CustomerContacts
        public IQueryable<CustomerContact> GetCustomerContacts()
        {
            return db.CustomerContacts;
        }

        // GET: api/CustomerContacts/5
        [ResponseType(typeof(CustomerContact))]
        public IHttpActionResult GetCustomerContact(int id)
        {
            CustomerContact customerContact = db.CustomerContacts.Find(id);
            if (customerContact == null)
            {
                return NotFound();
            }

            return Ok(customerContact);
        }

        // PUT: api/CustomerContacts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomerContact(int id, CustomerContact customerContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerContact.Id)
            {
                return BadRequest();
            }

            db.Entry(customerContact).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerContactExists(id))
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

        // POST: api/CustomerContacts
        [ResponseType(typeof(CustomerContact))]
        public IHttpActionResult PostCustomerContact(CustomerContact customerContact)
        {
            customerContact.CompanyId = 1;
            customerContact.LocationId = 1;
            customerContact.Createdby = 1;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CustomerContacts.Add(customerContact);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customerContact.Id }, customerContact);
        }

        // DELETE: api/CustomerContacts/5
        [ResponseType(typeof(CustomerContact))]
        public IHttpActionResult DeleteCustomerContact(int id)
        {
            CustomerContact customerContact = db.CustomerContacts.Find(id);
            if (customerContact == null)
            {
                return NotFound();
            }

            db.CustomerContacts.Remove(customerContact);
            db.SaveChanges();

            return Ok(customerContact);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerContactExists(int id)
        {
            return db.CustomerContacts.Count(e => e.Id == id) > 0;
        }
    }
}