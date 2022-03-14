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
    /// Interaction logic for rProducto.xaml
    /// </summary>
    public partial class rProducto : Window
    {
        Producto producto = new Producto();
        public rProducto()
        {
            InitializeComponent();
            this.DataContext = producto;
        }
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = producto;

        }
        private void Limpiar()
        {
            this.producto = new Producto();
            this.DataContext = producto;
        }
        private bool Validar()
        {
            bool esValido = true;
            if (TextProductoId.Text.Length ==0)
            {
                esValido = false;
                MessageBox.Show("Campo ProductoId vacio", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return esValido;

            }
            if (TextNombre.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Campo Nombre vacio", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return esValido;

            }
            if (TextCosto.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Campo Costo vacio", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return esValido;

            }
            if (TextVenta.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Campo Venta vacio", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return esValido;

            }
            if (TextProveedor.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Campo Proveedor vacio", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return esValido;

            }           
            if (TextStock.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Campo Stock vacio", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return esValido;

            }
            
            return esValido;
        }
        private void BtnLimpiar(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Seguro que quieres cancelar?" , "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Limpiar();
            }
            else
            {
                MessageBox.Show("Le diste a NO");
            }
        }

        private void BtnGuardar(object sender, RoutedEventArgs e)
        {

            if (!Validar())
                return;
            var paso = ProductoBLL.Guardar(producto);

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

        private void BtnBuscar(object sender, RoutedEventArgs e)
        {
            if (TextProductoId.Text.Length == 0 || Utilidades.ToInt(TextProductoId.Text) == 0)
            {
                MessageBox.Show("Producto no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            var encontrado = ProductoBLL.Buscar(producto.ProductoId);

            if (encontrado != null)
            {
                producto = encontrado;
                Cargar();

            }
            else
            {
                Limpiar();
                MessageBox.Show("Producto no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
