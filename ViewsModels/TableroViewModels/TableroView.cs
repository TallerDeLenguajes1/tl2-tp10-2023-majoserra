using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;

namespace MVC.ViewModel;

public class TableroView{

    public int Id;
    public int Id_usuario_propetario;
    public string NombreTablero;
    public string Descripcion;

    public TableroView(){}

    public TableroView(Tablero t){
        Id = t.Id;
        Id_usuario_propetario = t.Id_usuario_propetario;
        NombreTablero = t.Nombre;
        Descripcion = t.Descripcion;
    }
}