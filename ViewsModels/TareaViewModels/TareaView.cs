using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;

namespace MVC.ViewModel;

public class TareaView{
    public int Id {get;set;}
    public int Id_tablero {get;set;}
    public string Nombre {get;set;}
    public EstadoTarea Estado {get;set;}
    public string Descripcion {get;set;}
    public string Color {get;set;}
    public int Id_usuario_asignado {get;set;}

    public string NombreUsuario {get;set;}

    public TareaView(){}

    public TareaView(Tarea t){
        Id = t.Id;
        Id_tablero = t.Id_tablero;
        Nombre = t.Nombre;
        Estado = t.Estado;
        Descripcion = t.Descripcion;
        Color = t.Color;
        Id_usuario_asignado = t.Id_usuario_asignado;
    }



}