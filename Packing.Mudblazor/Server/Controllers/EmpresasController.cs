using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Packing.Mudblazor.Server.Data;
using Packing.Mudblazor.Shared;

namespace Packing.Mudblazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public EmpresasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Empresa>>> ObtenerEmpresas()
        {
            return await context.Empresas.ToListAsync();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> InsertarEmpresa(Empresa empresaEntrante)
        {
           await context.Empresas.AddAsync(empresaEntrante);
           return await context.SaveChangesAsync() > 0
               ? 1
               : throw new Exception("Problema al guardar el producto");
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<int>> ActualizarImpresa(Empresa empresaEntrante)
        {
            return null;
        }
    }
}
