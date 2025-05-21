using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public abstract class Perfil
    {
        public abstract string Nombre { get; set; }
        public abstract bool EsFamilia { get; }
        public string Tipo { get; set; }
        public int ID { get; set; }
        public abstract void Agregar(Perfil perfil);
        public abstract void Remover(Perfil perfil);
        public abstract bool TienePermiso(TipoPermiso permiso);
    }
}
