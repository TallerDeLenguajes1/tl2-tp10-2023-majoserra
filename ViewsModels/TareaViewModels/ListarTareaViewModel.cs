using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;


namespace MVC.ViewModel;

public class ListarTareaViewModel{

    private List<TareaView> tareasView;
    private List<TareaView> mitablerotarea;
    private List<TareaView> asignadas;
    public List<TareaView> TareasView { get => tareasView; set => tareasView = value; }
    public List<TareaView> Mitablerotarea { get => mitablerotarea; set => mitablerotarea = value; }
    public List<TareaView> Asignadas { get => asignadas; set => asignadas = value; }

    public ListarTareaViewModel(List<Tarea> tareas, List<Usuario> usuarios){

        tareasView = new List<TareaView>();

        foreach (var t in tareas)
        {
            var tareaView = new TareaView(t);
            var usuario = usuarios.FirstOrDefault(u => u.Id == tareaView.Id_usuario_asignado);
            if (usuario != null)
            {
                tareaView.NombreUsuario = usuario.NombreDeUsuario;
            }
            tareasView.Add(tareaView);
        }
 

    }
    public ListarTareaViewModel(List<Tarea> todas, List<Tarea> mitablerotarea,List<Tarea> asignadas,List<Usuario> usuarios){

        tareasView = new List<TareaView>();
        this.mitablerotarea = new List<TareaView>();
        this.asignadas = new List<TareaView>();

        foreach (var t in todas)
        {
            var tareaView = new TareaView(t);
            var usuario = usuarios.FirstOrDefault(u => u.Id == tareaView.Id_usuario_asignado);
            if (usuario != null)
            {
                tareaView.NombreUsuario = usuario.NombreDeUsuario;
            }
            tareasView.Add(tareaView);
        }

        foreach (var t in mitablerotarea)
        {
            var tareaView = new TareaView(t);
            var usuario = usuarios.FirstOrDefault(u => u.Id == tareaView.Id_usuario_asignado);
            if (usuario != null)
            {
                tareaView.NombreUsuario = usuario.NombreDeUsuario;
            }
            this.mitablerotarea.Add(tareaView);
        }

        foreach (var t in asignadas)
        {
            var tareaView = new TareaView(t);
            var usuario = usuarios.FirstOrDefault(u => u.Id == tareaView.Id_usuario_asignado);
            if (usuario != null)
            {
                tareaView.NombreUsuario = usuario.NombreDeUsuario;
            }
            this.asignadas.Add(tareaView);
        }

    }

}