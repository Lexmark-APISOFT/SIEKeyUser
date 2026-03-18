using System;
using System.IO;
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
    public partial class actualizar_clinica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                String MyVarNom = Session["nombre"].ToString();

                nombre.Text = MyVarNom;

                GetClinica();
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("Default.aspx");
            }
        }

        public void GetClinica()
        {
            var res = DbUtil.GetCursor("sp_verGenerarClinica");

            GridView1.DataSource = res;
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Actualizacion_datos.aspx");
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView1.HeaderRow.Cells[3].CssClass = "boundAgreg";
                e.Row.Cells[3].CssClass = "boundAgreg";
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            TextWriter clinica = new StreamWriter(Server.MapPath(@"~\Virtual\archivos\actualizar_datos\clinica.txt"));

            GridView1.AllowPaging = false;
            GridView1.DataBind();

            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    DateTime dt = DateTime.Parse(row.Cells[3].Text.ToString());
                    string cl = row.Cells[1].Text.ToString();

                    clinica.WriteLine(row.Cells[0].Text.ToString() + ",10,\"" + dt.ToString("dd/MM/yyyy") + "\",\"CB_CLINICA\"," + cl.Replace(" ", string.Empty) + ",\"\"," + "\"Act clínica del IMSS\"");

                    var codigo = row.Cells[0].Text.ToString();
                    var fecha = row.Cells[3].Text.ToString();

                    var res = DbUtil.ExecuteProc("sp_UpdateClinica",
                        new SqlParameter("@codigo", codigo),
                        new SqlParameter("@fecha", fecha)
                        );
                }
            }

            clinica.Close();

            GridView1.AllowPaging = true;
            GridView1.DataBind();

            Response.Redirect("confirmar_datos.aspx");
        }
    }
}