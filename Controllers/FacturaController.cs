using KazumiAPI.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace KazumiAPI.Controllers
{
    [Authorize]
    public class FacturaController : ApiController
    {
        DocumentoController controller = new DocumentoController();

        public FacturaController()
        {
            //db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Factura
        public IQueryable<documento> Getfactura()
        {
            return controller.db.documento.Where(x => x.codigo_tipo_documento.Equals("F"));
        }

        // GET: api/Factura/5
        [ResponseType(typeof(documento))]
        public async Task<IHttpActionResult> Getfactura(int id)
        {
            return await controller.Getdocumento(id);
        }

        // PUT: api/Factura/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putfactura(int id, documento documento)
        {
            return await controller.Putdocumento(id, documento);
        }

        // POST: api/Factura
        [ResponseType(typeof(documento))]
        public async Task<IHttpActionResult> Postdocumento(documento documento)
        {
            return await controller.Postdocumento(documento);
        }

        // DELETE: api/Factura/5
        [ResponseType(typeof(documento))]
        public async Task<IHttpActionResult> Deletedocumento(int id)
        {
            return await controller.Deletedocumento(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                controller.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
