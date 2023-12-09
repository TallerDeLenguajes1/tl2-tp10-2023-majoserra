using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;

namespace MVC.ViewModel;

public class ListarTableroViewModel{
    private List<TableroView> tablerosView;
    public List<TableroView> TablerosView { get => tablerosView; set => tablerosView = value; }


    public ListarTableroViewModel(List<Tablero> listado){
        
        tablerosView = new List<TableroView>(); // inicializamos el tablero 

        foreach (var t in listado)
        {
            var tablero = new TableroView(t); // creamos un tablero view
            TablerosView.Add(tablero); // lo cargamos a la lista 
        }
    }

   }