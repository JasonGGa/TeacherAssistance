using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpieHorarios
{
    public class DBInstance
    {
        private static readonly DBInstance instance = new DBInstance();

        private SqlConnection con;

        private DBInstance()
        {
            con = new SqlConnection();
        }

        public static void OpenDBConnection()
        {
            string constr = "Database=EpieHorarios;Data Source=DESKTOP-I13JUQ1;Integrated Security=True";            
            instance.con.ConnectionString = constr;
            Trace.WriteLine("Conneción con Base de Datos abierta.");
            instance.con.Open();
        }

        public static void CloseDBConnection()
        {
            Trace.WriteLine("Conección con Base de Datos cerrada.");
            instance.con.Close();
        }

        public static SqlConnection Instance
        {
            get
            {
                return instance.con;
            }
        }
    }
}
