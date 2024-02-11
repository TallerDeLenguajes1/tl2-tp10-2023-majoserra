using System.Diagnostics;
using EspacioTablero;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_majoserra.Models;
using EspacioRepositorios;
using MVC.ViewModel;
namespace tl2_tp10_2023_majoserra.Controllers;

public class TableroController : Controller
{
    private ITableroRepository manejoTablero;
    private readonly ILogger<TableroController> _logger;

    public TableroController(ILogger<TableroController> logger,ITableroRepository manejoTablero)
    {
        _logger = logger;
        this.manejoTablero = manejoTablero;
    }


    [HttpGet]
    public IActionResult CrearTablero(){
        try
        {
            if(!IsLogin()) return RedirectToRoute(new { Controller = "Login", Action = "Index"});
            return View(new CrearTableroViewModel()); // ya sea admin u operario puede crear tableros propios 
            /*if(IsAdmin()){
                return View(new CrearTableroViewModel());
            }else{
                return RedirectToRoute(new { Controller = "Home", Action = "Index"});
            }*/
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error"); // enviamos a  error 
        }
        
    }
    
    [HttpPost]
    public IActionResult CrearTablero(CrearTableroViewModel t){

        try
        {
            if(!IsLogin()) return RedirectToRoute(new { Controller = "Login", Action = "Index"});
    
            var id = Int32.Parse(HttpContext.Session.GetString("Id")!); // el ! saca los nulos
                
            var tablero = new Tablero(){
            Id_usuario_propetario = id,
            Nombre = t.NombreTablero,
            Descripcion = t.Descripcion
            };
            manejoTablero.CrearTablero(tablero);
            return RedirectToAction("ListarTablero");
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error"); // enviamos a  error 
        }    
    }

    [HttpGet]
    public IActionResult ListarTablero(){

        try
        {
            if(!IsLogin()) return RedirectToRoute (new {Controller = "Login", Action = "Index"}); // si no esta logueado lo mandamos al formulario asi se loguea 
            if(IsAdmin()){
                List<Tablero> tableros = new List<Tablero>();
                tableros = manejoTablero.GetTodos();
                return View(new ListarTableroViewModel(tableros));
            }else
            {
                return RedirectToAction("ListarTableroOperador");
                
            } 
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error"); // enviamos a  error 
        }  

        
    }

    [HttpGet]
    public IActionResult ListarTableroOperador(){
        if(!IsLogin()) return RedirectToRoute (new {Controller = "Login", Action = "Index"}); // si no esta logueado lo mandamos al formulario asi se loguea 
        if(!IsAdmin()){
        var id = Int32.Parse(HttpContext.Session.GetString("Id")!); // el ! saca los nulos
        List<Tablero> tableros = new List<Tablero>();
        tableros = manejoTablero.GetTodos();
        List<Tablero> mistableros = manejoTablero.GetTableroUsuario(id);
        return View(new ListarTableroViewModel(tableros, mistableros));
        }else
        {
            return RedirectToAction("Error");
        }
    }
    [HttpGet]
    public IActionResult ModificarTablero(int id){
        try
        {
            if(!IsLogin()) return RedirectToRoute (new {Controller = "Login", Action = "Index"}); // si no esta logueado lo mandamos al formulario asi se loguea 
            return View(new ModificarTableroViewModel(manejoTablero.GetById(id)));
            
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error"); // enviamos a  error 
        }  
        
        
    }
    [HttpPost]
    public IActionResult ModificarTablero(ModificarTableroViewModel tablero){

        try
        {
            if(!IsLogin()) return RedirectToRoute (new {Controller = "Login", Action = "Index"}); // si no esta logueado lo mandamos al formulario asi se loguea 
            if(IsAdmin()){

                var t = new Tablero(){
                    Id = tablero.Id,
                    Id_usuario_propetario=tablero.Id_usuario_propetario,
                    Nombre = tablero.NombreTablero,
                    Descripcion = tablero.Descripcion
                };

                manejoTablero.Update(t.Id, t);
                return RedirectToAction("ListarTablero");
            }else{
                var id = Int32.Parse(HttpContext.Session.GetString("Id")!); // el id de la persona que desea crear una tarea

                var listado = manejoTablero.GetTableroUsuario(id);
                var se_puede = listado.FirstOrDefault(tablero => tablero.Id_usuario_propetario == id);

                if (se_puede!=null)// es porque es mi tablero
                {
                    var t = new Tablero(){
                    Id = tablero.Id,
                    Id_usuario_propetario=tablero.Id_usuario_propetario,
                    Nombre = tablero.NombreTablero,
                    Descripcion = tablero.Descripcion
                    };

                    manejoTablero.Update(t.Id, t);
                    return RedirectToAction("ListarTablero");
                }else{
                    return RedirectToAction("Error");
                }
            
                
            }
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error"); // enviamos a  error 
        }  
        
    }

    public IActionResult EliminarTablero(int id){
        try
        {
            manejoTablero.Remove(id);
            return RedirectToAction("ListarTablero");
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error"); // enviamos a  error 
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