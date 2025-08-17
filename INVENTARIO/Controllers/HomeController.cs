using INVENTARIO.Models;
using INVENTARIO.Repositorio;
using INVENTARIO.Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace INVENTARIO.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepositorioUsuario repositorioUsuario;
        private readonly IRepositorioProducto repositorioProducto;
        public HomeController(IRepositorioUsuario repositorioUsuario, IRepositorioProducto repositorioProducto)
        {
            this.repositorioUsuario = repositorioUsuario;
            this.repositorioProducto = repositorioProducto;
        }
        public IActionResult Entrada()
        {
            var productos = repositorioProducto.ListarProductos();

            if (productos == null)
            {
                productos = new List<ProductoModel>();
            }

            return View(productos);
        }



        public async Task<IActionResult> InformacionPersonal()
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                // Redirigir al login si no hay sesión
                TempData["ErrorLogin"] = "Debe iniciar sesión para acceder a esta sección.";
                return RedirectToAction("Login", "Logins");
            }

            var usuario = await repositorioUsuario.ObtenerPorId(usuarioId.Value);

            if (usuario == null)
            {
                return View("Error", new ErrorViewModel
                {
                    message = "Usuario no encontrado",
                    RequestId = HttpContext.TraceIdentifier
                });
            }

            return View("~/Views/Configuracion/InformacionPersonal.cshtml", usuario);
        }

        public IActionResult Seguridad()
        {
            return View("~/Views/Configuracion/Seguridad.cshtml");
        }

        public IActionResult Configuracion()
        {
            return View("~/Views/Configuracion/Configuracion.cshtml");
        }

        public IActionResult Menu()
        {
            return View("~/Views/Home/Menu.cshtml");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

