using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.queries
{
    public abstract class UsuarioQuery
    {
        /*public const string SeleccionarTodos = @"SELECT u.*, r.nombre as perfil_nombre FROM Usuario u
                                 JOIN Perfil r ON u.perfil_id = r.id WHERE u.activo = 1";
        public const string Insertar = @"INSERT INTO Usuario (dni, apellido, nombre, usuario, pass, perfil_id, email, idioma)
                                 VALUES (@Dni, @Apellido, @Nombre, @Usuario, @Pass, @PerfilId, @Email, @Idioma)";
        public const string BuscarDni = @"SELECT u.*, r.nombre as perfil_nombre FROM Usuario u
                                 JOIN Perfil r ON u.perfil_id = r.id WHERE u.dni = @Dni";*/
        public const string ModificarUsuario = @"update Usuarios set IsBlock = @bloqueo, intentos = @intentos, perfil_Id = @perfil, apellido = @apellido,
                                                nombre = @nombre, dni = @dni, password = @password, email = @email where id = @id;";

        public const string BuscarPorEmail = @"select u.*, r.nombre as perfil_nombre from Usuarios u JOIN Perfiles r ON u.perfil_id = r.id WHERE u.email = @email;";
    }
}
