using SIE_KEY_USER.model;
using SIE_KEY_USER.model.Courses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIE_KEY_USER.Views.Cursos
{
    public partial class MenuSessions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                if (!IsPostBack)
                {
                    string requestedCourse = Request.QueryString["CourseID"] ?? "";
                    Course course = new Course(requestedCourse);
                    Session["CourseID"] = course.CourseID;
                    if (course == null)
                    {
                    }
                    RenderSessions(course);
                }
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("../../Views/Default.aspx");
            }
        }

        public void RenderSessions(Course course) {
            DivSubtitleCourse.InnerHtml = $@"{course.CourseID}";
            foreach (Session session in course.Sessions) {
                session.GetSessionInfo();
                SessionCardContainer.InnerHtml += $@" <div class=""col-sm-3"">
                        <div class=""thumbnail"">
                            <div class=""caption"">
                                <h3>{session.SessionID}</h3>
                                <p>{session.Date.ToString("dddd, dd MMMM yyyy", new CultureInfo("ES"))} - {session.Time}</p>
                                <p>Lugar: {session.Place}</p>
                                <p><a href=""./Sessions?SessionID={session.SessionID}"" class=""btn btn-success"" role=""button"" onclick=""disablePage()"" >Detalles</a></p>
                            </div>
                        </div>
                    </div>";
            }

        }

        protected void btnBackPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("./MenuCourses.aspx");
        }
    }
}