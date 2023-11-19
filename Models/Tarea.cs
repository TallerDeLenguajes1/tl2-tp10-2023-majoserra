namespace EspacioTablero;
public enum EstadoTarea{
    ToDo,
    Doing,
    Review,
    Done
}
public class Tarea{
    private int id;
    private int id_tablero;
    private string nombre;
    private EstadoTarea estado;
    private string descripcion;
    private string color;
    private int id_usuario_asignado;

    public int Id { get => id; set => id = value; }
    public int Id_tablero { get => id_tablero; set => id_tablero = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public EstadoTarea Estado { get => estado; set => estado = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public string Color { get => color; set => color = value; }
    public int Id_usuario_asignado { get => id_usuario_asignado; set => id_usuario_asignado = value; }
}