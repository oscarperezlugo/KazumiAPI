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
    public class ProveedorController : ApiController
    {
        private kazumiEntities db = new kazumiEntities();

        // GET: api/Proveedor
        public IQueryable<proveedor> Getproveedor()
        {
            return db.proveedor;
        }

        // GET: api/Proveedor/5
        [ResponseType(typeof(proveedor))]
        public async Task<IHttpActionResult> Getproveedor(int id)
        {
            proveedor proveedor = await db.proveedor.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return Ok(proveedor);
        }

        // PUT: api/Proveedor/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putproveedor(int id, proveedor proveedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != proveedor.id_proveedor)
            {
                return BadRequest();
            }

            db.Entry(proveedor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!proveedorExists(id))
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

        // POST: api/Proveedor
        [ResponseType(typeof(proveedor))]
        public async Task<IHttpActionResult> Postproveedor(proveedor proveedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.proveedor.Add(proveedor);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = proveedor.id_proveedor }, proveedor);
        }

        // DELETE: api/Proveedor/5
        [ResponseType(typeof(proveedor))]
        public async Task<IHttpActionResult> Deleteproveedor(int id)
        {
            proveedor proveedor = await db.proveedor.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            db.proveedor.Remove(proveedor);
            await db.SaveChangesAsync();

            return Ok(proveedor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool proveedorExists(int id)
        {
            return db.proveedor.Count(e => e.id_proveedor == id) > 0;
        }
    }
}