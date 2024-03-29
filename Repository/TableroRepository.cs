using System.Data.SQLite;
using EspacioTablero;

namespace EspacioRepositorios
{
    class TableroRepository : ITableroRepository
    {
        private string cadenaConexion = "Data Source=DB/kanban.db;Cache=Shared";

        public void CrearTablero(Tablero tablero)
        {
            var query = $"INSERT INTO Tablero (id_usuario_propietario,nombre,descripcion) VALUES (@idUsuario, @nombre, @descripcion);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {

                
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@idUsuario", tablero.Id_usuario_propetario));
                command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));
                
                connection.Open();
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Update(int id, Tablero tablero)
        {
            var query = $"UPDATE Tablero SET id_usuario_propietario = (@IDpropietario), nombre = (@nombreT), descripcion = (@descripcion) WHERE id = (@id);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@IDpropietario", tablero.Id_usuario_propetario));

                command.Parameters.Add(new SQLiteParameter("@nombreT", tablero.Nombre));

                command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));

                command.Parameters.Add(new SQLiteParameter("@id", id));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        
        public List<Tablero> GetTableroDondeTengoTareas(int idUsuario) // obtenemos los tableros de un usuario propietario en particular 
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            List<Tablero> listatablero = new List<Tablero>();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT DISTINCT Tablero.* FROM Tablero INNER JOIN Tarea ON Tablero.id = Tarea.id_tablero WHERE Tarea.id_usuario_asignado = @idUsuario AND Tablero.id_usuario_propietario <> @idUsuario;";
            command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));
            connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tablero = new Tablero();
                    tablero.Id = Convert.ToInt32(reader["id"]);
                    tablero.Id_usuario_propetario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();
                    listatablero.Add(tablero);
                }
            }
            connection.Close();
            if (listatablero == null) {
                throw new Exception("Tableros no encontrados");
            }

            return (listatablero);

        }

        public Tablero GetById(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            var tablero = new Tablero();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Tablero WHERE id = @id";
            command.Parameters.Add(new SQLiteParameter("@id", id));
            connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tablero.Id = Convert.ToInt32(reader["id"]);
                    tablero.Id_usuario_propetario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();
                }
            }
            connection.Close();
            if (tablero == null) {
                throw new Exception("Tablero no encontrado");
            }

            return (tablero);
        }
        public List<Tablero> GetTableroUsuario(int idUsuario) // obtenemos los tableros de un usuario propietario en particular 
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            List<Tablero> listatablero = new List<Tablero>();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Tablero WHERE id_usuario_propietario = @idUsuario";
            command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));
            connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tablero = new Tablero();
                    tablero.Id = Convert.ToInt32(reader["id"]);
                    tablero.Id_usuario_propetario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();
                    listatablero.Add(tablero);
                }
            }
            connection.Close();
            if (listatablero == null) {
                throw new Exception("Tableros no encontrados");
            }

            return (listatablero);

        }
        
        public List<Tablero> GetTodos()
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            List<Tablero> listatablero = new List<Tablero>();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Tablero ";
            connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tablero = new Tablero();
                    tablero.Id = Convert.ToInt32(reader["id"]);
                    tablero.Id_usuario_propetario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();
                    listatablero.Add(tablero);
                }
            }
            connection.Close();
            if (listatablero == null) {
                throw new Exception("Tableros no encontrados");
            }

            return (listatablero);
        }

        public void Remove(int id)
        {
            // usar using
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            // usar AddParameter
            command.CommandText = $"DELETE FROM tablero WHERE id = '{id}';";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void RemoveTableroUsuario(int idUsuario)
        {
            // usar using
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            // usar AddParameter
            command.CommandText = $"DELETE FROM tablero WHERE id_usuario_propietario = '{idUsuario}';";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }


    }
}