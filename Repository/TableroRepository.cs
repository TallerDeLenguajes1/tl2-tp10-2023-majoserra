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
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            // No usar así usar, el AddParameter
            command.CommandText = $"UPDATE Tablero SET id_usuario_propietario = '{tablero.Id_usuario_propetario}', nombre = '{tablero.Nombre}', descripcion = '{tablero.Descripcion}' WHERE id = '{tablero.Id}';";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
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

            return (tablero);
        }
        public List<Tablero> GetTableroUsuario(int idUsuario)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            List<Tablero> listatablero = new List<Tablero>();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Tablero WHERE id_usuario_propietario = @idUsuario";
            command.Parameters.Add(new SQLiteParameter("@id", idUsuario));
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

    }
}