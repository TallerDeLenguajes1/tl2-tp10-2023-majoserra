using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;

namespace MVC.ViewModel;

public class CrearTableroViewModel{

    // [Range(1, int.MaxValue, ErrorMessage = "El campo Id_usuario_propetario debe ser mayor que 0.")]
    // public int Id_usuario_propetario {get;set;}

    [Required(ErrorMessage = "El campo es obligatorio")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "La longitud de la cadena debe estar entre 2 y 50 caracteres")]    
    [Display(Name = "Nombre")] // nombre del campo
    public string NombreTablero {get;set;}

    [Required(ErrorMessage = "El campo es obligatorio")]
    [StringLength(300, MinimumLength = 2, ErrorMessage = "La longitud de la cadena debe estar entre 2 y 50 caracteres")]    
    [Display(Name = "Descripcion")] // nombre del campo
    public string Descripcion {get;set;}

    public CrearTableroViewModel(){}



}