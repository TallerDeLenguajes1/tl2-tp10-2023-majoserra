using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;

namespace MVC.ViewModel;

public class ListarUsuarioViewModel{

    private List<UsuarioView> usuariosViews;
    public List<UsuarioView> UsuariosViews { get => usuariosViews; set => usuariosViews = value; }
    public Usuario UsuarioLogin { get => usuarioLogin; set => usuarioLogin = value; }

    private Usuario usuarioLogin;
    

    public ListarUsuarioViewModel(List<Usuario> usuarios,int id){

        usuariosViews = new List<UsuarioView>(); // Lista de UsuariosViews

        foreach (var u in usuarios)
        {
            if (u.Id != 0) // no listar el usuario que tengo por defecto "Sin usuario"
            {
                if (u.Id == id)
                {
                    usuarioLogin = u;
                }
                var usuario = new UsuarioView(u); // creamos un usuarioView es lo mismo que usuario sin la contraseña
                usuariosViews.Add(usuario);
            }
            
        }
    }
}