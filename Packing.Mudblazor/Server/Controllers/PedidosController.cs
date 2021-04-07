using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Packing.Mudblazor.Server.Data;
using Packing.Mudblazor.Shared;

namespace Packing.Mudblazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public PedidosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost,Route("InsertarPedido")]
        public async Task<ActionResult> InsertarPedido(PedidoDto request)
        {
            var estado = await ObtenerEstadoPedido(request.IdEstado);
            var empresa = await ObtenerEmpresa(request.IdEmpresa);
            var nuevoPedido = new Pedido()
            {
                EmpresaSolicitante = empresa,
                Estado = estado,
                FechaSolicitud = DateTime.Now.ToShortDateString(),
                FechaUltimaModificacion = DateTime.Now.ToShortDateString(),
                UserNameSolicitante = request.UserNameSolicitante,
                Observaciones = request.Observaciones
            };
            await _context.Pedidos.AddAsync(nuevoPedido);
            if (await _context.SaveChangesAsync() <= 0)
                return BadRequest("Ha ocurrido un error al guardar el producto.");
            foreach (var detalle in request.DetallesDtos)
            {
                var producto = await ObtenerProducto(detalle.IdProducto);
                await _context.DetallesPedidos.AddAsync(new DetallePedido()
                {
                    CantidadProducto = detalle.CantidadProducto,
                    CantidadesUnitarias = detalle.CantidadesUnitarias,
                    PedidoCabecera = nuevoPedido,
                    ProductoEnDetalle = producto
                });
            }

            return await _context.SaveChangesAsync() > 0 ? Ok() : BadRequest();
        }

        [HttpGet]
        public async Task<List<Pedido>> ObtenerPedidos(int idEmpresa)
        {
            return await _context.Pedidos.Where(pedido => pedido.EmpresaSolicitante.IdEmpresa == idEmpresa).ToListAsync();
        }

        private async Task<EstadoPedido> ObtenerEstadoPedido(int idPedido)
        {
            return await _context.EstadosPedidos.Where(pedido => pedido.IdEstado == idPedido).FirstAsync();
        }

        private async Task<Empresa> ObtenerEmpresa(int idEmpresa)
        {
            return await _context.Empresas.Where(empresa => empresa.IdEmpresa == idEmpresa).FirstAsync();
        }

        private async Task<Producto> ObtenerProducto(int idProducto)
        {
            return await _context.Productos.Where(producto => producto.IdProducto == idProducto).FirstAsync();
        }

        private async Task<Pedido> ObtenerPedidoCabecera(int idPedido)
        {
            return await _context.Pedidos.Where(pedido => pedido.IdPedido == idPedido).FirstAsync();
        }
    }
}
