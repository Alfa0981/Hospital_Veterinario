using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class DV
    {
        public string Tabla { get; set; }
        public int IdRegistro { get; set; } // o la clave primaria del registro
        public long DVH { get; set; }
    }

    public class VerificacionResultadoClass
    {
        public string Tabla { get; set; }
        public List<int> RegistrosAlterados { get; set; } = new List<int>();
        public bool TablaIntegra => RegistrosAlterados.Count == 0;
        public List<ColumnaAlterada> ColumnasAlteradas { get; set; } = new List<ColumnaAlterada>();
    }
    public class ColumnaAlterada
    {
        public int IdRegistro { get; set; }
        public string Columna { get; set; }
    }
}
