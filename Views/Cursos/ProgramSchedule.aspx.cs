using MsBarco;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Word;
using SIE_KEY_USER.model.Courses;
using System.Web.Providers.Entities;
using Microsoft.Vbe.Interop;

namespace SIE_KEY_USER.Views
{
    public class EventOfTheDay
    {
        public string SessionID { get; set; }
        public string CourseID { get; set; }
        public string CourseName { get; set; }
        public string Date { get; set; }
        public string HourToHour { get; set; }
        public string Room { get; set; }
    }
    public partial class ProgramSchedule : System.Web.UI.Page
    {
        public static string fechaI_inscripciones{get;set;}
        public static string fechaCierre_inscripciones{get;set;}
        public static string fechaI_Cursos{get;set;}
        public static string fechaCierre_Cursos{get;set;}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                Calendar1.DayStyle.HorizontalAlign = HorizontalAlign.Right;
                Calendar1.DayStyle.VerticalAlign = VerticalAlign.Top;
                Calendar1.DayStyle.Height = new Unit(100);
                if (!IsPostBack)
                {
                    ViewState["RefUrlCalendar"] = Request.UrlReferrer.ToString();

                    getFechas();
                }
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("..\\Default.aspx");
            }
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            HtmlGenericControl div1 = new HtmlGenericControl("div");
            div1.Attributes["class"] = "divEventsInsideCalendarCell";
            e.Cell.Controls.Add(div1);


