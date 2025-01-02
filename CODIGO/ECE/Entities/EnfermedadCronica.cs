using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TsaakAPI.Entities
{
    [Table("tc_enfermedad_cronica")]
    public class EnfermedadCronica
    {
       

        public int id_enf_cronica { get; set; }
        public string? nombre { get; set; }
        public string? descripcion { get; set; }
        public DateOnly fecha_registro { get; set; }
        public DateOnly fecha_inicio { get; set; }
        public bool estado { get; set; }
        public DateOnly fecha_actualizacion { get; set; }
    }
}