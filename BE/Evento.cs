using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Evento
    {
		private DateTime fecha;
		private TimeSpan hora;
		private Usuario usuario;
		private string modulo;
		private int criticidad;
		private string descripcion;
		private int id;

		public int Id
		{
			get { return id; }
			set { id = value; }
		}


		public string Descripcion
		{
			get { return descripcion; }
			set { descripcion = value; }
		}


		public int Criticidad
		{
			get { return criticidad; }
			set { criticidad = value; }
		}


		public string Modulo
		{
			get { return modulo; }
			set { modulo = value; }
		}


		public Usuario Usuario
		{
			get { return usuario; }
			set { usuario = value; }
		}


		public TimeSpan Hora
		{
			get { return hora; }
			set { hora = value; }
		}


		public DateTime Fecha
		{
			get { return fecha; }
			set { fecha = value; }
		}
	}
}
