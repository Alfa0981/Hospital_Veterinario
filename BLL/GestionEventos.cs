using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class GestionEventos
    {
        MpEvento mpEvento = new MpEvento();

        /// <summary>
        /// Registra un evento en la base de datos con la criticidad, el módulo y la descripción proporcionados.
        /// El evento queda asociado al usuario actualmente logueado en SessionManager.
        /// </summary>
        public void persistirEvento(string tipoEvento, string modulo, int criticidad)
        {
            if (SessionManager.GetInstance != null)
            {
                Evento evento = new Evento()
                {
                    Criticidad = criticidad,
                    Descripcion = tipoEvento,
                    Modulo = modulo,
                    Usuario = SessionManager.GetInstance.Usuario,
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.TimeOfDay,
                };
                mpEvento.persitirEvento(evento);
            }
        }

        /// <summary>
        /// Obtiene todos los eventos registrados en la base de datos.
        /// </summary>
        public List<Evento> obtenerEventos()
        {
            return mpEvento.obtenerTodos();
        }
    }
}
