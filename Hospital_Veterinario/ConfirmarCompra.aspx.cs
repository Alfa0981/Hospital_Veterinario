using BE;
using BLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Veterinario
{
    public partial class ConfirmarCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarResumen();
        }

        private void CargarResumen()
        {
            string ruta = Request.QueryString["xml"];
            if (string.IsNullOrEmpty(ruta) || !System.IO.File.Exists(ruta))
            {
                Response.Write("<script>alert('No se encontró el archivo del carrito');window.location='Productos.aspx';</script>");
                return;
            }

            List<CarritoItem> carrito = XmlHelper.LeerLista<CarritoItem>(ruta);

            gvResumen.DataSource = carrito;
            gvResumen.DataBind();
            lblTotal.Text = $"Total: {carrito.Sum(x => x.Subtotal):C}";
        }

        protected void btnConfirmarPago_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert('Pago confirmado. ¡Gracias por su compra!');window.location='Productos.aspx';</script>");
        }
    }
}