﻿using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ZKFPEngXControl;

namespace EpieHorarios
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private ZKFPEngX sensor = SensorInstance.Instance;
        List<Profesor> profesores;
        int fpcHandle;
        Dia hoy;
        Profesor[] profes = new Profesor[200];
        DispatcherTimer reloj = new DispatcherTimer();
        DispatcherTimer limpiar = new DispatcherTimer();
        TimeSpan tlimpiar;

        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DBInstance.OpenDBConnection();
            Identificacion_Activada();

            reloj.Tick += new EventHandler(Timer_Tick);
            reloj.Interval = new TimeSpan(0, 0, 1);
            reloj.Start();

            limpiar.Tick += Limpiar_Tick;
            limpiar.Interval = new TimeSpan(0, 0, 1);            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Time.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        private void Limpiar_Tick(object sender, EventArgs e)
        {
            if (tlimpiar == TimeSpan.Zero)
            {
                Nombre.Text = "";
                Asistencia.Text = "";
                Curso.Text = "";
                Finger.Source = null;
                limpiar.Stop();
            }
            tlimpiar = tlimpiar.Add(TimeSpan.FromSeconds(-1));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Identificacion_Desactivada();
            DBInstance.CloseDBConnection();            
        }

        private void Fp_OnImageReceived(ref bool AImageValid)
        {
            //Getting image of fingerprint
            object imgdata = new object();
            if (sensor.GetFingerImage(ref imgdata) == true)
            {
                //Suene el sensor
                sensor.ControlSensor(13, 1);
                sensor.ControlSensor(13, 0);  

                //Muestre imagen en ventana
                BitmapImage bm;
                using (var ms = new MemoryStream((byte[])imgdata))
                {
                    bm = new BitmapImage();
                    bm.BeginInit();
                    bm.CacheOption = BitmapCacheOption.OnLoad;
                    bm.StreamSource = ms;
                    bm.EndInit();
                }

                Finger.Source = bm;
            }
        }
        
        private void Fp_OnCapture(bool ActionResult, object ATemplate)
        {
            int id = 0, processNum = 0, score = 0;
            id = sensor.IdentificationInFPCacheDB(fpcHandle, ATemplate, ref score, ref processNum);

            if (id != -1)
            {
                Profesor profe = profes[id];
                Nombre.Text = profe.nombre + " " + profe.apellido;
                int horaActual = DateTime.Now.Hour * 60 + DateTime.Now.Minute;
                Horario hora = DBServices.ObtenerHorarioActualDeProfesor(profe, hoy, horaActual);
                if (hora != null)
                {
                    Asistencia.Text = "Asistencia marcada a las " + DateTime.Now.ToString("HH:mm");
                    Curso.Text = "Curso: " + hora.curso.nombre;
                }
                else
                    Asistencia.Text = "No tiene clases ahora";
            }
            else
            {
                Nombre.Text = "Profesor no registrado";
            }

            limpiar.Stop();
            tlimpiar = TimeSpan.FromSeconds(5);
            limpiar.Start();
        }

        private void Button_Admin(object sender, RoutedEventArgs e)
        {
            ControlAdmin ca = new ControlAdmin();
            ca.ShowDialog();
            bool res = ca.Estado;

            if (res)
            {
                AdminWindow admin = new AdminWindow(this);
                admin.Show();
                admin.Closed += Admin_Closed;
                this.Hide();
                Identificacion_Desactivada();
            }
        }
        
        private void Admin_Closed(object sender, EventArgs e)
        {
            Identificacion_Activada();
        }        

        private void Identificacion_Activada()
        {
            SensorInstance.OpenSensorConnection();

            if (sensor.Active)
            {
                fpcHandle = sensor.CreateFPCacheDB();
                
                sensor.OnCapture += Fp_OnCapture;
                sensor.OnImageReceived += Fp_OnImageReceived;

                hoy = Dia.ObtenerDia();

                profesores = DBServices.ObtenerProfesores();

                foreach (var profe in profesores)
                {
                    profes[profe.id] = profe;
                    string huella = profe.huella;
                    string huella10 = profe.huella10;

                    if (sensor.AddRegTemplateStrToFPCacheDBEx(fpcHandle, profe.id, huella, huella10) == 1)
                        Trace.WriteLine("Huella ingresada " + profe.nombre);
                    else
                        Trace.WriteLine("Huella no ingresada");
                }               
            }
            else
            {
                 MessageBoxResult res =  MessageBox.Show("Desea cerrar el programa?", "Sensor no conectado", MessageBoxButton.YesNo);

                if (res == MessageBoxResult.Yes)
                {                    
                    this.Close();
                    Environment.Exit(0);
                }
            }
        }

        private void Identificacion_Desactivada()
        {
            if (sensor.Active)
            {                
                sensor.OnCapture -= Fp_OnCapture;
                sensor.OnImageReceived -= Fp_OnImageReceived;

                if (fpcHandle > 0)
                {
                    sensor.FreeFPCacheDB(fpcHandle);
                }

                SensorInstance.CloseSensorConnection();
            }            
        }
    }
}