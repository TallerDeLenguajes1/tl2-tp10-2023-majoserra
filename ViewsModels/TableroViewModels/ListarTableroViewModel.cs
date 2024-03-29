using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;

namespace MVC.ViewModel;

public class ListarTableroViewModel{
    private List<TableroView> tablerosView;
    private List<TableroView> mistablerosView;
    public List<TableroView> TablerosView { get => tablerosView; set => tablerosView = value; }
    public List<TableroView> MistablerosView { get => mistablerosView; set => mistablerosView = value; }

    

    public ListarTableroViewModel(List<Tablero> listado, List<Usuario> usuarios){
        
        tablerosView = new List<TableroView>(); // inicializamos el tablero 

        foreach (var t in listado)
        {
            var tablero = new TableroView(t); // creamos un tablero view
            var usuario = usuarios.FirstOrDefault(u => u.Id == tablero.Id_usuario_propetario);
            if (usuario != null)
            {
                tablero.NombrePropietario = usuario.NombreDeUsuario;
            }
            TablerosView.Add(tablero); // lo cargamos a la lista 
        }
    }

    public ListarTableroViewModel(List<Tablero> listado, List<Tablero> mistablero,  List<Usuario> usuarios){
        
        tablerosView = new List<TableroView>(); // inicializamos el tablero 
        mistablerosView = new List<TableroView>(); // inicializamos mis tableros 

        foreach (var t in listado)
        {
            var tablero = new TableroView(t); // creamos un tablero view
            var usuario = usuarios.FirstOrDefault(u => u.Id == tablero.Id_usuario_propetario);
            if (usuario != null)
            {
                tablero.NombrePropietario = usuario.NombreDeUsuario;
            }
            TablerosView.Add(tablero); // lo cargamos a la lista 
        }
        foreach (var t in mistablero)
        {
            var tablero = new TableroView(t); // creamos un tablero view
            var usuario = usuarios.FirstOrDefault(u => u.Id == tablero.Id_usuario_propetario);
            if (usuario != null)
            {
                tablero.NombrePropietario = usuario.NombreDeUsuario;
            }
            MistablerosView.Add(tablero); // lo cargamos a la lista 
        }
    }

   }