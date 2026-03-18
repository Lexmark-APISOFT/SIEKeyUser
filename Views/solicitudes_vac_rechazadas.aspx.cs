using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using MsBarco;

namespace SIE_KEY_USER.Views
{
    public partial class solicitudes_vac_rechazadas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {

                if (!IsPostBack)
                {
                    String nombreEmpleado = Session["nombre"].ToString();
                    lblNombreEmpleado.Text = nombreEmpleado;
                    String MyvarNum = Session["numero"].ToString();
                    getSolicitudesVac();
                }
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }
        }

        protected void getSolicitudesVac()
        {
            var res2 = DbUtil.GetCursor("sp_s_solicitud_AR_keyUs",
                new SqlParameter("@tipo_solicitudes", "rechazadas"));
            gv_CheckList.DataSource = res2;
            gv_CheckList.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                if (txtSearch.Text == String.Empty)
                {
                    getSolicitudesVac();
                }
                else
                {
                    System.Threading.Thread.Sleep(500);
                    String codigo = txtSearch.Text;
                    var res = DbUtil.GetCursor("sp_s_solicitudes_vac_AR_keyUs",
                       new SqlParameter("@codigo", codigo),
                       new SqlParameter("@tipo_solicitudes", "rechazadas"));
                    gv_CheckList.DataSource = res;
                    gv_CheckList.DataBind();
                    txtSearch.Focus();
                }

            }
        }

        public void enviarSolicitudesPen_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gv_CheckList.Rows)
            {

                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (CheckBox)row.FindControl("chkb_status") as CheckBox;

                    if (chkRow.Checked)
                    {
                        Label lblIdSol = (Label)row.FindControl("hfIdSolVac") as Label;
                        var id_sol_vac = lblIdSol.Text.ToString();

                            var res = DbUtil.ExecuteProc("sp_u_solicitud_vacaciones",
                        new SqlParameter("@id_sol_vac", id_sol_vac),
                        new SqlParameter("@aceptado", '0'),
                        new SqlParameter("@rechazado", '0'),
                        new SqlParameter("@pendiente", '1'));

                        getSolicitudesVac();
                    }

                }
            }
        }
        protected void HideSelectedRows_Click(object sender, EventArgs e)
        {

            foreach (GridViewRow row in gv_CheckList.Rows)
            {

                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (CheckBox)row.FindControl("chkb_status") as CheckBox;

                    if (chkRow.Checked)
                    {
                        Label lblIdSol = (Label)row.FindControl("hfIdSolVac") as Label;
                        var id_sol_vac = lblIdSol.Text.ToString();

                        var res = DbUtil.ExecuteProc("sp_hide_solicitud_vac",
                            new SqlParameter("@id_solicitud", id_sol_vac));
                        Response.Redirect("solicitudes_vac_rechazadas.aspx");
                    }

                }
            }

        }
      
        protected void btnRegresar_Click(object sender, EventArgs e)
        {

            Response.Redirect("solicitud_vacaciones.aspx");

        }

        protected void gv_CheckList_PreRender(object sender, EventArgs e)
        {
            var myGrid = sender as GridView;
            if (myGrid.Rows.Count > 0)
            {
                myGrid.UseAccessibleHeader = true;

                // This will add the <thead> and <tbody> elements
                myGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

    }
}
