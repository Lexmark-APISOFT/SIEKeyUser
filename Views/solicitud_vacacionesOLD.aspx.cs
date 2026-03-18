using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using MsBarco;

namespace SIE_KEY_USER.Views
{
	public partial class solicitud_vacaciones : System.Web.UI.Page
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
                    
                    //foreach(GridViewRow row in gv_CheckList.Rows)
                    //{
                    //    CheckBox chkRow = (CheckBox)row.FindControl("chkb_status") as CheckBox;
                    //    chkRow.Checked = true;
                    //}

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
            var res2 = DbUtil.GetCursor("sp_s_solicitud_vac_keyUs");
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
                    var res = DbUtil.GetCursor("sp_s_solicitudes_vac",
                        new SqlParameter("@codigo", codigo));
                    gv_CheckList.DataSource = res;
                    gv_CheckList.DataBind();
                    txtSearch.Focus();
                }

            }
        }
        protected void gv_CheckList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_CheckList.PageIndex = e.NewPageIndex; // Set the new page index
            getSolicitudesVac();                                         // Your code to bind the data to the GridView
        }
        ////protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gv_CheckList.PageIndex = e.NewPageIndex;
        //    getSolicitudesVac();
        //}


        public void Aceptar_Click(object sender, EventArgs e)
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
                        new SqlParameter("@aceptado", '1'),
                        new SqlParameter("@rechazado", '0'),
                        new SqlParameter("@pendiente", '0'));

                        getSolicitudesVac();
                    }

                }
            }
        }

        public void Rechazar_Click(object sender, EventArgs e)
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

                        var numero = row.Cells[2].Text;
                        var duracion = row.Cells[5].Text;
                        //Test
                        Label hfIdSolVac = (Label)row.FindControl("hfIdSolVac");
                        //

                        var res = DbUtil.ExecuteProc("sp_u_solicitud_vacaciones",
                        new SqlParameter("@id_sol_vac", id_sol_vac),
                        new SqlParameter("@aceptado", '0'),
                        new SqlParameter("@rechazado", '1'),
                        new SqlParameter("@pendiente", '0'));
                        

                        var res2 = DbUtil.ExecuteProc("sp_u_solicitud_vac_KeyUser",
                        new SqlParameter("@codigo", numero),
                        new SqlParameter("@duracion", duracion));
                        getSolicitudesVac();
                        //test
                        //DbUtil.ExecuteProc("sp_liberar_disp",
                        //new SqlParameter("@id_sol_vac", hfIdSolVac.Text));
                        //
                    }
                

                }
                
            }
            Response.Redirect("solicitud_vacaciones.aspx");
        }

        protected void modificarDias_Click(object sender, EventArgs e)
        {
            Response.Redirect("actualizar_dias_vacaciones.aspx");
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuKey.aspx");

        }

        protected void verSolicitudesAceptadas_Click(object sender, EventArgs e)
        {
            Response.Redirect("solicitudes_vac_aceptadas.aspx");

        }
        protected void verSolicitudesRechazadas_Click(object sender, EventArgs e)
        {
            Response.Redirect("solicitudes_vac_rechazadas.aspx");

        }
        protected void modificarDisp_Click(object sender, EventArgs e)
        {
            Response.Redirect("matriz_disponibilidad.aspx");
        }

        protected void gv_CheckList_PreRender(object sender, EventArgs e)
        {
            var myGrid = sender as GridView;
            if (myGrid.Rows.Count > 0)
            {
                //myGrid.UseAccessibleHeader = true;

                // This will add the <thead> and <tbody> elements
                myGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    }
}