using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModel;
public class LoginViewModel
{
    [Required(ErrorMessage = "Este campo es requerido")] // mensaje de error si no se manda nada
    [Display(Name = "Nombre de Usuario")] // nombre del campo
    public string NombreUsuario {get;set;}  

    [Required(ErrorMessage = "Este campo es requerido")]
    [PasswordPropertyText] // indicamos el tipo de la contrasenia
    [Display(Name = "Contraseña")]
    public string Contrasenia {get;set;}
    
}