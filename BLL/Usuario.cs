using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Usuario
    {
        MpUsuario mpUsuario = new MpUsuario();
        GestionEventos gestionEventos = new GestionEventos();

        /// <summary>
        /// Aplica hashing SHA-256 a una cadena de texto.
        /// Utilizado para proteger contraseñas antes de compararlas o almacenarlas.
        /// </summary>
        public static string HashSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Realiza el proceso de login de un usuario:
        /// - Verifica la integridad del sistema con DVH/DVV.
        /// - Hashea la contraseña ingresada.
        /// - Busca el usuario por email y valida las credenciales.
        /// - Maneja intentos fallidos y bloqueo por seguridad.
        /// - Registra evento de login y reinicia contador de intentos si es exitoso.
        /// Devuelve true si se detectaron alteraciones de integridad en las tablas.
        /// </summary>
        public bool login(BE.Usuario usuarioALoguear, out List<BE.VerificacionResultadoClass> resultadoDV)
        {
            var verificador = new ServiceDV();
            resultadoDV = verificador.VerificarIntegridad();

            usuarioALoguear.Password = HashSHA256(usuarioALoguear.Password);
            BE.Usuario usuarioCargado = mpUsuario.BuscarPorEmail(usuarioALoguear.Email);

            if (!validarUsuario(usuarioALoguear, usuarioCargado))
            {
                usuarioCargado.Intentos++;
                if (usuarioCargado.Intentos > 3)
                {
                    bloquearUsuario(usuarioCargado);
                    throw new Exception("El usuario ha sido bloqueado por 3 intentos fallidos");
                }
                else
                {
                    mpUsuario.modificarUsuario(usuarioCargado);
                    verificador.RecalcularDVH();
                    throw new Exception("Credenciales inválidas");
                }
            }

            SessionManager.Login(usuarioCargado);
            gestionEventos.persistirEvento("Login", BE.Modulos.Users.ToString(), 3);
            usuarioCargado.Intentos = 0;
            mpUsuario.modificarUsuario(usuarioCargado);

            return resultadoDV.Any(r => r.RegistrosAlterados.Count > 0); // true si hay errores
        }

        /// <summary>
        /// Valida si las credenciales ingresadas coinciden con las del usuario cargado desde la base.
        /// También verifica si el usuario está bloqueado.
        /// </summary>
        private bool validarUsuario(BE.Usuario usuarioALoguear, BE.Usuario usuarioCargado)
        {
            if (usuarioCargado == null)
                throw new Exception("Usuario no encontrado");
            if (usuarioCargado.Bloqueo)
                throw new Exception("El usuario esta bloqueado");
            if (usuarioCargado.Password == usuarioALoguear.Password)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Bloquea al usuario marcándolo como bloqueado y actualizando su estado en la base de datos.
        /// </summary>
        private void bloquearUsuario(BE.Usuario usuarioCargado)
        {
            usuarioCargado.Bloqueo = true;
            mpUsuario.modificarUsuario(usuarioCargado);
        }

        /// <summary>
        /// Ejecuta el proceso de logout del sistema:
        /// - Registra el evento de cierre de sesión.
        /// - Cierra la sesión actual en el SessionManager.
        /// </summary>
        public void logout()
        {
            gestionEventos.persistirEvento("Logout", BE.Modulos.Users.ToString(), 3);
            SessionManager.Logout();
        }
    }
}
