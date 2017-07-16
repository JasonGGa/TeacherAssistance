using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpieHorarios
{
    public class Horario
    {
        public Horario() { }

        public Horario(Profesor profesor, Curso curso, Dia dia, string horaini, string horafin, bool estado)
        {
            this.profesor = profesor;
            this.curso = curso;
            this.dia = dia;
            this.horaini = horaini;
            this.horafin = horafin;
            this.estado = estado;
        }

        public Horario(int id, Profesor profesor, Curso curso, Dia dia, int horaini, int horafin)
        {
            this.id = id;
            this.profesor = profesor;
            this.curso = curso;
            this.dia = dia;
            this.horaini = HoraIntToStr(horaini);
            this.horafin = HoraIntToStr(horafin);
        }

        public Horario(int id, Profesor profesor, Curso curso, Dia dia, int horaini, int horafin, bool estado)
        {
            this.id = id;
            this.profesor = profesor;
            this.curso = curso;
            this.dia = dia;
            this.horaini = HoraIntToStr(horaini);
            this.horafin = HoraIntToStr(horafin);
            this.estado = estado;
        }

        public int id { get; set; }
        public Profesor profesor { get; set; }
        public Curso curso { get; set; }
        public Dia dia { get; set; }
        public string horaini { get; set; }
        public string horafin { get; set; }
        public bool estado { get; set; }

        public static int HoraStrToInt (string hora)
        {
            string[] arr = hora.Split(':');
            int res = Int32.Parse(arr[0]) * 60;
            res += Int32.Parse(arr[1]);
            return res;
        }

        public static string HoraIntToStr(int hora)
        {
            string h = (hora / 60).ToString("00");
            string m = (hora % 60).ToString("00");
            return h + ":" + m;
        }
    }
}