            if ( e.Day.Date < Convert.ToDateTime(fechaI_inscripciones) || e.Day.Date > Convert.ToDateTime(fechaCierre_Cursos) )
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.Gray;
                e.Cell.Enabled = false;
            }
            else if ( e.Day.Date >= Convert.ToDateTime(fechaI_inscripciones) && e.Day.Date <= Convert.ToDateTime(fechaCierre_inscripciones) )
            {
                e.Cell.BackColor = System.Drawing.Color.CadetBlue;
                e.Day.IsSelectable = true;

                if (e.Day.Date == Convert.ToDateTime(fechaI_Cursos) || e.Day.Date == Convert.ToDateTime(fechaCierre_Cursos))
                {
                    HtmlGenericControl divInscriptions = new HtmlGenericControl("div");
                    divInscriptions.Attributes["class"] = "divInscriptionsInsideCell";
                    div1.Controls.Add(divInscriptions);
                }
                else if (e.Day.Date > Convert.ToDateTime(fechaI_Cursos) && e.Day.Date < Convert.ToDateTime(fechaCierre_Cursos))
                {
                    HtmlGenericControl divCourses = new HtmlGenericControl("div");
                    divCourses.Attributes["class"] = "divCoursesInsideCell";
                    div1.Controls.Add(divCourses);
                }

                

            }
            else if ((e.Day.Date == Convert.ToDateTime(fechaI_Cursos) && (e.Day.Date != Convert.ToDateTime(fechaI_inscripciones) && e.Day.Date != Convert.ToDateTime(fechaCierre_inscripciones)))
                || (e.Day.Date == Convert.ToDateTime(fechaCierre_Cursos) && (e.Day.Date != Convert.ToDateTime(fechaI_inscripciones) && e.Day.Date != Convert.ToDateTime(fechaCierre_inscripciones))))
            {
                e.Cell.BackColor = System.Drawing.Color.Coral;
                e.Day.IsSelectable = true;
            }
            else if (e.Day.Date > Convert.ToDateTime(fechaI_Cursos) && e.Day.Date < Convert.ToDateTime(fechaCierre_Cursos))
            {
                e.Cell.BackColor = System.Drawing.Color.FromArgb(0, 255, 241, 174);  //rgba(255, 241, 174, 0.8)
                e.Day.IsSelectable = true;
            }

        }

        protected void getFechas()
        {

            var Fechas = DbUtil.GetCursor("sp_get_periodo");

            fechaI_inscripciones = Fechas.Rows[0].ItemArray.GetValue(2).ToString();

            fechaCierre_inscripciones = Fechas.Rows[0].ItemArray.GetValue(3).ToString();
            //fechaI_Cursos = DateTime.Parse(Fechas.Rows[0].ItemArray.GetValue(3).ToString());

            fechaI_Cursos = Fechas.Rows[1].ItemArray.GetValue(2).ToString();
            //inicioCursos = DateTime.Parse(Fechas.Rows[1].ItemArray.GetValue(2).ToString());

            fechaCierre_Cursos = Fechas.Rows[1].ItemArray.GetValue(3).ToString();
        }

        protected void getEvents(object sender, EventArgs e) 
        {
            var calendar = (Calendar)sender;
            var daySelected = calendar.SelectedDate.Date;
            

            List<EventOfTheDay> eventsOfTheDay = new List<EventOfTheDay>();

            if (daySelected.ToString("yyyy-MM-dd") == fechaI_inscripciones)
            {

                EventOfTheDay evEnt = new EventOfTheDay()
                {
                    SessionID = null,
                    CourseID = null,
                    CourseName = "Inicio de inscripciones",
                    Date = DateTime.Parse(fechaI_inscripciones).ToString("dd-MM-yyyy"),
                    HourToHour = DateTime.Parse(fechaI_inscripciones).ToString("hh:mm tt"),
                    Room = null
                };
                eventsOfTheDay.Add(evEnt);
            }
            else if (daySelected > DateTime.Parse(fechaI_inscripciones) && daySelected < DateTime.Parse(fechaCierre_inscripciones))
            {

                EventOfTheDay evEnt = new EventOfTheDay()
                {
                    SessionID = null,
                    CourseID = null,
                    CourseName = "Periodo de nscripciones",
                    Date = DateTime.Parse(fechaI_inscripciones).ToString("dd-MM-yyyy") + " - " +DateTime.Parse(fechaCierre_inscripciones).ToString("dd-MM-yyyy"),
                    HourToHour =null,
                    Room = null
                };
                eventsOfTheDay.Add(evEnt);
                
            }

            if (daySelected.ToString("yyyy-MM-dd") == fechaCierre_inscripciones)
            {
                EventOfTheDay evEnt = new EventOfTheDay()
                {
                    SessionID = null,
                    CourseID = null,
                    CourseName = "Cierre de inscripciones",
                    Date = DateTime.Parse(fechaCierre_inscripciones).ToString("dd-MM-yyyy"),
                    HourToHour = DateTime.Parse(fechaCierre_inscripciones).ToString("hh:mm tt"),
                    Room = null
                };
                eventsOfTheDay.Add(evEnt);
            }
            if (daySelected.ToString("yyyy-MM-dd") == fechaI_Cursos)
            {
                EventOfTheDay evEnt = new EventOfTheDay()
                {
                    SessionID = null,
                    CourseID = null,
                    CourseName = "Inicio de cursos",
                    Date = DateTime.Parse(fechaI_Cursos).ToString("dd-MM-yyyy"),
                    HourToHour = DateTime.Parse(fechaI_Cursos).ToString("hh:mm tt"),
                    Room = null
                };
                eventsOfTheDay.Add(evEnt);
            }
            if (daySelected.ToString("yyyy-MM-dd") == fechaCierre_Cursos)
            {
                EventOfTheDay evEnt = new EventOfTheDay()
                {
                    SessionID = null,
                    CourseID = null,
                    CourseName = "Finalizacion de cursos",
                    Date = DateTime.Parse(fechaCierre_Cursos).ToString("dd-MM-yyyy"),
                    HourToHour = DateTime.Parse(fechaCierre_Cursos).ToString("hh:mm tt"),
                    Room = null
                };
                eventsOfTheDay.Add(evEnt);
            }

            string SqlconString = ConfigurationManager.ConnectionStrings["db"].ToString();
            string query = "select SE_FOLIO, ss.CU_CODIGO, cr.CU_NOMBRE, SE_FEC_INI, SE_HOR_INI, SE_HOR_FIN, SE_LUGAR from CommonDB.dbo.Sesion ss left " +
                "join CommonDB.dbo.Cursos_Regulatorios cr on ss.CU_CODIGO = cr.CU_CODIGO where SE_FEC_INI = '"+daySelected.ToString("yyyy-MM-dd") + "' and(cr.CU_CODIGO like 'NSM%' or cr.CU_CODIGO like 'REG%')";

            using (SqlConnection conn = new SqlConnection(SqlconString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var hrIni = transformHours(reader["SE_HOR_INI"].ToString());
                        var hrFin = transformHours(reader["SE_HOR_FIN"].ToString());
                        var fecha = reader["SE_FEC_INI"].ToString();
                        EventOfTheDay evEnt = new EventOfTheDay()
                        {
                            SessionID = reader["SE_FOLIO"].ToString(),
                            CourseID = reader["CU_CODIGO"].ToString().Trim(),
                            CourseName = reader["CU_NOMBRE"].ToString(),
                            Date = DateTime.Parse(fecha).ToString("dd-MM-yyyy"),
                            HourToHour = hrIni + " - " + hrFin,
                            Room = reader["SE_LUGAR"].ToString()
                        };
                        eventsOfTheDay.Add(evEnt);
                    }
                }

                conn.Close();

            }

            showPop(eventsOfTheDay, calendar.SelectedDate.Date.ToString("dd-MM-yyyy"));

        }
        protected string transformHours(string hrToTransform)
        {

            if (hrToTransform == null)
            {
                hrToTransform  = "--:---";
            }
            else
            {
                hrToTransform = hrToTransform.Substring(0, (hrToTransform.Length - 2)) + ':' + hrToTransform.Substring((hrToTransform.Length - 2), hrToTransform.Length - (hrToTransform.Length - 2));
                hrToTransform = DateTime.Parse(hrToTransform).ToString("hh:mm tt");
            }

            return hrToTransform;
        }

        protected void showPop(List<EventOfTheDay> eventsEnlisted, string dateInQuestion)
        {
            addEventsToPop(eventsEnlisted, dateInQuestion);
            popup_window.Visible = true;
        }

        protected void addEventsToPop(List<EventOfTheDay> eventS, string interestedDate)
        {
            titleDay.InnerText = "Lista de Eventos "+interestedDate;
            for (int i = 0; i<eventS.Count;i++)
            {
                //< div class="divEachEventDetails">
                var divEachDtls = new HtmlGenericControl();
                divEachDtls.Attributes["class"] = "divEachEventDetails";
                detailsP.Controls.Add(divEachDtls);
                //<p class="popupDetailsPs" style="font-weight:600" id="courseSess" runat="server"></p
                var eventTitle = new HtmlGenericControl("p");
                if (eventS[i].SessionID == null && eventS[i].CourseID == null)
                {
                    eventTitle.InnerText = eventS[i].CourseName;
                }
                else
                {
                    eventTitle.InnerText = eventS[i].CourseID + " - " + eventS[i].CourseName + "( Sesion: " + eventS[i].SessionID + ')';
                }
                eventTitle.Attributes["class"] = "popupDetailsPs";
                eventTitle.Attributes["style"] = "font-weight:600";
                eventTitle.ID = "courseSess";
                divEachDtls.Controls.Add(eventTitle);
                //<p class="popupDetailsPs" style="font-size:min(30em, 1.5vw);margin-left:10%;color:gray;font-style:italic" id="dateEvent" runat="server">- Hoy</p>
                var eventDetail1 = new HtmlGenericControl("p");
                eventDetail1.InnerText = "- " + eventS[i].Date;
                eventDetail1.Attributes["class"] = "popupDetailsPs";
                eventDetail1.Attributes["style"] = "font-size:min(30em, 1.5vw);margin-left:10%;color:gray;font-style:italic";
                eventDetail1.ID = "dateEvent";
                divEachDtls.Controls.Add(eventDetail1);

                //<p class="popupDetailsPs" style="font-size:min(30em, 1.5vw);margin-left:10%;color:gray;font-style:italic" id="hrTohr" runat="server">- 12:00 - 1:00</p>
                if (eventS[i].Room != null)
                {
                    var eventDetail2 = new HtmlGenericControl("p");
                    eventDetail2.InnerText = "- " + eventS[i].HourToHour;
                    eventDetail2.Attributes["class"] = "popupDetailsPs";
                    eventDetail2.Attributes["style"] = "font-size:min(30em, 1.5vw);margin-left:10%;color:gray;font-style:italic";
                    eventDetail2.ID = "dateEvent";
                    divEachDtls.Controls.Add(eventDetail2);
                }

                //<p class="popupDetailsPs" style="font-size:min(30em, 1.5vw);margin-left:10%;color:gray;font-style:italic" id="room" runat="server">- Sala 2</p>
                if (eventS[i].Room != null)
                {
                    var eventDetail3 = new HtmlGenericControl("p");
                    eventDetail3.InnerText = "- " + eventS[i].Room;
                    eventDetail3.Attributes["class"] = "popupDetailsPs";
                    eventDetail3.Attributes["style"] = "font-size:min(30em, 1.5vw);margin-left:10%;color:gray;font-style:italic";
                    eventDetail3.ID = "dateEvent";
                    divEachDtls.Controls.Add(eventDetail3);
                }

            }


        }

        protected void closePop(object sender, EventArgs e)
        {
            detailsP.Controls.Clear();
            Calendar1.SelectedDates.Clear();
            popup_window.Visible = false;
        }

        protected void btnBackPage_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrlCalendar"];
            Response.Redirect(refUrl.ToString());
        }
    }
}