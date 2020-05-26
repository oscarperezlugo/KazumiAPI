using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KazumiAPI.Models
{
    public class UsuarioInfo
    {
        public Guid Uid { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}