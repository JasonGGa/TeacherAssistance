using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpieHorarios
{
    public class Dia
    {
        public Dia(int id, string nombre)
        {
            this.id = id;
            this.nombre = nombre;
        }

        public int id { get; set; }
        public String nombre { get; set; }
    }
}
