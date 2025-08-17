using iText.Kernel.Pdf.Canvas.Wmf;
using System.ComponentModel.DataAnnotations;

namespace INVENTARIO.Models
{
    public class ProductoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Campo {0} Es Requerido")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El Campo {0} Es Requerido")]
        public string Nombre { get; set; }

        public string Marca { get; set; }

        [Required(ErrorMessage = "El Campo {0} Es Requerido")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El Campo {0} Es Requerido")]
        public string Precio { get; set; }

        [Required(ErrorMessage = "El Campo {0} Es Requerido")]
        public string Unidades { get; set; }

        [Required(ErrorMessage = "El Campo {0} Es Requerido")]
        public Estado estado { get; set; }

        public string urlimagen { get; set; }

        [Required(ErrorMessage = "Por Favor, Seleccione Una Imagen")]
        [DataType(DataType.Upload)]
        public IFormFile ImageFile { get; set; }

        // 👇 Nueva propiedad: se llena automáticamente en SQL
        [ScaffoldColumn(false)] // evita que se genere en vistas por scaffolding
        public DateTime FechaCreacion { get; set; }
    }
}
