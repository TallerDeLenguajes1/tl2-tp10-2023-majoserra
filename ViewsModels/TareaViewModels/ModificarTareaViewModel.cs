using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;


namespace MVC.ViewModel;

public class ModificarTareaViewModel{
    public int Id{get;set;}
    public int Id_tablero {get;set;}

    
    [Required(ErrorMessage = "El campo es obligatorio")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "La longitud de la cadena debe estar entre 2 y 50 caracteres")]    
    [Display(Name = "Nombre Tarea")] // nombre del campo
    public string Nombre {get;set;}

    [Required(ErrorMessage = "Este campo no puede estar vacio")]
    [Display(Name = "Estado Tarea")]
    public EstadoTarea Estado {get;set;}

    [Required(ErrorMessage = "El campo es obligatorio")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "La longitud de la cadena debe estar entre 2 y 50 caracteres")]    
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
    
    public ModificarTareaViewModel(){}
    public ModificarTareaViewModel(Tarea t, List<Usuario> usuarios){
        Nombre = t.Nombre;
        Descripcion = t.Descripcion;
        Color = t.Color;
        Estado = t.Estado;
        Id = t.Id;
        Id_tablero = t.Id_tablero;
        Id_usuario_asignado = t.Id_usuario_asignado;
        this.Usuarios = usuarios;
    }
}