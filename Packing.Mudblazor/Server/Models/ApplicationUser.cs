using Microsoft.AspNetCore.Identity;

namespace Packing.Mudblazor.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string NombreCompleto { get; set; }

    }
}
