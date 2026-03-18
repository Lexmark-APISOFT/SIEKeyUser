using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using MsBarco;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using System.Web.Services;
using Microsoft.Ajax.Utilities;
using System.Data;
using System.Configuration;
using Microsoft.Office.Interop.Word;
using System.Web.Providers.Entities;
using System.IO;
using System.IO.Ports;
using System.Security.Cryptography;
using static DotNetOpenAuth.OpenId.Extensions.AttributeExchange.WellKnownAttributes.Contact;

namespace SIE_KEY_USER.Views
{

    public partial class WebForm1 : System.Web.UI.Page
    {
        public string folios { get; set; }
        public int currPage { get; set; }
        public string filePath { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {

                if (!IsPostBack)
                {
                    String nombreEmpleado = Session["nombre"].ToString();
                    lblNombreEmpleado.Text = nombreEmpleado;
                    String MyvarNum = Session["numero"].ToString();
                    fsFolios.Visible = false;

                    getSolicitudesVac();


                    GridViewRow headerRow = gv_CheckList.HeaderRow;
                    System.Web.UI.WebControls.CheckBox chkAll = (System.Web.UI.WebControls.CheckBox)headerRow.FindControl("chkSelectAll") as System.Web.UI.WebControls.CheckBox;
                    chkAll.Checked = true;

                    foreach (GridViewRow row in gv_CheckList.Rows)
                    {
                        System.Web.UI.WebControls.CheckBox chkRow = (System.Web.UI.WebControls.CheckBox)row.FindControl("chkb_status") as System.Web.UI.WebControls.CheckBox;
                        chkRow.Checked = true;
                    }

                }

            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }
        }

        protected void gv_CheckList_PageIndexChanging(object sender, EventArgs e)
        {
            currPage = gv_CheckList.PageIndex;
        }

        [WebMethod]
        public int currPageIndex()
        {
            return currPage;
        }

        protected void getSolicitudesVac()
        {
            var res2 = DbUtil.GetCursor("sp_s_solicitud_vac_keyus");
            gv_CheckList.DataSource = res2;
            gv_CheckList.DataBind();
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

        protected void gv_CheckList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_CheckList.PageIndex = e.NewPageIndex; // Set the new page index
            getSolicitudesVac();                                         // Your code to bind the data to the GridView
        }

        protected void backButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuKey.aspx");
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
        public void Aceptar_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gv_CheckList.Rows)
            {

                if (row.RowType == DataControlRowType.DataRow)
                {
                    System.Web.UI.WebControls.CheckBox chkRow = (System.Web.UI.WebControls.CheckBox)row.FindControl("chkb_status") as System.Web.UI.WebControls.CheckBox;

                    if (chkRow.Checked)
                    {
                        Label lblIdSol = (Label)row.FindControl("hfIdSolVac") as Label;
                        var id_sol_vac = lblIdSol.Text.ToString();

                        var res = DbUtil.ExecuteProc("sp_u_solicitud_vacaciones",
                        new SqlParameter("@id_sol_vac", id_sol_vac),
                        new SqlParameter("@aceptado", '1'),
                        new SqlParameter("@rechazado", '0'),
                        new SqlParameter("@pendiente", '0'));

                        //getSolicitudesVac();
                    }

                }
            }

