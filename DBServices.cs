using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EpieHorarios
{
    public class DBServices
    {
        public static void AgregarProfesor(Profesor profesor)
        {
            SqlConnection con = DBInstance.Instance;
            SqlCommand com = new SqlCommand("uspAgregarProfesor", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@nombre", profesor.nombre));
            com.Parameters.Add(new SqlParameter("@apellido", profesor.apellido));
            com.Parameters.Add(new SqlParameter("@contra", profesor.contraseña));
            com.Parameters.Add(new SqlParameter("@huella", profesor.huella));
            com.Parameters.Add(new SqlParameter("@huella10", profesor.huella10));
            com.ExecuteNonQuery();
        }

        public static void AgregarCurso(Curso curso)
        {
            SqlConnection con = DBInstance.Instance;
            SqlCommand com = new SqlCommand("uspAgregarCurso", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@nombre", curso.nombre));
            com.ExecuteNonQuery();
        }

        public static void AgregarHorario(Horario horario)
        {
            SqlConnection con = DBInstance.Instance;
            SqlCommand com = new SqlCommand("uspAgregarHorario", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@cursoid", horario.curso.id));
            com.Parameters.Add(new SqlParameter("@profeid", horario.profesor.id));
            com.Parameters.Add(new SqlParameter("@diaid", horario.dia.id));
            com.Parameters.Add(new SqlParameter("@horaini", Horario.HoraStrToInt(horario.horaini)));
            com.Parameters.Add(new SqlParameter("@horafin", Horario.HoraStrToInt(horario.horafin)));
            com.ExecuteNonQuery();
        }

        public static void AgregarAsistencia(Horario horario)
        {
            SqlConnection con = DBInstance.Instance;
            SqlCommand com = new SqlCommand("uspAgregarAsistencia", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@horarioid", horario.id));
            com.ExecuteNonQuery();
        }

        public static void ActualizarProfesor(Profesor profesor)
        {
            SqlConnection con = DBInstance.Instance;
            SqlCommand com = new SqlCommand("uspActualizarProfesor", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@id", profesor.id));
            com.Parameters.Add(new SqlParameter("@nombre", profesor.nombre));
            com.Parameters.Add(new SqlParameter("@apellido", profesor.apellido));
            com.Parameters.Add(new SqlParameter("@contra", profesor.contraseña));
            com.Parameters.Add(new SqlParameter("@huella", profesor.huella));
            com.Parameters.Add(new SqlParameter("@huella10", profesor.huella10));
            com.ExecuteNonQuery();
        }

        public static void ActualizarCurso(Curso curso)
        {
            SqlConnection con = DBInstance.Instance;
            SqlCommand com = new SqlCommand("uspActualizarCurso", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@id", curso.id));
            com.Parameters.Add(new SqlParameter("@nombre", curso.nombre));
            com.ExecuteNonQuery();
        }

        public static void ActualizarHorario(Horario horario)
        {
            SqlConnection con = DBInstance.Instance;
            SqlCommand com = new SqlCommand("uspActualizarHorario", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@id", horario.id));
            com.Parameters.Add(new SqlParameter("@cursoid", horario.curso.id));
            com.Parameters.Add(new SqlParameter("@profeid", horario.profesor.id));
            com.Parameters.Add(new SqlParameter("@diaid", horario.dia.id));
            com.Parameters.Add(new SqlParameter("@horaini", Horario.HoraStrToInt(horario.horaini)));
            com.Parameters.Add(new SqlParameter("@horafin", Horario.HoraStrToInt(horario.horafin)));
            com.ExecuteNonQuery();
        }

        public static string ObtenerContraAdmin(string usuario)
        {
            SqlConnection con = DBInstance.Instance;
            SqlCommand com = new SqlCommand("uspObtenerContraAdmin", con);
            string contra = "";
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@user", usuario));
            com.ExecuteNonQuery();

            using (SqlDataReader reader = com.ExecuteReader())
            {
                while (reader.Read())
                {
                    Trace.WriteLine(String.Format("{0}", reader[0]));
                    contra = (string)reader[0];
                }
            }
            return contra;
        }

        public static Dia ObtenerDia(int id)
        {
            SqlConnection con = DBInstance.Instance;
            SqlCommand com = new SqlCommand("uspObtenerDia", con);
            Dia dia = new Dia();
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@id", id));
            com.ExecuteNonQuery();

            using (SqlDataReader reader = com.ExecuteReader())
            {
                while (reader.Read())
                {
                    Trace.WriteLine(String.Format("{0}", reader[0]));
                    dia.id = id;
                    dia.nombre = (string)reader[0];
                    
                }
            }
            return dia;
        }

        public static List<Dia> ObtenerDias()
        {
            SqlConnection con = DBInstance.Instance;
            List<Dia> list = new List<Dia>();
            SqlCommand com = new SqlCommand("select * from Dia", con);
            com.ExecuteNonQuery();

            using (SqlDataReader reader = com.ExecuteReader())
            {
                while (reader.Read())
                {
                    Trace.WriteLine(String.Format("{0} \t | {1}", reader[0], reader[1]));
                    Dia dia = new Dia((int)reader[0], (string)reader[1]);
                    list.Add(dia);
                }
            }
            return list;
        }

        public static Profesor ObtenerUltimoProfesor()
        {
            SqlConnection con = DBInstance.Instance;
            SqlCommand com = new SqlCommand("uspObtenerProfesor", con);
            Profesor profesor = null;
            com.CommandType = System.Data.CommandType.StoredProcedure;            
            com.ExecuteNonQuery();

            using (SqlDataReader reader = com.ExecuteReader())
            {
                while (reader.Read())
                {
                    Trace.WriteLine(String.Format("{0} \t | {1} \t | {2} \t | {3} \t | {4}", reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]));
                    profesor = new Profesor();
                    profesor.id = (int)reader[0];
                    profesor.nombre = (string)reader[1];
                    profesor.apellido = (string)reader[2];
                    profesor.contraseña = (string)reader[3];
                    profesor.huella = (string)reader[4];
                    profesor.huella10 = (string)reader[5];
                }
            }
            return profesor;
        }

        public static List<Profesor> ObtenerProfesoresPorDia(Dia dia)
        {
            SqlConnection con = DBInstance.Instance;
            SqlCommand com = new SqlCommand("uspObtenerPorDia", con);
            List<Profesor> list = new List<Profesor>();
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@iddia", dia.id));
            com.ExecuteNonQuery();

            using (SqlDataReader reader = com.ExecuteReader())
            {
                while (reader.Read())
                {
                    Trace.WriteLine(String.Format("{0} \t | {1} \t | {2} \t | {3} \t | {4}", reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]));
                    Profesor profe = new Profesor((int)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], (string)reader[4], (string)reader[5]);
                    list.Add(profe);
                }
            }
            return list;
        }

        public static List<Profesor> ObtenerProfesores()
        {
            SqlConnection con = DBInstance.Instance;
            List<Profesor> list = new List<Profesor>();
            SqlCommand com = new SqlCommand("select * from Profesor", con);
            com.ExecuteNonQuery();

            using (SqlDataReader reader = com.ExecuteReader())
            {
                while (reader.Read())
                {
                    Trace.WriteLine(String.Format("{0} \t | {1} \t | {2} \t | {3} \t | {4}", reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]));
                    string huella10 = "";
                    try
                    {
                        huella10 = (string)reader[5];
                    }
                    catch (Exception)
                    {
                        Trace.WriteLine("Sin huella10");
                    }
                    Profesor profe = new Profesor((int)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], (string)reader[4], huella10);
                    list.Add(profe);
                }
            }
            return list;
        }

        public static Curso ObtenerCurso(int id)
        {
            SqlConnection con = DBInstance.Instance;
            SqlCommand com = new SqlCommand("uspObtenerCurso", con);
            Curso curso = new Curso();
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@id", id));
            com.ExecuteNonQuery();

            using (SqlDataReader reader = com.ExecuteReader())
            {
                while (reader.Read())
                {
                    Trace.WriteLine(String.Format("{0}", reader[0]));
                    curso.id = id;
                    curso.nombre = (string)reader[0];
                }
            }
            return curso;
        }

        public static List<Curso> ObtenerCursos()
        {
            SqlConnection con = DBInstance.Instance;
            List<Curso> list = new List<Curso>();
            SqlCommand com = new SqlCommand("select * from Curso", con);
            com.ExecuteNonQuery();

            using (SqlDataReader reader = com.ExecuteReader())
            {
                while (reader.Read())
                {
                    Trace.WriteLine(String.Format("{0} \t | {1}", reader[0], reader[1]));
                    Curso curso = new Curso((int)reader[0], (string)reader[1]);
                    list.Add(curso);
                }
            }
            return list;
        }

        public static Horario ObtenerHorarioActualDeProfesor(Profesor profe, Dia dia, int hora, int tole)
        {
            SqlConnection con = DBInstance.Instance;
            List<Horario> list = new List<Horario>();
            SqlCommand com = new SqlCommand("uspObtenerHorarioActualProfesor", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@profeid", profe.id));
            com.Parameters.Add(new SqlParameter("@diaid", dia.id));
            com.Parameters.Add(new SqlParameter("@hora", hora));
            com.Parameters.Add(new SqlParameter("@tole", tole));
            com.ExecuteNonQuery();

            using (SqlDataReader reader = com.ExecuteReader())
            {
                while (reader.Read())
                {
                    Trace.WriteLine(String.Format("{0} \t | {1} \t | {2} \t | {3} \t | {4} \t | {5}", reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]));
                    Curso curso = new Curso((int)reader[1], (string)reader[2]);
                    Horario horario = new Horario((int)reader[0], profe, curso, dia, (int)reader[4], (int)reader[5], (bool)reader[3]);
                    list.Add(horario);
                }
            }            
            return list.FirstOrDefault();
        }

        public static List<Horario> ObtenerHorariosDeProfesor(Profesor profesor)
        {
            SqlConnection con = DBInstance.Instance;
            List<Horario> list = new List<Horario>();
            SqlCommand com = new SqlCommand("uspObtenerHorariosProfesor", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@profeid", profesor.id));
            com.ExecuteNonQuery();

            using (SqlDataReader reader = com.ExecuteReader())
            {
                while (reader.Read())
                {
                    Trace.WriteLine(String.Format("{0} \t | {1} \t | {2} \t | {3} \t | {4} \t | {5} \t | {6}", reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6]));
                    Curso curso = new Curso((int)reader[1], (string)reader[2]);
                    Dia dia = new Dia((int)reader[3], (string)reader[4]);
                    Horario horario = new Horario((int)reader[0], profesor, curso, dia, (int)reader[5], (int)reader[6]);
                    list.Add(horario);
                }
            }
            return list;
        }

        public static List<Asistencia> ObtenerAsistencias()
        {
            SqlConnection con = DBInstance.Instance;
            List<Asistencia> list = new List<Asistencia>();
            SqlCommand com = new SqlCommand("uspObtenerAsistencias", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.ExecuteNonQuery();

            using (SqlDataReader reader = com.ExecuteReader())
            {
                while (reader.Read())
                {
                    Trace.WriteLine(String.Format("{0} \t | {1} \t | {2} \t | {3}", reader[0], reader[1], reader[2], reader[3], reader[4]));
                    Asistencia asis = new Asistencia((int)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], (DateTime)reader[4]);
                    list.Add(asis);
                }
            }
            return list;
        }

        public static void ObtenerAsistenciasDeProfesor(Profesor profe)
        {
            SqlConnection con = DBInstance.Instance;
            SqlCommand com = new SqlCommand("uspObtenerAsistenciasProfesor", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@profeid", profe.id));
            com.ExecuteNonQuery();

            using (SqlDataReader reader = com.ExecuteReader())
            {
                while (reader.Read())
                {
                    Trace.WriteLine(String.Format("{0} \t | {1}", reader[0], reader[1]));
                }
            }
        }

        public static void BorrarProfesor(Profesor profesor)
        {
            SqlConnection con = DBInstance.Instance;
            SqlCommand com = new SqlCommand("uspBorrarProfesor", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@id", profesor.id));
            com.ExecuteNonQuery();
        }

        public static void BorrarCurso(Curso curso)
        {
            SqlConnection con = DBInstance.Instance;
            SqlCommand com = new SqlCommand("uspBorrarCurso", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@id", curso.id));
            com.ExecuteNonQuery();
        }

        public static void BorrarHorario(Horario horario)
        {
            SqlConnection con = DBInstance.Instance;
            SqlCommand com = new SqlCommand("uspBorrarHorario", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@id", horario.id));
            com.ExecuteNonQuery();
        }

        public static void ActualizarContra(string user, string contra)
        {
            SqlConnection con = DBInstance.Instance;
            SqlCommand com = new SqlCommand("uspActualizarContraAdmin", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@user", user));
            com.Parameters.Add(new SqlParameter("@contra", contra));
            com.ExecuteNonQuery();
        }
    }
}