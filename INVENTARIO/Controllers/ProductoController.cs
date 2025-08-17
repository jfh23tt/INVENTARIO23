using INVENTARIO.Models;
using INVENTARIO.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INVENTARIO.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IRepositorioProducto repositorioProducto;

        public ProductoController(IRepositorioProducto repoProducto)
        {
            this.repositorioProducto = repoProducto;
        }

        // GET: Producto/Producto
        [HttpGet]
        public IActionResult Producto()
        {
            // Traer todos los productos para mostrar en la vista
            var productos = repositorioProducto.ListarProductos();
            return View("~/Views/Inventario/Producto.cshtml", productos);
        }

        // POST: Guardar producto
        [HttpPost]
        public async Task<IActionResult> GuardarProducto(ProductoModel ccc)
        {
            try
            {
                if (ccc == null)
                {
                    TempData["ErrorMessage"] = "⚠️ El modelo llegó vacío.";
                    return RedirectToAction("Producto");
                }

                if (ccc.ImageFile != null && ccc.ImageFile.Length > 0)
                {
                    var extension = Path.GetExtension(ccc.ImageFile.FileName);
                    var NuevoNombre = Guid.NewGuid().ToString() + extension;

                    var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Produimage");

                    // ✅ Crear carpeta si no existe
                    if (!Directory.Exists(carpeta))
                        Directory.CreateDirectory(carpeta);

                    var filePath = Path.Combine(carpeta, NuevoNombre);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ccc.ImageFile.CopyToAsync(stream);
                    }

                    // Guardar ruta relativa
                    ccc.urlimagen = "/Produimage/" + NuevoNombre;

                    // 👇 Setear fecha de creación en caso de que el repositorio no lo haga
                    ccc.FechaCreacion = DateTime.UtcNow;

                    // ✅ Usar el método correcto
                    bool insertado = await repositorioProducto.InsertarProducto(ccc);

                    if (insertado)
                        TempData["SuccessMessage"] = "✅ El producto se guardó exitosamente.";
                    else
                        TempData["ErrorMessage"] = "❌ No se pudo guardar el producto.";
                }
                else
                {
                    TempData["ErrorMessage"] = "⚠️ Debes seleccionar una imagen.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "❌ Error: " + ex.Message;
            }

            return RedirectToAction("Producto");
        }


        // GET: Detalle de producto por Id
        [HttpGet]
        public async Task<JsonResult> detalleproducto(int id)
        {
            var detalle = await repositorioProducto.Detalleproducto(id);

            if (detalle == null)
            {
                return Json(new { error = "Producto no encontrado" });
            }

            return Json(detalle);
        }

        [HttpGet]
        public string Mensaje()
        {
            return "✅ Backend funcionando correctamente.";
        }

        // Métodos CRUD vacíos (si quieres implementar después)
        public ActionResult Edit(int id) => View();
        public ActionResult Delete(int id) => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try { return RedirectToAction(nameof(Producto)); }
            catch { return View(); }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try { return RedirectToAction(nameof(Producto)); }
            catch { return View(); }
        }
    }
}
