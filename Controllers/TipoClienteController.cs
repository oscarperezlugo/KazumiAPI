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
    public class TipoClienteController : ApiController
    {
        private kazumiEntities db = new kazumiEntities();

        // GET: api/TipoCliente
        public IQueryable<tipo_cliente> Gettipo_cliente()
        {
            return db.tipo_cliente;
        }

        // GET: api/TipoCliente/5
        [ResponseType(typeof(tipo_cliente))]
        public async Task<IHttpActionResult> Gettipo_cliente(int id)
        {
            tipo_cliente tipo_cliente = await db.tipo_cliente.FindAsync(id);
            if (tipo_cliente == null)
            {
                return NotFound();
            }

            return Ok(tipo_cliente);
        }

        // PUT: api/TipoCliente/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttipo_cliente(int id, tipo_cliente tipo_cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipo_cliente.id_tipo_cliente)
            {
                return BadRequest();
            }

            db.Entry(tipo_cliente).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tipo_clienteExists(id))
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

        // POST: api/TipoCliente
        [ResponseType(typeof(tipo_cliente))]
        public async Task<IHttpActionResult> Posttipo_cliente(tipo_cliente tipo_cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tipo_cliente.Add(tipo_cliente);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tipo_cliente.id_tipo_cliente }, tipo_cliente);
        }

        // DELETE: api/TipoCliente/5
        [ResponseType(typeof(tipo_cliente))]
        public async Task<IHttpActionResult> Deletetipo_cliente(int id)
        {
            tipo_cliente tipo_cliente = await db.tipo_cliente.FindAsync(id);
            if (tipo_cliente == null)
            {
                return NotFound();
            }

            db.tipo_cliente.Remove(tipo_cliente);
            await db.SaveChangesAsync();

            return Ok(tipo_cliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tipo_clienteExists(int id)
        {
            return db.tipo_cliente.Count(e => e.id_tipo_cliente == id) > 0;
        }
    }
}