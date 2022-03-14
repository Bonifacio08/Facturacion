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

namespace Facturacion.UI.Consultas
{
    /// <summary>
    /// Interaction logic for cInventario.xaml
    /// </summary>
    public partial class cInventario : Window
    {
        public cInventario()
        {
            InitializeComponent();
        }

        private void BtnInventario(object sender, RoutedEventArgs e)
        {
            var listado = new List<Producto>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0: //UsuarioId
                        listado = ProductoBLL.GetList(e => e.ProductoId == Utilidades.ToInt(CriterioTextBox.Text));
                        DesdeDataPicker.SelectedDate = null;
                        HastaDatePicker.SelectedDate = null;
                        break;

                    case 1: //Nombre                      
                        listado = ProductoBLL.GetList(e => e.NombreProducto.ToLower().Contains(CriterioTextBox.Text.ToLower()));
                        DesdeDataPicker.SelectedDate = null;
                        HastaDatePicker.SelectedDate = null;
                        break;
                }
            }
            else
            {
                listado = ProductoBLL.GetList(c => true);
            }
            if (listado == null)
            {


                if (DesdeDataPicker.SelectedDate != null)
                    listado = ProductoBLL.GetList(c => c.Fecha.Date >= DesdeDataPicker.SelectedDate);

                if (HastaDatePicker.SelectedDate != null)
                    listado = ProductoBLL.GetList(c => c.Fecha.Date <= HastaDatePicker.SelectedDate);
            }
            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
        private void enter(object sender, MouseEventArgs e)
        {
            CriterioTextBox.Text = null;
            FiltroComboBox.SelectedItem = null;
        }

        private void limpiar(object sender, MouseEventArgs e)
        {
            CriterioTextBox.Text = null;
            FiltroComboBox.SelectedItem = null;
        }

    }
}
