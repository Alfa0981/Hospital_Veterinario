using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Producto
    {
        private readonly MpProducto _mp = new MpProducto();

        public List<BE.Producto> ObtenerCatalogoActivos() => _mp.ListarActivos();

        public BE.Producto ObtenerPorId(int id)
        {
            var p = _mp.BuscarPorId(id);
            if (p == null) throw new Exception("Producto no encontrado.");
            if (p.Estado != 'A') throw new Exception("Producto inactivo.");
            return p;
        }

        /// Valida cantidad contra stock REMPLAZAR POR WEB SERVICE
        public void ValidarCantidad(int productoId, int cantidad)
        {
            if (cantidad <= 0) throw new Exception("La cantidad debe ser mayor a 0.");
            var prod = ObtenerPorId(productoId);

            if (cantidad > prod.StockActual)
                throw new Exception($"No hay stock suficiente. Disponible: {prod.StockActual}.");

            // Alerta si al vender quedás por debajo del stock mínimo 
            int stockResultante = prod.StockActual - cantidad;
            if (stockResultante < prod.StockMinimo)
            {
                // Disparar evento
            }
        }
    }
}
