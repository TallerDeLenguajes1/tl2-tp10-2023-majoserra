namespace EspacioTablero;

public class Tablero
{
    private int id;
    private int id_usuario_propetario;
    private string nombre;
    private string descripcion;

    public int Id { get => id; set => id = value; }
    public int Id_usuario_propetario { get => id_usuario_propetario; set => id_usuario_propetario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
}