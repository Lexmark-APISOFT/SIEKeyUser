using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIE_KEY_USER.model.Courses;
using SIE_KEY_USER.model;
using System.Globalization;
using Microsoft.Ajax.Utilities;
using System.EnterpriseServices.Internal;
using System.Web.Services;
using System.Reflection;
using Microsoft.Office.Interop.Word;

namespace SIE_KEY_USER.Cursos
{
    public partial class Attendance : System.Web.UI.Page
    {
        public class haveToAssist
        {
            public string cb_emp { get; set; }
            public string se_folio { get; set; }
            public int attended { get; set; }

        }
        public static List<haveToAssist> takenList { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {

                if (!IsPostBack)
                {
                    ViewState["RefUrl"] = Request.UrlReferrer.ToString();

                    takenList = new List<haveToAssist>();
                    //retrieves the session ID from the URL params
                    string SessionInURL = Request.Params["SessionID"].ToString() != "" ? Request.Params["SessionID"].ToString() : "";
                    Session session = new Session(SessionInURL);
                    //RenderSessionInfo(session.SessionID);
                    RenderSessionInfo(session.SessionID);
                    //List<Enrollment> _enrolled=new List<Enrollment>();
                    List<Enrollment> _enrolled = session.GetEnrolledEmployees();
                    RenderAttendees(_enrolled, SessionInURL);  
                }

            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("../../Views/Default.aspx");
            }

        }

        public void RenderAttendees(List<Enrollment> attendees, string sessionID)
        {
            int index = 1;
            foreach (Enrollment enrolled in attendees)
            {
                tableAttendees.InnerHtml += $@"
                            <tr>
                                <th scope=""row"">{index++}</th>
                                <td>{enrolled.Employee.EmployeeNumber}</td>
                                <td>{enrolled.Employee.Names}</td>
                                <td>{enrolled.Employee.PaternalSurname} {enrolled.Employee.MateralSurnames}</td>
                                <td>
                                    <input type=""checkbox"" name=""verifyEnrolled"" value=""{enrolled.Employee.EmployeeNumber +","+ sessionID}"" />
                                </td>
                            </tr>
                    ";
            }
        }
        public void RenderSessionInfo(string SessionID)
        {
            Session session = new Session(SessionID);

            session.GetSessionInfo();

            SessionInfoContainer.InnerHtml += $@"
                    <div class=""col-md-4"">
                        <p style=""font-size: 15px; font-family: 'Century Gothic'; font-weight: lighter;"">Curso: {session.CourseID}</p>
                    </div>
                    <div class=""col-md-4"">
                        <p style=""font-size: 15px; font-family: 'Century Gothic'; font-weight: lighter;"">Sesion: {SessionID} - {session.Time}</p>
                    </div>
                    <div class=""col-md-3"">
                        <p style=""font-size: 15px; font-family: 'Century Gothic'; font-weight: lighter;"">Status: {session.Status}</p>
                    </div>

            ";
        }

        public List<Enrollment> GetAttendees(string SessionID) {
            Session session = new Session(SessionID);
            //List<Enrollment> attendees = new List<Enrollment>();
            List<Enrollment> attendees = session.GetEnrolledEmployees();
            return attendees;
        }

        [WebMethod]
        public static void takeList(string sessionList)
        {
            var list = sessionList.Split(';');

            foreach(string attendant in list)
            {
                var attendantStatus = attendant.Split(',');
                haveToAssist attdnt = new haveToAssist()
                {
                    cb_emp= attendantStatus[0],
                    se_folio= attendantStatus[1],
                    attended = int.Parse(attendantStatus[2])
                };
                takenList.Add(attdnt);
            }
        }

        protected void registerAttendence(object sender, EventArgs e)
        {
            string result = null;
            //sp_RegisterAssistance
            string SqlconString = ConfigurationManager.ConnectionStrings["db"].ToString();
            foreach (haveToAssist hTA in takenList)
            {               
                try
                {
                    using (SqlConnection conn = new SqlConnection(SqlconString))
                    using (SqlCommand cmd = new SqlCommand("sp_RegisterAssistance", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();

                        cmd.Parameters.Add(new SqlParameter("@COD_EMP", SqlDbType.Int,10));
                        cmd.Parameters.Add(new SqlParameter("@SE_FOLIO", SqlDbType.VarChar, 10));
                        cmd.Parameters.Add(new SqlParameter("@Attended", SqlDbType.TinyInt,5));
                        cmd.Parameters["@COD_EMP"].Value = int.Parse(hTA.cb_emp);
                        cmd.Parameters["@SE_FOLIO"].Value = hTA.se_folio;
                        cmd.Parameters["@Attended"].Value = hTA.attended;

                        using (var reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            result = reader[0].ToString();
                        }

                        if (result != "Exito")
                        {
                            break;
                        }

                        conn.Close();

                    }
                }
                catch (Exception err)
                {
                    continue;
                }
            }

            object refUrl = ViewState["RefUrl"];


            Response.Write("<script>alert('" + result + " ');window.location = '"+ refUrl + "';</script></script>");

        }
    }
}