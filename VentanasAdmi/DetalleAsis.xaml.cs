using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica de interacción para DetalleAsis.xaml
    /// </summary>
    public partial class DetalleAsis : Window
    {
        List<Asistencia> asistencias = DBServices.ObtenerAsistencias();
        List<Asistencia> filtroProfe;
        List<string> filtroFecha = new List<string>() { "Todas", "Hoy", "Última Semana", "Último Mes", "Último Semestre" };

        public DetalleAsis()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            Profesores.ItemsSource = DBServices.ObtenerProfesores();
            Fechas.ItemsSource = filtroFecha;
            Fechas.SelectedValue = "Todas";
            AsisList.ItemsSource = asistencias;
            filtroProfe = asistencias;
        }

        private void Profesores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int profesor = ((Profesor)Profesores.SelectedItem).id;
            List<Asistencia> filtroTemp = new List<Asistencia>();
            foreach (var item in asistencias)
                if (item.Id == profesor) filtroTemp.Add(item);
            filtroProfe = filtroTemp;
            AsisList.ItemsSource = filtroProfe;
        }

        private void Fechas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string fecha = (string)Fechas.SelectedItem;
            if (fecha == filtroFecha[0])
            {
                AsisList.ItemsSource = filtroProfe;
            }
            else if (fecha == filtroFecha[1])
            {
                List<Asistencia> filtroFecha = new List<Asistencia>();
                foreach (var item in filtroProfe)
                    if (DateTime.Compare(item.Fecha, DateTime.Now.Date) > 0) filtroFecha.Add(item);
                AsisList.ItemsSource = filtroFecha;
            }
            else if (fecha == filtroFecha[2])
            {
                int diasemana = (int)(DateTime.Now.DayOfWeek + 6) % 7;
                DateTime lastweek = DateTime.Now.AddDays(-1 * diasemana);                
                List<Asistencia> filtroFecha = new List<Asistencia>();
                foreach (var item in filtroProfe)
                    if (DateTime.Compare(item.Fecha, lastweek.Date) > 0) filtroFecha.Add(item);
                AsisList.ItemsSource = filtroFecha;
            }
            else if (fecha == filtroFecha[3])
            {
                DateTime lastmonth = DateTime.Now.AddDays(-1 * (DateTime.Now.Day - 1));                 
                List<Asistencia> filtroFecha = new List<Asistencia>();
                foreach (var item in filtroProfe)
                    if (DateTime.Compare(item.Fecha, lastmonth.Date) > 0) filtroFecha.Add(item);
                AsisList.ItemsSource = filtroFecha;
            }
            else
            {
                DateTime lastmonth = DateTime.Now.AddDays(-180);
                List<Asistencia> filtroFecha = new List<Asistencia>();
                foreach (var item in filtroProfe)
                    if (DateTime.Compare(item.Fecha, lastmonth.Date) > 0) filtroFecha.Add(item);
                AsisList.ItemsSource = filtroFecha;                
            }
        }
    }
}