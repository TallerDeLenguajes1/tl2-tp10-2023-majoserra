using System.Diagnostics;
using EspacioTablero;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_majoserra.Models;
using EspacioRepositorios;
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
    public IActionResult CrearUsuario(){
        return View(new Usuario());
    }

    [HttpPost]

    public IActionResult CrearUsuario(Usuario usuario){
        manejoUsuario.Create(usuario);
        return RedirectToAction("ListarUsuario");
    }

    // Listar Usuario
    [HttpGet]
    public IActionResult ListarUsuario(){
        List<Usuario> listaUsuario = manejoUsuario.GetAll();
        return View(listaUsuario);
    }

    // Modificar Usuario
    [HttpGet]
    public IActionResult ModificarUsuario(int id)
    {
        return View(manejoUsuario.GetById(id));
    }
    [HttpPost]
    public IActionResult ModificarUsuario(int id, string Nombre){
        manejoUsuario.Update(id, Nombre);
        return RedirectToAction("ListarUsuario");
    }

    

}