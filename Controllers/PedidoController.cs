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
    public class PedidoController : ApiController
    {
        DocumentoController controller = new DocumentoController();

        public PedidoController()
        {
            //db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Pedido
        public IQueryable<documento> Getpedido()
        {
            return controller.db.documento.Where(x => x.codigo_tipo_documento.Equals("P"));
        }

        // GET: api/Pedido/5
        [ResponseType(typeof(documento))]
        public async Task<IHttpActionResult> Getpedido(int id)
        {
            return await controller.Getdocumento(id);
        }

        // PUT: api/Pedido/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putpedido(int id, documento documento)
        {
            return await controller.Putdocumento(id, documento);
        }

        // POST: api/Pedido
        [ResponseType(typeof(documento))]
        public async Task<IHttpActionResult> Postdocumento(documento documento)
        {
            return await controller.Postdocumento(documento);
        }

        // DELETE: api/Pedido/5
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
