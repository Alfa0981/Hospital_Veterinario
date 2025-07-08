using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MpEvento
    {
        Acceso acceso = new Acceso();

        /// <summary>
        /// Persiste un evento en la base de datos utilizando parámetros SQL.
        /// Registra criticidad, módulo, usuario asociado y descripción.
        /// </summary>
        public void persitirEvento(BE.Evento evento)
        {
            SqlParameter[] sqlParameters = new SqlParameter[4];

            sqlParameters[0] = new SqlParameter("@criticidad", evento.Criticidad);
            sqlParameters[1] = new SqlParameter("@modulo", evento.Modulo);
            sqlParameters[2] = new SqlParameter("@usuario_Id", evento.Usuario.Id);
            sqlParameters[3] = new SqlParameter("@descripcion", evento.Descripcion);

            acceso.escribir(queries.EventoQuery.Insertar, sqlParameters);
        }

        /// <summary>
        /// Obtiene todos los eventos registrados en la base de datos,
        /// y los convierte en una lista de objetos de tipo Evento.
        /// </summary>
        public List<BE.Evento> obtenerTodos()
        {
            DataTable table = acceso.leer(queries.EventoQuery.SeleccionarTodos, null);
            List<Evento> eventos = new List<Evento>();

            foreach (DataRow row in table.Rows)
            {
                eventos.Add(ConvertirDataRowAEvento(row));
            }
            return eventos;
        }

        /// <summary>
        /// Convierte una fila del DataTable en un objeto Evento,
        /// incluyendo datos del usuario que generó el evento.
        /// </summary>
        private Evento ConvertirDataRowAEvento(DataRow row)
        {
            Evento evento = new Evento()
            {
                Id = Convert.ToInt32(row["id"]),
                Fecha = Convert.ToDateTime(row["fecha"]),
                Hora = TimeSpan.Parse(row["hora"].ToString()),
                Modulo = row["modulo"].ToString(),
                Descripcion = row["descripcion"].ToString(),
                Criticidad = Convert.ToInt32(row["criticidad"]),
                Usuario = new Usuario()
                {
                    Nombre = row["nombre"].ToString(),
                    Apellido = row["apellido"].ToString(),
                    Email = row["email"].ToString()
                }
            };

            return evento;
        }
    }
}
