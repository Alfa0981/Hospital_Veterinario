using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Familia : Perfil
    {
        private readonly List<Perfil> _perfiles = new List<Perfil>();
        public Familia(string nombre)
        {
            Nombre = nombre;
        }

        public Familia() { }    
        public Familia(string nombre, params Perfil[] perfiles) : this(nombre)
        {
            _perfiles.AddRange(perfiles);
        }

        public override string Nombre { get; set; }

        public override void Agregar(Perfil componente)
        {
            _perfiles.Add(componente);
        }

        public override void Remover(Perfil componente)
        {
            _perfiles.Remove(componente);
        }

        public override bool TienePermiso(TipoPermiso permiso)
        {
            foreach (var componente in _perfiles)
            {
                if (componente.TienePermiso(permiso))
                {
                    return true;
                }
            }
            return false;
        }
        public List<Perfil> ObtenerSubFamilias()
        {
            return _perfiles;
        }
        public override bool EsFamilia => true;

        public override string ToString()
        {
            return Nombre;
        }

    }
}
