using System.Data;
using Dapper;
using INVENTARIO.Models;
using Microsoft.Data.SqlClient; // ✅ Para SQL Server

namespace INVENTARIO.Servicios
{
    public interface IRepositorioInventario
    {
        Task<List<InventarioModel>> ObtenerInventarioPorUsuario(int usuarioId);
        Task<List<InventarioModel>> ObtenerInventario();
        Task GuardarProducto(InventarioModel producto);
        Task<InventarioModel> ObtenerPorId(int id);
        Task ActualizarProducto(InventarioModel producto);
        Task Eliminar(int id);
    }

    public class RepositorioInventario : IRepositorioInventario
    {
        private readonly string connectionString;

        public RepositorioInventario(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<InventarioModel>> ObtenerInventario()
        {
            using var connection = new SqlConnection(connectionString);
            var query = "SELECT * FROM Inventario";
            var resultado = await connection.QueryAsync<InventarioModel>(query);
            return resultado.ToList();
        }

        public async Task GuardarProducto(InventarioModel producto)
        {
            using var connection = new SqlConnection(connectionString);
            var query = @"INSERT INTO Inventario 
                  (Nombre, Categoria, Precio, Cantidad, Proveedor, FechaIngreso, UsuarioId) 
                  VALUES (@Nombre, @Categoria, @Precio, @Cantidad, @Proveedor, @FechaIngreso, @UsuarioId)";

            await connection.ExecuteAsync(query, producto);
        }

        public async Task<InventarioModel> ObtenerPorId(int id)
        {
            using var connection = new SqlConnection(connectionString);
            var query = "SELECT * FROM Inventario WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<InventarioModel>(query, new { Id = id });
        }

        public async Task<List<InventarioModel>> ObtenerInventarioPorUsuario(int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            var query = "SELECT * FROM Inventario WHERE UsuarioId = @UsuarioId";
            var resultado = await connection.QueryAsync<InventarioModel>(query, new { UsuarioId = usuarioId });
            return resultado.ToList();
        }

        public async Task ActualizarProducto(InventarioModel producto)
        {
            using var connection = new SqlConnection(connectionString);
            var query = @"UPDATE Inventario SET 
                          Nombre = @Nombre, 
                          Categoria = @Categoria, 
                          Precio = @Precio, 
                          Cantidad = @Cantidad, 
                          Proveedor = @Proveedor
                          WHERE Id = @Id";
            await connection.ExecuteAsync(query, producto);
        }

        public async Task Eliminar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            var query = "DELETE FROM Inventario WHERE Id = @Id";
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}
