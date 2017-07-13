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
    /// Lógica de interacción para DetalleHorario.xaml
    /// </summary>
    public partial class DetalleHorario : Window
    {
        private Profesor profesor;
        private Horario horario;

        public DetalleHorario(Profesor profe)
        {
            profesor = profe;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> horas = new List<string>();
            for (int i = 420; i <= 1260; i+=30)
            {
                horas.Add(Horario.HoraIntToStr(i));
            }
            CursoList.ItemsSource = DBServices.ObtenerCursos();
            DiaList.ItemsSource = DBServices.ObtenerDias();
            HInicioList.ItemsSource = horas;
            HFinList.ItemsSource = horas;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {    
            try
            {
                if (CursoList.SelectedItem == null)
                {
                    MessageBox.Show("Selecciona un curso");
                    return;
                }
                if (DiaList.SelectedItem == null)
                {
                    MessageBox.Show("Selecciona un dia");
                    return;
                }
                if (HInicioList.SelectedItem == null)
                {
                    MessageBox.Show("Selecciona una hora de inicio de clase");
                    return;
                }
                if (HFinList.SelectedItem == null)
                {
                    MessageBox.Show("Selecciona una hora de fin de clase");
                    return;
                }

                horario = new Horario()
                {
                    profesor = profesor,
                    curso = (Curso)CursoList.SelectedItem,
                    dia = (Dia)DiaList.SelectedItem,
                    horaini = (string)HInicioList.SelectedItem,
                    horafin = (string)HFinList.SelectedItem
                };
                DBServices.AgregarHorario(horario);
                MessageBox.Show("Horario agregado exitosamente");
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Ingresa datos de profesor primero");
            }
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
