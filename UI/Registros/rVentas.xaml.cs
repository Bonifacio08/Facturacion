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
    /// Interaction logic for rVentas.xaml
    /// </summary>
    public partial class rVentas : Window
    {
        private Venta venta = new Venta();
        public rVentas()
        {
            InitializeComponent();
        }
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = venta;

        }
        private void Limpiar()
        {
            this.venta = new Venta();
            this.DataContext = venta;
        }
        private bool Validar()
        {
            bool esValido = true;
            if (TextVentaId.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Transaccion Fallida", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                TextVentaId.Focus();
                return esValido;

            }

            if (ClienteComboBox.SelectedValue == null)
            {
                esValido = false;
                MessageBox.Show("Seleccione un cliente", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return esValido;
            }

            if (UsuarioComboBox.SelectedValue == null)
            {
                esValido = false;
                MessageBox.Show("Seleccione el usuario", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return esValido;
            }
            return esValido;
        }
        private void BtnBuscar(object sender, RoutedEventArgs e)
        {
            if (TextVentaId.Text.Length == 0 || Utilidades.ToInt(TextVentaId.Text) == 0)
            {
                TextVentaId.Focus();
                return;
            }

            var encontrado = VentaBLL.Buscar(venta.VentaId);

            if (encontrado != null)
            {
                venta = encontrado;
                Cargar();

            }
            else
            {
                Limpiar();
                MessageBox.Show("Venta no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private bool ValidarAgregar()
        {
            bool esValido = true;
            if (TextVentaId.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Transaccion Fallida", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return esValido;

            }

            if (ClienteComboBox.SelectedValue == null)
            {
                esValido = false;
                MessageBox.Show("Seleccione un cliente", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return esValido;
            }


            if (ProductoComboBox.SelectedValue == null)
            {
                esValido = false;
                MessageBox.Show("Seleccione un articulo", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return esValido;
            }
            if (TextCantidadProducto.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Cantidad vacia", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                TextCantidadProducto.Focus();
                return esValido;
            }
            if (TextPrecio.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Precio vacio", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                TextPrecio.Focus();
                return esValido;
            }

            return esValido;
        }
        private void BtnAgregarFila(object sender, RoutedEventArgs e)
        {
            if (!ValidarAgregar())
                return;

            venta.Detalle.Add(new VentaDetalle
            {
                VentaId = venta.VentaId,
                ProductoId = ((Producto)ProductoComboBox.SelectedItem).ProductoId,
                Producto = (Producto)ProductoComboBox.SelectedItem,
                Cliente = (Cliente)ClienteComboBox.SelectedItem,
                CantidadProducto = Utilidades.ToInt(TextCantidadProducto.Text),
                Precio = Utilidades.ToInt(TextPrecio.Text),
            });



            Cargar();


            TextCantidadProducto.Clear();
            TextCantidadProducto.Focus();
            TextPrecio.Clear();
        }
        private void BtnRemoverFila(object sender, RoutedEventArgs e)
        {
            if (DetalleDataGrid.Items.Count >= 1 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count - 1)
            {
                if (DetalleDataGrid.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione la fila que desea eliminar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                venta.Detalle.RemoveAt(DetalleDataGrid.SelectedIndex);
                Cargar();
            }
        }
        private void BtnNuevo(object sender, RoutedEventArgs e)
        {
                Limpiar();
        }
        private void BtnGuardar(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;
            var paso = VentaBLL.Guardar(venta);

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No se pudo guardar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void BtnEliminar(object sender, RoutedEventArgs e)
        {
            var existe = VentaBLL.Buscar(venta.VentaId);

            if (existe == null)
            {
                MessageBox.Show("No existe el proyecto", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                VentaBLL.Eliminar(venta.VentaId);
                Limpiar();
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }
    }
}
