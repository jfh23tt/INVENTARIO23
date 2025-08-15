using System.Reflection;
using INVENTARIO.Models;
using INVENTARIO.Repositorio;
using INVENTARIO.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INVENTARIO.Controllers
{
    public class LoginsController : Controller
    {
        private readonly IRepositorioUsuario repositorioUsuario;
        public LoginsController(IRepositorioUsuario repositorioUsuario)
        {
            this.repositorioUsuario = repositorioUsuario;
        }
        // GET: LoginsController
        public IActionResult Logins(LoginsModel GuardarL)
        {
            return View(GuardarL);
        }
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Logins", "Logins");
        }

        [HttpPost]
        public async Task<IActionResult> inicio(LoginsModel informacion)
        {
            ErrorViewModel errorViewModel = new ErrorViewModel();

            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Logins", "Logins");
                }

                Encriptar clave = new Encriptar();
                informacion.contraseña = clave.Encrypt(informacion.contraseña);

                var usuario = await repositorioUsuario.ValidarUsuario(informacion);

                if (usuario != null)
                {
                    // ✅ Aquí guardas el ID del usuario en la sesión
                    HttpContext.Session.SetInt32("UsuarioId", usuario.Id);

                    // Luego lo rediriges a la vista deseada
                    return View("~/Views/Home/Menu.cshtml");
                }

                TempData["ErrorLogin"] = "Correo o contraseña incorrecta";
                return RedirectToAction("Logins", "Logins");
            }
            catch (Exception ex)
            {
                errorViewModel.RequestId = ex.HResult.ToString();
                errorViewModel.message = ex.Message;
                return View("Error", errorViewModel);
            }
        }






        // GET: LoginsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
