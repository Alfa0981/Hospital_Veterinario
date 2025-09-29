using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Hospital_Veterinario
{
    /// <summary>
    /// Descripción breve de WebServiceValidator
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceValidator : System.Web.Services.WebService
    {
        public class ItemEntrada
        {
            public int ProductoId { get; set; }
            public int CantidadSolicitada { get; set; }
            public int StockActual { get; set; }
            public int StockMinimo { get; set; }
        }

        public class ItemDetalle
        {
            public int ProductoId { get; set; }
            public int Solicitada { get; set; }
            public int Disponible { get; set; }
            public bool Warning { get; set; }
            public string Mensaje { get; set; }
        }

        public class ResultadoValidacion
        {
            public bool Valido { get; set; }           // true si TODOS los items son vendibles
            public string Mensaje { get; set; }        // resumen general
            public List<ItemDetalle> Detalle { get; set; }
        }

        [WebMethod]
        public ResultadoValidacion ValidarCarrito(List<ItemEntrada> items)
        {
            var salida = new ResultadoValidacion
            {
                Valido = true,
                Mensaje = "Carrito válido.",
                Detalle = new List<ItemDetalle>()
            };

            if (items == null || items.Count == 0)
            {
                salida.Valido = false;
                salida.Mensaje = "No se recibieron ítems para validar.";
                return salida;
            }

            foreach (var it in items)
            {
                var d = new ItemDetalle
                {
                    ProductoId = it.ProductoId,
                    Solicitada = it.CantidadSolicitada,
                    Disponible = it.StockActual
                };

                // Validacion stock
                if (it.CantidadSolicitada > it.StockActual)
                {
                    salida.Valido = false;
                    d.Warning = false;
                    d.Mensaje = $"Faltante: solicitado {it.CantidadSolicitada}, disponible {it.StockActual}.";
                }
                else
                {
                    // Calculamos stock remanente y marcamos warning si queda por debajo del mínimo
                    int remanente = it.StockActual - it.CantidadSolicitada;
                    d.Warning = remanente < it.StockMinimo;
                    d.Mensaje = d.Warning
                        ? $"Válido con warning: remanente {remanente} < stock mínimo {it.StockMinimo}."
                        : "Válido.";
                }

                salida.Detalle.Add(d);
            }

            if (!salida.Valido)
                salida.Mensaje = "Carrito inválido: hay ítems con faltante.";
            else if (salida.Detalle.Exists(x => x.Warning))
                salida.Mensaje = "Carrito válido con advertencias de stock mínimo.";
            else
                salida.Mensaje = "Carrito válido.";

            return salida;
        }
    }
}
