using Facturacion.BLL;
using Facturacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Facturacion.UI.Registros
{
    /// <summary>
    /// Interaction logic for rCliente.xaml
    /// </summary>
    public partial class rCliente : Window
    {
        private Cliente cliente = new Cliente();
        public rCliente()
        {
            InitializeComponent();
            this.DataContext = cliente;
        }
               
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = cliente;

        }
        private void Limpiar()
        {
            this.cliente = new Cliente();
            this.DataContext = cliente;
        }
        private bool Validar()
        {
            bool esValido = true;
            if (TextClienteId.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Campo ProductoId vacio", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                TextClienteId.Focus();
                return esValido;

            }
            if (TextNombre.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Campo Nombre vacio", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                TextNombre.Focus();
                return esValido;

            }
            if (TextDireccion.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Campo Costo vacio", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                TextDireccion.Focus();
                return esValido;

            }
            if (TextCelular.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Campo Venta vacio", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                TextCelular.Focus();
                return esValido;

            }
            if (TextEmail.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Campo Proveedor vacio", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                TextEmail.Focus();
                return esValido;

            }
            return esValido;
        }
        private void BtnBuscar(object sender, RoutedEventArgs e)
        {
            if (TextClienteId.Text.Length == 0 || Utilidades.ToInt(TextClienteId.Text) == 0)
            {
                MessageBox.Show("Cliente no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            var encontrado = ClienteBLL.Buscar(cliente.ClienteId);

            if (encontrado != null)
            {
                cliente = encontrado;
                Cargar();

            }
            else
            {
                Limpiar();
                MessageBox.Show("Cliente no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnGuardar(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;
            var paso = ClienteBLL.Guardar(cliente);

            if (paso)
            {
                if (MessageBox.Show("¿Seguro que desea Guardar este producto?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Guardado!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                    Limpiar();
                }
            }
            else
                MessageBox.Show("No se pudo guardar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnEliminar(object sender, RoutedEventArgs e)
        {
            if (TextClienteId.Text.Length == 0)
            {
                MessageBox.Show("ClienteId vacio", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (ClienteBLL.Eliminar(Convert.ToInt32(TextClienteId.Text)))
            {
                Limpiar();
                MessageBox.Show("Registro eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No fue posible eliminar el registro", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
        }//Hay que hacerle una confirmacion...

        private void BtnCancelar(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Seguro que quieres cancelar?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Limpiar();
            }
            else
            {
                MessageBox.Show("Le diste a NO");
            }
        }

    }
}
