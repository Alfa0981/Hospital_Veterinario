using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.queries
{
    internal class EventoQuery
    {
        public const string SeleccionarTodos = @"SELECT 
                                                e.id, u.nombre, u.apellido, u.id, e.fecha, e.hora, e.modulo, e.descripcion, e.criticidad, u.email
                                                FROM 
                                                    Eventos e
                                                INNER JOIN 
                                                    Usuarios u ON e.usuario_Id = u.id";
        public const string Insertar = @"INSERT INTO Eventos (usuario_Id, fecha, hora, modulo,
                                         descripcion, criticidad) VALUES (@usuario_Id, GETDATE(),
                                        GETDATE(), @modulo, @descripcion, @criticidad)";
    }
}
