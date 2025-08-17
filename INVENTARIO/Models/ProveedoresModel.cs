using System.ComponentModel.DataAnnotations;

namespace INVENTARIO.Models
{
    public class ProveedoresModel
    {

        [Required(ErrorMessage = "El Campo {0} Es Requerido")]
        public string Identificacion { get; set; }
        [Required(ErrorMessage = "El Campo {0} Es Requerido")]

        public string Nombre { get; set; }
        [Required(ErrorMessage = "El Campo {0} Es Requerido")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El Campo {0} Es Requerido")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "El Campo {0} Es Requerido")]
        public string Correo { get; set; }
        [Required(ErrorMessage = "El Campo {0} Es Requerido")]
        public string Empresa { get; set; }
        [Required(ErrorMessage = "El Campo {0} Es Requerido")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El Campo {0} Es Requerido")]
        public string Fecha { get; set; }



    }
}
