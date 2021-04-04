using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Packing.Mudblazor.Server.Models;
using Packing.Mudblazor.Shared;

namespace Packing.Mudblazor.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Formato> Formatos { get; set; }
        public DbSet<Presentacion> Presentaciones { get; set; }
        public DbSet<GrupoProducto> Grupos { get; set; }

        public DbSet<Notificacion> Notificaciones { get; set; }
    }

}
