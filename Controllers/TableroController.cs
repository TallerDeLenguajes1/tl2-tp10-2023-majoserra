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
        if(!IsLogin()) return RedirectToRoute(new { Controller = "Login", Action = "Index"});
        if(IsAdmin()){
            return View(new CrearTableroViewModel());
        }else{
            return RedirectToAction("Error");
        }
    }
    
    [HttpPost]
    public IActionResult CrearTablero(CrearTableroViewModel t){

        if(!IsLogin()) return RedirectToRoute(new { Controller = "Login", Action = "Index"});
    
        var id = Int32.Parse(HttpContext.Session.GetString("Id")!); // el ! saca los nulos
            
        var tablero = new Tablero(){
        Id_usuario_propetario = id,
        Nombre = t.NombreTablero,
        Descripcion = t.Descripcion
        };
        manejoTablero.CrearTablero(tablero);
        return RedirectToAction("ListarTablero");
    }

    [HttpGet]
    public IActionResult ListarTablero(){

        if(!IsLogin()) return RedirectToRoute (new {Controller = "Login", Action = "Index"}); // si no esta logueado lo mandamos al formulario asi se loguea 
        if(IsAdmin()){
            List<Tablero> tableros = new List<Tablero>();
            tableros = manejoTablero.GetTodos();
            return View(new ListarTableroViewModel(tableros));
        }else
        {
            var id = Int32.Parse(HttpContext.Session.GetString("Id")!); // el ! saca los nulos
            List<Tablero> miTableros = manejoTablero.GetTableroUsuario(id);
            return View(new ListarTableroViewModel(miTableros));
        }
    }
    [HttpGet]
    public IActionResult ModificarTablero(int id){
        if(!IsLogin()) return RedirectToRoute (new {Controller = "Login", Action = "Index"}); // si no esta logueado lo mandamos al formulario asi se loguea 
        if(IsAdmin()){
            return View(new ModificarTableroViewModel(manejoTablero.GetById(id)));
        }else
        {
            return RedirectToAction("Error");
        }
        
    }
    [HttpPost]
    public IActionResult ModificarTablero(ModificarTableroViewModel tablero){
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
            return RedirectToAction("Error");
        }
    }

    public IActionResult EliminarTablero(int id){
        manejoTablero.Remove(id);
        return RedirectToAction("ListarTablero");
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