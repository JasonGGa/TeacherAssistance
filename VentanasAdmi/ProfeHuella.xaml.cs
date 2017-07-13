using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
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
using ZKFPEngXControl;

namespace EpieHorarios
{
    /// <summary>
    /// Lógica de interacción para ProfeHuella.xaml
    /// </summary>
    public partial class ProfeHuella : Window
    {
        private ZKFPEngX sensor = SensorInstance.Instance;
        private Profesor profe;

        public ProfeHuella(Profesor profe)
        {
            this.profe = profe;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SensorInstance.OpenSensorConnection();

            sensor.OnImageReceived += Fp_OnImageReceived;
            sensor.OnEnroll += Fp_OnEnroll;
            sensor.OnFeatureInfo += Sensor_OnFeatureInfo;            

            sensor.CancelEnroll();
            sensor.EnrollCount = 3;
            sensor.BeginEnroll();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            sensor.OnImageReceived -= Fp_OnImageReceived;
            sensor.OnEnroll -= Fp_OnEnroll;
            sensor.OnFeatureInfo -= Sensor_OnFeatureInfo;

            SensorInstance.CloseSensorConnection();
        }

        private void Fp_OnImageReceived(ref bool AImageValid)
        {
            //Muestre imagen en ventana
            object imgdata = new object();
            if (sensor.GetFingerImage(ref imgdata))
            {
                BitmapImage bm;
                using (var ms = new MemoryStream((byte[])imgdata))
                {
                    bm = new BitmapImage();
                    bm.BeginInit();
                    bm.CacheOption = BitmapCacheOption.OnLoad;
                    bm.StreamSource = ms;
                    bm.EndInit();
                }

                Huella.Source = bm;
            }
        }
        
        private void Fp_OnEnroll(bool ActionResult, object ATemplate)
        {
            string sRegTemplate = "", sRegTemplate10 = "";

            if (ActionResult)
            {
                sRegTemplate = sensor.GetTemplateAsStringEx("9");
                sRegTemplate10 = sensor.GetTemplateAsStringEx("10");

                if (sRegTemplate.Length > 0 && sRegTemplate10.Length > 0)
                {                    
                    profe.huella = sRegTemplate;
                    profe.huella10 = sRegTemplate10;
                    Trace.WriteLine("Ingreso de huella exitoso. " + sRegTemplate + sRegTemplate10);
                    MessageBox.Show("Huella Guardada");
                    this.Close();
                }
                else
                {
                    Trace.WriteLine("Tamaño de template es cero");
                    MessageBox.Show("Huella No Guardada");
                    this.Close();
                }
            }
            else
            {
                Trace.WriteLine("Ingreso de huella fallido");
                MessageBox.Show("Huella No Guardada");
                this.Close();
            }
        }

        private void Sensor_OnFeatureInfo(int AQuality)
        {
            string str = "", cali = "";

            if (sensor.IsRegister)
                str = "Ingresa huella " + (sensor.EnrollIndex - 1) + " veces mas";
            
            if (AQuality != 0)
                cali = "Baja calidad de imagen =" + sensor.LastQuality;
            else
                cali = "Calidad de imagen =" + sensor.LastQuality;

            Trace.WriteLine(cali);
            Estado.Content = str;
        }
    }
}
