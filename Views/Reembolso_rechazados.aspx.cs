using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using MsBarco;

namespace SIE_KEY_USER.Views
{
    public partial class Reembolso_rechazados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                String MyVarNom = Session["nombre"].ToString();
                String MyVarNum = Session["numero"].ToString();

                nombre.Text = MyVarNom;
                getReembolsoRechazados();

            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }
        }
        public void getReembolsoRechazados()
        {
            String MyVarNum = Session["numero"].ToString();

            var codigo = TextBox1.Text;
            var res = DbUtil.GetCursor("sp_verReembolsoR",
                new SqlParameter("@codigo", codigo));

            GridView1.DataSource = res;
            GridView1.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Reembolso_escolar.aspx");
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            getReembolsoRechazados();
        }
        protected void des_buscar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(500);
            getReembolsoRechazados();

            TextBox1.Focus();
            mensaje.Text = "";
        }
    }
}