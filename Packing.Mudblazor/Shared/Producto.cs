using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packing.Mudblazor.Shared
{
    public class Producto
    {
        [Required,Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProducto { get; set; }
        [Required,MaxLength(150),MinLength(20)]
        public string NombreProducto { get; set; }
        [Required]
        public Formato Formato { get; set; }
        [Required]
        public Presentacion Presentacion { get; set; }
        [Required]
        public bool Vigente { get; set; }
        public string Imagen { get; set; }
        [Required]
        public GrupoProducto GrupoProducto { get; set; }
    }

    public class ProductoDdo
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int IdFormato { get; set; }
        public string NombreFormato { get; set; }
        public int IdPresentacion { get; set; }
        public string NombrePresentacion { get; set; }
        public bool Vigente { get; set; }
        public string Imagen { get; set; }
        public int IdGrupo { get; set; }
        public string NombreGrupo { get; set; }
    }

    public class ProductoDto
    {
        [Required, MaxLength(150), MinLength(20)]
        public string NombreProducto { get; set; }
        [Required]
        public int IdFormato { get; set; }
        [Required]
        public int IdPresentacin { get; set; }
        [Required]
        public bool Vigente { get; set; }
        public string Imagen { get; set; }
        [Required]
        public int IdGrupo { get; set; }
    }

    public class Formato
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFormato { get; set; }
        [Required,MinLength(10),MaxLength(100)]
        public string NombreFormato { get; set; }
    }

    public class Presentacion
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPresentacion { get; set; }
        [Required, MinLength(10), MaxLength(100)]
        public string NombrePresentacion { get; set; }
    }

    public class GrupoProducto
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGrupo { get; set; }
        [Required, MinLength(5), MaxLength(75)]
        public string NombreGrupo { get; set; }
    }
}