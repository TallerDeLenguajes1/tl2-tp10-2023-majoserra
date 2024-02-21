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
    private ITableroRepository _tableroRepository;
    private ITareaRepository _tareaRepository;
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository manejoUsuario, ITableroRepository tableroRepository,ITareaRepository tareaRepository)
    {
        _logger = logger;
        // Inyeccion de Dependencia
        this.manejoUsuario = manejoUsuario;
        _tableroRepository = tableroRepository;
        _tareaRepository = tareaRepository;
    }

    // Crear usuario

    [HttpGet]
    public IActionResult CrearUsuario(){ // consultar si es necesario este doble control 
        
        try
        {
            if(IsAdmin()){
                return View(new CrearUsuarioViewModel()); // hacemos uso de la viewmodel 
            }
            return RedirectToRoute (new { Controller = "Login", action = "Index"});
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error"); // enviamos a  error 
        }
    }

    [HttpPost]

    public IActionResult CrearUsuario(CrearUsuarioViewModel usuario){ // usamos la view model 
        
        try
        {
            if(!ModelState.IsValid) return RedirectToAction("CrearUsuario"); //si por alguna razon el modelo no es valido lo mandamos al formulario de nuevo 
            if (IsAdmin())
            {
                var nuevo = new Usuario(){ // creamos un usuario usando la viewmodel 
                    NombreDeUsuario = usuario.NombreDeUsuario,
                    Contrasenia = usuario.Contrasenia,
                    Rol = usuario.Rol
                };
                manejoUsuario.Create(nuevo); // le mandamos el nuevo usuario
                return RedirectToAction("ListarUsuario"); // podemos ver los usuarios 
            }
            return RedirectToRoute (new { Controller = "Login", action = "Index"}); // lo mandamos al login 

        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
        
    }

    // Listar Usuario
    [HttpGet]
    public IActionResult ListarUsuario(){

        try
        {
            if (!IsLogin()) return RedirectToRoute(new { controller = "Login", action = "Index"}); // si no esta logueado lo llevamos a login
            if (IsAdmin())
            {
                var id = Int32.Parse(HttpContext.Session.GetString("Id")!); // el id de la persona que desea crear una tarea
                List<Usuario> listaUsuario = manejoUsuario.GetAll(); // obtenemos la lista de usuarios
                var listadoView = new ListarUsuarioViewModel(listaUsuario, id);  // creamos la lista de usuarios view
                return View(listadoView); // devolvemos la vista de usuarios view 
            }else{
                return RedirectToAction("ListarUsuarioOperador");
            }
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    
    [HttpGet]

    public IActionResult ListarUsuarioOperador(){
        try
        {
        if (!IsLogin()) return RedirectToRoute(new { controller = "Login", action = "Index"}); // si no esta logueado lo llevamos a login
        if (!IsAdmin())
        {
            List<Usuario> listaUsuario = manejoUsuario.GetAll(); // obtenemos la lista de usuarios
            var id = Int32.Parse(HttpContext.Session.GetString("Id")!); // el id de la persona que desea crear una tarea
            var listadoView = new ListarUsuarioViewModel(listaUsuario, id);  // creamos la lista de usuarios view
            return View(listadoView); // devolvemos la vista de usuarios view 
        }else{
            return RedirectToAction("Error");
        }
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
        
    }

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
    public IActionResult ModificarUsuario(int id) // van los try catch aqui?
    {
        try
        {
            if(!IsLogin()) return RedirectToRoute( new { controller = "Login", action = "Index"});
            /*if(IsAdmin()){
                return View(new ModificarUsuarioViewModel(manejoUsuario.GetById(id)));
            }else{
                return RedirectToRoute( new { controller = "Home", action = "Index"});
            }*/
             if(IsAdmin()) {
                var usuarioView = new ModificarUsuarioViewModel(manejoUsuario.GetById(id));
                return View("ModificarUsuario",usuarioView);

            } else if(Int32.Parse(HttpContext.Session.GetString("Id")!) == id){
                var usuarioView = new ModificarUsuarioViewModel(manejoUsuario.GetById(id));
                return View("ModificarUsuarioOperador",usuarioView);
            }
            return RedirectToAction("ListarUsuario");
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
        
        
    }
    [HttpPost]
    public IActionResult ModificarUsuarioOperador(int id, ModificarUsuarioViewModel usuario){
        try
        {
            
            if(!IsLogin()) return RedirectToRoute( new { controller = "Login", action = "Index"});
            if(!IsAdmin()){
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
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
        
    }
    [HttpPost]
    public IActionResult ModificarUsuario(int id, ModificarUsuarioViewModel usuario){
        try
        {
            if(!ModelState.IsValid) return RedirectToAction("ModificarUsuario");
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
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
        
    }

    // Eliminar Usuario REALIZAR UN MEJOR CONTROL
    // NO AÑADIR EL HTTPPOST (consultar porque no funciona con el)
    public IActionResult EliminarUsuario(int id){
        try
        {
            if(!IsLogin()) return RedirectToRoute( new { controller = "Login", action = "Index"});
            if (IsAdmin())
            {
                _tareaRepository.UpdateTareaAsignada(id);
                _tareaRepository.RemoveTareaUsuario(id);
                _tableroRepository.RemoveTableroUsuario(id);
                manejoUsuario.Remove(id);
                return RedirectToAction("ListarUsuario");
            }else
            {
                return RedirectToAction("Error");
            } 
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
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