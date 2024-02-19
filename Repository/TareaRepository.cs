using System.Data.SQLite;
using EspacioTablero;

namespace EspacioRepositorios
{
    class TareaRepository : ITareaRepository
    {
        private string cadenaConexion = "Data Source=DB/kanban.db;Cache=Shared";

        public void CrearTarea(Tarea tarea)
        {
            var query = $"INSERT INTO Tarea (id_tablero, nombre, estado, descripcion, color, id_usuario_asignado) VALUES (@id_tablero, @name, @estado, @descripcion, @color, @usuario)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {

                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@id_tablero", tarea.Id_tablero)); // porque le estamos mandando por parametro 
                command.Parameters.Add(new SQLiteParameter("@name", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", (int)tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@usuario", tarea.Id_usuario_asignado));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public void Update(int id, Tarea tarea)
        {
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();

                using (SQLiteCommand command = connection.CreateCommand())
                {
                    // Usar parámetros en lugar de concatenar valores en la cadena de consulta
                    command.CommandText = "UPDATE Tarea SET nombre = @nombre, descripcion = @descripcion, color = @color, estado = @estado, id_tablero = @id_tablero, id_usuario_asignado = @id_usuario_asignado WHERE id = @id;";
                    command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                    command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
                    command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                    command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
                    command.Parameters.Add(new SQLiteParameter("@id", id));
                    command.Parameters.Add(new SQLiteParameter("@id_tablero", tarea.Id_tablero));
                    command.Parameters.Add(new SQLiteParameter("@id_usuario_asignado", tarea.Id_usuario_asignado));
                    
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
        public void UpdateEstado(int id, EstadoTarea estado)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            // No usar así usar, el AddParameter
            var query = $"UPDATE Tarea SET estado = ('{estado}') WHERE id = '{id}';";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public List<Tarea> GetAll()
        {
            var queryString = @"SELECT * FROM Tarea;";
            List<Tarea> tareas = new List<Tarea>();
            
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Tarea tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.Id_tablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.Id_usuario_asignado = Convert.ToInt32(reader["id_usuario_asignado"]); // Corrige aquí
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            return tareas;
        }
        
        public Tarea GetById(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            var tarea = new Tarea();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Tarea WHERE id = @id;";
            command.Parameters.Add(new SQLiteParameter("@id", id));
            connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tarea.Id = Convert.ToInt32(reader["id"]);
                    tarea.Id_tablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Estado = (EstadoTarea)Enum.ToObject(typeof(EstadoTarea), Convert.ToInt32(reader["estado"])); // Hacer metodo por separado?
                    tarea.Descripcion = reader["descripcion"].ToString();
                    tarea.Color = reader["color"].ToString();
                    tarea.Id_usuario_asignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                }
            }
            connection.Close();

            return tarea;
        
        }
        
        public List<Tarea> GetTareaMiTablero(int idUsuario) // obtengo todas las tareas de mi tablero 
        {
            var queryString = @"SELECT Tarea.id, Tarea.id_tablero, Tarea.nombre, Tarea.estado, Tarea.descripcion, Tarea.color, Tarea.id_usuario_asignado FROM Tarea INNER JOIN Tablero ON Tarea.id_tablero = Tablero.id WHERE Tablero.id_usuario_propietario = @idUsuario;";
            List<Tarea> tareas = new List<Tarea>();

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario)); // Agrega el parámetro

                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Tarea tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.Id_tablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.Id_usuario_asignado = Convert.ToInt32(reader["id_usuario_asignado"]); // Corrige aquí
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            return tareas;
        }

        public List<Tarea> GetTareaAsignadas(int idUsuario) // obtiene todas las tareas donde el usuario asignado soy yo 
        {
            var queryString = @"SELECT DISTINCT Tarea.* FROM Tarea INNER JOIN Tablero ON Tarea.id_tablero = Tablero.id WHERE Tarea.id_usuario_asignado = @idUsuario AND Tablero.id_usuario_propietario <> @idUsuario;"; // cambiar la condicion a que no me traiga las de mi tablero 
            List<Tarea> tareas = new List<Tarea>();

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario)); // Agrega el parámetro

                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Tarea tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.Id_tablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.Id_usuario_asignado = Convert.ToInt32(reader["id_usuario_asignado"]); // Corrige aquí
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            return tareas;
        }
        public List<Tarea> GetTareaTablero(int idTablero)
        {
            var queryString = @"SELECT * FROM Tarea WHERE id_tablero = @idTablero;";
            List<Tarea> tareas = new List<Tarea>();

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();
                command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero)); // Agrega el parámetro



                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Tarea tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.Id_tablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.Id_usuario_asignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            return tareas;
        }

        public void AsignarUsuario(int idTarea, int idUsuario)
        {
            var query = $"UPDATE Tarea SET id_usuario_asignado = (@usuario) WHERE id = (@id);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@usuario", idUsuario));

                command.Parameters.Add(new SQLiteParameter("@id", idTarea));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public void RemoveTarea(int id)
        {
            // usar using
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            // usar AddParameter
            command.CommandText = $"DELETE FROM tarea WHERE id = '{id}';";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

    }
}
