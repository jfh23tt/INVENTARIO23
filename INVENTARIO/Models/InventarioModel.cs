namespace INVENTARIO.Models
{
    public class InventarioModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Categoria { get; set; }

        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public string Proveedor { get; set; }

        public DateTime FechaIngreso { get; set; } = DateTime.Now;
        public int UsuarioId { get; set; }
    }

}
