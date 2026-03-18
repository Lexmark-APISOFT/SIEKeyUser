using Microsoft.Ajax.Utilities;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;

namespace SIE_KEY_USER.model.Courses
{
    public class Session
    {
        public string SessionID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImpartedBy { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string CourseID { get; set; }
        public int Space { set; get; }
        public string Place { set; get; }
        public int Status { get; set; }
        public int AvailablePlaces { get; set; }

        public List<Enrollment> _enrolled;
        //public List<Enrollment> Enrolled {
        //    get
        //    {
        //        if (_enrolled == null)
        //        {
        //            _enrolled = GetEnrolledEmployees();
        //        }
        //        return _enrolled;
        //    }
        //}
        public bool IsActive { get; set; }

        public Session(string SessionID) {
           
            this.SessionID = SessionID;
            //Enrolled = GetEnrolledEmployees(SessionID);
        }

        public void GetSessionInfo()
        {
            string SqlconString = ConfigurationManager.ConnectionStrings["dbCur"].ToString();
            string Query = $@"
                               SELECT TOP 1 
                                    [sess].SE_FOLIO,
                                    MA_CODIGO, 
                                    SE_FEC_INI, 
                                    SE_HOR_ini + ' ' + SE_HOR_FIN as 'schedule',
                                    curs.CU_CODIGO,
                                    SE_CUPO,
                                    SE_LUGAR, 
                                    SE_STATUS,
                                    CASE
                                          WHEN GETDATE() > SE_FEC_INI THEN 1
                                          ELSE 0
                                      END AS 'is_active',
	                                [sess].SE_CUPO - (SELECT COUNT(CB_CODIGO) FROM SIE.dbo.RT_Inscripciones WHERE SE_FOLIO = [sess].SE_FOLIO) AS 'available_spaces'
                                FROM 
                                    CommonDB.dbo.Sesion [sess] (NOLOCK) INNER JOIN
                                    CommonDB.dbo.Cursos_Regulatorios [curs] (NOLOCK)
                                    ON  [sess].CU_CODIGO = [curs].CU_CODIGO 
                                WHERE  sess.SE_FOLIO='{SessionID}'";
            using (SqlConnection conn = new SqlConnection(SqlconString))
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                SqlCommand command = new SqlCommand(Query, conn);
                command.Parameters.Add("@SessionID", SqlDbType.VarChar);
                command.Parameters["@SessionID"].Value = SessionID;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        this.SessionID = reader["SE_FOLIO"].ToString();
                        this.ImpartedBy = reader["MA_CODIGO"].ToString().ToLower();
                        this.Date = DateTime.Parse(reader["SE_FEC_INI"].ToString());
                        this.Time = FormatTime(reader["schedule"].ToString());
                        this.CourseID = reader["CU_CODIGO"].ToString();
                        this.Space = Int32.Parse(reader["SE_CUPO"].ToString());
                        this.Place = reader["SE_LUGAR"].ToString();
                        this.Status = Int32.Parse(reader["SE_STATUS"].ToString());
                        this.IsActive = reader["is_active"].ToString() == "1" ? true : false;
                        this.AvailablePlaces = Int32.Parse(reader["available_spaces"].ToString());
                    }
                    conn.Close();
                }
            }
        }

        public List<Enrollment> GetEnrolledEmployees()
        {
            List<Enrollment> enrolled = new List<Enrollment>();

            string query = $@"SELECT  
	                            [RTIN].CB_CODIGO AS 'EmployeeNumber',
	                            [RTIN].FECHA_INSCRIPCION
                            FROM SIE.dbo.RT_Inscripciones [RTIN]
                            INNER JOIN CommonDB.dbo.Colaborav2 [CBV2] ON [RTIN].CB_CODIGO = [CBV2].CB_CODIGO 
                            WHERE [RTIN].SE_FOLIO = '{this.SessionID}' and ([RTIN].Vigente!=0 or [RTIN].Vigente is null) ORDER BY [CBV2].CB_APE_PAT ASC
                            ";
            string SqlconString = ConfigurationManager.ConnectionStrings["dbCur"].ToString();

            using (SqlConnection conn = new SqlConnection(SqlconString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add("@SessionID", SqlDbType.VarChar);
                //command.Parameters["@SessionID"].Value = SessionID;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int EmployeeNumber = Int32.Parse(reader["EmployeeNumber"].ToString());
                        Employee employee = new Employee(EmployeeNumber);
                        Enrollment enrollment = new Enrollment
                        {
                            Employee = employee,
                            EnrollmentDate = DateTime.Parse(reader["FECHA_INSCRIPCION"].ToString())
                        };
                        enrolled.Add(enrollment);
                    }
                    conn.Close();
                }
            }
            return enrolled;

        }
        public string FormatTime(string Time) {
             
            string finalFormat = "";

            if (Time == null) 
            {
                finalFormat = "No time registered";
            }
            else
            {
                string[] Hours = Time.Split(' ');

                foreach (var hour in Hours)
                {
                    if (hour.Length > 0 && hour != " ")
                    {
                        DateTime dt = DateTime.ParseExact(hour, "HHmm", CultureInfo.InvariantCulture);
                        finalFormat += dt.ToString("h:mm tt") + " ";
                    }
                    else
                    {
                        finalFormat += "-- -- ";

                    }

                }

            }
            return finalFormat;


        }

    }
}