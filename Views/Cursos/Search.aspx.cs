using SIE_KEY_USER.model.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIE_KEY_USER.Views.Cursos
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                if (!IsPostBack)
                { 

                    if(Request.UrlReferrer.AbsolutePath.ToString() != "/Views/Cursos/EnrolledEmployee.aspx")
                    {
                        Session["RefUrlSearch"] = Request.UrlReferrer.ToString();
                    }
                }
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("../../Views/Default.aspx");
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect($"./EnrolledEmployee.aspx?EmployeeID={txtID.Text}");
        }

        protected void btnBackPage_Click(object sender, EventArgs e)
        {

            Uri myUri = new Uri(Session["RefUrlSearch"].ToString(), UriKind.Absolute);
            if (myUri.AbsolutePath != "Views/Cursos/Search.aspx")
            {
                object refUrl = Session["RefUrlSearch"];
                Response.Redirect(refUrl.ToString());

            }
            else
            {
                Response.Redirect(".\\MenuCourses.aspx");
            }
        }
    }
}