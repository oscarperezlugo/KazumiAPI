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
    public class FacturaProductoController : ApiController
    {
        private kazumiEntities db = new kazumiEntities();

        //// GET: api/FacturaProducto
        //public IQueryable<documento_producto> Getdocumento_producto()
        //{
        //    return db.documento_producto;
        //}

        // GET: api/FacturaProducto/5
        [ResponseType(typeof(documento_producto))]
        public List<documento_producto> Getdocumento_producto(int id)
        {

            var x= db.getDocumentoProducto(id).ToList();
            //if (documento_productos == null)
            //{
            //    return NotFound();
            //}

            return x;
        }

        // PUT: api/FacturaProducto/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putdocumento_producto(int id, documento_producto documento_producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != documento_producto.id_documento_producto)
            {
                return BadRequest();
            }

            db.Entry(documento_producto).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!documento_productoExists(id))
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

        // POST: api/FacturaProducto
        [ResponseType(typeof(documento_producto))]
        public async Task<IHttpActionResult> Postdocumento_producto(documento_producto documento_producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.documento_producto.Add(documento_producto);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = documento_producto.id_documento_producto }, documento_producto);
        }

        // DELETE: api/FacturaProducto/5
        [ResponseType(typeof(documento_producto))]
        public async Task<IHttpActionResult> Deletedocumento_producto(int id)
        {
            documento_producto documento_producto = await db.documento_producto.FindAsync(id);
            if (documento_producto == null)
            {
                return NotFound();
            }

            db.documento_producto.Remove(documento_producto);
            await db.SaveChangesAsync();

            return Ok(documento_producto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool documento_productoExists(int id)
        {
            return db.documento_producto.Count(e => e.id_documento_producto == id) > 0;
        }
    }
}