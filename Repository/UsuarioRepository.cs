using System.Data.SQLite;
using EspacioTablero;

namespace EspacioRepositorios
{
    class UsuarioRepository : IUsuarioRepository
    {
        private string cadenaConexion;

        public UsuarioRepository(string CadenaDeConexion)
        {
            cadenaConexion = CadenaDeConexion;
        }

        public void Create(Usuario usu)
        {
            var query = $"INSERT INTO Usuario (nombre_de_usuario, rol, contrasenia) VALUES (@nombre_de_usuario, @rol, @contrasenia)";

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", usu.NombreDeUsuario));
                command.Parameters.Add(new SQLiteParameter("@rol", usu.Rol));
                command.Parameters.Add(new SQLiteParameter("@contrasenia", usu.Contrasenia));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        //Listar todos los usuarios registrados. (devuelve un List de Usuarios)
        public List<Usuario> GetAll()
        {
            var queryString = @"SELECT * FROM Usuario;";
            List<Usuario> usuarios = new List<Usuario>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        usuario.Contrasenia = reader["contrasenia"].ToString();
                        usuario.Rol = (Roles)Convert.ToInt32(reader["rol"]);  
                        usuarios.Add(usuario);
                    }
                }
                connection.Close();
            }
            return usuarios;
        }

        //Modificar un usuario existente. (recibe un Id y un objeto Usuario)

        public void Update(int id, Usuario u)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            // No usar así usar, el AddParameter
            command.CommandText = $"UPDATE Usuario SET nombre_de_usuario = '{u.NombreDeUsuario}', contrasenia = '{u.Contrasenia}', rol = @rol   WHERE id = '{id}';";

            connection.Open();
            command.Parameters.Add(new SQLiteParameter("@rol", u.Rol));
            command.ExecuteNonQuery();
            connection.Close();
        }
        //Obtener detalles de un usuario por su ID. (recibe un Id y devuelve un Usuario)

        public Usuario GetById(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            var usuario = new Usuario();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Usuario WHERE id = @id";
            command.Parameters.Add(new SQLiteParameter("@id", id));
            connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                    usuario.Contrasenia = reader["contrasenia"].ToString();
                    usuario.Rol = (Roles)Convert.ToInt32(reader["rol"]);  
                }
            }
            connection.Close();

            return usuario;
        }
        public void Remove(int id)
        {
            // usar using
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            // usar AddParameter
            command.CommandText = $"DELETE FROM usuario WHERE id = '{id}';";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

    }
}


