using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packing.Mudblazor.Shared
{
    public class Pedido
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity),Key]
        public int IdPedido { get; set; }
        public EstadoPedido Estado { get; set; }
        public string FechaSolicitud { get; set; }
        public string FechaUltimaModificacion { get; set; }
        public Empresa EmpresaSolicitante { get; set; }
        [Required, MinLength(2)]
        public string UserNameSolicitante { get; set; }
        public string Observaciones { get; set; }
        public List<DetallePedido> Detalles { get; set; }
    }

    public class PedidoDto
    {
        public int IdEstado { get; set; }
        public int IdEmpresa { get; set; }
        public string UserNameSolicitante { get; set; }
        public string Observaciones { get; set; }
        public List<DetallePedidoDto> DetallesDtos { get; set; }
    }


    public class EstadoPedido
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity),Key]
        public int IdEstado { get; set; }
        [Required,MinLength(5),MaxLength(30)]
        public string NombreEstado { get; set; }
    }

    public class DetallePedido
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity),Key]
        public int IdDetalle { get; set; }
        public Pedido PedidoCabecera { get; set; }
        public Producto ProductoEnDetalle { get; set; }
        public int CantidadProducto { get; set; }
        public int CantidadesUnitarias { get; set; }
    }

    public class DetallePedidoDto
    {
        public int IdPedidoCabecera {get; set; }
        public int IdProducto { get; set; }
        public int CantidadProducto { get; set; }
        public int CantidadesUnitarias { get; set; }
    }
}