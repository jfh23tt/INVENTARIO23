using INVENTARIO.Models;
using INVENTARIO.Repositorio;
using INVENTARIO.Servicios;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Rotativa;

namespace INVENTARIO.Controllers
{
    public class InventarioController : Controller
    {
        private readonly IRepositorioInventario repositorioInventario;

        public InventarioController(IRepositorioInventario repositorioInventario)
        {
            this.repositorioInventario = repositorioInventario;
        }


        public async Task<IActionResult> Inventario()
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                return RedirectToAction("Inventario", "Inventario");
            }

            var inventario = await repositorioInventario.ObtenerInventarioPorUsuario(usuarioId.Value);
            return View(inventario); // ← Aquí ya estás pasando la lista correctamente
        }





        [HttpPost]

        public async Task<IActionResult> GuardarProducto([FromBody] InventarioModel producto)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
                return Unauthorized("Sesión expirada.");

            producto.UsuarioId = usuarioId.Value;

            if (!ModelState.IsValid)
            {
                return BadRequest("Datos inválidos.");
            }

            try
            {
                await repositorioInventario.GuardarProducto(producto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }




        //public async Task<IActionResult> GuardarProducto([FromBody] InventarioModel producto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState); // En vez de retornar una vista
        //    }

        //    await repositorioInventario.GuardarProducto(producto);
        //    return Ok(); // Indicar éxito
        //}

        public async Task<IActionResult> Editar(int id)
        {
            var producto = await repositorioInventario.ObtenerPorId(id);
            if (producto == null)
            {
                return NotFound();
            }

            return View("~/Views/Inventario/Editar.cshtml", producto);
        }

        [HttpPost]
        public async Task<IActionResult> Edita([FromBody] InventarioModel producto)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Inventario/Editar.cshtml", producto);
            }

            await repositorioInventario.ActualizarProducto(producto);
            return Ok();
        }

        [HttpPost]
        [Route("Inventario/EliminarProducto")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = await repositorioInventario.ObtenerPorId(id);
            if (producto == null)
            {
                return NotFound();
            }

            await repositorioInventario.Eliminar(id);
            return Ok();
        }





        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> ConfirmarEliminar(int id)
        {
            await repositorioInventario.Eliminar(id);
            return RedirectToAction("Inventario");
        }
    }
}
