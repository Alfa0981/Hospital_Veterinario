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
    public class MpProducto
    {
        private readonly Acceso _acceso = new Acceso();

        public List<Producto> ListarActivos()
        {
            var tabla = _acceso.leer(@"
            SELECT id, nombre, descripcion, precio, stock_actual, stock_minimo, estado
            FROM productos
            WHERE estado = 'A'", null);
            var salida = new List<Producto>();
            foreach (DataRow row in tabla.Rows)
                salida.Add(Map(row));
            return salida;
        }

        public Producto BuscarPorId(int id)
        {
            var pars = new[] { new SqlParameter("@id", id) };
            var tabla = _acceso.leer(@"
            SELECT id, nombre, descripcion, precio, stock_actual, stock_minimo, estado
            FROM productos
            WHERE id = @id", pars);
            if (tabla.Rows.Count == 0) return null;
            return Map(tabla.Rows[0]);
        }

        private Producto Map(DataRow row)
        {
            return new Producto
            {
                Id = row["id"] == DBNull.Value ? 0 : Convert.ToInt32(row["id"]),
                Nombre = row["nombre"] == DBNull.Value ? "" : row["nombre"].ToString(),
                Descripcion = row["descripcion"] == DBNull.Value ? "" : row["descripcion"].ToString(),
                Precio = row["precio"] == DBNull.Value ? 0m : Convert.ToDecimal(row["precio"]),
                StockActual = row["stock_actual"] == DBNull.Value ? 0 : Convert.ToInt32(row["stock_actual"]),
                StockMinimo = row["stock_minimo"] == DBNull.Value ? 0 : Convert.ToInt32(row["stock_minimo"]),
                Estado = row["estado"] == DBNull.Value ? 'A' : row["estado"].ToString().Trim()[0],
            };
        }
    }
}
