using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Packing.Mudblazor.Server.Areas.Identity.Pages.Account;
using Packing.Mudblazor.Server.Models;

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
        private readonly UrlEncoder _urlEncoder;

        public UsuariosController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, UrlEncoder urlEncoder)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _urlEncoder = urlEncoder;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Registrar()
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("Cliente"));
                await _roleManager.CreateAsync(new IdentityRole("Developer"));
            }

            
            //var user = new ApplicationUser()
            //{
            //    Email = "me@felipealvarez.dev",
            //    EmailConfirmed = true,
            //    //Id = Guid.NewGuid().ToString(),
            //    NombreCompleto = "Felipe Alvarez Ortiz",
            //    UserName = "AdminDev",
            //    PhoneNumber = "+56944194679",
            //    NormalizedUserName = "ADMINDEV",
            //    NormalizedEmail = "ME@FELIPEALVAREZ.DEV",
            //    PhoneNumberConfirmed = true,
            //    TwoFactorEnabled = false,
            //};
            //var result = await _userManager.CreateAsync(user,"FelipeMaineaAlarak199522-");
            //if (result.Succeeded)
            //{
            //    await _userManager.AddToRoleAsync(user,"Admin");
            //    await _userManager.AddToRoleAsync(user, "Developer");
            //}
            //else
            //{
            //    Console.WriteLine(result.Errors.ToString());
            //}
            return Ok();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginModel.InputModel loginModel)
        //{
        //    if (loginModel is not null)
        //    {
        //        var result = await _signInManager
        //            .PasswordSignInAsync(loginModel.Email,loginModel.Password,loginModel.RememberMe,lockoutOnFailure:false);
        //        if ()
        //        {

        //        }
        //    }
        //}
    }
}
