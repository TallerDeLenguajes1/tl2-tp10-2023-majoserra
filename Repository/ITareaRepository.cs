using EspacioTablero;

namespace EspacioRepositorios
{
    public interface ITareaRepository
    {
        public List<Tarea> GetTareaMiTablero(int idUsuario);
        public void CrearTarea(Tarea tarea);//Crear una nueva tarea en un tablero. (recibe un idTablero, devuelve un objeto Tarea)
        public void Update(int id, Tarea tarea); //Modificar una tarea existente. (recibe un id y un objeto Tarea)
        public Tarea GetById(int id);//Obtener detalles de una tarea por su ID. (devuelve un objeto Tarea)
        public List<Tarea> GetTareaUsuario(int idUsuario);/*Listar todas las tareas asignadas a un usuario específico.(recibe un idUsuario,devuelve un list de tareas)*/
        public List<Tarea> GetTareaTablero(int idTablero);/*Listar todas las tareas de un tablero específico. (recibe un idTablero, devuelve un list de tareas)*/
        public List<Tarea> GetAll(); 
        public void RemoveTarea(int id);//Eliminar una tarea (recibe un IdTarea)
        public void AsignarUsuario(int idUsuaruio, int idTarea);
        public void UpdateEstado(int id, EstadoTarea estado);

    }
}