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
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class MoradorController : ApiController
    {
        private GestaoCondominioEntities db = new GestaoCondominioEntities();

        // GET: api/Morador
        public IQueryable<morador> Getmorador()
        {
            return db.morador;
        }

        // GET: api/Morador/5
        [ResponseType(typeof(morador))]
        public IHttpActionResult Getmorador(int id)
        {
            morador morador = db.morador.Find(id);
            if (morador == null)
            {
                return NotFound();
            }

            return Ok(morador);
        }

        // PUT: api/Morador/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putmorador(int id, morador morador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != morador.id)
            {
                return BadRequest();
            }

            db.Entry(morador).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!moradorExists(id))
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

        // POST: api/Morador
        [ResponseType(typeof(morador))]
        public IHttpActionResult Postmorador(morador morador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.morador.Add(morador);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = morador.id }, morador);
        }

        // DELETE: api/Morador/5
        [ResponseType(typeof(morador))]
        public IHttpActionResult Deletemorador(int id)
        {
            morador morador = db.morador.Find(id);
            if (morador == null)
            {
                return NotFound();
            }

            db.morador.Remove(morador);
            db.SaveChanges();

            return Ok(morador);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool moradorExists(int id)
        {
            return db.morador.Count(e => e.id == id) > 0;
        }

    }
}