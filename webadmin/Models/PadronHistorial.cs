using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UPM.Entities;

namespace webadmin.Models
{
    public class PadronHistorial
    {
        public Padron padron { get; set; }
        public IEnumerable<HistorialContacto> historial { get; set; }
    }
}