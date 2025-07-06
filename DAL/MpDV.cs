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
        public void EliminarDVHsExistentes(string tabla)
        {
            string sql = $"DELETE FROM DVH_{tabla}";
            acceso.escribir(sql, null);
        }

        public void EliminarDVVsExistentes(string tabla)
        {
            string sql = $"DELETE FROM DVV_{tabla}";
            acceso.escribir(sql, null);
        }
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

        public void GuardarDVV(string tabla, string columna, long dv)
        {
            string query = $"UPDATE DVV_{tabla} SET DV = @dv WHERE Columna = @col";
            SqlParameter[] parametros = {
        new SqlParameter("@dv", dv),
        new SqlParameter("@col", columna)
    };
            acceso.escribir(query, parametros);
        }

        /*public void InsertarDVV(string tabla, string columna, long dv)
        {
            string query = $"INSERT INTO DVV_{tabla}(Columna, DV) VALUES(@col, @dv)";
            SqlParameter[] parametros = {
        new SqlParameter("@dv", dv),
        new SqlParameter("@col", columna)
    };
            acceso.escribir(query, parametros);
        }*/

        public Dictionary<string, long> ObtenerDVVs(string tabla)
        {
            string query = $"SELECT Columna, DV FROM DVV_{tabla}";
            DataTable dt = acceso.leer(query, null);

            return dt.AsEnumerable().ToDictionary(
                row => row.Field<string>("Columna"),
                row => row.Field<long>("DV"));
        }

        //nuevos metodos de prueba para DVH por campo
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

        public Dictionary<string, long> ObtenerDVHPorCampo(string tabla, int idRegistro)
        {
            string query = $"SELECT Columna, DVH FROM DVH_Detalle_{tabla} WHERE IdRegistro = @id";
            SqlParameter[] parametros = { new SqlParameter("@id", idRegistro) };
            DataTable dt = acceso.leer(query, parametros);

            return dt.AsEnumerable().ToDictionary(
                row => row.Field<string>("Columna"),
                row => row.Field<long>("DVH"));
        }



        public void EliminarDVHPorCampo(string tabla)
        {
            string query = $"DELETE FROM DVH_Detalle_{tabla}";
            acceso.escribir(query, null);
        }


    }
}

