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
    /// Lógica de interacción para DetalleProfe.xaml
    /// </summary>
    public partial class DetalleProfe : Window
    {
        private Profesor profe;
        private bool isnuevoprofe;
        public DetalleProfe()
        {
            isnuevoprofe = true;
            profe = new Profesor();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        public DetalleProfe(Profesor profe)
        {
            isnuevoprofe = false;
            this.profe = profe;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isnuevoprofe)
            {
                Huella.Content = "Cambiar Huella";
                Datos.Content = "Actualizar Datos";
                Nombre.Text = profe.nombre;
                Apellido.Text = profe.apellido;
                Contra.Password = profe.contraseña;

                List<Horario> list = DBServices.ObtenerHorariosDeProfesor(profe);
                if (list.Count >= 1) HorarioGrid.ItemsSource = list;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }

        private void Huella_Click(object sender, RoutedEventArgs e)
        {
            ProfeHuella ph = new ProfeHuella(profe);
            ph.ShowDialog();            
        }

        private void Datos_Click(object sender, RoutedEventArgs e)
        {
            if (Nombre.Text.Trim() == "")
            {
                MessageBox.Show("Ingresa nombre");
                return;
            }
            if (Apellido.Text.Trim() == "")
            {
                MessageBox.Show("Ingresa apellidos");
                return;
            }
            if (Contra.Password.Trim() == "")
            {
                MessageBox.Show("Ingresa contraseña");
                return;
            }
            if (profe.huella == null)
            {
                MessageBox.Show("Ingresa huella");
                return;
            }

            profe.nombre = Nombre.Text;
            profe.apellido = Apellido.Text;
            profe.contraseña = Contra.Password;

            if (isnuevoprofe)
            {
                DBServices.AgregarProfesor(profe);
                MessageBox.Show("Datos de profesor ingresados exitosamente");
                profe = DBServices.ObtenerUltimoProfesor();
                Huella.Content = "Cambiar Huella";
                Datos.Content = "Actualizar Datos";
            }
            else
            {
                DBServices.ActualizarProfesor(profe);
                MessageBox.Show("Datos de profesor actualizados exitosamente");
            }
        }

        private void AgregarHorario_Click(object sender, RoutedEventArgs e)
        {
            DetalleHorario dh = new DetalleHorario(profe);
            dh.Closed += Dh_Closed; ;
            dh.ShowDialog();
        }

        private void Dh_Closed(object sender, EventArgs e)
        {
            List<Horario> list = DBServices.ObtenerHorariosDeProfesor(profe);
            HorarioGrid.ItemsSource = list;
        }

        private void BorrarHorario_Click(object sender, RoutedEventArgs e)
        {
            Horario horario = (Horario)HorarioGrid.SelectedItem;

            if (horario != null)
            {
                DBServices.BorrarHorario(horario);
                MessageBox.Show("Horario eliminado exitosamente");
                List<Horario> list = DBServices.ObtenerHorariosDeProfesor(profe);
                HorarioGrid.ItemsSource = list;
            }
            else
            {
                Trace.WriteLine("Ningun horario seleccionado");
                MessageBox.Show("Seleccione horario a eliminar");
            }
        }
    }
}