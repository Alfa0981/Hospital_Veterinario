using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Acceso
    {
        private SqlConnection conn;
        private SqlTransaction transaction;

        /// <summary>
        /// Propiedad pública para acceder o modificar la conexión SQL activa.
        /// </summary>
        public SqlConnection Conn
        {
            get { return conn; }
            set { conn = value; }
        }

        /// <summary>
        /// Establece la conexión con la base de datos utilizando una cadena de conexión fija.
        /// </summary>
        public void conectar()
        {
            conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=.;Initial Catalog=Hospital_Veterinario;Integrated Security=True";
            conn.Open();
        }

        /// <summary>
        /// Cierra y libera la conexión activa con la base de datos.
        /// </summary>
        public void desconectar()
        {
            conn.Close();
            conn.Dispose();
        }

        /// <summary>
        /// Inicia una transacción sobre la conexión actual.
        /// Si la conexión no está abierta, la establece primero.
        /// </summary>
        public void comenzarTransaccion()
        {
            if (conn == null || conn.State != ConnectionState.Open)
            {
                conectar();
            }
            transaction = conn.BeginTransaction();
        }

        /// <summary>
        /// Confirma (commitea) la transacción actual y cierra la conexión.
        /// </summary>
        public void confirmarTransaccion()
        {
            transaction?.Commit();
            desconectar();
        }

        /// <summary>
        /// Revierte (rollback) la transacción actual y cierra la conexión.
        /// </summary>
        public void revertirTransaccion()
        {
            transaction?.Rollback();
            desconectar();
        }

        /// <summary>
        /// Ejecuta una consulta de tipo escritura (INSERT, UPDATE o DELETE)
        /// utilizando los parámetros indicados, y la conexión actual.
        /// Si hay una transacción activa, la utiliza.
        /// </summary>
        public void escribir(string query, SqlParameter[] sqlParameters)
        {
            if (conn == null || conn.State != ConnectionState.Open)
            {
                conectar();
            }

            SqlCommand cmd = new SqlCommand
            {
                Connection = conn,
                CommandType = CommandType.Text,
                CommandText = query
            };

            if (transaction != null)
            {
                cmd.Transaction = transaction;
            }

            if (sqlParameters != null)
            {
                cmd.Parameters.AddRange(sqlParameters);
            }

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Error en la base de datos: " + " " + e.GetBaseException());
            }
        }

        /// <summary>
        /// Ejecuta una consulta de lectura (SELECT) y devuelve los resultados en un DataTable.
        /// Aplica los parámetros y transacción actual si están definidos.
        /// </summary>
        public DataTable leer(string query, SqlParameter[] sqlParameters)
        {
            if (conn == null || conn.State != ConnectionState.Open)
            {
                conectar();
            }

            DataTable tabla = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter
            {
                SelectCommand = new SqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = query,
                    Connection = conn
                }
            };

            if (sqlParameters != null)
            {
                sqlDataAdapter.SelectCommand.Parameters.AddRange(sqlParameters);
            }

            if (transaction != null)
            {
                sqlDataAdapter.SelectCommand.Transaction = transaction;
            }

            sqlDataAdapter.Fill(tabla);
            return tabla;
        }
    }
}
