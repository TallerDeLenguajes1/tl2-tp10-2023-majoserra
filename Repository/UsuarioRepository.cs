using System.Data.SQLite;
using EspacioTablero;

namespace EspacioRepositorios
{
    class UsuarioRepository : IUsuarioRepository
    {
        private string cadenaConexion; // Cadena de conexion a la base de datos

        public UsuarioRepository(string CadenaDeConexion)  // inyeccion de la cadena, desde appsetting 
        {
            cadenaConexion = CadenaDeConexion;
        }

        public void Create(Usuario usu) // crear un usuario
        {
            // consulta que queremos realizar 
            var query = $"INSERT INTO Usuario (nombre_de_usuario, rol, contrasenia) VALUES (@nombre_de_usuario, @rol, @contrasenia)";

            //Establecer conexion a la base de datos
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open(); // abrir la conexion a la base de datos 
                var command = new SQLiteCommand(query, connection);

                // Agregando parametros al SQL para evitar la inyeccion de SQL 
                command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", usu.NombreDeUsuario)); 
                command.Parameters.Add(new SQLiteParameter("@rol", usu.Rol));
                command.Parameters.Add(new SQLiteParameter("@contrasenia", usu.Contrasenia));

                // es un método que se utiliza para ejecutar comandos SQL que no devuelven resultados
                command.ExecuteNonQuery();

                // cerramos la conexion 

                connection.Close();
            }
        }

        //Listar todos los usuarios registrados. (devuelve un List de Usuarios)
        public List<Usuario> GetAll()
        {
            var queryString = @"SELECT * FROM Usuario;"; // consulta

            List<Usuario> usuarios = new List<Usuario>(); // listar de usuarios, inicializada

            // establecemos la conexion a la bdd
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);


                connection.Open(); // abrimos la conexion 

                using (SQLiteDataReader reader = command.ExecuteReader()) // este objeto se lo utiliza para poder leer los resultados de la consulta
                {
                    while (reader.Read()) // el metodo read avanza en las filas 
                    {
                        var usuario = new Usuario(); // se crea un objeto usuario
                        usuario.Id = Convert.ToInt32(reader["id"]); // convertimos en un int al id
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString(); // en un string al nombre
                        usuario.Contrasenia = reader["contrasenia"].ToString(); // en un string a la contrasenia
                        usuario.Rol = (Roles)Convert.ToInt32(reader["rol"]);   // en un int del tipo rol al rol 
                        usuarios.Add(usuario);
                    }
                }
                connection.Close(); // cerramos la conexion 
            }

            if (usuarios == null)
            {
                throw new Exception("Lista de usuario No creada");
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
            if (usuario == null)
            {
                throw new Exception("Usuario no creado");
            }

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


