using System.Diagnostics;
using EspacioTablero;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_majoserra.Models;
using EspacioRepositorios;
using MVC.ViewModel;
namespace tl2_tp10_2023_majoserra.Controllers;

public class UsuarioController : Controller
{
    private IUsuarioRepository manejoUsuario;
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        manejoUsuario = new UsuarioRepository();
    }

    // Crear usuario

    [HttpGet]
    public IActionResult CrearUsuario(){ // consultar si es necesario este doble control 
        if (!IsLogin()) return RedirectToRoute (new { Controller = "Login", action = "Index"});
        if(IsAdmin()){
            return View(new CrearUsuarioViewModel()); // hacemos uso de la viewmodel 
        }else{
            return RedirectToAction("Error");
        }
    }

    [HttpPost]

    public IActionResult CrearUsuario(CrearUsuarioViewModel usuario){ // usamos la view model 
        if (IsAdmin())
        {
            var nuevo = new Usuario(){ // creamos un usuario usando la viewmodel 
                NombreDeUsuario = usuario.NombreDeUsuario,
                Contrasenia = usuario.Contrasenia,
                Rol = usuario.Rol
            };
            manejoUsuario.Create(nuevo); // le mandamos el nuevo usuario
            return RedirectToAction("ListarUsuario"); // podemos ver los usuarios 
        }else
        {
            return RedirectToAction("Error"); // si no soy admin mostramos el error, ya que no vamos a poder crear usuarios :(
        }
    }

    // Listar Usuario
    [HttpGet]
    public IActionResult ListarUsuario(){

        if (!IsLogin()) return RedirectToRoute(new { controller = "Login", action = "Index"}); // si no esta logueado lo llevamos a login
        
        if (IsAdmin())
        {
            List<Usuario> listaUsuario = manejoUsuario.GetAll(); // obtenemos la lista de usuarios
            var listadoView = new ListarUsuarioViewModel(listaUsuario);  // creamos la lista de usuarios view
            return View(listadoView); // devolvemos la vista de usuarios view 
        }else{
            return RedirectToAction("Error");
            
        }
     
    }
    // [HttpGet]

    // public IActionResult MostrarUsuario(){
    //     if (!IsLogin()) return RedirectToRoute(new { controller = "Login", action = "Index"}); // si no esta logueado lo llevamos a login
        
    //     if (IsAdmin())
    //     {
    //         return RedirectToAction("ListarUsuario");
    //     }else{ // si es operador
    //         var id = Int32.Parse(HttpContext.Session.GetString("Id")!); // el ! saca los nulos, obtenemos el id
    //         var usuario =  manejoUsuario.GetById(id);
    //         var usuarioView = new UsuarioView(usuario);
    //         return View(usuarioView);
    //     }  
    // }

    // Modificar Usuario
    [HttpGet]
    public IActionResult ModificarUsuario(int id)
    {
        return View(new ModificarUsuarioViewModel(manejoUsuario.GetById(id)));
    }
    [HttpPost]
    public IActionResult ModificarUsuario(int id, ModificarUsuarioViewModel usuario){
        
        if(!IsLogin()) return RedirectToRoute( new { controller = "Login", action = "Index"});
        if(IsAdmin()){
            var nuevo = new Usuario(){
                NombreDeUsuario = usuario.NombreDeUsuario,
                Contrasenia = usuario.Contrasenia,
                Rol = usuario.Rol
            };
            
            manejoUsuario.Update(id, nuevo);
            return RedirectToAction("ListarUsuario");
        }else{
            return RedirectToAction("Error");
        }
    }

    // Eliminar Usuario REALIZAR UN MEJOR CONTROL
    public IActionResult EliminarUsuario(int id){
        manejoUsuario.Remove(id);
        return RedirectToAction("ListarUsuario");
    }

    private bool IsAdmin()
    {
        if(HttpContext.Session != null && HttpContext.Session.GetString("Rol") == Enum.GetName(Roles.Administrador)) return true;
        return false;
    }

    private bool IsLogin()
    {
        if (HttpContext.Session.GetString("Id") != null) 
            return true;
            return false;
    }
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


}