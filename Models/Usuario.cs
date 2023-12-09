using Microsoft.AspNetCore.Authorization.Infrastructure;
using MVC.ViewModel;

public enum Roles{
    Administrador = 0,
    Operador = 1
}
namespace EspacioTablero{
    public class Usuario{
        private int id;
        private string nombreDeUsuario;
        private string contrasenia;
        private Roles rol;

        /*public Usuario(LoginViewModel loginViewModel){
            NombreDeUsuario = loginViewModel.NombreUsuario;
            Contrasenia = loginViewModel.Contrasenia;
        }

        public Usuario(){}
*/
        public int Id { get => id; set => id = value; }
        public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
        public string Contrasenia { get => contrasenia; set => contrasenia = value; }
        public Roles Rol { get => rol; set => rol = value; }
    }
}