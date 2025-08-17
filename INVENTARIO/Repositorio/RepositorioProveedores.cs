using INVENTARIO.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
namespace INVENTARIO.Repositorio
{

    public interface IRepositorioProveedor
    {
        Task<bool> Iproveedores(ProveedoresModel proveedores);

    }

    public class RepositorioProveedor : IRepositorioProveedor
    {
        private readonly string cnx;
        public RepositorioProveedor(IConfiguration configuration)
        {
            cnx = configuration.GetConnectionString("DefaultConnection");

        }

        public async Task<bool> Iproveedores(ProveedoresModel proveedores)
        {
            bool isInserted = false;
            try
            {

                var connection = new SqlConnection(cnx);
                isInserted = await connection.ExecuteAsync(
                    @"INSERT INTO Proveedores (Identificacion,Nombre,Apellido,Telefono,Correo,Empresa,Direccion,Fecha)
                 VALUES (@Identificacion,@Nombre,@Apellido,@Telefono,@Correo,@Empresa,@Direccion,@Fecha)", proveedores) > 0;
            }
            catch (Exception ex) { }
            return isInserted;
        }
    }
}
