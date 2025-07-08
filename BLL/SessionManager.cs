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
        public static List<BE.VerificacionResultadoClass> DVResultados { get; private set; }

        /// <summary>
        /// Propiedad pública para acceder al usuario actualmente autenticado en la sesión.
        /// </summary>
        public BE.Usuario Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        /// <summary>
        /// Devuelve la instancia actual de la sesión.
        /// Si no hay sesión activa, devuelve null.
        /// </summary>
        public static SessionManager GetInstance
        {
            get
            {
                return session;
            }
        }

        /// <summary>
        /// Inicia una nueva sesión para el usuario especificado.
        /// Si ya existe una sesión activa, lanza una excepción.
        /// </summary>
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

        /// <summary>
        /// Finaliza la sesión actual, eliminando la instancia activa.
        /// </summary>
        public static void Logout()
        {
            if (session != null)
            {
                session = null;
            }
        }

        /// <summary>
        /// Asocia a la sesión actual los resultados de verificación de integridad (DV).
        /// Permite tener acceso global a los errores detectados durante el login.
        /// </summary>
        public static void SetDVResultados(List<BE.VerificacionResultadoClass> resultados)
        {
            DVResultados = resultados;
        }
    }
}
