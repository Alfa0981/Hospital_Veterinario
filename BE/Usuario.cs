using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Usuario
    {
        private int id;
        private string dni;
        private string apellido;
        private string nombre;
        private string password;
        private string email;
        private bool bloqueo; //por defecto 0
        private Perfil perfil;
        private int intentos;

        public Usuario (string password, string email)
        {
            this.password = password;
            this.email = email;
            this.bloqueo = false;
        }

        public int Intentos
        {
            get { return intentos; }
            set { intentos = value; }
        }


        public Perfil Perfil
        {
            get { return perfil; }
            set { perfil = value; }
        }

        public Usuario()
        {
            this.Bloqueo = false;
        }

        public bool Bloqueo
        {
            get { return bloqueo; }
            set { bloqueo = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }

        public string Dni
        {
            get { return dni; }
            set { dni = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}
