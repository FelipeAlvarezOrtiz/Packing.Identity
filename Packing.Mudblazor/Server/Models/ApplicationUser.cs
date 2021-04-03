using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Packing.Mudblazor.Shared;

namespace Packing.Mudblazor.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Este dato es requerido.")]
        public string NombreCompleto { get; set; }
        [Required(ErrorMessage = "Todo usuario debe tener una empresa asociada")]
        public Empresa EmpresaPerteneciente { get; set; }
    }

    public class ApplicationUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int IdEmpresa { get; set; }
        public string Telefono { get; set; }
        public string NombreCompleto { get; set; }
        public string Role { get; set; }
    }

    public class ApplicationUserDdo
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string NombreEmpresa { get; set; }
        public int IdEmpresa { get; set; }
        public string Telefonp { get; set; }
        public string NombreContacto { get; set; }
        public string Role { get; set; }
    }
}
