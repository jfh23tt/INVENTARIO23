using Dapper;
using INVENTARIO.Models;
using System.Data.SqlClient; // ✅ Para SQL Server

namespace INVENTARIO.Servicios
{
    public interface Irepositoriopdf
    {
        List<InventarioModel> Invetariopdf(InventarioModel pdfinventario);
    }

    public class Repositoriopdf : Irepositoriopdf
    {
        private readonly string _cnx;

        public Repositoriopdf(IConfiguration configuration)
        {
            _cnx = configuration.GetConnectionString("DefaultConnection");
        }

        public List<InventarioModel> Invetariopdf(InventarioModel pdfinventario)
        {
            using var connection = new SqlConnection(_cnx); // ✅ Cambiado a SqlConnection
            string query = "SELECT * FROM Inventario";
            return connection.Query<InventarioModel>(query).ToList();
        }
    }
}
