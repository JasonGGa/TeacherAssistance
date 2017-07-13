using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpieHorarios
{
    public class Dia
    {
        public Dia()
        {        
        }
        public Dia(int id, string nombre)
        {
            this.id = id;
            this.nombre = nombre;
        }

        public int id { get; set; }
        public String nombre { get; set; }

        public static Dia ObtenerDia()
        {
            int numdia = 0;
            DayOfWeek hoy = DateTime.Now.DayOfWeek;
            if (hoy == DayOfWeek.Monday) numdia = 1;
            else if (hoy == DayOfWeek.Tuesday) numdia = 2;
            else if (hoy == DayOfWeek.Wednesday) numdia = 3;
            else if (hoy == DayOfWeek.Thursday) numdia = 4;
            else if (hoy == DayOfWeek.Friday) numdia = 5;
            else if (hoy == DayOfWeek.Saturday) numdia = 6;
            else numdia = 7;

            List<Dia> dias = DBServices.ObtenerDias();

            foreach (var dia in dias)
            {
                if (dia.id == numdia) return dia;
            }
            return null;
        }
    }
}