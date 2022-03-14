using Facturacion.DAL;
using Facturacion.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Facturacion.BLL
{
    public class VentaBLL
    {
        public static bool Guardar(Venta venta)
        {
            if (!Existe(venta.VentaId))//si no existe insertamos
                return Insertar(venta);
            else
                return Modificar(venta);
        }
        private static bool Insertar(Venta venta)
        {
            bool paso = false, validar = true;
            Contexto contexto = new Contexto();

            try
            {
                //Agregar la entidad que se desea insertar al contexto
                contexto.Venta.Add(venta);

                foreach (var detalle in venta.Detalle)
                {
                    contexto.Entry(detalle.Producto).State = EntityState.Modified;

                    if (detalle.Producto.Stock < detalle.CantidadProducto)
                    {
                        MessageBox.Show($"No queda suficiente {detalle.Producto.Cliente} para realizar la venta. Producto disponibles: {detalle.Producto.Stock}", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        validar = false;
                    }
                    else
                    {
                        venta.Total += detalle.Precio * detalle.CantidadProducto;

                        detalle.Producto.Stock -= detalle.CantidadProducto;
                    }
                }
                if (validar)
                {
                    if (contexto.Venta.Add(venta) != null)
                        paso = contexto.SaveChanges() > 0;
                }


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        private static bool Modificar(Venta venta)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var VentaAnterior = contexto.Venta
                     .Where(x => x.VentaId == venta.VentaId)
                     .Include(x => x.Detalle)
                     .AsNoTracking()
                     .SingleOrDefault();

                Producto producto;

                //busca la entidad en la base de datos y la elimina
                foreach (var detalle in VentaAnterior.Detalle)
                {
                    producto = contexto.Producto.Find(detalle.ProductoId);
                    producto.Stock += detalle.CantidadProducto;

                    venta.Total -= detalle.Precio * detalle.CantidadProducto;

                }
                //  }

                contexto.Database.ExecuteSqlRaw($"Delete FROM VentaDetalle Where VentaId={venta.VentaId}");


                foreach (var item in venta.Detalle)
                {




                    venta.Total += item.Precio * item.CantidadProducto;

                    producto = contexto.Producto.Find(item.Producto);
                    producto.Stock -= item.CantidadProducto;

                    contexto.Entry(producto).State = EntityState.Modified;
                    contexto.Entry(item).State = EntityState.Added;





                }

                //marcar la entidad como modificada para que el contexto sepa como proceder
                contexto.Entry(venta).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                //buscar la entidad que se desea eliminar
                var ventas = contexto.Venta.Find(id);

                if (ventas != null)
                {
                    //busca la entidad en la base de datos y la elimina
                    foreach (var detalle in ventas.Detalle)
                    {
                        detalle.Producto.ProductoId -= 1;

                        detalle.DetalleId -= 1;
                    }

                    contexto.Venta.Remove(ventas); //remover la entidad
                    paso = contexto.SaveChanges() > 0;
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
        public static Venta Buscar(int id)
        {
            Venta ventas;
            Contexto contexto = new Contexto();

            try
            {
                // ventas = contexto.Ventas.Find(id);
                ventas = contexto.Venta.Include(x => x.Detalle)
                    .Where(x => x.VentaId == id)
                     .Include(x => x.Detalle)
                    .ThenInclude(x => x.Producto)
                    .Include(x => x.Detalle)
                    .SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return ventas;
        }
        public static List<Venta> GetList(Expression<Func<Venta, bool>> criterio)
        {
            List<Venta> Lista = new List<Venta>();
            Contexto contexto = new Contexto();

            try
            {
                //obtener la lista y filtrarla según el criterio recibido por parametro.
                Lista = contexto.Venta.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Lista;
        }
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Venta.Any(e => e.VentaId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }
    }
}
