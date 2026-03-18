using MsBarco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIE_KEY_USER.Views
{
    public partial class Cursos_regulatorios_modificar_periodo : System.Web.UI.Page
    {

        protected static DateTime finInscripciones;
        protected static DateTime inicioCursos;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                if (!IsPostBack)
                {
                    Session["RefUrlPeriodo"] = Request.UrlReferrer.ToString();
                    getFechas();
                }
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("Default.aspx");
            }
        }
        protected void getFechas()
        {

            var Fechas = DbUtil.GetCursor("sp_get_periodo");

            dpInicioInscripciones.Text = Fechas.Rows[0].ItemArray.GetValue(2).ToString();

            dpFinInscripciones.Value = Fechas.Rows[0].ItemArray.GetValue(3).ToString();
            finInscripciones = DateTime.Parse(Fechas.Rows[0].ItemArray.GetValue(3).ToString());

            dpInicioCursos.Value = Fechas.Rows[1].ItemArray.GetValue(2).ToString();
            inicioCursos = DateTime.Parse(Fechas.Rows[1].ItemArray.GetValue(2).ToString());

            dpFinCursos.Value = Fechas.Rows[1].ItemArray.GetValue(3).ToString();
        }

        protected void actualizar_fechas(int id, String fecha_inicio, String fecha_cierre){
            var res = DbUtil.ExecuteProc("sp_actualizar_periodo",
                new System.Data.SqlClient.SqlParameter("@fecha_inicio", fecha_inicio),
                new System.Data.SqlClient.SqlParameter("@fecha_cierre", fecha_cierre),
                new System.Data.SqlClient.SqlParameter("@id", id)
                );
        }

        protected void btnModificarInscripciones_Click(object sender, EventArgs e)
        {
            finInscripciones = DateTime.Parse(dpFinInscripciones.Value);

            if (finInscripciones <= inicioCursos)
            {
                actualizar_fechas(1, dpInicioInscripciones.Text, dpFinInscripciones.Value);
                lblMensaje.Attributes.Add("class", "alert alert-success");
                lblMensaje.Text = "El periodo de inscripciones ha sido actualizado";
            }
            else {
                lblMensaje.Attributes.Add("class", "alert alert-danger");
                lblMensaje.Text = "El cierre de inscripciones tiene que ser antes del inicio de cursos";
            }
        }

        protected void btnModificarCursos_Click(object sender, EventArgs e)
        {
            inicioCursos = DateTime.Parse(dpInicioCursos.Value);

            if (inicioCursos >= finInscripciones)
            {
                actualizar_fechas(2, dpInicioCursos.Value, dpFinCursos.Value);
                lblMensaje.Attributes.Add("class", "alert alert-success");
                lblMensaje.Text = "El periodo de cursos ha sido actualizado";
            }
            else {
                lblMensaje.Attributes.Add("class", "alert alert-danger");
                lblMensaje.Text = "El inicio de cursos tiene que ser despues del cierre de inscripciones";
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            
            Uri myUri = new Uri(Session["RefUrlPeriodo"].ToString(), UriKind.Absolute);
            if (myUri.AbsolutePath != "Views/Cursos_regulatorios_modificar_periodo.aspx")
            {
                object refUrl = Session["RefUrlPeriodo"];
                Response.Redirect(refUrl.ToString());

            }
            else
            {
                Response.Redirect(".\\MenuCourses.aspx");
            }

        }
    }
}