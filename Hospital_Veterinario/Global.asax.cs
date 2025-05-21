using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Hospital_Veterinario
{
    public class Global : HttpApplication
    {
        BLL.Usuario gestorUsuario;
        void Application_Start(object sender, EventArgs e)
        {
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