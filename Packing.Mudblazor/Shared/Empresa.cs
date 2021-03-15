using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packing.Mudblazor.Shared
{
    public class Empresa
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "Este dato es requerido."), MaxLength(200, ErrorMessage = "Límite de carácteres excedido.")]
        public string RazonSocial { get; set; }
        [Required(ErrorMessage = "Este dato es requerido.")]
        public bool Vigente { get; set; }
        public string Imagen { get; set; }
        [Required]
        public string RutEmpresa { get; set; }
        [Required(ErrorMessage = "Este dato es requerido."), MaxLength(150)]
        public string DireccionEmpresa { get; set; }
        [MaxLength(50, ErrorMessage = "Límite de carácteres excedido.")]
        public string TelefonoContacto { get; set; }
        [Required, MinLength(20), MaxLength(150)]
        public string Giro { get; set; }
        [Required, MinLength(20), MaxLength(150)]
        public string PersonaContacto { get; set; }
    }
}
