namespace INVENTARIO.Models
{
    public class Registrarse
    {
        public int Id { get; set; }
        public TipoC TipoC { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public Rol Rol { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public DateTime Fechadenacimiento { get; set; }
        public Tiposexo Tiposexo { get; set; }
    }
}
