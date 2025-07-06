using BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Veterinario
{
    public partial class Restore : System.Web.UI.Page
    {
        ServiceRestore restoreService = new ServiceRestore();
        protected void Page_Load(object sender, EventArgs e)
        {
            fuBackup.Visible = true;
        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            
            if (fuBackup.HasFile)
            {
                string extension = Path.GetExtension(fuBackup.FileName).ToLower();

                // Validamos que sea un archivo .bak
                if (extension == ".bak")
                {
                    try
                    {
                        // Ruta donde se va a guardar el archivo
                        string backupDirectory = @"C:\Users\Public\Hospital_Veterinario_BCK.bak"; // Asegúrate de que la carpeta existe y tiene permisos
                        string fileName = Path.GetFileName(fuBackup.FileName);
                        string savePath = Path.Combine(backupDirectory, fileName);

                        // Guardamos el archivo en la ruta especificada
                        fuBackup.SaveAs(savePath);

                        lblMensaje.ForeColor = System.Drawing.Color.Green;
                        lblMensaje.Text = "Backup subido correctamente a: " + savePath;

                        restoreService.RealizarRestore(backupDirectory);
                        lblMensaje.Text = "Restauración realizada con éxito.";
                        savePath = "";
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
                        lblMensaje.Text = "Error al subir el archivo: " + ex.Message;
                    }
                }
                else
                {
                    lblMensaje.Text = "Solo se permiten archivos .bak";
                }
            }
            else
            {
                lblMensaje.Text = "Por favor seleccione un archivo .bak";
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("DVForm.aspx");
        }
    }
}