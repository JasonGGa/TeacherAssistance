using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            CursoList.ItemsSource = DBServices.ObtenerCursos();
            DiaList.ItemsSource = DBServices.ObtenerDias();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {    
            try
            {
                string horaInicio = HInicio.Text.Trim();
                string horaFin = HFin.Text.Trim();

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
                if (horaInicio == null)
                {
                    MessageBox.Show("Ingresa una hora de inicio de clase");
                    return;
                }
                if (horaFin == null)
                {
                    MessageBox.Show("Ingresa una hora de fin de clase");
                    return;
                }
                string patron = @"^(?:0[7-9]|1[0-9]|2[0-1]):[0-5][0-9]$";
                Match matchini = Regex.Match(horaInicio, patron);
                if (!matchini.Success)
                {
                    MessageBox.Show("Ingresa una hora de inicio válida 'HH:mm'");
                    return;
                }
                Match matchfin = Regex.Match(horaFin, patron);
                if (!matchfin.Success)
                {
                    MessageBox.Show("Ingresa una hora de fin válida 'HH:mm'");
                    return;
                }

                horario = new Horario()
                {
                    profesor = profesor,
                    curso = (Curso)CursoList.SelectedItem,
                    dia = (Dia)DiaList.SelectedItem,
                    horaini = horaInicio,
                    horafin = horaFin
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
