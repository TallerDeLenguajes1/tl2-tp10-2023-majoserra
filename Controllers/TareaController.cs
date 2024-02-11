using System.Diagnostics;
using EspacioTablero;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_majoserra.Models;
using EspacioRepositorios;
using MVC.ViewModel;
namespace tl2_tp10_2023_majoserra.Controllers;

public class TareaController : Controller
{
    private ITareaRepository manejoTarea;
    private ITableroRepository _tableroRepository;
    private IUsuarioRepository _usuarioRepository;
    private readonly ILogger<TareaController> _logger;

    public TareaController(ILogger<TareaController> logger, ITableroRepository tableroRepository,ITareaRepository manejoTarea,IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _tableroRepository = tableroRepository;
        _usuarioRepository = usuarioRepository;
        this.manejoTarea = manejoTarea;
    }

    [HttpGet]
    public IActionResult CrearTarea() // crea una tarea 
    {
        try
        {
            if(!IsLogin()) return RedirectToRoute(new { Controller = "Login", Action = "Index"}); // controlamos que este loggeado 
            
            // mandamos la lista de mis tableros asi no se genere error alguno (?)
            var id = Int32.Parse(HttpContext.Session.GetString("Id")!); // el id de la persona que desea crear una tarea

            return View(new CrearTareaViewModel(new Tarea(), _tableroRepository.GetTableroUsuario(id), _usuarioRepository.GetAll()));   
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error"); // enviamos a  error 
        }    
    }
    [HttpPost]
    public IActionResult CrearTarea(CrearTareaViewModel t)
    {
        try
        {
            if(!IsLogin()) return RedirectToRoute(new { Controller = "Login", Action = "Index"});
            var id = Int32.Parse(HttpContext.Session.GetString("Id")!); // el id de la persona que desea crear una tarea

            var listado = _tableroRepository.GetTodos();    

            var se_puede = listado.FirstOrDefault(tablero => tablero.Id == t.Id_tablero  && tablero.Id_usuario_propetario == id);
            // si el tablero existe y ademas yo soy su duenio, entonces puedo crear tareas :)

            if (se_puede!=null)// es porque es mi tablero
            {
                var tarea  = new Tarea(){ // creamos la tarea 
                Id_tablero = t.Id_tablero,
                Id_usuario_asignado = t.Id_usuario_asignado,
                Nombre = t.Nombre,
                Descripcion = t.Descripcion,
                Estado = t.Estado,
                Color = t.Color
                };
                manejoTarea.CrearTarea(tarea); // mandamos la tarea 
                return RedirectToAction("ListarTarea"); // redireccionamos a la lista de tareas 
            }else{
                return RedirectToAction("Error");
            }
            
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error"); // enviamos a  error 
        }
    }
    [HttpGet]
    public IActionResult ListarTarea()
    {
        try
        {
            if (!IsLogin()) return RedirectToRoute(new { Controller = "Login", Action = "Index"});
            if(IsAdmin()){ // si es admin podre ver las tareas de todos, es decir todas las tareas
                var tareas = new ListarTareaViewModel(manejoTarea.GetAll());
                return View(tareas);
            }else{ // si soy operador solo podre ver  mis tareas asignadas 
                var id = Int32.Parse(HttpContext.Session.GetString("Id")!); // el ! saca los nulos
                var tareas = new ListarTareaViewModel(manejoTarea.GetTareaUsuario(id));
                return View(tareas);
            }
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error"); // enviamos a  error 
        }
        
    }
    [HttpGet]
    public IActionResult ModificarTarea(int id)  // no puedo modificar las tareas que no sean mias 
    {
        try
        {
           if(!IsLogin()) return RedirectToRoute(new { Controller = "Login", Action = "Index"});
            var idUsuario = Int32.Parse(HttpContext.Session.GetString("Id")!); // el id de la persona que desea crear una tarea
            var t = manejoTarea.GetById(id); // tarea
            var listado = _tableroRepository.GetTodos(); // tableros     
            var se_puede = listado.FirstOrDefault(tablero => tablero.Id == t.Id_tablero  && tablero.Id_usuario_propetario == idUsuario);
            if (se_puede!= null)
            {
                return View(new ModificarTareaViewModel(manejoTarea.GetById(id)));
            }else{
                return RedirectToAction("Error");
            } 
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error"); // enviamos a  error 
        }
        
    }
    [HttpPost]
    public IActionResult ModificarTarea(int id, ModificarTareaViewModel tarea)
    {
        try
        {
            var nuevo = new Tarea(){
            Nombre = tarea.Nombre,
            Descripcion = tarea.Descripcion,
            Color = tarea.Color
            };
            manejoTarea.Update(id, nuevo);
            return RedirectToAction("ListarTarea");
            
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error"); // enviamos a  error 
        }
        
    }

    public IActionResult EliminarTarea(int id)
    {
        try
        {
            if(!IsLogin()) return RedirectToRoute(new { Controller = "Login", Action = "Index"});
            manejoTarea.RemoveTarea(id);   
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error"); // enviamos a  error 
        }
        
        return RedirectToAction("ListarTarea");
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