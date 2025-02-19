﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KazumiAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class kazumiEntities : DbContext
    {
        public kazumiEntities()
            : base("name=kazumiEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<cliente> cliente { get; set; }
        public virtual DbSet<documento> documento { get; set; }
        public virtual DbSet<documento_producto> documento_producto { get; set; }
        public virtual DbSet<producto> producto { get; set; }
        public virtual DbSet<proveedor> proveedor { get; set; }
        public virtual DbSet<tipo_cliente> tipo_cliente { get; set; }
        public virtual DbSet<tipo_documento> tipo_documento { get; set; }
        public virtual DbSet<empleado> empleado { get; set; }
        public virtual DbSet<usuario> usuario { get; set; }
        public virtual DbSet<cargo> cargo { get; set; }
    
        public virtual ObjectResult<documento_producto> getDocumentoProducto(Nullable<int> numero)
        {
            var numeroParameter = numero.HasValue ?
                new ObjectParameter("numero", numero) :
                new ObjectParameter("numero", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<documento_producto>("getDocumentoProducto", numeroParameter);
        }
    
        public virtual ObjectResult<documento_producto> getDocumentoProducto(Nullable<int> numero, MergeOption mergeOption)
        {
            var numeroParameter = numero.HasValue ?
                new ObjectParameter("numero", numero) :
                new ObjectParameter("numero", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<documento_producto>("getDocumentoProducto", mergeOption, numeroParameter);
        }
    }
}
