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
    public class DocumentoController : ApiController
    {
        public kazumiEntities db = new kazumiEntities();

        // GET: api/Documento
        public IQueryable<documento> Getdocumento()
        {
            return db.documento;
        }

        //[ResponseType(typeof(documento))]
        //public IQueryable<documento> Getdocumento1(string tipo = "")
        //{
        //    if(tipo == "")
        //        return db.documento;

        //    return db.documento.Where(x => x.codigo_tipo_documento.Equals(tipo));
        //}

        // GET: api/Documento/5
        [ResponseType(typeof(documento))]
        public async Task<IHttpActionResult> Getdocumento(int id)
        {
            documento documento = await db.documento.FindAsync(id);
            if (documento == null)
            {
                return NotFound();
            }

            return Ok(documento);
        }

        // PUT: api/Documento/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putdocumento(int id, documento documento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != documento.id_documento)
            {
                return BadRequest();
            }

            db.Entry(documento).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!documentoExists(id))
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

        // POST: api/Documento
        [ResponseType(typeof(documento))]
        public async Task<IHttpActionResult> Postdocumento(documento documento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.documento.Add(documento);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = documento.id_documento }, documento);
        }

        // DELETE: api/Documento/5
        [ResponseType(typeof(documento))]
        public async Task<IHttpActionResult> Deletedocumento(int id)
        {
            documento documento = await db.documento.FindAsync(id);
            if (documento == null)
            {
                return NotFound();
            }

            db.documento.Remove(documento);
            await db.SaveChangesAsync();

            return Ok(documento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool documentoExists(int id)
        {
            return db.documento.Count(e => e.id_documento == id) > 0;
        }
    }
}