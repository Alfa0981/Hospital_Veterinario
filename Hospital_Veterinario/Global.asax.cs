using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace Hospital_Veterinario
{
    public class Global : HttpApplication
    {
        BLL.Usuario gestorUsuario;
        void Application_Start(object sender, EventArgs e)
        {

            ScriptResourceDefinition jqueryDefinition = new ScriptResourceDefinition
            {
                Path = "~/Scripts/jquery-3.6.0.min.js",
                DebugPath = "~/Scripts/jquery-3.6.0.js",
                CdnPath = "https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js",
                CdnDebugPath = "https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.js",
                CdnSupportsSecureConnection = true,
                LoadSuccessExpression = "window.jQuery"
            };

            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", jqueryDefinition);
            gestorUsuario = new BLL.Usuario();
            // Código que se ejecuta al iniciar la aplicación
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_End(object sender, EventArgs e)
        {
            // Lógica cuando la sesión de un usuario termina
            gestorUsuario.logout();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            // Lógica cuando la aplicación se cierra o recicla
            gestorUsuario.logout();
        }
    }
}