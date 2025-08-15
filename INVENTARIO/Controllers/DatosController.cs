using INVENTARIO.Models;
using INVENTARIO.Repositorio;
using INVENTARIO.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INVENTARIO.Controllers
{
    public class DatosController : Controller
    {

        private readonly IRepositorioUsuario repositorioUsuario;
        public DatosController(IRepositorioUsuario repositorioUsuario)
        {
            this.repositorioUsuario = repositorioUsuario;
        }


        // GET: DatosController
        public IActionResult Registrarse(Registrarse usuario)
        {
            return View("~/Views/Registrarse/Registrase.cshtml");
        }

        public IActionResult Registrase(Registrarse usuario)
        {
            // Validar modelo
            if (!ModelState.IsValid)
            {
                return View("~/Views/Registrarse/Registrase.cshtml", usuario);
            }

            // Validar que el usuario no exista
            var usuarioExistente = repositorioUsuario.RegistroUsuario(usuario);
            if (usuarioExistente != null)
            {
                ModelState.AddModelError("", "El usuario ya existe.");
                return View("~/Views/Registrarse/Registrase.cshtml", usuario);
            }

            // Encriptar contraseña
            Encriptar encriptar = new Encriptar();
            usuario.Contraseña = encriptar.Encrypt(usuario.Contraseña);

            // Guardar usuario
            repositorioUsuario.RegistroUsuario(usuario);

            // Mensaje de éxito
            TempData["MensajeExito"] = "Cuenta creada exitosamente.";
            return RedirectToAction("Menu", "Home");
        }


        // GET: DatosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DatosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DatosController/Create
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

        // GET: DatosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DatosController/Edit/5
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

        // GET: DatosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DatosController/Delete/5
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
