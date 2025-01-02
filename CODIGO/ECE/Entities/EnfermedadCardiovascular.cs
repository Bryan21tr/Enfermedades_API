using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TsaakAPI.Entities
{
    [Table("tc_enfermedad_cardiovascular")]
    public class EnfermedadCardiovascular
    {
       

        public int id_enf_cardiovascular { get; set; }
        public string? nombre { get; set; }
        public string? descripcion { get; set; }
        public DateTime fecha_registro { get; set; }
        public DateTime fecha_inicio { get; set; }
        public bool estado { get; set; }
        public DateTime fecha_actualizacion { get; set; }
    }
}