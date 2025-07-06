using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ServiceRestore
    {
        MpRestore mpRestore = new MpRestore();  
        public void RealizarRestore(string backupFilePath)
        {
            mpRestore.RealizarRestore(backupFilePath);
        }
    }
}
