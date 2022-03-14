using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Entidades
{
    public class Venta
    {
        [Key]
        public int VentaId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int ClienteId { get; set; }
        public string Descripcion { get; set; }
        public float Total { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente cliente { get; set; }

        [ForeignKey("VentaId")]
        public List<VentaDetalle> Detalle { get; set; } = new List<VentaDetalle>();
    }
}
