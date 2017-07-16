using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpieHorarios
{
    public class Asistencia
    {
        public Asistencia(int id, string apellidos, string nombres, string curso, DateTime fecha)
        {
            Id = id;
            Apellidos = apellidos;
            Nombres = nombres;
            Curso = curso;
            Fecha = fecha;
        }

        public int Id { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string Curso { get; set; }
        public DateTime Fecha { get; set; }
    }
}
