using SIE_KEY_USER.model.Courses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
//using System.Web.Providers.Entities;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace SIE_KEY_USER.Cursos
{
    public partial class Sessions : System.Web.UI.Page
    {
        public static string sess_id{ get; set; }
        public static string course_id { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            { 
                if (!IsPostBack)
                {
                    //retrieves the session ID from the URL params
                    string SessionInURL = Request.Params["SessionID"].ToString()!= "" ? Request.Params["SessionID"].ToString() : "";
                    sess_id = SessionInURL;
                    Session SessionToRender = RetriveSessionData(SessionInURL);
                    RenderSessionInfo(SessionToRender);
                    course_id = SessionToRender.CourseID;
                }

            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("../../Views/Default.aspx");
            }
        }

        protected void goBack(object sender, EventArgs e)
        {
            Response.Redirect("./MenuSessions.aspx?CourseID=" + course_id);
        }


        private void RenderSessionInfo(Session Session)
        {
            List<Enrollment> EnrolledEmployees = Session.GetEnrolledEmployees();
            //It places the basic info of the session in the fields it belongs to
            DivSubtitleSession.InnerHtml += Session.SessionID.ToString();
            int available = Session.Space - EnrolledEmployees.Count;
            TxtImpartedBy.Text=Session.ImpartedBy;
            TxtDate.Text = Session.Date.ToString("dddd, dd MMMM yyyy", new CultureInfo("ES"));
            TxtPlace.Text = Session.Place;
            TxtEnrolled.Text = EnrolledEmployees.Count.ToString();
            TxtSpce.Text = Session.Space.ToString() ;
            TxtAvailable.Text = available.ToString();
            LblSession.Text = Session.SessionID; 
            LblCurso.Text = new Course(Session.CourseID).CourseID.ToString() + " | " + new Course(Session.CourseID).CourseName.ToString();
            LblDateTime.Text = Session.Date.ToString("dddd, dd MMMM yyyy", new CultureInfo("ES")) + " - "+Session.Time.ToString();
            LblStatus.Text = Session.Status.ToString();

            //Iterates in a foreach loop, for every people enrolled in the session 
            //it creates a new table row
            foreach (Enrollment enrolled in EnrolledEmployees) {
                DivTableBody.InnerHtml += $@" 
                               <tr>
                                    <td>{enrolled.Employee.EmployeeNumber}</td>
                                    <td>{enrolled.Employee.PaternalSurname} {enrolled.Employee.MateralSurnames}, {enrolled.Employee.Names}</td>
                                    <td>{enrolled.EnrollmentDate}</td>
                                    <td>
                                        <a id=""{enrolled.Employee.EmployeeNumber}"" name=""unenrollButtons"" class=""glyphicon glyphicon-trash"" style=""color:red;"" onclick=""unenroll(this.id)""></a>
                                        <a id=""{enrolled.Employee.EmployeeNumber}details"" name=""detailsEmpButton"" class=""glyphicon glyphicon-info-sign"" style=""color:blue;"" href=""EnrolledEmployee.aspx?EmployeeID={enrolled.Employee.EmployeeNumber}"" target=""_blank""></a>
                                    </td>


                                </tr>";
            }
        }

        private Session RetriveSessionData(string SessionID) {
            Session session = new Session(SessionID);
            session.GetSessionInfo();
            return session;
        }

        protected void btnBackPage_Click(object sender, EventArgs e)
        {
            string CourseID = Session["CourseID"].ToString();
            string DestinyUrl = "./MenuSessions.aspx?CourseID=" + CourseID;
            Response.Redirect(DestinyUrl);
        }

        protected void btnAttendance_Click(object sender, EventArgs e)
        {
            Response.Redirect("./SessionAttendance.aspx?SessionID=" + LblSession.Text);
        }

        [WebMethod]
        public static string unEnrollFromSessAjax(string idEmploy)
        {
            Sessions thisPage= new Sessions();
            string whatHappened=thisPage.unEnrollFromSess(idEmploy);
            return whatHappened;
        }

        protected string unEnrollFromSess(string idEmp)
        {
            string result=null;
            try
            {
                string SqlconString = ConfigurationManager.ConnectionStrings["db"].ToString();
                string query = "update RT_Inscripciones set Vigente = 0 where CB_CODIGO = '" + idEmp + "' and SE_FOLIO = '" + sess_id + "'";

                using (SqlConnection conn = new SqlConnection(SqlconString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    cmd.ExecuteNonQuery();

                    conn.Close();
                }

                result = "Empleado "+idEmp+" dado de baja con exito";
            }
            catch
            {
                result = "No fue posible completar la operacion";
            }
           
            return result;
        }
    }
}