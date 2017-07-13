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
    /// Lógica de interacción para ControlAdmin.xaml
    /// </summary>
    public partial class ControlAdmin : Window
    {
        string PASS = "admin";
        public bool Estado { get; set; }

        public ControlAdmin()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Estado = false;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Contra.Password.Trim() == "")
            {
                MessageBox.Show("Ingresa contraseña");
                return;
            }
            else
            {
                if (Contra.Password == PASS) Estado = true;
                else MessageBox.Show("Contraseña incorrecta");
                this.Close();
            }
        }
    }
}