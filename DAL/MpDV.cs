using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MpDV
    {
        private Acceso acceso = new Acceso();

        /// <summary>
        /// Devuelve todos los registros de la tabla indicada, validando previamente
        /// que se trate de una tabla permitida (Usuarios o Mascotas).
        /// </summary>
        public DataTable ObtenerTablaCompleta(string tabla)
        {
            if (string.IsNullOrWhiteSpace(tabla))
                throw new ArgumentException("El nombre de la tabla no puede ser nulo ni vacío.");

            // Validación contra un listado explícito de tablas permitidas
            var tablasPermitidas = new List<string> { "Usuarios", "Mascotas" };
            if (!tablasPermitidas.Contains(tabla))
                throw new ArgumentException($"La tabla '{tabla}' no está permitida para verificación.");
            string query = $"select * from {tabla}";
            return acceso.leer(query, null);
        }

        /// <summary>
        /// Elimina todos los registros de la tabla DVH asociada a la entidad indicada,
        /// eliminando así los DVH globales almacenados previamente.
        /// </summary>
        public void EliminarDVHsExistentes(string tabla)
        {
            string sql = $"DELETE FROM DVH_{tabla}";
            acceso.escribir(sql, null);
        }

        /// <summary>
        /// Obtiene todos los DVH globales registrados para una tabla específica.
        /// Devuelve una lista con el ID del registro y su DVH correspondiente.
        /// </summary>
        public List<BE.DV> ObtenerDVHs(string tabla)
        {
            var lista = new List<BE.DV>();
            string query = $"SELECT IdRegistro, DVH FROM DVH_{tabla}";
            var dt = acceso.leer(query, null);

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new BE.DV
                {
                    Tabla = tabla,
                    IdRegistro = Convert.ToInt32(row["IdRegistro"]),
                    DVH = Convert.ToInt64(row["DVH"])
                });
            }
            return lista;
        }

        /// <summary>
        /// Actualiza el valor del DVH global de un registro ya existente en la tabla DVH.
        /// </summary>
        public void GuardarDVH(string tabla, int idRegistro, long dvh)
        {
            string query = $"UPDATE DVH_{tabla} SET DVH = @dvh WHERE IdRegistro = @id";
            SqlParameter[] parametros =
            {
                new SqlParameter("@dvh", dvh),
                new SqlParameter("@id", idRegistro)
            };
            acceso.escribir(query, parametros);
        }

        /// <summary>
        /// Inserta un nuevo DVH global para un registro de la tabla indicada.
        /// </summary>
        public void InsertarDVH(string tabla, int idRegistro, long dvh)
        {
            string query = $"INSERT INTO DVH_{tabla}(IdRegistro, DVH) VALUES(@id, @dvh)";
            SqlParameter[] parametros =
            {
                new SqlParameter("@dvh", dvh),
                new SqlParameter("@id", idRegistro)
            };
            acceso.escribir(query, parametros);
        }

        /// <summary>
        /// Inserta un DVH asociado a una columna específica de un registro en la tabla DVH_Detalle.
        /// Permite almacenar DVH por campo para verificar alteraciones puntuales.
        /// </summary>
        public void InsertarDVHPorCampo(string tabla, int idRegistro, string columna, long dvh)
        {
            string query = $"INSERT INTO DVH_Detalle_{tabla}(IdRegistro, Columna, DVH) VALUES(@id, @columna, @dvh)";
            SqlParameter[] parametros =
            {
                new SqlParameter("@id", idRegistro),
                new SqlParameter("@columna", columna),
                new SqlParameter("@dvh", dvh)
            };
            acceso.escribir(query, parametros);
        }

        /// <summary>
        /// Devuelve los DVH por columna para un registro específico de la tabla indicada.
        /// Retorna un diccionario con el nombre de la columna como clave y el DVH esperado como valor.
        /// </summary>
        public Dictionary<string, long> ObtenerDVHPorCampo(string tabla, int idRegistro)
        {
            string query = $"SELECT Columna, DVH FROM DVH_Detalle_{tabla} WHERE IdRegistro = @id";
            SqlParameter[] parametros = { new SqlParameter("@id", idRegistro) };
            DataTable dt = acceso.leer(query, parametros);

            return dt.AsEnumerable().ToDictionary(
                row => row.Field<string>("Columna"),
                row => row.Field<long>("DVH"));
        }

        /// <summary>
        /// Elimina todos los DVH por campo de la tabla DVH_Detalle correspondiente a la entidad indicada.
        /// </summary>
        public void EliminarDVHPorCampo(string tabla)
        {
            string query = $"DELETE FROM DVH_Detalle_{tabla}";
            acceso.escribir(query, null);
        }
    }
}
