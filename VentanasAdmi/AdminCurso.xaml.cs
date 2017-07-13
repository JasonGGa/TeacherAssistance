using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para AdminCurso.xaml
    /// </summary>
    public partial class AdminCurso : Window
    {
        public AdminCurso()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Curso> list = DBServices.ObtenerCursos();
            CursoGrid.ItemsSource = list;
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            DetalleCurso dc = new DetalleCurso();
            dc.Closed += Dc_Closed;           
            dc.ShowDialog();
        }

        private void Dc_Closed(object sender, EventArgs e)
        {
            List<Curso> list = DBServices.ObtenerCursos();
            CursoGrid.ItemsSource = list;
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            Curso curso = (Curso)CursoGrid.SelectedItem;

            if (curso != null)
            {
                DBServices.BorrarCurso(curso);
                MessageBox.Show("Curso eliminado exitosamente");
                List<Curso> list = DBServices.ObtenerCursos();
                CursoGrid.ItemsSource = list;
            }
            else
            {
                Trace.WriteLine("Ningun curso seleccionado");
                MessageBox.Show("Seleccione profesor a eliminar");
            }
        }
    }
}
