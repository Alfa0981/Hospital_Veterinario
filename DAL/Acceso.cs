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

        public SqlConnection Conn
        {
            get { return conn; }
            set { conn = value; }
        }

        public void conectar()
        {
            conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=.;Initial Catalog=Hospital_Veterinario;Integrated Security=True";
            conn.Open();
        }

        public void desconectar()
        {
            conn.Close();
            conn.Dispose();
        }

        public void comenzarTransaccion()
        {
            if (conn == null || conn.State != ConnectionState.Open)
            {
                conectar();
            }
            transaction = conn.BeginTransaction();
        }

        public void confirmarTransaccion()
        {
            transaction?.Commit();
            desconectar();
        }

        public void revertirTransaccion()
        {
            transaction?.Rollback();
            desconectar();
        }

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
