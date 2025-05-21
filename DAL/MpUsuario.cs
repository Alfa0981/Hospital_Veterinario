using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MpUsuario
    {
        Acceso acceso = new Acceso();

        public Usuario BuscarPorEmail(string email)
        {
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@email", email);

            DataTable tabla = acceso.leer(queries.UsuarioQuery.BuscarPorEmail, sqlParameters);
            if (tabla.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return ConvertirDataRowAUsuario(tabla.Rows[0]);
            }
        }
        private Usuario ConvertirDataRowAUsuario(DataRow row)
        {
            Usuario usuario = new Usuario
            {
                Id = Convert.ToInt32(row["id"]),
                Dni = row["dni"].ToString(),
                Apellido = row["apellido"].ToString(),
                Nombre = row["nombre"].ToString(),
                Password = row["password"].ToString(),
                Perfil = new Familia
                {
                    ID = Convert.ToInt32(row["perfil_id"]),
                    Nombre = row["perfil_nombre"].ToString(),
                    Tipo = "Perfil",
                },
                Email = row["email"].ToString(),
                Bloqueo = Convert.ToBoolean(row["isBlock"]),
                Intentos = Convert.ToInt32(row["intentos"]),
            };

            return usuario;
        }

        public void modificarUsuario(Usuario usuario)
        {
            SqlParameter[] sqlParameters = new SqlParameter[9];

            sqlParameters[0] = new SqlParameter("@dni", usuario.Dni);
            sqlParameters[1] = new SqlParameter("@apellido", usuario.Apellido);
            sqlParameters[2] = new SqlParameter("@nombre", usuario.Nombre);
            sqlParameters[3] = new SqlParameter("@perfil", usuario.Perfil.ID);
            sqlParameters[4] = new SqlParameter("@email", usuario.Email);
            sqlParameters[5] = new SqlParameter("@bloqueo", usuario.Bloqueo ? 1 : 0);
            sqlParameters[6] = new SqlParameter("@intentos", usuario.Intentos);
            sqlParameters[7] = new SqlParameter("@password", usuario.Password);
            sqlParameters[8] = new SqlParameter("@id", usuario.Id);

            acceso.escribir(queries.UsuarioQuery.ModificarUsuario, sqlParameters);
        }
    }
}
