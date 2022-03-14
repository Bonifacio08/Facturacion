using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Entidades
{
    public class VentaDetalle
    {
        [Key]
        public int DetalleId { get; set; }
        public int ProductoId { get; set; }
        public int CantidadProducto { get; set; }
        public int VentaId { get; set; }
        public float Precio { get; set; }

        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }
    }
}
