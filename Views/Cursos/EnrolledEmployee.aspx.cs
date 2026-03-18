using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using SIE_KEY_USER.Cursos;
using SIE_KEY_USER.model;
using SIE_KEY_USER.model.Courses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.IO;
using System.Web.Providers.Entities;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Word;

namespace SIE_KEY_USER.Views.Cursos
{
    public partial class EnrolledEmployee : System.Web.UI.Page
    {
        public static string fromPage { get; set; }
        public static string requestedEmployee { get; set; }

        public static Employee enrolledEmp { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                if (!IsPostBack)
                {
                    //fromPage = previousPage();



                    //ViewState["RefUrlEnrolledEmployee"] = Request.UrlReferrer.AbsolutePath.ToString();

                    string backPage = Request.UrlReferrer.AbsolutePath.ToString();
                    

                    if (backPage == "/Views/Cursos/Sessions")
                    {
                        btnBackPage.Visible=false;
                    }

                    requestedEmployee = Request.QueryString["EmployeeID"] ?? "";
                    
                    enrolledEmp = int.TryParse(requestedEmployee, out var parsedValue) ? new Employee(parsedValue) : null;
                    if (enrolledEmp == null ) {
                        Response.Redirect("Sessions.aspx");
                    }
                    

                }
                DivCorrespondingCourses.InnerHtml = "";
                ActiveSesionsContainer.InnerHtml = "";
                RenderEnrolledInfo(enrolledEmp);
                RenderReprogrammings(enrolledEmp);

            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("../../Views/Default.aspx");
            }

        }


        public void RenderReprogrammings(Employee employee)
        {
            try
            {
                ReprogrammingTable.InnerHtml = "";
                foreach (string reprogramming in employee.GetReprogramming())
                {
                    string[] array = reprogramming.Split(',');
                    ReprogrammingTable.InnerHtml += $@"
                    <tr>
                        <td>{array[0]}</td>
                        <td>{array[1]}</td>
                        <td>{array[2]}</td>
                        <td>{array[3]}</td>
                        <td>{array[4]}</td>
                        <td>{array[5]}</td>
                        <td>{array[6]}</td>
                    </tr>
                ";
                }
            }
            catch
            {
                ReprogrammingTable.InnerHtml = "<h3>No hay reprogramaciones</h3>";
            }

        }

        //protected string previousPage(){
        //    string prevPage= Request.UrlReferrer.AbsoluteUri;

        //    return prevPage;
        //}

        protected void goBack(object sender, EventArgs e)
        {
            Response.Redirect(".\\Search.aspx");
        }

