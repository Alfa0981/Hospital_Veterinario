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
            //Se obtiene el usuario a traves de la Session y se le da la bienvenida
            BE.Usuario usuario = SessionManager.GetInstance?.Usuario;
            gestorUsuario = new BLL.Usuario();
            lblBienvenida.Text = "¡Bienvenido, " + usuario.Nombre + "!";

            // Ocultar botones según el perfil
            string perfil = usuario.Perfil?.Nombre;

            switch (perfil)
            {
                case "WebMaster":
                    // WebMaster ve todo
                    break;

                case "Admin":
                    btnUsuarios.Visible = false;
                    btnBitacora.Visible = false;
                    break;

                case "Cliente":
                    btnUsuarios.Visible = false;
                    btnBitacora.Visible = false;
                    btnProductos.Visible = false;
                    btnComercio.Visible = false;
                    break;

                default:
                    // Si no tiene perfil asignado, redirigir o bloquear todo
                    Response.Redirect("AccesoDenegado.aspx");
                    break;
            }
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