using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;


namespace MVC.ViewModel;

public class ListarTareaViewModel{

    private List<TareaView> tareasView;
    public List<TareaView> TareasView { get => tareasView; set => tareasView = value; }

    public ListarTareaViewModel(List<Tarea> tareas){

        tareasView = new List<TareaView>();

        foreach (var t in tareas)
        {
            var tareaView = new TareaView(t);
            tareasView.Add(tareaView);
        }

    }

}