        protected void RenderEnrolledInfo(Employee employee)
        {
            TxtEmployeeNo.Text = employee.EmployeeNumber.ToString() ?? "";
            TxtNames.Text = employee.Names ?? "";
            TxtLastName.Text = employee.PaternalSurname + " " + employee.MateralSurnames ?? "";
            TxtPosition.Text = employee.Position ?? "";
            TxtLastPosition.Text = employee.LastPositionTime ?? "";
            try
            {
                employee.getOfEmployeeEnrollments();
                foreach (Enrollment enrollment in employee.Enrollments)
                {

                    string courseIdPhoto = enrollment.Session.CourseID.Replace('/', '-');
                    string ImgPath;
                    if (File.Exists(Server.MapPath("~\\Images\\imgsCourses\\img\\" + courseIdPhoto + ".PNG")))
                    {
                        ImgPath = "../../Images/imgsCourses/img/" + courseIdPhoto + ".PNG";
                    }
                    else
                    {
                        ImgPath = "../../Images/imgsCourses/img/no.PNG";

                    }

                    ActiveSesionsContainer.InnerHtml += $@"
                    <div class=""col-md-3"">
                        <div class=""thumbnail"" style=""overflow: hidden; height: 380px;"">
                            <img src=""{ImgPath}"" />
                            <div class=""caption"">
                                <h3 style=""white-space: nowrap; overflow: hidden; text-overflow: ellipsis;"" title="" - {new Course(enrollment.Session.CourseID).CourseName}"">{enrollment.Session.CourseID} - {new Course(enrollment.Session.CourseID).CourseName}</h3>
                                <p>{enrollment.Session.SessionID.ToString()} | {enrollment.Session.Date.ToString("dddd, dd MMMM yyyy", new CultureInfo("ES"))} - {enrollment.Session.Time}</p>
                                <p><button id=""{enrollment.Session.SessionID}-{enrollment.Session.CourseID}"" name=""botonReprogramar"" type=""button"" class=""btn btn-success"" data-toggle=""modal"" data-target=""#exampleModal"" role=""button"" onclick=""loadSessionsPendingCourse(this.id,this.name)"">Reprogramar</button> <button type=""button"" name=""botonBaja""  class=""btn btn-danger"" role=""button""  data-toggle=""modal"" data-target=""#exampleModal"" ID=""{enrollment.Session.SessionID}"" onclick=""unenrollEmployeePop(this.id,this.name)"">Baja</button></p>
                            </div>
                        </div>
                    </div>
                    ";
                }
            }
            catch
            {
                ActiveSesionsContainer.InnerHtml = "<h3>No hay sesiones activas</h3>";
            }
            foreach (Course course in employee.GetMandatoryCourses()) {
                if (!(Enrollment.IsEnrolledToCourse(employee.EmployeeNumber.ToString(), course.CourseID)))
                {
                    DivCorrespondingCourses.InnerHtml += $@"<div>
                                <label style=""font-size: 18px; font-family: 'Century Gothic'; font-weight: bold;"">{course.CourseID}</label>
                                <button id=""{course.CourseID}"" name=""botonInscribir"" type=""button"" class=""btn btn-default glyphicon glyphicon-list-alt"" role=""button""  data-toggle=""modal"" data-target=""#exampleModal"" onclick=""loadSessionsPendingCourse(this.id,this.name)""></button>
                            </div>";

                }
            }

        }


        [WebMethod]
        public static string UnenrollEmployee(string SessionID) {
            string result = "";
            //SIE_KEY_USER.model.Courses.Session session = new SIE_KEY_USER.model.Courses.Session(SessionID);
            //session.GetSessionInfo();

            //result = $@"<h4>Sesion: {SessionID}</h4>
            //                <p>Curso: {session.CourseID}</p>
            //                <p>Nombre del curso: {new Course(session.CourseID).CourseName}</p>
            //                <p>Lugar: {session.Place}</p>
            //                <p>Fecha y Hora: {session.Date.ToString("dddd, dd MMMM yyyy", new CultureInfo("ES"))} - {session.Time}</p>";
            string cb_EMP= enrolledEmp.EmployeeNumber.ToString();

            result=enrolledEmp.unEnrollEmployee(SessionID,cb_EMP);

            return result;

        }

        
        [WebMethod]
        public static string AvailableSession(string CourseID) {
            string result = "";
            List<SIE_KEY_USER.model.Courses.Session> AvailableSessions = Course.GetAvailableSessionsByCourse(CourseID,requestedEmployee);
            result = JsonConvert.SerializeObject(AvailableSessions);
            //EnrolledEmployee enrolledEmployee = new EnrolledEmployee();
            //enrolledEmployee.fillModalOfSessions(AvailableSessions, enrolledEmployee);
            return result;
        }

        [WebMethod]
        public static string btnAcceptEnnroll(string id_sessn)
        {
            EnrolledEmployee enrolledEmployee = new EnrolledEmployee();
            string inscripctionResult=enrolledEmployee.enrollEmployee(id_sessn);

            return inscripctionResult;
        }
        protected string enrollEmployee(string idSession)
        {
           
            string result= enrolledEmp.SuscribeToCourse(idSession, requestedEmployee);

            return result;
        }


        [WebMethod]
        public static string btnAcceptReprogramming(string newSess, string prevSess, string courseCod)
        {
            EnrolledEmployee enrolledEmployee = new EnrolledEmployee();
            string inscripctionResult = enrolledEmployee.reprogramEmployee(newSess, prevSess, courseCod, 1);

            return inscripctionResult;
        }
        protected string reprogramEmployee(string newIdSession, string previousSess, string courseId, int response)
        {

            string result = enrolledEmp.reprogramEmployee(newIdSession, requestedEmployee, previousSess, courseId, response, 1);

            return result;

        }

    }
}