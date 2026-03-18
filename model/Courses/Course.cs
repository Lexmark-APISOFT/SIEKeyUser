using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace SIE_KEY_USER.model.Courses
{
    public class Course
    {
        public string CourseID { set; get; }
        public string  CourseName { set; get; }
        public string CourseDate { set; get; }
        public string CourseType { set; get; } //this mean if is whereas  reg or nsm
        public string Clasification { set; get; }
        public string Type { set; get; } //this means the tipe of course such as department, area, etc.

        public List<Session> _sessions;
        public List<Session> Sessions {
            get { 
                if (_sessions == null)
                {
                    _sessions = GetAllSessions();
                }
                return _sessions;
            }
        }



        public Course()
        {
            
        }

        public Course(string CourseID) {
            string SqlconString = ConfigurationManager.ConnectionStrings["dbCur"].ToString();
            string Query = $@"
                              SELECT TOP 1 
	                            CU_CODIGO,
	                            CU_CLASIFI,
	                            CU_NOMBRE,
	                            CU_CLASE,
	                            CU_FEC_REV
                             FROM CommonDB.dbo.Cursos_Regulatorios
                             WHERE CU_CODIGO = '{CourseID}'";
            using (SqlConnection conn = new SqlConnection(SqlconString))
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                SqlCommand command = new SqlCommand(Query, conn);
                command.Parameters.Add("@CourseID", SqlDbType.VarChar);
                command.Parameters["@CourseID"].Value = CourseID;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        this.CourseID = CourseID.ToString();
                        this.CourseName = reader["CU_NOMBRE"].ToString();
                        this.CourseDate = DateTime.Parse(reader["CU_FEC_REV"].ToString()).ToString("dddd, dd MMMM yyyy", new CultureInfo("ES-ES"));
                        this.Clasification = reader["CU_CLASIFI"].ToString();
                        this.Type = reader["CU_CLASE"].ToString();
                        this.CourseType = reader["CU_CODIGO"].ToString().Contains("REG") ? "REG" : "NSM";
                    }
                    conn.Close();
                }
            }
            
        }

        protected List<Session> GetAllSessions()
        {
            string query = $@"
		                    SELECT 
			                    SE_FOLIO
		                    FROM CommonDB.dbo.Sesion 
		                    WHERE CU_CODIGO = '{this.CourseID}' 
		                    AND SE_FEC_INI BETWEEN (SELECT (PERIODO_INICIO) FROM SIE.dbo.RT_Periodo WHERE PERIODO_NAME = 'PERIODO_CURSOS')
							AND (SELECT (PERIODO_FINAL) FROM SIE.dbo.RT_Periodo WHERE PERIODO_NAME = 'PERIODO_CURSOS')";
            string SqlconString = ConfigurationManager.ConnectionStrings["dbCur"].ToString();

            using (SqlConnection conn = new SqlConnection(SqlconString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add("@CourseID", SqlDbType.VarChar);
                command.Parameters["@CourseID"].Value = CourseID;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    List<Session> sessions = new List<Session>();

                    while (reader.Read())
                    {
                        string SessionID = reader["SE_FOLIO"].ToString();
                        Session session=new Session(SessionID);
                        sessions.Add(session);
                    }
                    conn.Close();
                    return sessions;
                }
            }
        }


        public static List<Session> GetAvailableSessionsByCourse(string CourseID, string cbEmp)
         {
            List<Session> sessions = new List<Session>();



            //string queryGeneralResults = $@"SELECT 
            //                    SE_FOLIO 
            //                  FROM CommonDB.dbo.Sesion 
            //                  WHERE CU_CODIGO = '{CourseID}'
            //                ";

            //string SqlconString = ConfigurationManager.ConnectionStrings["dbCur"].ToString();

            //using (SqlConnection conn = new SqlConnection(SqlconString))
            //using (SqlCommand cmd = new SqlCommand(queryGeneralResults, conn))
            //{
            //    SqlCommand command = new SqlCommand(queryGeneralResults, conn);
            //    command.Parameters.Add("@SessionID", SqlDbType.VarChar);
            //    //command.Parameters["@SessionID"].Value = SessionID;
            //    conn.Open();
            //    using (var reader = cmd.ExecuteReader())
            //    {

            //        while (reader.Read())
            //        {
            //            Session session = new Session(reader["SE_FOLIO"].ToString());
            //            session.GetSessionInfo();
            //            sessions.Add(session);
            //        }
            //        conn.Close();
            //    }
            //}


            string SqlconString = ConfigurationManager.ConnectionStrings["db"].ToString();
            
            using (SqlConnection conn = new SqlConnection(SqlconString))

            using (SqlCommand cmd = new SqlCommand("dbo.sp_GetAplicable_Sessions", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@codEmp", SqlDbType.VarChar, 10);
                cmd.Parameters.Add("@course", SqlDbType.VarChar, 10);
                conn.Open();

                cmd.Parameters["@codEmp"].Value = cbEmp.ToString();
                cmd.Parameters["@course"].Value = CourseID.ToString();
                using (var reader = cmd.ExecuteReader())
                {
                    //string columns = null;
                    while (reader.Read())
                    {
                        bool alreadyEnr = alreadyEnrolledInSessionValidation(reader.GetValue(0).ToString(), cbEmp);
                        if (!alreadyEnr)
                        {
                            Session session = new Session(reader["SE_FOLIO"].ToString());
                            session.GetSessionInfo();
                            sessions.Add(session);
                        }
                        
                    }

                }
                conn.Close();
            }

            return sessions;

        }

        private static bool alreadyEnrolledInSessionValidation(string session, string cbEmp)
        {
            string current_course = null;
            bool isEnrolledalready = false;

            string SqlconString = ConfigurationManager.ConnectionStrings["db"].ToString();
            string query = "select SE_FOLIO from RT_Inscripciones rt where CB_CODIGO = '"+cbEmp+"' and rt.SE_FOLIO = '"+session+ "' and (Vigente !=0 or Vigente is null)";

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

            if (current_course == session)
            {
                isEnrolledalready = true;
            }

            return isEnrolledalready;

        }


    }
}