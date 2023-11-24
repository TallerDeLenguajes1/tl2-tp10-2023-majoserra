using System.Diagnostics;
using EspacioTablero;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_majoserra.Models;
using EspacioRepositorios;
namespace tl2_tp10_2023_majoserra.Controllers;

public class TareaController : Controller
{
    const int idTableroPredefinido = 1;
    private ITareaRepository manejoTarea;
    private readonly ILogger<TareaController> _logger;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        manejoTarea = new TareaRepository();
    }

    [HttpGet]
    public IActionResult CrearTarea(){
        return View(new Tarea());
    }
    [HttpPost]
    public IActionResult CrearTarea(int id, Tarea t){
        manejoTarea.CrearTarea(idTableroPredefinido, t);
        return RedirectToAction("ListarTarea");
    }
    [HttpGet]
    public IActionResult ListarTarea(){
        List<Tarea> Tareas = new List<Tarea>();
        Tareas = manejoTarea.GetTareaTablero(idTableroPredefinido);
        return View(Tareas);
    }
    [HttpGet]
    public IActionResult ModificarTarea(int id){
        return View(manejoTarea.GetById(id));
    }
    [HttpPost]
    public IActionResult ModificarTarea(int id,string nombreT){
        manejoTarea.Update(id, nombreT);
        return RedirectToAction("ListarTarea");
    }

    public IActionResult EliminarTarea(int id){
        manejoTarea.RemoveTarea(id);
        return RedirectToAction("ListarTarea");
    }

}