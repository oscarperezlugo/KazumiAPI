using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using KazumiAPI.Models;

namespace KazumiAPI.Controllers
{
    [Authorize]
    public class CargoController : ApiController
    {
        private kazumiEntities db = new kazumiEntities();

        // GET: api/Cargo
        public IQueryable<cargo> Getcargo()
        {
            return db.cargo;
        }

        // GET: api/Cargo/5
        [ResponseType(typeof(cargo))]
        public async Task<IHttpActionResult> Getcargo(int id)
        {
            cargo cargo = await db.cargo.FindAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }

            return Ok(cargo);
        }

        // PUT: api/Cargo/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcargo(int id, cargo cargo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cargo.id_cargo)
            {
                return BadRequest();
            }

            db.Entry(cargo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!cargoExists(id))
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

        // POST: api/Cargo
        [ResponseType(typeof(cargo))]
        public async Task<IHttpActionResult> Postcargo(cargo cargo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.cargo.Add(cargo);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cargo.id_cargo }, cargo);
        }

        // DELETE: api/Cargo/5
        [ResponseType(typeof(cargo))]
        public async Task<IHttpActionResult> Deletecargo(int id)
        {
            cargo cargo = await db.cargo.FindAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }

            db.cargo.Remove(cargo);
            await db.SaveChangesAsync();

            return Ok(cargo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool cargoExists(int id)
        {
            return db.cargo.Count(e => e.id_cargo == id) > 0;
        }
    }
}