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
        BLL.ServiceDV gestorDV;

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
                bool hayErrores = gestorUsuario.login(usuario, out List<BE.VerificacionResultadoClass> erroresDV);

                if (hayErrores)
                {
                    Session["ErroresDV"] = erroresDV;
                    Response.Redirect("DVForm.aspx");
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected void btnDV_Click(object sender, EventArgs e)
        {
            var gestorDV = new ServiceDV();
            gestorDV.RecalcularDVH();
            //gestorDV.RecalcularDVV();
        }
    }
}