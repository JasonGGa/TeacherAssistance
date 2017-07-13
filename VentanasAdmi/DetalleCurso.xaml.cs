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

namespace EpieHorarios
{
    /// <summary>
    /// Lógica de interacción para DetalleCurso.xaml
    /// </summary>
    public partial class DetalleCurso : Window
    {
        private Curso curso;

        public DetalleCurso()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            curso = new Curso();

            if (Nombre.Text.Trim() != "") curso.nombre = Nombre.Text.Trim();
            else
            {
                MessageBox.Show("Ingresa nombre curso");
                return;
            }

            DBServices.AgregarCurso(curso);
            MessageBox.Show("Curso agregado exitosamente");
            this.Close();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
