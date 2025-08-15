using Dapper;
using INVENTARIO.Models;
using Microsoft.Data.SqlClient;

namespace INVENTARIO.Servicios
{
    public interface IRepositorioUsuario
    {
        Task<bool> RegistroUsuario(Registrarse usuario);
        Task<Registrarse> ValidarUsuario(LoginsModel informacion);
        Task<Registrarse> ObtenerPorId(int id);
    }
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private readonly string cnx;
        public RepositorioUsuario(IConfiguration configuration)
        {
            cnx = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<Registrarse> ObtenerPorId(int id)
        {
            using var connection = new SqlConnection(cnx);
            string query = "SELECT * FROM Registrarse WHERE Id = @Id";
            var usuario = await connection.QueryFirstOrDefaultAsync<Registrarse>(query, new { Id = id });
            return usuario;
        }


        public async Task<Registrarse> ValidarUsuario(LoginsModel informacion)
        {
            using var connection = new SqlConnection(cnx);
            string query = @"SELECT * FROM Registrarse WHERE correo = @correo AND contraseña = @contraseña";
            var usuario = await connection.QueryFirstOrDefaultAsync<Registrarse>(query, new { informacion.correo, informacion.contraseña });
            return usuario;
        }


        public async Task<bool> RegistroUsuario(Registrarse usuario)
        {
            bool isInserted = false;
            try
            {

                var connection = new SqlConnection(cnx);
                isInserted = await connection.ExecuteAsync(
                    @"INSERT INTO Registrarse (TipoC,Identificacion,Nombre,Apellido,Telefono,Rol,Tiposexo,Fechadenacimiento,Correo,Contraseña)
                 VALUES (@TipoC,@Identificacion,@Nombre,@Apellido,@Telefono,@Rol,@Tiposexo,@Fechadenacimiento,@Correo,@Contraseña)", usuario) > 0;
            }
            catch (Exception ex) { }
            return isInserted;
        }
    }
}
