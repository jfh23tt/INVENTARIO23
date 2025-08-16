using Dapper;
using INVENTARIO.Models;
using INVENTARIO.Repositorio;
using Microsoft.Data.SqlClient;
using System.Data;

namespace INVENTARIO.Servicios
{
    public interface IRepositorioUsuario
    {
        Task<bool> RegistroUsuario(Registrarse usuario);
        Task<Registrarse> ValidarUsuario(string correo, string contrasena);
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


        //public async Task<Registrarse> ValidarUsuario(LoginsModel informacion)
        //{
        //    using var connection = new SqlConnection(cnx);
        //    string query = @"SELECT * FROM Registrarse WHERE correo = @correo AND contraseña = @contraseña";
        //    var usuario = await connection.QueryFirstOrDefaultAsync<Registrarse>(query, new { informacion.correo, informacion.contraseña });
        //    return usuario;
        //}
        public async Task<Registrarse> ValidarUsuario(string correo, string contrasena)
        {
            try
            {
                using IDbConnection db = new SqlConnection(cnx);

                // Encriptar contraseña antes de comparar
                Encriptar enc = new Encriptar();
                string contrasenaEncriptada = enc.Encrypt(contrasena);

                const string sql = @"
            SELECT TOP 1 *
            FROM Registrarse
            WHERE Correo = @Correo
              AND Contraseña = @Contraseña;
        ";

                return await db.QueryFirstOrDefaultAsync<Registrarse>(sql, new
                {
                    Correo = correo,
                    Contraseña = contrasenaEncriptada
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en ValidarUsuario: " + ex.Message);
                return null;
            }
        }


        public async Task<bool> RegistroUsuario(Registrarse usuario)
        {
            try
            {
                using IDbConnection db = new SqlConnection(cnx);

                // Validar si el correo ya existe
                const string sqlExiste = "SELECT COUNT(*) FROM Registrarse WHERE Correo = @Correo;";
                var existe = await db.ExecuteScalarAsync<int>(sqlExiste, new { usuario.Correo });

                if (existe > 0)
                {
                    throw new Exception("El correo ya está registrado.");
                }

                //// Encriptar contraseña antes de guardar
                //Encriptar enc = new Encriptar();
                //usuario.Contraseña = enc.Encrypt(usuario.Contraseña);

                const string sql = @"
            INSERT INTO Registrarse
            (TipoC, Identificacion, Nombre, Apellido, Telefono, Rol, Tiposexo, Fechadenacimiento, Correo, Contraseña)
            VALUES (@TipoC, @Identificacion, @Nombre, @Apellido, @Telefono, @Rol, @Tiposexo, @Fechadenacimiento, @Correo, @Contraseña);
        ";

                var rows = await db.ExecuteAsync(sql, usuario);
                return rows > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en RegistroUsuario: " + ex.Message);
                return false;
            }
        }
    }
}
