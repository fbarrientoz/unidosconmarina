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
    using System.ComponentModel.DataAnnotations;

    public partial class Padron
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Padron()
        {
            this.HistorialContactoes = new HashSet<HistorialContacto>();
        }
    
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Required]
        [Display(Name = "Apellido Paterno")]
        public string paterno { get; set; }
        public string materno { get; set; }
        [Required]
        [Display(Name = "Apellido Paterno")]
        public string telefono { get; set; }
        public string celular { get; set; }
        public string direccion { get; set; }
        public string comentario { get; set; }
        public string rfc { get; set; }
        public string curp { get; set; }
        public string claveElectoral { get; set; }
        public int votante { get; set; }
        public string usuarioRegistro { get; set; }
        public string usuarioUpdate { get; set; }
        public Nullable<System.DateTime> fechaRegistro { get; set; }
        public Nullable<System.DateTime> fechaUpdate { get; set; }
        public string email { get; set; }
        public Nullable<bool> registroCompleto { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistorialContacto> HistorialContactoes { get; set; }
        public virtual TipoVotante TipoVotante { get; set; }
    }
}
