using BLL;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Veterinario
{
    public partial class _Default : Page
    {
        BLL.Usuario gestorUsuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            gestorUsuario = new BLL.Usuario();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtUsername.Text;
            string pass = txtPassword.Text;

            BE.Usuario usuario = new BE.Usuario(pass, email);

            try
            {
                gestorUsuario.login(usuario);

                // Redirigir a la página de inicio después de un inicio de sesión exitoso

                Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.Replace("'", "\\'"); // Escapa comillas simples
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "alertaLogin",
                    $"alert('{mensaje}');",
                    true
                );
            }
        }
    }
}