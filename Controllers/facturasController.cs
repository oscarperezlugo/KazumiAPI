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
using Kazumi_backend.Models;

namespace Kazumi_backend.Controllers
{
    public class facturasController : ApiController
    {
        private kasumiEntities db = new kasumiEntities();

        // GET: api/facturas
        public IQueryable<factura> Getfactura()
        {
            return db.factura;
        }

        // GET: api/facturas/5
        [ResponseType(typeof(factura))]
        public async Task<IHttpActionResult> Getfactura(int id)
        {
            factura factura = await db.factura.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }

            return Ok(factura);
        }

        // PUT: api/facturas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putfactura(int id, factura factura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != factura.id_factura)
            {
                return BadRequest();
            }

            db.Entry(factura).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!facturaExists(id))
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

        // POST: api/facturas
        [ResponseType(typeof(factura))]
        public async Task<IHttpActionResult> Postfactura(factura factura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.factura.Add(factura);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = factura.id_factura }, factura);
        }

        // DELETE: api/facturas/5
        [ResponseType(typeof(factura))]
        public async Task<IHttpActionResult> Deletefactura(int id)
        {
            factura factura = await db.factura.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }

            db.factura.Remove(factura);
            await db.SaveChangesAsync();

            return Ok(factura);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool facturaExists(int id)
        {
            return db.factura.Count(e => e.id_factura == id) > 0;
        }
    }
}