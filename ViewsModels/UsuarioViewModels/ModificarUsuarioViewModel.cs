using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;

namespace MVC.ViewModel;
    public class ModificarUsuarioViewModel{

        public int Id {get;set;}

        [Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "La longitud de la cadena debe estar entre 2 y 50 caracteres")]    
        [Display(Name = "Nombre de Usuario")] // nombre del campo
        public string NombreDeUsuario {get;set;}

        [Required(ErrorMessage = "Este campo es requerido")]
        [PasswordPropertyText] // indicamos el tipo de la contrasenia
        [Display(Name = "Contraseña")]
        public string Contrasenia {get;set;}

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Tipo Usuario")]
        public Roles Rol {get;set;}

        public ModificarUsuarioViewModel(){}

        public ModificarUsuarioViewModel(Usuario usuario){
            NombreDeUsuario = usuario.NombreDeUsuario;
            Contrasenia = usuario.Contrasenia;
            Rol = usuario.Rol;
            Id = usuario.Id;
        }
    }