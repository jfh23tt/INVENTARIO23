namespace INVENTARIO.Models
{
    public class carritoModel
    {
        public int Codigo { get; set; }
        public string nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal precio { get; set; }
        public string urlimagen { get; set; }
        public string marca { get; set; }
    }
    public class carroitem
    {
        public carritoModel Producto { get; set; }
        public int cantidad { get; set; }
    }
    public class productoSelecionados
    {
        public List<carroitem> Items { get; set; } = new List<carroitem>();
        public decimal TotalPrecio => Items.Sum(item => item.Producto.precio * item.cantidad);
    }
}
