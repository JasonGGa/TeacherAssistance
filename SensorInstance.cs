using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZKFPEngXControl;

namespace EpieHorarios
{
    class SensorInstance
    {
        private static readonly SensorInstance instance = new SensorInstance();

        private ZKFPEngX fp;

        private SensorInstance()
        {
            fp = new ZKFPEngX();
        }

        public static void OpenSensorConnection()
        {
            if (Instance.InitEngine() == 0)
            {                
                Trace.WriteLine("Conección con Sensor abierta.");
            }
            else
            {
                Trace.WriteLine("Conección con Sensor fallida.");
            }
        }

        public static void CloseSensorConnection()
        {
            Instance.EndEngine();
            Trace.WriteLine("Conección con Sensor cerrada.");
        }

        public static ZKFPEngX Instance
        {
            get
            {
                return instance.fp;
            }
        }
    }
}