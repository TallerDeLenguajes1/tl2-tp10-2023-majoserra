using System.Diagnostics;
using EspacioTablero;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_majoserra.Models;
using EspacioRepositorios;
namespace tl2_tp10_2023_majoserra.Controllers;

public class TableroController : Controller
{
    private ITableroRepository manejoTablero;
    private readonly ILogger<TableroController> _logger;

    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        manejoTablero = new TableroRepository();
    }

    [HttpGet]
    public IActionResult CrearTablero(){
        return View(new Tablero());
    }
    [HttpPost]
    public IActionResult CrearTablero(Tablero t){
        manejoTablero.CrearTablero(t);
        return RedirectToAction("ListarTablero");
    }
    [HttpGet]
    public IActionResult ListarTablero(){
        List<Tablero> tableros = new List<Tablero>();
        tableros = manejoTablero.GetTodos();
        return View(tableros);
    }
    [HttpGet]
    public IActionResult ModificarTablero(int id){
        return View(manejoTablero.GetById(id));
    }
    [HttpPost]
    public IActionResult ModificarTablero(Tablero tablero){
        manejoTablero.Update(tablero.Id, tablero);
        return RedirectToAction("ListarTablero");
    }

    public IActionResult EliminarTablero(int id){
        manejoTablero.Remove(id);
        return RedirectToAction("ListarTablero");
    }

}