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
    /// Lógica de interacción para CambiarContra.xaml
    /// </summary>
    public partial class CambiarContra : Window
    {
        public CambiarContra()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void Cambiar_Click(object sender, RoutedEventArgs e)
        {
            string acontra = ContraAnte.Password.Trim();
            string ncontra = ContraNueva.Password.Trim();
            if (acontra == "")
            {
                MessageBox.Show("Ingresa contraseña antigua");
                return;
            }
            else if (acontra != DBServices.ObtenerContraAdmin("admin"))
            {
                MessageBox.Show("Contraseña antigua no es correcta");
                return;
            }
            if (ncontra == "")
            {
                MessageBox.Show("Ingresa nueva contrseña");
                return;
            }

            try
            {
                DBServices.ActualizarContra("admin", ncontra);
                MessageBox.Show("Contraseña actualizada exitosamente");                
            }
            catch (Exception)
            {
                MessageBox.Show("Contraseña no actualizada");
            }
            this.Close();
        }
    }
}
