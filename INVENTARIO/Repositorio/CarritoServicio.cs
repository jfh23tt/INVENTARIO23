using INVENTARIO.Models;
using System.Data.SqlClient;
using Dapper;
using System.Text.Json;
using System.Data;
namespace INVENTARIO.Repositorio
{
    public interface ICarritoServicio
    {
        void agregarItemCarro(carritoModel producto, int cantidad);
        //void Limpiarcarro();
        List<carroitem> listarItemsCarro();
        void eliminarItemCarro(int productoId);
        void actualizarItemsCarro(int productoId, int cantidad);
        //Task<ProductoModel> ObtenerProductoPorCodigo(string Codigo);

    }
    public class carritoServicio : ICarritoServicio
    {
        private readonly string cnx;
        private readonly productoSelecionados _productoSelecionados;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        public carritoServicio(productoSelecionados productoSelecionados, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _productoSelecionados = productoSelecionados;
            cnx = _configuration.GetConnectionString("DefaultConnection");
        }
        //public async Task<ProductoModel> ObtenerProductoPorCodigo(string codigo)
        //{
        //    using (IDbConnection db = new SqlConnection(cnx))
        //    {
        //        string query = "SELECT Codigo,Nombre, Descripcion, Precio, Unidades FROM Producto WHERE Codigo=@Codigo";
        //        ProductoModel model = db.QuerySingleOrDefault<ProductoModel>(query, new { codigo });
        //        return model;
        //    }
        //}
        private productoSelecionados obtenerItemsSesion()
        {
            // recuperar la Variable de sesion con los valores almacenados
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = session.GetString("carrito");
            var jh = string.IsNullOrEmpty(cartJson)
                ? new productoSelecionados() :
                JsonSerializer.Deserialize<productoSelecionados>(cartJson);
            return jh;


        }
        private void guardarItemsSesion(productoSelecionados cart)
        {
            /// crea la variable de sesion si no existe
            var session = _httpContextAccessor.HttpContext.Session;
            session.SetString("carrito", JsonSerializer.Serialize(cart));
        }
        public void agregarItemCarro(carritoModel Product, int cantidad)
        {
            var cart = obtenerItemsSesion();//obtine los productos que se estan guardados en la session
            ///verificar que exista el producto
            var existingItem = cart.Items.FirstOrDefault(i => i.Producto.Codigo == Product.Codigo);
            if (existingItem != null)
            {

                existingItem.cantidad += cantidad; // aumenta la cantidad si ya existe


            }
            else
            {
                cart.Items.Add(new carroitem { Producto = Product, cantidad = cantidad });
            }
            guardarItemsSesion(cart);


        }

        public void eliminarItemCarro(int productoId)
        {
            var cart = obtenerItemsSesion();
            var item = cart.Items.FirstOrDefault(i => i.Producto.Codigo == productoId);

            if (item != null)
            {
                cart.Items.Remove(item);// elimina el producto del carrito
                guardarItemsSesion(cart);//Guardar el carrito actualizado


            }


        }
        public decimal obtenerTotal()
        {
            return _productoSelecionados.TotalPrecio;
            //total el precio
        }
        public void actualizarItemsCarro(int productoId, int cantidad)
        {
            var cart = obtenerItemsSesion();
            var existeItem = cart.Items.FirstOrDefault(i => i.Producto.Codigo == productoId);
            if (existeItem != null)
            {
                existeItem.cantidad = cantidad;//actualiza la cantidad

            }
            guardarItemsSesion(cart);// guarda los cambios
        }
        public List<carroitem> listarItemsCarro()
        {
            return obtenerItemsSesion().Items;
        }
    }

}
