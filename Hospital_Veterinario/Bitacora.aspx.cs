using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hospital_Veterinario
{
	public partial class Bitacora : System.Web.UI.Page
	{
		BLL.GestionEventos gestionEventos; 
		protected void Page_Load(object sender, EventArgs e)
		{
			gestionEventos = new BLL.GestionEventos();
            if (!IsPostBack)
            {
                CargarBitacora();
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            gestionEventos = new BLL.GestionEventos();
            var eventos = gestionEventos.obtenerEventos();

            // Obtiene los valores de los filtros
            string filtroNombreApellido = txtNombreApellido.Text.Trim().ToLower();
            string filtroModulo = txtModulo.Text.Trim().ToLower();
            string filtroEmail = txtEmail.Text.Trim().ToLower();
            string filtroCriticidad = txtCriticidad.Text.Trim();
            int criticidad;
            int? filtroCriticidadNum = int.TryParse(filtroCriticidad, out criticidad) ? (int?)criticidad : null;

            DateTime fechaDesde, fechaHasta;
            bool tieneFechaDesde = DateTime.TryParse(txtFechaDesde.Text, out fechaDesde);
            bool tieneFechaHasta = DateTime.TryParse(txtFechaHasta.Text, out fechaHasta);

            // Filtra la lista
            var eventosFiltrados = eventos.Where(ev =>
                (string.IsNullOrEmpty(filtroNombreApellido) ||
                    (ev.Usuario != null && (
                        ($"{ev.Usuario.Nombre} {ev.Usuario.Apellido}".ToLower().Contains(filtroNombreApellido)) ||
                        ($"{ev.Usuario.Apellido} {ev.Usuario.Nombre}".ToLower().Contains(filtroNombreApellido))
                    ))
                ) &&
                (string.IsNullOrEmpty(filtroModulo) || (ev.Modulo != null && ev.Modulo.ToLower().Contains(filtroModulo))) &&
                (string.IsNullOrEmpty(filtroEmail) || (ev.Usuario != null && ev.Usuario.Email.ToLower().Contains(filtroEmail))) &&
                (!filtroCriticidadNum.HasValue || ev.Criticidad == filtroCriticidadNum.Value) &&
                (!tieneFechaDesde || ev.Fecha >= fechaDesde) &&
                (!tieneFechaHasta || ev.Fecha <= fechaHasta)
            ).Select(ev => new
            {
                NombreApellido = ev.Usuario != null ? $"{ev.Usuario.Nombre} {ev.Usuario.Apellido}" : "",
                Modulo = ev.Modulo,
                Fecha = ev.Fecha.ToString("dd/MM/yyyy"),
                Hora = ev.Hora.ToString(@"hh\:mm\:ss"),
                Descripcion = ev.Descripcion,
                Criticidad = ev.Criticidad,
                Email = ev.Usuario != null ? ev.Usuario.Email : ""
            }).ToList();

            gvBitacora.DataSource = eventosFiltrados;
            gvBitacora.DataBind();
        }

        private void CargarBitacora()
        {
            var eventos = gestionEventos.obtenerEventos();

            var datos = eventos.Select(ev => new
            {
                NombreApellido = ev.Usuario != null ? $"{ev.Usuario.Nombre} {ev.Usuario.Apellido}" : "",
                Modulo = ev.Modulo,
                Descripcion = ev.Descripcion,
                Fecha = ev.Fecha.ToString("dd/MM/yyyy"),
                Hora = ev.Hora.ToString(@"hh\:mm\:ss"),
                Criticidad = ev.Criticidad,
                Email = ev.Usuario != null ? ev.Usuario.Email : ""
            }).ToList();

            gvBitacora.DataSource = datos;
            gvBitacora.DataBind();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Limpiar los filtros
            txtNombreApellido.Text = "";
            txtModulo.Text = "";
            txtEmail.Text = "";
            txtFechaDesde.Text = "";
            txtFechaHasta.Text = "";
            txtCriticidad.Text = "";

            // Recargar la bitácora sin filtros
            CargarBitacora();
        }
    }
}