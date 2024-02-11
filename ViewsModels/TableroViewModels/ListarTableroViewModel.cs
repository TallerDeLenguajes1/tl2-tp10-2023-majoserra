using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;

namespace MVC.ViewModel;

public class ListarTableroViewModel{
    private List<TableroView> tablerosView;
    private List<TableroView> mistablerosView;
    public List<TableroView> TablerosView { get => tablerosView; set => tablerosView = value; }
    public List<TableroView> MistablerosView { get => mistablerosView; set => mistablerosView = value; }

    

    public ListarTableroViewModel(List<Tablero> listado){
        
        tablerosView = new List<TableroView>(); // inicializamos el tablero 

        foreach (var t in listado)
        {
            var tablero = new TableroView(t); // creamos un tablero view
            TablerosView.Add(tablero); // lo cargamos a la lista 
        }
    }

    public ListarTableroViewModel(List<Tablero> listado, List<Tablero> mistablero){
        
        tablerosView = new List<TableroView>(); // inicializamos el tablero 
        mistablerosView = new List<TableroView>(); // inicializamos mis tableros 

        foreach (var t in listado)
        {
            var tablero = new TableroView(t); // creamos un tablero view
            TablerosView.Add(tablero); // lo cargamos a la lista 
        }
        foreach (var t in mistablero)
        {
            var tablero = new TableroView(t); // creamos un tablero view
            MistablerosView.Add(tablero); // lo cargamos a la lista 
        }
    }

   }