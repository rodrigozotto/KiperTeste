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
    public class ApartamentoController : ApiController
    {
        private GestaoCondominioEntities db = new GestaoCondominioEntities();

        // GET: api/Apartamento
        public IQueryable<apartamento> Getapartamento()
        {
            return db.apartamento;
        }

        // GET: api/Apartamento/5
        [ResponseType(typeof(apartamento))]
        public IHttpActionResult Getapartamento(int id)
        {
            apartamento apartamento = db.apartamento.Find(id);
            if (apartamento == null)
            {
                return NotFound();
            }

            return Ok(apartamento);
        }

        // PUT: api/Apartamento/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putapartamento(int id, apartamento apartamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != apartamento.id)
            {
                return BadRequest();
            }

            db.Entry(apartamento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!apartamentoExists(id))
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

        // POST: api/Apartamento
        [ResponseType(typeof(apartamento))]
        public IHttpActionResult Postapartamento(apartamento apartamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.apartamento.Add(apartamento);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = apartamento.id }, apartamento);
        }

        // DELETE: api/Apartamento/5
        [ResponseType(typeof(apartamento))]
        public IHttpActionResult Deleteapartamento(int id)
        {
            apartamento apartamento = db.apartamento.Find(id);
            if (apartamento == null)
            {
                return NotFound();
            }

            db.apartamento.Remove(apartamento);
            db.SaveChanges();

            return Ok(apartamento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool apartamentoExists(int id)
        {
            return db.apartamento.Count(e => e.id == id) > 0;
        }

        
        // GET: api/Apartamento/Moradores/{id_apartamenmto}
        
        [Route("api/apartamento/moradores/")]
        [HttpGet]
        public IHttpActionResult moradores(int id)
        {
            IEnumerable<morador> lstMorador = db.morador.Where(p => p.id_apartamento == id).ToList<morador>();
            if (lstMorador == null)
            {
                return NotFound();
            }
            return Ok(lstMorador);
        }
    }
}