using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpieHorarios
{
    public class Profesor
    {
        public Profesor() { }
        public Profesor(string nombre, string apellido, string contraseña, string huella, string huella10)
        {            
            this.nombre = nombre;
            this.apellido = apellido;
            this.contraseña = contraseña;
            this.huella = huella;
            this.huella10 = huella10;
        }
        public Profesor(int id, string nombre, string apellido, string contraseña, string huella, string huella10)
        {
            this.id = id;
            this.nombre = nombre;
            this.apellido = apellido;
            this.contraseña = contraseña;
            this.huella = huella;
            this.huella10 = huella10;
        }

        public int id { get; set; }
        public String nombre { get; set; }
        public String apellido { get; set; }
        public String contraseña { get; set; }
        public String huella { get; set; }
        public String huella10 { get; set; }
    }
}
