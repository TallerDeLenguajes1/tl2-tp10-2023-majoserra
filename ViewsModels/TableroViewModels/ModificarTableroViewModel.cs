using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;

namespace MVC.ViewModel;

public class ModificarTableroViewModel{

    public int Id {get;set;}

    public int Id_usuario_propetario {get;set;}

    [Required(ErrorMessage = "El campo es obligatorio")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "La longitud de la cadena debe estar entre 2 y 50 caracteres")]    
    [Display(Name = "Nombre Tablero")] // nombre del campo
    public string NombreTablero {get;set;}

    [Required(ErrorMessage = "El campo es obligatorio")]
    [StringLength(300, MinimumLength = 2, ErrorMessage = "La longitud de la cadena debe estar entre 2 y 50 caracteres")]    
    [Display(Name = "Nombre Tablero")] // nombre del campo
    public string Descripcion {get;set;}

    public ModificarTableroViewModel(){}
    public ModificarTableroViewModel(Tablero t){
        Id_usuario_propetario = t.Id_usuario_propetario;
        NombreTablero = t.Nombre;
        Descripcion = t.Descripcion;
    }
    
}