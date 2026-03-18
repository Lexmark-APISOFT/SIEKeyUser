using MsBarco;
using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace SIE_KEY_USER.Views
{
    public partial class Cursos_regulatorios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                if (!IsPostBack)
                {
                    getReprogramaciones();
                }
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("Default.aspx");
            }
        }

        protected void getReprogramaciones() {
            var res = DbUtil.GetCursor("sp_get_reprogramaciones");
            gvReprogramaciones.DataSource = res;
            gvReprogramaciones.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cursos_regulatorios_programacion_manual");
        }
        protected void btnAceptar_Click(object sender, EventArgs e) {
            foreach (GridViewRow row in gvReprogramaciones.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkSeleccionar") as CheckBox);

                    if (chkRow.Checked)
                    {
                        string row_id = row.Cells[1].Text;
                        var res = DbUtil.ExecuteProc("sp_regulatorios_actualizar_status",
                                       new SqlParameter("@id", row_id),
                                       new SqlParameter("@Aprobado", "Aprobado"));
                    }
                }
            }
            getReprogramaciones();
        }
        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvReprogramaciones.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkSeleccionar") as CheckBox);

                    if (chkRow.Checked)
                    {
                        string row_id = row.Cells[1].Text;
                        var res = DbUtil.ExecuteProc("sp_regulatorios_actualizar_status",
                                       new SqlParameter("@id", row_id),
                                       new SqlParameter("@Aprobado", "No autorizado"));
                    }
                }
            }
            getReprogramaciones();
        }
        protected void gvReprogramaciones_DataBound(object sender, EventArgs e)
        {
            
        }

        protected void gvReprogramaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                gvReprogramaciones.HeaderRow.Cells[1].CssClass = "boundAgreg";
                e.Row.Cells[1].CssClass = "boundAgreg";
            }
        }

        protected void btnSesion_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cursos_regulatorios_detalles_sesion.aspx");
        }

        protected void btnRegresar_Click(object sender, EventArgs e) {
            Response.Redirect("MenuKey.aspx");
        }
        protected void btnPeriodo_Click(object sender, EventArgs e) {
            Response.Redirect("Cursos_regulatorios_modificar_periodo.aspx");
        }

        protected void btnPendientes_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cursos_regulatorios_pendientes.aspx");
        }

        protected void btnReprogramaciones_Click(object sender, EventArgs e)
        {
            Response.Redirect("listaReprogramaciones.aspx");
        }
    }
}
       