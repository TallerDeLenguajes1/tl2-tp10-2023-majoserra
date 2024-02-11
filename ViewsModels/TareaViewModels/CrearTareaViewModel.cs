using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioRepositorios;
using EspacioTablero;


namespace MVC.ViewModel;

public class CrearTareaViewModel{

    [Required (ErrorMessage ="Este campo es requerido")]
    public int Id {get;set;}


    [Required (ErrorMessage ="Este campo es requerido")]
    public int Id_tablero {get;set;}

    [Required(ErrorMessage = "El campo es obligatorio")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "La longitud de la cadena debe estar entre 2 y 50 caracteres")]    
    [Display(Name = "Nombre de Tarea")] // nombre del campo
    public string Nombre {get;set;}
    public EstadoTarea Estado {get;set;}

    [Required(ErrorMessage = "El campo es obligatorio")]
    [StringLength(2000, MinimumLength = 2, ErrorMessage = "La longitud de la cadena debe estar entre 2 y 50 caracteres")]    
    [Display(Name = "Descripcion")] // nombre del campo
    public string Descripcion {get;set;}

    [Required(ErrorMessage = "El campo es obligatorio")]
    [StringLength(15, MinimumLength = 2, ErrorMessage = "La longitud de la cadena debe estar entre 2 y 50 caracteres")]    
    [Display(Name = "Color")] // nombre del campo
    public string Color {get;set;}
    [Required (ErrorMessage ="Este campo es requerido")]
    public int Id_usuario_asignado {get;set;}


    public List<Tablero> Tableros;
    public List<Usuario> Usuarios;
    
    public CrearTareaViewModel(){}
    public CrearTareaViewModel(Tarea t, List<Tablero> tableros, List<Usuario> usuarios){
        Nombre = t.Nombre;
        Descripcion = t.Descripcion;
        Color = t.Color;
        Id_usuario_asignado = t.Id_usuario_asignado;
        this.Tableros = tableros;
        this.Usuarios = usuarios;
    }
}