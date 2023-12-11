using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_majoserra.Models;
using EspacioRepositorios;
using EspacioTablero;
using MVC.ViewModel;
using Microsoft.AspNetCore.Session;

namespace tl2_tp10_2023_majoserra.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private IUsuarioRepository manejoUsuario;
    

    public LoginController(ILogger<LoginController> logger, IUsuarioRepository manejoUsuario)
    {
        _logger = logger;
        this.manejoUsuario = manejoUsuario;
    }

    public IActionResult Index(){
        return View(new LoginViewModel());
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel usuario){

        try
        {
            List<Usuario> usuarios = manejoUsuario.GetAll(); // obtenemos la lista de usuario
            // buscamos el usuario
            var usuarioLogeado = usuarios.FirstOrDefault(u => u.NombreDeUsuario == usuario.NombreUsuario && u.Contrasenia == usuario.Contrasenia);
            // si el usuario no existe lo devolvemos al index
            if(usuarioLogeado == null) {
                return RedirectToAction("Error"); // En caso de no estar logueado se muestra un mensaje de error 
            }else{
                //muestre por consola logueo de tipo info
                 _logger.LogInformation("El Usuario " + usuarioLogeado.NombreDeUsuario + " Ingreso Correctamente");
                // Registramos el usuario 
                loguearUsuario(usuarioLogeado);
                // Devolvemos el Usuario al Home
                return RedirectToRoute(new { controller = "Tablero", action = "ListarTablero"});
            }   
        }catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            //muestre por consola logueo de tipo warning 
            _logger.LogWarning("Intento de acceso invalido - Usuario: " + usuario.NombreUsuario + " Clave ingresada: " + usuario.Contrasenia);
            TempData["ErrorMessage"] = "Nombre de usuario o contraseña incorrectos.";
            return RedirectToAction("Index");
        }
    }

    private void loguearUsuario(Usuario usuario)
    {
        HttpContext.Session.SetString("Id", usuario.Id.ToString());
        HttpContext.Session.SetString("NombreDeUsuario", usuario.NombreDeUsuario);
        HttpContext.Session.SetString("Contrasenia", usuario.Contrasenia);
        HttpContext.Session.SetString("Rol", usuario.Rol.ToString());
    }

    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    
}