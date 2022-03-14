using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Entidades
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string NombreProducto { get; set; }
        public float Costo { get; set; }
        public float Venta { get; set; }
        public string Descripcion { get; set; }
        public string Proveedor { get; set; }
        public int Stock { get; set; }//Inventario

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }
    }
}
