using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;

namespace MVC.ViewModel;

public class UsuarioView{
    
    public int Id {get;set;} 
    public string NombreDeUsuario {get;set;} 
    public Roles Rol {get;set;} 
   // public string Contrasenia {get;set;}

    public UsuarioView(Usuario usuario){
        Id = usuario.Id;
        NombreDeUsuario = usuario.NombreDeUsuario;
        Rol = usuario.Rol;
       // Contrasenia = usuario.Contrasenia;
    }
}