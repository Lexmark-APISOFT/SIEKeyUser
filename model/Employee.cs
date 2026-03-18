using SIE_KEY_USER.model.Courses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Microsoft.Office.Interop.Word;
using System.Security.Cryptography;
using System.Web.Services.Description;
using SIE_KEY_USER.Cursos;
using System.Web.Providers.Entities;

namespace SIE_KEY_USER.model
{
    public class Employee
    {
        public int EmployeeNumber { get; set; }
        public string Names { get; set; }
        public string PaternalSurname { get; set; }
        public string MateralSurnames { get; set; }
        public string Position { get; set; }
        public string LastPositionTime { get; set; }   
        
        public List<Enrollment> _enrollments;
        public List<Enrollment> Enrollments { get; set; }
        public List<Course> AssignedCoursesByPosition { get; set; }

        public Employee()
        {
            
        }

        public void getOfEmployeeEnrollments()
        {
            //if (_enrollments == null)
            //{
            this.Enrollments = GetEmployeeEnrollments();
            //}
            //return _enrollments;
        }
        public Employee(int EmployeeNumber)
        {
            this.EmployeeNumber = EmployeeNumber;
            string query = $@"SELECT TOP 1
                                [CBV2].CB_NOMBRES,
                                [CBV2].CB_APE_PAT,
                                [CBV2].CB_APE_MAT,
                                [CBV2].PU_DESCRIP,
                                CASE
                                    WHEN [KDPU].cb_fecha IS NULL OR [KDPU].cb_fecha = '' THEN 'No changes'
                                    ELSE CONCAT(
                                        DATEDIFF(YEAR, [KDPU].cb_fecha, GETDATE()),
                                        ' year, ',
                                        DATEDIFF(MONTH, [KDPU].cb_fecha, GETDATE()) % 12,
                                        ' month'
                                    )
                                END AS date_span
                            FROM CommonDB.dbo.ColaboraV2 [CBV2] (NOLOCK)
                            LEFT JOIN CommonDB.dbo.Kardex_Puesto [KDPU] (NOLOCK) 
                                ON [CBV2].CB_CODIGO = [KDPU].cb_codigo
                            WHERE [CBV2].CB_CODIGO = '"+EmployeeNumber+"' ORDER BY [KDPU].cb_fecha DESC";

            string SqlconString = ConfigurationManager.ConnectionStrings["dbCur"].ToString();
            using (SqlConnection conn = new SqlConnection(SqlconString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add("@EmployeeNumber", SqlDbType.Int);
                command.Parameters["@EmployeeNumber"].Value = EmployeeNumber.ToString();
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        this.Names = reader["CB_NOMBRES"].ToString();
                        this.PaternalSurname = reader["CB_APE_PAT"].ToString() ;
                        this.MateralSurnames = reader["CB_APE_MAT"].ToString();
                        this.Position = reader["PU_DESCRIP"].ToString();
                        this.LastPositionTime = reader["date_span"].ToString();

                    }
                    conn.Close();
                }
            }
        }

