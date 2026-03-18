using MsBarco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIE_KEY_USER.Views
{
    public partial class Cursos_regulatorios_sesiones_cursos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                if (!IsPostBack)
                {
                    getSesiones(HttpContext.Current.Session["cu_codigo"].ToString());
                }
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("Default.aspx");
            }
        }
        private void getSesiones(String curso)
        {
            string prevFolio = HttpContext.Current.Session["se_folio_previo"].ToString();
            string cb_codigo = Session["codigo_empleado"].ToString();
            var res = DbUtil.GetCursor("sp_get_sesiones",
                new System.Data.SqlClient.SqlParameter("@cb_codigo", cb_codigo),
                new System.Data.SqlClient.SqlParameter("@cu_codigo", curso),
                new System.Data.SqlClient.SqlParameter("@se_folio_previo", prevFolio)
                );
            gvSesiones.DataSource = res;
            gvSesiones.DataBind();

        }

        protected void gvSesiones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                //Determine the RowIndex of the Row whose LinkButton was clicked.
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvSesiones.Rows[rowIndex];

                string folio = row.Cells[1].Text;

                Session.Add("se_folio", folio);
                Response.Redirect("Cursos_regulatorios_elegir_sesion.aspx");
            }
        }

        protected void gvSesiones_DataBound(object sender, EventArgs e)
        {
            int cupo;
            string prevFolio = HttpContext.Current.Session["se_folio_previo"].ToString();
            foreach (GridViewRow row in gvSesiones.Rows)
            {
                LinkButton lkbSeleccionar = (LinkButton)row.FindControl("");
                cupo = int.Parse(row.Cells[5].Text);

                if (cupo <= 0)
                {
                    row.Cells[0].Text = "";
                    row.Cells[5].Text = "0";
                }
                else {
                    lkbSeleccionar = (LinkButton)row.FindControl("lkbSeleccionar");
                    lkbSeleccionar.Text = "Seleccionar";
                }


                if (row.Cells[6].Text == "Fuera de Horario")
                {
                    row.Visible ^= true;
                }
            }
        }
        [WebMethod]
        protected void btnSwitch_Click(object sender, EventArgs e)
        {
            if (btnSwitch.Text == "Cursos dentro de Horario")
            {
                btnSwitch.Text = "Cursos fuera de Horario";
            }
            else {
                btnSwitch.Text = "Cursos dentro de Horario";
            }

            foreach (GridViewRow row in gvSesiones.Rows)
            {
                if (row.Cells[6].Text == "Fuera de Horario")
                {
                    row.Visible ^= true;
                }
                else
                {
                    row.Visible ^= true;
                }
            }
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cursos_regulatorios.aspx");
        }
    }
}