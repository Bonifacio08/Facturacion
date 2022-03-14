using Facturacion.UI.Consultas;
using Facturacion.UI.Registros;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Facturacion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void BtnRegistrarProducto(object sender, RoutedEventArgs e)
        {
            rProducto producto = new rProducto();
            producto.Show();
        }

        private void BtnIntentario(object sender, RoutedEventArgs e)
        {
            cInventario inventario = new cInventario();
            inventario.Show();
        }

        private void BtnRegistrarCliente(object sender, RoutedEventArgs e)
        {
            rCliente cliente = new rCliente();
            cliente.Show();
        }

        private void BtnFacturacion(object sender, RoutedEventArgs e)
        {
            rVentas ventas = new rVentas();
            ventas.Show();
        }
    }
}