        public List<Course> GetMandatoryCourses()
        {
            string SqlconString = ConfigurationManager.ConnectionStrings["dbCur"].ToString();

            try
            {
                using (SqlConnection conn = new SqlConnection(SqlconString))
                using (SqlCommand cmd = new SqlCommand("SIE.dbo.sp_GetAplicable_Courses", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    cmd.Parameters.Add(new SqlParameter("@codEmp", SqlDbType.NVarChar, 50));
                    cmd.Parameters["@codEmp"].Value = EmployeeNumber;

                    using (var reader = cmd.ExecuteReader())
                    {
                        List<Course> courses = new List<Course>();

                        while (reader.Read())
                        {
                            Course course = new Course(reader["CU_CODIGO"].ToString());
                            bool skip=alreadyEnrolledValidation(course.CourseID);
                            if (!skip)
                            {
                                courses.Add(course);

                            }
                        }

                        conn.Close();
                        return courses;
                    }

                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        protected bool alreadyEnrolledValidation(string curso)
        {
            string current_course = null;
            bool isEnrolledalready = false;

            string SqlconString = ConfigurationManager.ConnectionStrings["db"].ToString();
            string query = "select top 1 CU_CODIGO from RT_Inscripciones where cb_codigo='"+this.EmployeeNumber+"' and CU_CODIGO = '"+curso+ "' and Year(FECHA_INSCRIPCION)=Year(GETDATE()) and (Vigente !=0 or Vigente is null) order by FECHA_INSCRIPCION desc";

            using (SqlConnection conn = new SqlConnection(SqlconString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        current_course = reader[0].ToString();

                }
                conn.Close();
            }

            if (current_course == curso)
            {
                isEnrolledalready = true;
            }

            return isEnrolledalready;

        }

        public List<Enrollment> GetEmployeeEnrollments()
        {
            string query = $@"
                                SELECT ID, RTI.SE_FOLIO, CB_CODIGO, STATUS_EXPORTADO, SES.CU_CODIGO, FECHA_INSCRIPCION
                                FROM sie.dbo.RT_Inscripciones RTI INNER JOIN CommonDB.dbo.Sesion SES ON RTI.SE_FOLIO = SES.SE_FOLIO
                                WHERE CB_CODIGO = '{this.EmployeeNumber}' AND EXISTS(SELECT TOP 1 SE_FOLIO FROM CommonDB.DBO.Sesion 
                                WHERE SE_FOLIO=RTI.SE_FOLIO) AND FECHA_CURSO BETWEEN (SELECT PERIODO_INICIO FROM SIE.dbo.RT_Periodo 
                                WHERE PERIODO_NAME = 'PERIODO_INSCRIPCIONES') AND (SELECT PERIODO_FINAL FROM SIE.dbo.RT_Periodo 
                                WHERE PERIODO_NAME = 'PERIODO_CURSOS') and (vigente !=0 or Vigente is null) ORDER BY SES.SE_FEC_INI ASC, SES.SE_HOR_INI ASC
                            ";
            string SqlconString = ConfigurationManager.ConnectionStrings["dbCur"].ToString();

            using (SqlConnection conn = new SqlConnection(SqlconString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add("@SessionID", SqlDbType.VarChar);
                command.Parameters["@SessionID"].Value = this.EmployeeNumber;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    List<Enrollment> enrolled = new List<Enrollment>();
                    Employee employee = this;
                    while (reader.Read())
                    {
                        SIE_KEY_USER.model.Courses.Session session = new SIE_KEY_USER.model.Courses.Session(reader["SE_FOLIO"].ToString());

                        session.GetSessionInfo();

                        Enrollment enrollment = new Enrollment
                        {
                            EnrollmentID = Int32.Parse(reader["ID"].ToString()),
                            EnrollmentDate = DateTime.Parse(reader["FECHA_INSCRIPCION"].ToString()),
                            Session = session,
                            Employee = employee,
                            Status = reader["STATUS_EXPORTADO"].ToString(),
                        };
                        enrolled.Add(enrollment);
                    }
                    conn.Close();
                    return enrolled;
                }
            }
        }


        public List<string> GetReprogramming() {
            string query = $@"
                                SELECT 
						            CU_CODIGO,
						            SE_FOLIO_PREVIO,
						            RE_FECHA,
						            SE_FOLIO,
						            RE_RAZON,
						            status_aprobado,
						            'TEST' as approved_by
					            FROM 
					            sie.dbo.RT_Reprogramaciones 
					            WHERE CB_CODIGO = '{this.EmployeeNumber}'
                            ";
            string SqlconString = ConfigurationManager.ConnectionStrings["dbCur"].ToString();

            using (SqlConnection conn = new SqlConnection(SqlconString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                SqlCommand command = new SqlCommand(query, conn);
                //command.Parameters.Add("@SessionID", SqlDbType.VarChar);
                //command.Parameters["@SessionID"].Value = this.EmployeeNumber;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    List<string> reprogramming = new List<string>();
                    Employee employee = this;
                    string reprogrammingInfo = "";
                    while (reader.Read())
                    {
                        
                        for (int i=0;i<reader.FieldCount;i++) {
                            reprogrammingInfo += reader.GetValue(i).ToString() + ",";
                        }
                        reprogramming.Add(reprogrammingInfo);
                        reprogrammingInfo = null;
                    }
                    conn.Close();
                    return reprogramming;
                }
            }

        }


        public string SuscribeToCourse(string id_session, string requestedEmployee)
        {
            string result;
            bool doubleEnroll = preventDobleEnrolling(id_session, requestedEmployee);

            if (doubleEnroll == false)
            {
                //sp_regulatorios_inscribir
                string SqlconString = ConfigurationManager.ConnectionStrings["db"].ToString();
                using (SqlConnection conn = new SqlConnection(SqlconString))
                using (SqlCommand cmd = new SqlCommand("sp_regulatorios_inscribir", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@se_folio", SqlDbType.NVarChar, 50);
                    cmd.Parameters.Add("@cb_codigo", SqlDbType.Int);
                    conn.Open();

                    cmd.Parameters["@se_folio"].Value = id_session;
                    cmd.Parameters["@cb_codigo"].Value = int.Parse(requestedEmployee);

                    //cmd.ExecuteNonQuery();

                    using (var reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        result = reader[0].ToString();
                    }

                    conn.Close();
                }

            }
            else
            {
                result = "Ya se encontraba registrado/a en la sesion.";
            }

            return result;
        }

        private bool preventDobleEnrolling(string sessionId, string employeeID)
        {
            string current_session = null;
            bool isEnrolledalready = false;

            string SqlconString = ConfigurationManager.ConnectionStrings["db"].ToString();
            string query = "select SE_FOLIO from SIE.dbo.RT_Inscripciones where SE_FOLIO = '" + sessionId + "' and CB_CODIGO='" + employeeID + "' and (vigente !=0 or Vigente is null)";

            using (SqlConnection conn = new SqlConnection(SqlconString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        current_session = reader[0].ToString();

                }
                conn.Close();
            }

            if (current_session == sessionId)
            {
                isEnrolledalready = true;
            }

            return isEnrolledalready;
        }

        public string reprogramEmployee(string seSSionID, string employeeID, string prevSessionID, string courseID, int responseAdmin, int isDirect)
        {
            
            string result;

            //sp_regulatorios_inscribir
            string SqlconString = ConfigurationManager.ConnectionStrings["db"].ToString();
            using (SqlConnection conn = new SqlConnection(SqlconString))
            using (SqlCommand cmd = new SqlCommand("sp_reprogramar_cursos", conn))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@NEW_SE_FOLIO", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@SE_FOLIO_PREVIO", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@CB_CODIGO", SqlDbType.Int);
                cmd.Parameters.Add("@RE_RAZON", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@CU_CODIGO", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@checked", SqlDbType.Int);
                cmd.Parameters.Add("@whoRequested", SqlDbType.Int);
                conn.Open();

                cmd.Parameters["@NEW_SE_FOLIO"].Value = seSSionID;
                cmd.Parameters["@SE_FOLIO_PREVIO"].Value = prevSessionID;
                cmd.Parameters["@CB_CODIGO"].Value = int.Parse(employeeID);
                cmd.Parameters["@RE_RAZON"].Value = "Admin. reprogramming";
                cmd.Parameters["@CU_CODIGO"].Value = courseID;
                cmd.Parameters["@checked"].Value = responseAdmin;
                cmd.Parameters["@whoRequested"].Value = isDirect;

                //cmd.ExecuteNonQuery();

                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    result = reader[0].ToString();
                }

                conn.Close();
            }

            return result;
            //result = "Ya habia una solicitud de reprogramacion.";

            
        }

        public string unEnrollEmployee(string sessID, string empNumber)
        {

            string result;
            
            //sp_regulatorios_inscribir
            string SqlconString = ConfigurationManager.ConnectionStrings["db"].ToString();
            string query = "update RT_Inscripciones set Vigente = 0 where SE_FOLIO = '"+sessID+"' and CB_CODIGO = '"+empNumber+"'";

            try
            {
                using (SqlConnection conn = new SqlConnection(SqlconString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    cmd.ExecuteNonQuery();

                    conn.Close();
                }

                result = "Se dio de baja con exito de la sesion al empleado";

            }
            catch
            {
                result = "No fue posible completar la baja de la sesion al empleado";
            }

            return result;


        }

    }
}