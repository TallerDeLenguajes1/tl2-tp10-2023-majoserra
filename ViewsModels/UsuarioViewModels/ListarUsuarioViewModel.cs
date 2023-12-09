using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;

namespace MVC.ViewModel;

public class ListarUsuarioViewModel{

    private List<UsuarioView> usuariosViews;
    public List<UsuarioView> UsuariosViews { get => usuariosViews; set => usuariosViews = value; }

    public ListarUsuarioViewModel(List<Usuario> usuarios){

        usuariosViews = new List<UsuarioView>(); // Lista de UsuariosViews

        foreach (var u in usuarios)
        {
            var usuario = new UsuarioView(u); // creamos un usuarioView es lo mismo que usuario sin la contraseña
            usuariosViews.Add(usuario);
        }
    }
}