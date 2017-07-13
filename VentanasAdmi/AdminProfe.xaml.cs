using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace EpieHorarios
{
    /// <summary>
    /// Lógica de interacción para AdminProfe.xaml
    /// </summary>
    public partial class AdminProfe : Window
    {        
        public AdminProfe()
        {            
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Profesor> list = DBServices.ObtenerProfesores();
            ProfeGrid.ItemsSource = list;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }

        private void Boton_Editar(object sender, RoutedEventArgs e)
        {
            Profesor profe = (Profesor)ProfeGrid.SelectedItem;
            
            if (profe != null)
            {
                DetalleProfe dp = new DetalleProfe(profe);
                dp.Closed += Dp_Closed;
                dp.ShowDialog();  
            }
            else
            {
                Trace.WriteLine("Ningun profesor seleccionado");
                MessageBox.Show("Seleccione un profesor para editar");
            }            
        }        

        private void Boton_Agregar(object sender, RoutedEventArgs e)
        {
            DetalleProfe dp = new DetalleProfe();
            dp.Closed += Dp_Closed;
            dp.ShowDialog();
        }

        private void Dp_Closed(object sender, EventArgs e)
        {
            List<Profesor> list = DBServices.ObtenerProfesores();
            ProfeGrid.ItemsSource = list;
        }

        private void Boton_Eliminar(object sender, RoutedEventArgs e)
        {
            Profesor profe = (Profesor)ProfeGrid.SelectedItem;

            if (profe != null)
            {
                DBServices.BorrarProfesor(profe);
                MessageBox.Show("Profesor eliminado exitosamente");
                List<Profesor> list = DBServices.ObtenerProfesores();
                ProfeGrid.ItemsSource = list;
            }
            else
            {
                Trace.WriteLine("Ningun profesor seleccionado");
                MessageBox.Show("Seleccione profesor a eliminar");
            }
        }        
    }
}
