using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using MsBarco;
using System.Text;
using Microsoft.Office.Interop.Excel;
using CheckBox = System.Web.UI.WebControls.CheckBox;
using Label = System.Web.UI.WebControls.Label;

namespace SIE_KEY_USER.Views
{
    public partial class solicitudes_vac_aceptadas : System.Web.UI.Page
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

                    foreach (GridViewRow row in gv_CheckList.Rows)
                    {
                        CheckBox chkRow = (CheckBox)row.FindControl("chkb_status") as CheckBox;
                        chkRow.Checked = true;
                    }
                }

            }
        }

        protected void getSolicitudesVac()
        {
            var res2 = DbUtil.GetCursor("sp_s_solicitud_AR_keyUs"
             ,new SqlParameter("@tipo_solicitudes", "aceptadas"));
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
                        new SqlParameter("@codigo", codigo)
                        ,new SqlParameter("@tipo_solicitudes", "aceptadas"));
                    gv_CheckList.DataSource = res;
                    gv_CheckList.DataBind();
                    txtSearch.Focus();
                }

            }
        }



        protected void generarArchivo_Click(object sender, EventArgs e)
        {
            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition",
            // "attachment;filename=ArchivoVacacionesAceptadas.csv");
            //Response.Charset = "";
            //Response.ContentType = "application/text";
            foreach (GridViewRow row in gv_CheckList.Rows)
            {
                CheckBox chkRow = (CheckBox)row.FindControl("chkb_status") as CheckBox;
                Label hfIdSolVac = (Label)row.FindControl("hfIdSolVac") as Label;

                var res = DbUtil.ExecuteProc("sp_u_solicitudesAceptadas",
                       new SqlParameter("@id_solicitud_vac", hfIdSolVac.Text));
            }

            StringBuilder sb = new StringBuilder();
            for (int k = 2; k < gv_CheckList.Columns.Count; k++)
            {
                //add separator
                sb.Append(gv_CheckList.Columns[k].HeaderText + ',');
            }
            //append new line
            sb.Append("\r\n");
            for (int i = 0; i < gv_CheckList.Rows.Count; i++)
            {
                for (int k = 2; k < gv_CheckList.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(gv_CheckList.Rows[i].Cells[k].Text + ',');
                }
                //append new line
                sb.Append("\r\n");
            }
            System.IO.File.WriteAllText(@"\\mxjrzapp11\Encryption\work\" + "vacaciones" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm") + ".csv", sb.ToString());
            //System.IO.File.WriteAllText(@"\\mxjrznas01\reports\SIE\" + "vacaciones" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm") + ".csv", sb.ToString());
            Response.Redirect("solicitudes_vac_aceptadas.aspx");
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