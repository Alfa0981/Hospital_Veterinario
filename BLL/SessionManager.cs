using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BLL
{
    public class SessionManager
    {
        private static SessionManager session;
        private BE.Usuario usuario;

        public BE.Usuario Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }


        public static SessionManager GetInstance
        {
            get
            {
                return session;
            }
        }
        public static void Login(BE.Usuario usuario)
        {
            if (session == null)
            {
                session = new SessionManager();
                session.Usuario = usuario;
            }
            else
            {
                throw new Exception("Sesión ya iniciada");
            }
        }

        public static void Logout()
        {
            if (session != null)
            {
                session = null;
            }
        }
    }
}
