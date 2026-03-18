using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using MsBarco;
using System.Data.Entity;
using System.Runtime.InteropServices;
using System.Configuration;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace SIE_KEY_USER.Views
{
    public partial class matriz_disponibilidadant : System.Web.UI.Page
    {

        static GridView reposUndo { get; set; }
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
                    /*
                    foreach (GridViewRow row in gv_CheckList.Rows)
                    {
                        CheckBox chkRow = (CheckBox)row.FindControl("chkb_status") as CheckBox;
                        //chkRow.Checked = true;
                        chkRow.Checked = false;
                    }
                    */
                }

            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }
        }

        //protected void getSolicitudesVac()
        //{
        //    var res2 = DbUtil.GetCursor("sp_s_allDisponibilidad_vac");
        //    gv_CheckList.DataSource = res2;
        //    gv_CheckList.DataBind();
        //}

        protected void getSolicitudesVac()
        {
            
            string SqlconString = ConfigurationManager.ConnectionStrings["db"].ToString();
            using (SqlConnection conn = new SqlConnection(SqlconString))
            using (SqlDataAdapter cmd = new SqlDataAdapter("select * from [SIE].[dbo].[m_disponibilidad_vac_puesto]", conn))
            {

                conn.Open();

                DataTable dataTable = new DataTable();

                cmd.Fill(dataTable);

                gv_CheckList.DataSource = dataTable;
                gv_CheckList.DataBind();

                conn.Close();

            }

        }


        /*protected void Button1_Click(object sender, EventArgs e)
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
        }*/
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_CheckList.PageIndex = e.NewPageIndex;
            getSolicitudesVac();
        }

        /* protected void Aceptar_Clicks(object sender, EventArgs e)
         {
             foreach (GridViewRow row in gv_CheckList.Rows)
             {

                 if (row.RowType == DataControlRowType.DataRow)
                 {
                     CheckBox chkRow = (CheckBox)row.FindControl("chkb_status") as CheckBox;

                     if (chkRow.Checked)
                     {
                        // if (txtdisponibilidad.Text == String.Empty)
                        // {
                        //     getSolicitudesVac();
                        // }
                        // else
                        // {
                           //  System.Threading.Thread.Sleep(500);
                             String disponibilidad = txtdisponibilidad.Text;

                             Label lblIdSol = (Label)row.FindControl("hfIdSolVac") as Label;
                             var id_disp = lblIdSol.Text.ToString();

                         var res = DbUtil.ExecuteProc("sp_u_cantidad_disp",
                         new SqlParameter("@id_disp", id_disp),
                         new SqlParameter("@cantidad_disp", disponibilidad),
                         MsBarco.DbUtil.NewSqlParam("@res", null, SqlDbType.VarChar, ParameterDirection.Output, 10));

                             int resultado = Int32.Parse(res["@res"].ToString());
                             if (resultado == 1)
                             {
                                 Info.Text = "El proceso se completo correctamente.";
                             }
                             else if(resultado == 0)
                             {
                                 Info.Text = "La disponibilidad insertada sobrepasa el límite de disponibilidad disponible. Intente con disponibilidad dentro del rango";
                             }

                             getSolicitudesVac();

                        // }
                        // getSolicitudesVac();
                     }

                 }
             }
         }*/

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("solicitud_vacacionesRemastered.aspx");

        }

        protected void gv_CheckList_PreRender(object sender, EventArgs e)
        {
            var myGrid = sender as GridView;
            if (myGrid.Rows.Count > 0)
            {
                myGrid.UseAccessibleHeader = true;

                myGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
                myGrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }


        protected void undoChanges(object sender, EventArgs e)
        {
            if (reposUndo != null)
            {
                int cantidad;
                int idrow = 0;


                foreach (GridViewRow row in reposUndo.Rows)
                {
                    idrow++;
                    if (row.RowType == DataControlRowType.DataRow)
                    {

                        cantidad = int.Parse(row.Cells[9].Text.Trim());

                        string SqlconString = ConfigurationManager.ConnectionStrings["db"].ToString();
                        using (SqlConnection conn = new SqlConnection(SqlconString))
                        using (SqlCommand cmd = new SqlCommand("sp_undoChangesTablaDisponibilidad", conn))
                        {


                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@id_row", idrow);
                            cmd.Parameters.AddWithValue("@prev_disp", cantidad);


                            conn.Open();
                            cmd.ExecuteNonQuery();

                            conn.Close();

                        }
                    }
                }
                //getSolicitudesVac();
                Response.Redirect("matriz_disponibilidad.aspx");
            }
        }

        protected void changeAllSolicitudesVac(object sender, EventArgs e)
        {

            int resultado = 0;
            string cantidad = String.Concat(txtdisponibilidad.Text.Where(c => !Char.IsWhiteSpace(c)));

            if (cantidad == null || cantidad == "" || !int.TryParse(txtdisponibilidad.Text, out int n))
            {
                Info.Text = "Debe ingresar una cantidad numerica positiva";
            }
            else
            {
                reposUndo = gv_CheckList;
                int disponibilidad = Int32.Parse(cantidad);
                string SqlconString = ConfigurationManager.ConnectionStrings["db"].ToString();
                using (SqlConnection conn = new SqlConnection(SqlconString))
                using (SqlCommand cmd = new SqlCommand("dbo.sp_u_cantidad_disp", conn))
                {


                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@all", 1);
                    cmd.Parameters.AddWithValue("@cantidad_disp", disponibilidad);
                    cmd.Parameters.Add("@res", SqlDbType.NVarChar, 10).Direction = ParameterDirection.Output;


                    conn.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToInt32(cmd.Parameters["@res"].Value);

                    conn.Close();

                }

                txtdisponibilidad.Text = null;

                if (resultado == 1)
                {
                    Info.Text = "El proceso se completo correctamente.";
                }
                else if (resultado == 0)
                {
                    Info.Text = "Hubo un error al intentar actualizar";
                }
                //getSolicitudesVac();
                Response.Redirect("matriz_disponibilidad.aspx");


            }



        }
        protected void Dispo_click(object sender, EventArgs e)
        {
            int resultado = 0;
            string cantidad= String.Concat(txtdisponibilidad.Text.Where(c => !Char.IsWhiteSpace(c)));

            if (cantidad == null || cantidad == "" || !int.TryParse(txtdisponibilidad.Text, out int n))
            {
                Info.Text = "Debe ingresar una cantidad numerica positiva";
            }
            else
            {
                reposUndo = gv_CheckList;
                int disponibilidad = Int32.Parse(cantidad);
                string SqlconString = ConfigurationManager.ConnectionStrings["db"].ToString();
                using (SqlConnection conn = new SqlConnection(SqlconString))
                using (SqlCommand cmd = new SqlCommand("dbo.sp_u_cantidad_disp", conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@all", 0);
                    cmd.Parameters.AddWithValue("@id_disp", 0);
                    cmd.Parameters.AddWithValue("@cantidad_disp", disponibilidad);
                    cmd.Parameters.Add("@res", SqlDbType.NVarChar, 10).Direction = ParameterDirection.Output;

                    foreach (GridViewRow row in gv_CheckList.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {

                            CheckBox chkRow = (CheckBox)row.FindControl("chkb_status") as CheckBox;
                            if (chkRow.Checked)
                            {
                                Label lblIdSol = (Label)row.FindControl("hfIdSolVac") as Label;
                                var id_disp = lblIdSol.Text.ToString();

                                cmd.Parameters["@id_disp"].Value = id_disp;
                                
                                cmd.ExecuteNonQuery();

                                resultado = Convert.ToInt32(cmd.Parameters["@res"].Value);


                            }
                        }
                    }
                   
                    conn.Close();


                    txtdisponibilidad.Text = null;
                    if (resultado == 1)
                    {
                        Info.Text = "El proceso se completo correctamente.";
                    }
                    else if (resultado == 0)
                    {
                        Info.Text = "Hubo un error al intentar actualizar";
                    }
                    //getSolicitudesVac();
                    Response.Redirect("matriz_disponibilidad.aspx");
                }
            }
        }
    }
}

