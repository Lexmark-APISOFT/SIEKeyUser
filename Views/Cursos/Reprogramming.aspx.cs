using SIE_KEY_USER.model.Courses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIE_KEY_USER.model;
using Microsoft.Office.Interop.Excel;
using System.Threading;

namespace SIE_KEY_USER.Cursos
{
    public partial class Reprogramming : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                if (!IsPostBack)
                {
                    ViewState["RefUrlReprogr"] = Request.UrlReferrer.ToString();
                    GetReprogrammings();
                }
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("../../Views/Default.aspx");
            }
        }
        public void GetReprogrammings()
        {
            string SqlconString = ConfigurationManager.ConnectionStrings["dbCur"].ToString();
            string Query = $@"SELECT 
	                            ID AS 'ID',
	                            CB_CODIGO AS 'No.Reloj',
	                            SE_FOLIO AS 'Nuevo Folio',
	                            SE_FOLIO_PREVIO AS 'Folio Previo',
	                            CU_CODIGO AS 'Curso',
	                            RE_FECHA AS 'Fecha Solicitud',
                                RE_RAZON AS 'Razon'
                            FROM SIE.dbo.RT_Reprogramaciones RTRE
                            WHERE status_aprobado = 'Pendiente' and YEAR(RE_FECHA) = YEAR(GETDATE())
                            ORDER BY RE_FECHA";
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlconString))
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    SqlCommand command = new SqlCommand(Query, conn);
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    GvRepprogrammings.DataSource = dataSet;
                    GvRepprogrammings.DataBind();
                    conn.Close();
                }
            }
            catch (Exception)
            {

            }

            if (GvRepprogrammings.Rows.Count < 1)
            {
                btnAcceptReprogramming.Visible = false;
                btnRejectReprogramming.Visible=false;
                NoSolLabel.Visible = true;
            }
            else
            {
                btnAcceptReprogramming.Visible = true;
                btnRejectReprogramming.Visible = true;
            }

        }

        protected void btnAcceptReprogramming_Click(object sender, EventArgs e)
        {

            //[sp_Admin_Reprogramming_Request_Response]
            //List<string> SelectedRowIDs = new List<string>();
            Employee getReprogrammingMethod = new Employee();
            string resultOperation=null;

            foreach (GridViewRow row in GvRepprogrammings.Rows)
            {

                if (((System.Web.UI.WebControls.CheckBox)row.FindControl("seleccionar")).Checked)
                {
                    //SelectedRowIDs.Add(row.Cells[1].Text);//gets the id of the selected rows, ID(first column)
                    try
                    {
                        //id,cb_codigo,se_folio,se_folio_previo,cu_codigo,re_fecha,re_razon
                        resultOperation = getReprogrammingMethod.reprogramEmployee(row.Cells[3].Text, row.Cells[2].Text, row.Cells[4].Text, row.Cells[5].Text,1,0);

                        Response.Write("<script>alert('" + row.Cells[1].Text + " - "+ resultOperation +".');</script>");

                        Thread.Sleep(1000);

                    }
                    catch
                    {
                        Response.Write("<script>alert('No fue posible generar la respuesta a la solicitud " + row.Cells[1].Text + ".');</script>");
                        continue;
                    }
                }
            }

            GetReprogrammings();
        }

        protected void btnRejectReprogramming_Click(object sender, EventArgs e)
        {
            Employee getReprogrammingMethod = new Employee();
            string resultOperation = null;
            //List<string> SelectedRowIDs = new List<string>();
            foreach (GridViewRow row in GvRepprogrammings.Rows)
            {
                if (((System.Web.UI.WebControls.CheckBox)row.FindControl("seleccionar")).Checked)
                {
                    //SelectedRowIDs.Add(row.Cells[1].Text);//gets the id of the selected rows, ID(first column)


                    //[sp_Admin_Reprogramming_Request_Response]
                    //string query = $@"
                    //                    UPDATE SIE.dbo.RT_Reprogramaciones
                    //                 SET  status_aprobado = 'Aprobado'
                    //                    WHERE ID IN ({string.Join(",", SelectedRowIDs)})";

                    //string SqlconString = ConfigurationManager.ConnectionStrings["dbCur"].ToString();

                    try
                    {
                        //id,cb_codigo,se_folio,se_folio_previo,cu_codigo,re_fecha,re_razon
                        resultOperation = getReprogrammingMethod.reprogramEmployee(row.Cells[3].Text, row.Cells[2].Text, row.Cells[4].Text, row.Cells[5].Text, 2, 0);

                        Response.Write("<script>alert('" + row.Cells[1].Text + " - " + resultOperation + ".');</script>");

                        Thread.Sleep(1000);

                    }
                    catch
                    {
                        Response.Write("<script>alert('No fue posible generar la respuesta a la solicitud " + row.Cells[1].Text + ".');</script>");
                        continue;
                    }

                    //try
                    //{
                    //    string SqlconString = ConfigurationManager.ConnectionStrings["db"].ToString();
                    //    using (SqlConnection conn = new SqlConnection(SqlconString))
                    //    using (SqlCommand cmd = new SqlCommand("sp_Admin_Reprogramming_Request_Response", conn))
                    //    {

                    //        cmd.CommandType = CommandType.StoredProcedure;

                    //        //@ID_sol varchar(10),
                    //        //@NEW_FOLIO nvarchar(10),
                    //        //@PREV_FOlIO varchar(10),
                    //        //@CB_CODIGO_EMP int,
                    //        //@CU_CODIGO_CURR nvarchar(10),
                    //        //@tinyOperation int

                    //        cmd.Parameters.Add("@ID_sol", SqlDbType.VarChar, 10);
                    //        cmd.Parameters.Add("@NEW_FOLIO", SqlDbType.NVarChar, 10);
                    //        cmd.Parameters.Add("@PREV_FOlIO", SqlDbType.NVarChar, 10);
                    //        cmd.Parameters.Add("@CB_CODIGO_EMP", SqlDbType.Int);
                    //        cmd.Parameters.Add("@CU_CODIGO_CURR", SqlDbType.NVarChar, 10);
                    //        cmd.Parameters.Add("@tinyOperation", SqlDbType.Int);

                    //        cmd.Parameters["@ID_sol"].Value = row.Cells[1].Text;
                    //        cmd.Parameters["@CB_CODIGO_EMP"].Value = int.Parse(row.Cells[2].Text);
                    //        cmd.Parameters["@NEW_FOLIO"].Value = row.Cells[3].Text;
                    //        cmd.Parameters["@PREV_FOlIO"].Value = row.Cells[4].Text;
                    //        cmd.Parameters["@CU_CODIGO_CURR"].Value = row.Cells[5].Text;
                    //        cmd.Parameters["@tinyOperation"].Value = 2;

                    //        conn.Open();

                    //        //cmd.Parameters["@se_folio"].Value = ;
                    //        //cmd.Parameters["@cb_codigo"].Value = int.Parse();

                    //        cmd.ExecuteNonQuery();


                    //        conn.Close();
                    //    }


                    //    Response.Write("<script>alert('Se ha rechazado la solicitud " + row.Cells[1].Text + ".');</script>");


                    //    Thread.Sleep(1000);

                    //    //using (SqlConnection conn = new SqlConnection(SqlconString))
                    //    //using (SqlCommand cmd = new SqlCommand(query, conn))
                    //    //{
                    //    //    SqlCommand command = new SqlCommand(query, conn);
                    //    //    command.Parameters.Add("@SessionID", SqlDbType.VarChar);
                    //    //    //command.Parameters["@SessionID"].Value = SessionID;
                    //    //    conn.Open();
                    //    //    using (var reader = cmd.ExecuteReader())
                    //    //    {

                    //    //        // Execute the update query
                    //    //        int rowsAffected = cmd.ExecuteNonQuery();

                    //    //        if (rowsAffected > 0)
                    //    //        {
                    //    //            Console.WriteLine($"Update successful. {rowsAffected} rows affected.");
                    //    //        }
                    //    //        else
                    //    //        {
                    //    //            Console.WriteLine("No rows were updated.");
                    //    //        }
                    //    //        conn.Close();
                    //    //    }
                    //    //}
                    //}
                    //catch
                    //{
                    //    Response.Write("<script>alert('No fue posible registrar la respuesta a solicitud " + row.Cells[1].Text + ".');</script>");
                    //    continue;
                    //}


                }
            }

            GetReprogrammings();

        }

        protected void btnBackPage_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrlReprogr"];
            Response.Redirect(refUrl.ToString());
        }
    }
}