using INVENTARIO.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INVENTARIO.Controllers
{
    public class CarritoController : Controller
    {
        private readonly IRepositorioProducto repositorioProducto;
        private readonly ICarritoServicio carritoServicio;

        public CarritoController(ICarritoServicio carritoServicio, IRepositorioProducto repositorioProducto)
        {
            this.repositorioProducto = repositorioProducto;
            this.carritoServicio = carritoServicio;
        }

        // ✅ Obtener producto por id (INT)
        [HttpGet]
        public async Task<IActionResult> ObtenerProductoPorId(int id)
        {
            if (id <= 0)
                return BadRequest("⚠️ El id debe ser mayor a 0.");

            var producto = await repositorioProducto.ObtenerProductoPorId(id);
            if (producto == null)
                return NotFound("❌ Producto no encontrado.");

            return Json(producto);
        }

        // ✅ Agregar producto al carrito
        [HttpPost]
        public IActionResult Agregar(int id, int cantidad)
        {
            if (cantidad < 1)
                return BadRequest("⚠️ La cantidad debe ser al menos 1.");

            var producto = repositorioProducto.selectcarro(id);
            if (producto != null)
            {
                carritoServicio.agregarItemCarro(producto, cantidad);
                TempData["SuccessMessage"] = "✅ Producto agregado al carrito.";
            }
            else
            {
                TempData["ErrorMessage"] = "❌ No se encontró el producto.";
            }

            var carroitem = carritoServicio.listarItemsCarro();
            return View("~/Views/Home/Carrito.cshtml", carroitem);
        }

        // ✅ Actualizar cantidad de un producto en el carrito
        [HttpPost]
        public IActionResult Actualizar(int productoid, int cantidad)
        {
            if (cantidad < 1)
                return BadRequest("⚠️ La cantidad debe ser al menos de 1.");

            carritoServicio.actualizarItemsCarro(productoid, cantidad);
            TempData["SuccessMessage"] = "✅ Cantidad actualizada.";

            var carroitem = carritoServicio.listarItemsCarro();
            return View("~/Views/Home/Carrito.cshtml", carroitem);
        }

        // ✅ Eliminar producto del carrito
        [HttpPost]
        public IActionResult Eliminar(int productoid)
        {
            carritoServicio.eliminarItemCarro(productoid);
            TempData["SuccessMessage"] = "✅ Producto eliminado del carrito.";

            var carroitem = carritoServicio.listarItemsCarro();
            return View("~/Views/Home/Carrito.cshtml", carroitem);
        }

        // ✅ Ver carrito directamente
        [HttpGet]
        public IActionResult Index()
        {
            var carroitem = carritoServicio.listarItemsCarro();
            return View("~/Views/Home/Carrito.cshtml", carroitem);
        }
    }
}
