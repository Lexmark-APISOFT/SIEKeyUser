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
using System.Web.Services;

namespace SIE_KEY_USER.Views
{
    public partial class matriz_disponibilidad : System.Web.UI.Page
    {
        static int buttonPressed { get; set; }
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

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_CheckList.PageIndex = e.NewPageIndex;
            getSolicitudesVac();
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

                myGrid.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }

        [WebMethod]
        public static void decideButton(int bPress)
        {
            buttonPressed = bPress;
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

        protected void changeCupos_Click(object sender, EventArgs e)
        {
            if (buttonPressed == 1)
            {
                btnApplySelection_Click();
            }
            else if (buttonPressed==2)
            {
                changeAllSolicitudesVac();
            }
        }

        protected void btnApplySelection_Click()
        {
            int resultado = 0;
            string cantidad = String.Concat(txtAvailabilityInput.Value.Where(c => !Char.IsWhiteSpace(c)));

            if (cantidad == null || cantidad == "" || !int.TryParse(txtAvailabilityInput.Value, out int n))
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


                    txtAvailabilityInput.Value = null;
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
        protected void changeAllSolicitudesVac()
        {

            int resultado = 0;
            string cantidad = String.Concat(txtAvailabilityInput.Value.Where(c => !Char.IsWhiteSpace(c)));

            if (cantidad == null || cantidad == "" || !int.TryParse(txtAvailabilityInput.Value, out int n))
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

                txtAvailabilityInput.Value = null;

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

