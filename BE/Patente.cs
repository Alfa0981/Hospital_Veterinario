using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Patente : Perfil
    {
        public Patente(string nombre)
        {
            Nombre = nombre;
        }

        public override string Nombre { get; set; }

        public override void Agregar(Perfil componente)
        {
            throw new NotImplementedException("No se puede agregar un permiso a un permiso individual.");
        }

        public override void Remover(Perfil componente)
        {
            throw new NotImplementedException("No se puede remover un permiso de un permiso individual.");
        }

        public override bool TienePermiso(TipoPermiso permiso)
        {
            return Nombre == permiso.ToString();
        }
        public override bool EsFamilia => false;
        public override string ToString()
        {
            return Nombre;
        }
    }
}
