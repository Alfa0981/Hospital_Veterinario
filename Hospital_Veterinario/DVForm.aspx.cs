using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;


namespace Hospital_Veterinario
{
    public partial class DVForm : System.Web.UI.Page
    {
        ServiceDV gestorDV;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var errores = Session["ErroresDV"] as List<BE.VerificacionResultadoClass>;
                if (errores != null)
                {
                    // Podés mostrarlos en un GridView o ListBox
                    dgvErrores.DataSource = errores.SelectMany(r =>
                        r.ColumnasAlteradas.Select(c => new
                        {
                            Tabla = r.Tabla,
                            Id = c.IdRegistro,
                            Columna = c.Columna
                        })
                    );
                    dgvErrores.DataBind();
                }
            }
        }

        protected void btnRecalcularDV_Click(object sender, EventArgs e)
        {
            gestorDV = new ServiceDV();
            try
            {
                gestorDV.RecalcularDVH();
                //gestorDV.RecalcularDVV(); // Si también querés recalcular DVV
                lblMensaje.Text = "Los DV han sido recalculados correctamente.";
                lblMensaje.ForeColor = System.Drawing.Color.Green;

                // Script para redirigir luego de 3 segundos
                string script = @"
            <script type='text/javascript'>
                setTimeout(function () {
                    window.location.href = 'Inicio.aspx';
                }, 3000); // 3000 ms = 3 segundos
            </script>";

                ClientScript.RegisterStartupScript(this.GetType(), "redirect", script);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al recalcular los DV: " + ex.Message;
            }
        }

        protected void btnRestaurarBackup_Click(object sender, EventArgs e)
        {
            Response.Redirect("Restore.aspx");
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {

        }
    }
}