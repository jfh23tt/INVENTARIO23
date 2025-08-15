using System.Data.SqlClient;
using Dapper;
using INVENTARIO.Models;

namespace INVENTARIO.Servicios

{
    public interface Irepositoriopdf
    {
        List<InventarioModel> Invetariopdf(InventarioModel pdfinventario);
    }
    public class Repositoriopdf : Irepositoriopdf
    {
        private readonly IConfiguration configuration;
        private readonly string cnx;
        public Repositoriopdf(IConfiguration configuration)
        {

            cnx = configuration.GetConnectionString("DefaultConnection");
        }
        public List<InventarioModel> Invetariopdf(InventarioModel pdfinventario)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqlConnection(cnx);
            var query = "SELECT * FROM Inventario";
            using var gernerarpdf = new SqlConnection(connectionString);
            var pdf = connection.Query<InventarioModel>(query).ToList();
            return pdf;
        }






    }
}
