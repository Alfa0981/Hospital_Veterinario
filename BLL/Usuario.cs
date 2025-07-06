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

        // Hashing de la contraseña
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

        /*public void login(BE.Usuario usuarioALoguear)
        {

            usuarioALoguear.Password = HashSHA256(usuarioALoguear.Password);
            BE.Usuario usuarioCargado = mpUsuario.BuscarPorEmail(usuarioALoguear.Email);

           
            if (validarUsuario(usuarioALoguear, usuarioCargado))
            {
                //Login exitoso
                SessionManager.Login(usuarioCargado);
                gestionEventos.persistirEvento("Login", BE.Modulos.Users.ToString(), 3);
                usuarioCargado.Intentos = 0;
                mpUsuario.modificarUsuario(usuarioCargado);

                var verificador = new ServiceDV();
                var resultado = verificador.VerificarIntegridad();
                var inconsistencias = resultado
                    .Where(r => r.RegistrosAlterados.Count > 0)
                    .Select(r => $"Tabla: {r.Tabla}, Registros alterados: {string.Join(", ", r.RegistrosAlterados)}")
                    .ToList();

                var detallesColumnas = new List<string>();

                foreach (var r in resultado.Where(r => r.RegistrosAlterados.Count > 0))
                {
                    if (r.Tabla != "Usuarios" && r.Tabla != "Mascotas")
                        continue; // ignorar cualquier tabla inesperada
                    foreach (var ca in r.ColumnasAlteradas)
                    {
                        detallesColumnas.Add($"Tabla: {r.Tabla}, Id: {ca.IdRegistro}, Columna: {ca.Columna}");
                    }
                }

                if (inconsistencias.Any())
                {
                    string mensaje = "⚠️ Se detectaron alteraciones en los datos:\n" +
                                     string.Join("\n", inconsistencias) + "\n\n" +
                                     "🧬 Detalles por columna:\n" +
                                     string.Join("\n", detallesColumnas);

                    SessionManager.Logout(); // salir por seguridad
                    throw new Exception(mensaje);
                }
            }
            else
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
                    throw new Exception("Credenciales invalidas");
                }
            }
        }*/

        /*public void login(BE.Usuario usuarioALoguear)
        {
            var verificador = new ServiceDV();
            var resultado = verificador.VerificarIntegridad();

            bool hayErrores = resultado.Any(r => r.RegistrosAlterados.Count > 0);
            // Paso 2: Login normal si todo está bien
            usuarioALoguear.Password = HashSHA256(usuarioALoguear.Password);
            BE.Usuario usuarioCargado = mpUsuario.BuscarPorEmail(usuarioALoguear.Email);

            if (validarUsuario(usuarioALoguear, usuarioCargado))
            {
                SessionManager.Login(usuarioCargado);
                gestionEventos.persistirEvento("Login", BE.Modulos.Users.ToString(), 3);
                usuarioCargado.Intentos = 0;
                mpUsuario.modificarUsuario(usuarioCargado);

            }else
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
                    throw new Exception("Credenciales inválidas");
                }
            }
           
            if (hayErrores)
            {
                // Guardar detalles si querés mostrarlos en el FormRepararDV
                SessionManager.SetDVResultados(resultado);

                // Mostrar el formulario de reparación
                

                return; // interrumpir login
            }
            
        }*/

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

        private void bloquearUsuario(BE.Usuario usuarioCargado)
        {
            usuarioCargado.Bloqueo = true;
            mpUsuario.modificarUsuario(usuarioCargado);
        }

        public void logout()
        {
            gestionEventos.persistirEvento("Logout", BE.Modulos.Users.ToString(), 3);
            SessionManager.Logout();
        }
    }
}
