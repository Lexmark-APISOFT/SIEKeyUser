using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace SIE_KEY_USER.model.Courses
{
    public class Enrollment
    {


        public int EnrollmentID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public Session Session { get; set; }
        public Employee Employee { get; set; }
        public string Status { get; set; }



        public Enrollment()
        {

        }
        public Enrollment(int EnrollmentID,DateTime EnrollmentDate,Session Session,Employee employee, string Status)
        {
            this.EnrollmentID = EnrollmentID;
            this.EnrollmentDate = EnrollmentDate;
            this.Session = Session;
            this.Employee = employee;
            this.Status = Status;
        }

        public static bool EnrrollEmployee(string EmployeeNumber, string SessionID)
        {

            return true;
        }

        public static bool UnenrrolEmployee(string EmployeeNumber,string SessionID)
        {
            
            return false;
        }

        public static bool IsEnrolledToSession(string EmployeeNumber,string SessionID)
        {
            try
            {
                // Get the connection string from the configuration
                string SqlconString = ConfigurationManager.ConnectionStrings["dbCur"].ToString();

                using (SqlConnection connection = new SqlConnection(SqlconString))
                {
                    connection.Open();

                    // Define the SQL query
                    string sqlQuery = "SELECT 1 FROM sie.dbo.RT_Inscripciones WHERE SE_FOLIO = @sessionId AND CB_CODIGO = @studentId";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        // Set parameter values
                        command.Parameters.AddWithValue("@sessionId", SessionID);
                        command.Parameters.AddWithValue("@studentId", EmployeeNumber);

                        // Execute the query
                        object result = command.ExecuteScalar();

                        // Check if a record exists
                        if (result != null && result != DBNull.Value)
                        {
                            return true; // Student is enrolled in the session
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log them, or return an appropriate error message
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return false;
        }

        public static bool IsEnrolledToCourse(string EmployeeNumber, string CourseID)
        {
            try
            {
                // Get the connection string from the configuration
                string SqlconString = ConfigurationManager.ConnectionStrings["dbCur"].ToString();

                using (SqlConnection connection = new SqlConnection(SqlconString))
                {
                    connection.Open();

                    // Define the SQL query
                    string sqlQuery = @"SELECT 1 FROM 
                                        sie.dbo.RT_Inscripciones 
                                        WHERE CU_CODIGO = @CourseId AND CB_CODIGO = @EmployeeNumber
                                        AND FECHA_INSCRIPCION BETWEEN (SELECT PERIODO_INICIO FROM SIE.dbo.RT_Periodo WHERE PERIODO_NAME = 'PERIODO_INSCRIPCIONES') AND
								                           (SELECT PERIODO_FINAL FROM SIE.dbo.RT_Periodo WHERE PERIODO_NAME = 'PERIODO_INSCRIPCIONES') and (Vigente !=0 or Vigente is null)
                                        ";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        // Set parameter values
                        command.Parameters.AddWithValue("@CourseId", CourseID);
                        command.Parameters.AddWithValue("@EmployeeNumber", EmployeeNumber);

                        // Execute the query
                        object result = command.ExecuteScalar();

                        // Check if a record exists
                        if (result != null && result != DBNull.Value)
                        {
                            return true; // Student is enrolled in the session
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log them, or return an appropriate error message
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return false;
        }

    }
}