using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Veterinario
{
	public partial class Inicio : System.Web.UI.Page
	{
        BLL.Usuario gestorUsuario;

        protected void Page_Load(object sender, EventArgs e)
		{
            gestorUsuario = new BLL.Usuario();
            lblBienvenida.Text = "¡Bienvenido, " + SessionManager.GetInstance.Usuario.Nombre + "!";
        }

        protected void btnMascotas_Click(object sender, EventArgs e)
        {
            Response.Redirect("Mascotas.aspx");
        }

        protected void btnProfesionales_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profesionales.aspx");
        }

        protected void btnProductos_Click(object sender, EventArgs e)
        {
            Response.Redirect("Productos.aspx");
        }

        protected void btnComercio_Click(object sender, EventArgs e)
        {
            Response.Redirect("Comercio.aspx");
        }

        protected void btnBitacora_Click(object sender, EventArgs e)
        {
            Response.Redirect("Bitacora.aspx");
        }

        protected void btnUsuarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("Usuarios.aspx");
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            gestorUsuario.logout();
            Response.Redirect("Default.aspx");
        }
    }
}