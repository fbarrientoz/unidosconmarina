//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UPM.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Banner
    {
        public int Id { get; set; }
        public string url_foto { get; set; }
        public bool estatus { get; set; }
        public byte[] link { get; set; }
        public string usuarioRegistro { get; set; }
        public string usuarioUpdate { get; set; }
        public Nullable<System.DateTime> fechaRegistro { get; set; }
        public Nullable<System.DateTime> fechaupdate { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
    }
}
