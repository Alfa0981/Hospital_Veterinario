using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MpRestore
    {
        Acceso acceso = new Acceso();
        public void RealizarRestore(string backupFilePath)
        {
            try
            {
                using (SqlConnection conn = acceso.Conn)
                {
                    using (SqlCommand setMaster = new SqlCommand("USE master;", conn))
                    {
                        setMaster.ExecuteNonQuery();
                    }

                    using (SqlCommand setSingleUser = new SqlCommand("ALTER DATABASE Hospital_Veterinario SET SINGLE_USER WITH ROLLBACK IMMEDIATE;", conn))
                    {
                        setSingleUser.ExecuteNonQuery();
                    }

                    string query = $"RESTORE DATABASE Hospital_Veterinario FROM DISK = '{backupFilePath}' WITH REPLACE;";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand setMultiUser = new SqlCommand("ALTER DATABASE Hospital_Veterinario SET MULTI_USER;", conn))
                    {
                        setMultiUser.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al restaurar la base de datos: {ex.Message}");
            }
        }
    }
}
