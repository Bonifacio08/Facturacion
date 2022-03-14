using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Entidades
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Celular { get; set; }
        public string Cedula { get; set; }
        public string Email { get; set; }
        public int UsuarioId { get; set; }

        [ForeignKey("Id")]
        public Producto producto { get; set; }
    }
}
