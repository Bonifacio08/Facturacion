using Facturacion.DAL;
using Facturacion.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.BLL
{
    public class ClienteBLL
    {
        public static bool Guardar(Cliente cliente)
        {
            if (!Existe(cliente.ClienteId))//si no existe insertamos
                return Insertar(cliente);
            else
                return Modificar(cliente);
        }
        private static bool Insertar(Cliente cliente)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {

                if (contexto.Cliente.Add(cliente) != null)
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
        private static bool Modificar(Cliente cliente)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {

                contexto.Entry(cliente).State = EntityState.Modified;
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
                var cliente = contexto.Cliente.Find(id);

                if (cliente != null)
                {

                    contexto.Cliente.Remove(cliente); //remover la entidad
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
        public static Cliente Buscar(int id)
        {
            Cliente cliente;
            Contexto contexto = new Contexto();

            try
            {
                cliente = contexto.Cliente.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return cliente;
        }
        public static List<Cliente> GetList(Expression<Func<Cliente, bool>> criterio)
        {
            List<Cliente> Lista = new List<Cliente>();
            Contexto contexto = new Contexto();

            try
            {
                //obtener la lista y filtrarla según el criterio recibido por parametro.
                Lista = contexto.Cliente.Where(criterio).ToList();
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
        public static List<Cliente> GetListado()
        {
            List<Cliente> Lista = new List<Cliente>();
            Contexto contexto = new Contexto();

            try
            {
                //obtener la lista y filtrarla según el criterio recibido por parametro.
                Lista = contexto.Cliente.ToList();
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
                encontrado = contexto.Cliente.Any(e => e.ClienteId == id);
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
