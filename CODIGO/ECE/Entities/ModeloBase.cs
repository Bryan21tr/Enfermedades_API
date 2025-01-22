using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TsaakAPI.Entities
{
    public class ModeloBase
    {
       public DateTime fecha_registro { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_actualizacion { get; set; }
    }
}