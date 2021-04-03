using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packing.Mudblazor.Server.Data;
using Packing.Mudblazor.Server.Models;
using Packing.Mudblazor.Shared;

namespace Packing.Mudblazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly UrlEncoder _urlEncoder;

        public UsuariosController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, UrlEncoder urlEncoder, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _urlEncoder = urlEncoder;
            _context = context;
        }

        [HttpPost]
        [Authorize]
        [Route("crearRole")]
        public async Task<IActionResult> InsertaRole(string nuevoRol)
        {
            if (await _roleManager.RoleExistsAsync(nuevoRol)) return BadRequest();
            await _roleManager.CreateAsync(new IdentityRole(nuevoRol));
            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("obtenerRoles")]
        public async Task<List<string>> ObtenerRoles()
        {
            var roles = await _context.Roles.ToListAsync();

            return roles.Select(rol => rol.Name).ToList();
        }

        [HttpPost]
        [Authorize]
        [Route("registrar")]
        public async Task<IActionResult> RegistrarUsuario(ApplicationUserDto userDto)
        {
            if (!await _roleManager.RoleExistsAsync(userDto.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole(userDto.Role));
            }
            var empresa = await ObtenerEmpresa(userDto.IdEmpresa);
            if (empresa is null)
            {
                return BadRequest();
            }
            var user = new ApplicationUser()
            {
                Email = userDto.Email,
                EmailConfirmed = true,
                NombreCompleto = userDto.NombreCompleto,
                UserName = userDto.Username,
                PhoneNumber = userDto.Telefono,
                NormalizedEmail = userDto.Email.ToUpper(),
                NormalizedUserName = userDto.Username.ToUpper(),
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = true,
                EmpresaPerteneciente = empresa
            };
            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded) return BadRequest();
            await _userManager.AddToRoleAsync(user, userDto.Role);
            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("obtenerUsuarios")]
        public async Task<List<ApplicationUserDdo>> ObtenerDataUsuarios()
        {
            var listaUsuarios = await _context.Users.ToListAsync();
            var listaRoles = await _context.Roles.ToListAsync();
            var listaDda = new List<ApplicationUserDdo>();
            foreach (var usuario in listaUsuarios)
            {
                listaDda.Add(new ApplicationUserDdo()
                {
                    Email = usuario.Email,
                    IdEmpresa = usuario.EmpresaPerteneciente.IdEmpresa,
                    NombreEmpresa = usuario.EmpresaPerteneciente.RazonSocial,
                    NombreContacto = usuario.EmpresaPerteneciente.PersonaContacto,
                    Username = usuario.UserName,
                    Role = listaRoles.FirstOrDefault(u => u.Id == usuario.Id).Name ??= "Sin rol",
                    Telefonp = usuario.PhoneNumber
                });
            }
            return listaDda;
        } 


        private async Task<Empresa> ObtenerEmpresa(int idEmpresa)
        {
            return await _context.Empresas.Where(empresa => empresa.IdEmpresa == idEmpresa).FirstAsync();
        }

    }
}
