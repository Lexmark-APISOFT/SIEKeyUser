using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using SIE_KEY_USER.model.Courses;
using SIE_KEY_USER.Views.Cursos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static DotNetOpenAuth.OpenId.Extensions.AttributeExchange.WellKnownAttributes.Contact;

namespace SIE_KEY_USER.Cursos
{

    public partial class Cursos : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                //if (!IsPostBack)
                //{ 
                    CoursesCardContainer.Controls.Clear();
                    List<Course> courses = RetrieveAllCourses();
                    RenderCourses(courses);

                //}

            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("../../Views/Default.aspx");
            }

        }

        protected void RenderCourses(List<Course> courses)
        {

            foreach (Course course in courses)
            {
                string courseIdPhoto = course.CourseID.Replace('/', '-');
                string ImgPath;
                if (File.Exists(Server.MapPath("~\\Images\\imgsCourses\\img\\" + courseIdPhoto + ".PNG")))
                {
                    ImgPath = "../../Images/imgsCourses/img/" + courseIdPhoto + ".PNG";
                }
                else
                {
                    ImgPath = "../../Images/imgsCourses/img/no.PNG";

                }
                //CoursesCardContainer.InnerHtml += $@"
                //    <div class=""col-md-2"">
                var div1 = new HtmlGenericControl("div");
                div1.Attributes["class"] = "col-md-2";
                CoursesCardContainer.Controls.Add(div1);
                //        <div class=""thumbnail thumbnails"" style=""overflow: scroll; height: 310px;"">
                var div11=new HtmlGenericControl("div");
                div11.Attributes["class"]= "thumbnail thumbnails";
                div11.Attributes["style"]= "overflow: scroll; height: 310px;";
                div1.Controls.Add(div11);
                //            <img src=""{ImgPath}"" />
                var img1 = new HtmlGenericControl("img");
                img1.Attributes["src"]= ImgPath;
                img1.Attributes["style"] = "height:50%";
                div11.Controls.Add(img1);
                //            <div class=""caption"">
                var div111= new HtmlGenericControl("div");
                div111.Attributes["style"] = "height:50%;max-width:100%;display: flex;flex-wrap: wrap;";
                div11.Controls.Add(div111);
                //               <div class=""caption"">
                var div1111 = new HtmlGenericControl("div");
                div1111.Attributes["class"] = "thumbnailsCaptions";
                div111.Controls.Add(div1111);
                //                  <h3 style=""white-space: nowrap; overflow: hidden; text-overflow: ellipsis;"" title=""{course.CourseID} -  {course.CourseName}"">{course.CourseID} - {course.CourseName}</h3>
                var hdr1 = new HtmlGenericControl("h3") { InnerText= course.CourseID + '-' + course.CourseName};
                hdr1.Attributes["style"] = "min-width: 100%;max-height: 100%;text-overflow: ellipsis;white-space: nowrap;font-size:large;";
                hdr1.Attributes["title"] = course.CourseID + '-' + course.CourseName;
                div1111.Controls.Add(hdr1);
                //                <asp:LinkButton runat=""server"" PostBackUrl=""MenuSessions.aspx?CourseID={course.CourseID}"" CssClass=""btn btn-success btnSessions"" Text=""Sesiones""></asp:LinkButton>
                var lkb1 = new LinkButton();
                lkb1.Attributes["runat"] = "server";
                lkb1.PostBackUrl= "MenuSessions.aspx?CourseID="+ course.CourseID;
                lkb1.CssClass= "btn btn-success btnSessions";
                lkb1.Text = "Sesiones";
                div111.Controls.Add(lkb1);
                //                <asp:LinkButton ID=""{course.CourseID}"" runat=""server"" CssClass=""btn btn-success btnDetails"" Text=""Detalles""></asp:LinkButton>
                var lkb2 = new LinkButton();
                lkb2.ID = course.CourseID;
                //lkb2.OnClientClick = "return false;";
                lkb2.Attributes["runat"] = "server";
                lkb2.CssClass = "btn btn-success btnDetails";
                lkb2.Click += Details_Click;
                lkb2.Text = "Detalles";
                div111.Controls.Add(lkb2);
            }

        }

        //<a href=""MenuSessions.aspx?CourseID={course.CourseID}""  role=""button"">Sesiones</a> 

        public List<Course> RetrieveAllCourses()
        {
            string SqlconString = ConfigurationManager.ConnectionStrings["dbCur"].ToString();

            try
            {
                using (SqlConnection conn = new SqlConnection(SqlconString))
                using (SqlCommand cmd = new SqlCommand("SIE.dbo.sp_get_all_courses", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    //cmd.Parameters.Add(new SqlParameter("@CourseID", SqlDbType.NVarChar, 50));
                    //cmd.Parameters["@CourseID"].Value = prefix;

                    using (var reader = cmd.ExecuteReader())
                    {
                        List<Course> courses = new List<Course>();

                        while (reader.Read())
                        {
                            Course course = new Course(reader["CU_CODIGO"].ToString());

                            courses.Add(course);
                        }

                        conn.Close();
                        return courses;
                    }

                }
            }catch(Exception e)
            {
                return null;
            }
        }
        public void ConnectToDB()
        {
        }

        protected void btnBackPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../Views/MenuKey.aspx");
        }
        protected void Details_Click(object sender, EventArgs e)
        {
            LinkButton pressed = sender as LinkButton;
            string session = pressed.ID;
            List<string> cardDetails = new List<string>();
            bringDetails(session, cardDetails);
            popup_window.Visible = true;
            CoursesCardContainer.Style.Add("z-index", "-100");
            CoursesCardContainer.Style.Add("opacity", "0.5");
            fillDatailsCard(cardDetails);
        }

        protected List<string> bringDetails(string sess, List<string> details)
        {
            string SqlconString = ConfigurationManager.ConnectionStrings["db"].ToString();

            string query = "select CU_CODIGO, CU_NOMBRE, CU_CLASIFI, CU_CLASE, CU_FEC_REV from CommonDB.dbo.Cursos_Regulatorios where CU_CODIGO='" + sess.ToString()+"'";

            using (SqlConnection conn = new SqlConnection(SqlconString))

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        for (int j = 0; j < reader.FieldCount; j++)
                            details.Add(reader.GetValue(j).ToString());

                        //employees_supervised.Add(reader.GetValue(j).ToString());
                    }

                }
                conn.Close();
            }

            return details;
        }

        protected void fillDatailsCard(List<string> fillers)
        {
            string[] detailsFields = { "Codigo del curso: ", "Nombre del curso: ", "Clasificacion: ", "Clase de curso: "};


            //for (int i = 0; i < fillers.Count; i++)
            //{
            //    var paraDetail = new HtmlGenericControl("p") { InnerText = detailsFields[i] + fillers[i] };
            //    paraDetail.Attributes["class"] = "popupDetailsPs";
            //    detailsP.Controls.Add(paraDetail);

            //}
            CodigoCur.InnerText = detailsFields[0] + fillers[0];
            NombreCur.InnerText = detailsFields[1] + fillers[1];
            ClasifCur.InnerText = detailsFields[2] + fillers[2];
            ClaseCur.InnerText = detailsFields[3] + fillers[3];


        }

        protected void closeDatails_Click(object sender, EventArgs e)
        {
            popup_window.Visible = false;
            CoursesCardContainer.Style.Remove("z-index");
            CoursesCardContainer.Style.Remove("opacity");
        }

    }
}