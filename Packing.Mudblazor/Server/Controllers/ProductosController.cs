using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Packing.Mudblazor.Server.Data;
using Packing.Mudblazor.Shared;

namespace Packing.Mudblazor.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductosController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        [Route("obtenerListaProductos")]
        public async Task<List<ProductoDdo>> ObtenerProductos()
        {
            var productos = await _context.Productos.ToListAsync();
            var productosToFront = new List<ProductoDdo>();
            foreach (var producto in productos)
            {
                productosToFront.Add(new ProductoDdo()
                {
                    NombreProducto = producto.NombreProducto,
                    Imagen = string.Empty,
                    IdFormato = producto.Formato.IdFormato,
                    IdGrupo = producto.GrupoProducto.IdGrupo,
                    IdPresentacion = producto.Presentacion.IdPresentacion,
                    IdProducto = producto.IdProducto,
                    Vigente = producto.Vigente,
                    NombreFormato = producto.Formato.NombreFormato,
                    NombreGrupo = producto.GrupoProducto.NombreGrupo,
                    NombrePresentacion = producto.Presentacion.NombrePresentacion
                });
            }
            return productosToFront;
        }

        [HttpPost]
        [Authorize]
        [Route("insertarProducto")]
        public async Task<IActionResult> InsertarProducto(ProductoDto request)
        {
            var formatoRequest = await ObtenerFormato(request.IdFormato);
            var grupoRequest = await ObtenerGrupo(request.IdGrupo);
            var presentacionRequest = await ObtenerPresentacion(request.IdPresentacin);
            await _context.Productos.AddAsync(new Producto()
            {
                Presentacion = presentacionRequest,
                GrupoProducto = grupoRequest,
                Formato = formatoRequest,
                Imagen = string.Empty,
                NombreProducto = request.NombreProducto,
                Vigente = request.Vigente
            });
            return await _context.SaveChangesAsync() > 0 ? Ok() : BadRequest();
        }

        [HttpGet]
        [Authorize]
        [Route("obtenerPresentacion")]
        public async Task<List<Presentacion>> ObtenerPresentaciones()
        {
            return await _context.Presentaciones.ToListAsync();
        }

        [HttpPost, Authorize, Route("insertarPresentacion")]
        public async Task<ActionResult> InsertarPresentacion([FromBody]string requestData)
        {
            await _context.Presentaciones.AddAsync(new Presentacion()
            {
                NombrePresentacion = requestData
            });
            return await _context.SaveChangesAsync() > 0 ? Ok() : BadRequest();
        }

        [HttpGet, Authorize, Route("obtenerFormatos")]
        public async Task<List<Formato>> ObtenerFormatos()
        {
            return await _context.Formatos.ToListAsync();
        }

        [HttpPost, Authorize, Route("insertarFormato")]
        public async Task<ActionResult> InsertarFormato(Formato request)
        {
            await _context.Formatos.AddAsync(new Formato()
            {
                NombreFormato = request.NombreFormato,
                UnidadesPorFormato = request.UnidadesPorFormato
            });
            return await _context.SaveChangesAsync() > 0 ? Ok() : BadRequest();
        }

        [HttpGet, Authorize, Route("obtenerGrupos")]
        public async Task<List<GrupoProducto>> ObtenerGrupos()
        {
            return await _context.Grupos.ToListAsync();
        }

        [HttpPost, Authorize, Route("insertarGrupo")]
        public async Task<ActionResult> InsertarGrupo(GrupoProducto request)
        {
            await _context.Grupos.AddAsync(new GrupoProducto()
            {
                NombreGrupo = request.NombreGrupo
            });
            return await _context.SaveChangesAsync() > 0 ? Ok() : BadRequest();
        }

        private async Task<GrupoProducto> ObtenerGrupo(int IdGrupo)
        {
            return await _context.Grupos.Where(grupo => grupo.IdGrupo == IdGrupo).FirstAsync();
        }
        private async Task<Formato> ObtenerFormato(int IdFormato)
        {
            return await _context.Formatos.Where(formato => formato.IdFormato == IdFormato).FirstAsync();
        }

        private async Task<Presentacion> ObtenerPresentacion(int IdPresentacion)
        {
            return await _context.Presentaciones.Where(presentacion => presentacion.IdPresentacion == IdPresentacion)
                .FirstAsync();
        }
    }
}
