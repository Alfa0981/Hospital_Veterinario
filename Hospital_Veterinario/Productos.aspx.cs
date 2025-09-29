using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Veterinario
{
    public partial class Productos : System.Web.UI.Page
    {
        private BLL.Producto gestorProd;

        private List<CarritoItem> Carrito
        {
            get
            {
                if (ViewState["Carrito"] == null)
                    ViewState["Carrito"] = new List<CarritoItem>();
                return (List<CarritoItem>)ViewState["Carrito"];
            }
            set { ViewState["Carrito"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
            gestorProd = new BLL.Producto();

            if (!IsPostBack)
            {
                BindCatalogo();
                BindCarrito();
            }
        }

        private void BindCatalogo(string filtro = null)
        {
            var data = gestorProd.ObtenerCatalogoActivos();

            if (!string.IsNullOrWhiteSpace(filtro))
                data = data.Where(p => (p.Nombre ?? "")
                              .IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0)
                              .ToList();

            gvProductos.DataSource = data;
            gvProductos.DataBind();
        }

        private void BindCarrito()
        {
            gvCarrito.DataSource = Carrito;
            gvCarrito.DataBind();
            lblTotal.Text = $"Total: {Carrito.Sum(x => x.Subtotal):C}";
            btnPagar.Enabled = Carrito.Any();
        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Agregar") return;

            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int productoId = Convert.ToInt32(gvProductos.DataKeys[rowIndex].Value);

                var row = gvProductos.Rows[rowIndex];
                var txtCant = (TextBox)row.FindControl("txtCantidad");

                if (!int.TryParse(txtCant.Text, out int cantidad) || cantidad <= 0)
                    throw new Exception("La cantidad debe ser mayor a 0.");

                // VALIDACIÓN VÍA WS 
                ValidarUnaCantidadConWS(productoId, cantidad);

                var p = gestorProd.ObtenerPorId(productoId);

                var item = Carrito.FirstOrDefault(c => c.ProductoId == productoId);
                if (item == null)
                {
                    Carrito.Add(new CarritoItem
                    {
                        ProductoId = p.Id,
                        Nombre = p.Nombre,
                        PrecioUnitario = p.Precio,
                        Cantidad = cantidad
                    });
                }
                else
                {
                    int nueva = item.Cantidad + cantidad;

                    // Revalidamos nueva cantidad acumulada con el WS
                    ValidarUnaCantidadConWS(productoId, nueva);

                    item.Cantidad = nueva;
                }

                Carrito = Carrito;
                BindCarrito();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void gvCarrito_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            int productoId = Convert.ToInt32(gvCarrito.DataKeys[rowIndex].Value);

            try
            {
                if (e.CommandName == "Quitar")
                {
                    Carrito.RemoveAll(c => c.ProductoId == productoId);
                }
                else if (e.CommandName == "Actualizar")
                {
                    var row = gvCarrito.Rows[rowIndex];
                    var txt = (TextBox)row.FindControl("txtCantidadCarrito");

                    if (!int.TryParse(txt.Text, out int nueva) || nueva <= 0)
                        throw new Exception("La cantidad debe ser mayor a 0.");

                    // VALIDACIÓN VÍA WS
                    ValidarUnaCantidadConWS(productoId, nueva);

                    var item = Carrito.First(c => c.ProductoId == productoId);
                    item.Cantidad = nueva;
                }

                Carrito = Carrito;
                BindCarrito();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void btnLimpiarCarrito_Click(object sender, EventArgs e)
        {
            Carrito = new List<CarritoItem>();
            BindCarrito();
        }

        protected void btnPagar_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert('El pago fue realizado correctamente');</script>");
        }

        private void ValidarUnaCantidadConWS(int productoId, int cantidad)
        {
            // Traemos datos de DAL
            var p = gestorProd.ObtenerPorId(productoId);

            var ws = new Hospital_Veterinario.WebServiceValidator();
            var entrada = new List<Hospital_Veterinario.WebServiceValidator.ItemEntrada>
            {
                new Hospital_Veterinario.WebServiceValidator.ItemEntrada
                {
                    ProductoId = productoId,
                    CantidadSolicitada = cantidad,
                    StockActual = p.StockActual,
                    StockMinimo = p.StockMinimo
                }
            };

            var resultado = ws.ValidarCarrito(entrada);

            // Caso inválido: lanzamos excepción
            if (!resultado.Valido)
            {
                // Tomamos el detalle del único ítem
                var d = resultado.Detalle[0];
                throw new Exception(
                    $"No hay stock suficiente. Solicitado {d.Solicitada}, disponible {d.Disponible}.");
            }

            // Caso válido con warning: mostramos alerta (no bloquea la carga)
            var det = resultado.Detalle[0];
            if (det.Warning)
            {
                ClientScript.RegisterStartupScript(
                    GetType(), "warn1",
                    $"alert('{EscapeForJs($"Advertencia: la venta deja stock por debajo del mínimo. {det.Mensaje}")}');",
                    true
                );
            }
        }


        private static string EscapeForJs(string s)
        {
            return (s ?? string.Empty)
                .Replace("\\", "\\\\")
                .Replace("'", "\\'")
                .Replace("\r", "")
                .Replace("\n", "\\n");
        }
    }
}