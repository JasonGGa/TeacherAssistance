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
    /// Lógica de interacción para AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        Window parent;

        public AdminWindow(Window window)
        {
            parent = window;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void AdminProfe_Click(object sender, RoutedEventArgs e)
        {
            AdminProfe adminprofe = new AdminProfe();
            adminprofe.ShowDialog();
        }

        private void AdminCurso_Click(object sender, RoutedEventArgs e)
        {
            AdminCurso admincurso = new AdminCurso();
            admincurso.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            parent.Show();
        }
    }
}