            Response.Redirect("solicitud_vacaciones.aspx");
        }

        public void Rechazar_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gv_CheckList.Rows)
            {

                if (row.RowType == DataControlRowType.DataRow)
                {
                    System.Web.UI.WebControls.CheckBox chkRow = (System.Web.UI.WebControls.CheckBox)row.FindControl("chkb_status") as System.Web.UI.WebControls.CheckBox;

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
                        //getSolicitudesVac();
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

        protected void openFSlayout(object sender, EventArgs e)
        {
            fsFolios.Visible = true;

            main.Style.Add("z-index", "-1");
            main.Style.Add("filter","alpha(opacity = 50)");
            main.Style.Add("opacity", "0.5");


        }

        [WebMethod]
        public static void acceptScanned(string folio)
        {

            string SqlconString = ConfigurationManager.ConnectionStrings["db"].ToString();
            using (SqlConnection conn = new SqlConnection(SqlconString))
            using (SqlCommand cmd = new SqlCommand("sp_u_solicitud_vacaciones", conn))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_sol_vac", SqlDbType.Int);
                cmd.Parameters.Add("@aceptado", SqlDbType.Int);
                cmd.Parameters.Add("@rechazado", SqlDbType.Int);
                cmd.Parameters.Add("@pendiente", SqlDbType.Int);

                conn.Open();

                int solicitud_num;

                bool parsed = int.TryParse(folio, out solicitud_num);

                if (parsed)
                {

                    cmd.Parameters["@id_sol_vac"].Value = solicitud_num;
                    cmd.Parameters["@aceptado"].Value = 1;
                    cmd.Parameters["@rechazado"].Value = 0;
                    cmd.Parameters["@pendiente"].Value = 0;
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }


        [WebMethod]
        public static string generarArchivo(string folios)
        {
            string[] foliosS = folios.Split(',');
            string filePath=generarArchivo_Click(foliosS);

            return filePath;
        }
        protected static string generarArchivo_Click(string[] folios)
        {
            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition",
            // "attachment;filename=ArchivoVacacionesAceptadas.csv");
            //Response.Charset = "";
            //Response.ContentType = "application/text";
            var rowsCSV = new List<string>();

            string SqlconString = ConfigurationManager.ConnectionStrings["db"].ToString();
            using (SqlConnection conn = new SqlConnection(SqlconString))
            using (SqlCommand cmd = new SqlCommand("sp_fastScan", conn))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_sol_vac", SqlDbType.Int);
                cmd.Parameters.Add("@accepted", SqlDbType.Int);
                cmd.Parameters.Add("@rowCSV", SqlDbType.NVarChar,500).Direction = ParameterDirection.Output;
                conn.Open();

                int solicitud_num;

                for (int i = 0; i < folios.Length; i++)
                {
                    
                    bool parsed = int.TryParse(folios[i], out solicitud_num);

                    if (parsed){ 

                        cmd.Parameters["@id_sol_vac"].Value = solicitud_num;
                        cmd.Parameters["@accepted"].Value = 1;
                        cmd.ExecuteNonQuery();
                        rowsCSV.Add(Convert.ToString(cmd.Parameters["@rowCSV"].Value));
                    
                    }

                    //var res = DbUtil.ExecuteProc("sp_fastScan_Acceptation",
                    //new SqlParameter("@id_sol_vac", solicitud_num));

                }
                conn.Close();
            }

            StringBuilder sb = new StringBuilder();

            //sacar los headers de la tabla
            sb.Append("# Reloj,Fecha inicio,Fecha final,Turno,Duracion,ID solicitud,Periodo de nómina,Fecha inicio-final,Fecha de pago");
            sb.Append("\r\n");
            //sacar los contenidos de las columnas donde esta ese folio
            for (int j = 0; j < rowsCSV.Count; j++)
            {
                //add separator
                sb.Append(rowsCSV[j]);
                sb.Append("\r\n");
            }

            string pathCSV = @"\\mxjrzapp11\Encryption\work\" + "vacaciones" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm") + ".csv";
            string pathCSVpruebas= "C:\\Users\\dmendozarodr\\Documents\\SIE Y KEYUSER\\" + "vacaciones" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm") + ".csv";

            System.IO.File.WriteAllText(pathCSV, sb.ToString());


            return pathCSV;

        }

        [WebMethod]
        public static string downloadCSV(string path)
        {

            //Read the File as Byte Array.
            byte[] bytes = File.ReadAllBytes(path);

            //Convert File to Base64 string and send to Client.
            return Convert.ToBase64String(bytes, 0, bytes.Length);

        }


        [WebMethod]
        public static void enviarSolicitudesPen(string folios)
        {
            string[] foliosS = folios.Split(',');
            enviarSolicitudesPen_Click(foliosS);
        }
        protected static void enviarSolicitudesPen_Click(string[] folios)
        {
            for(int i = 0; i < folios.Length; i++)
            {
                    var res = DbUtil.ExecuteProc("sp_u_solicitud_vacaciones",
                  new SqlParameter("@id_sol_vac", folios[i]),
                  new SqlParameter("@aceptado", '0'),
                  new SqlParameter("@rechazado", '0'),
                  new SqlParameter("@pendiente", '1'));
            }
        }
    }
}