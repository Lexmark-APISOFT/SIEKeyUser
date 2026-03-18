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
    public partial class Reembolso_escolar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                String MyVarNom = Session["nombre"].ToString();
                String MyVarNum = Session["numero"].ToString();

                nombre.Text = MyVarNom;
                getReembolso();
               
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }
        }
        public void getReembolso()
        {
            String MyVarNum = Session["numero"].ToString();

            var codigo = TextBox1.Text;
            var res = DbUtil.GetCursor("sp_verReembolsoPendKey",
                new SqlParameter("@codigo", codigo));

            GridView1.DataSource = res;
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("MenuKey.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            getReembolso();
        }
        protected void des_buscar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(500);
            getReembolso();

            TextBox1.Focus();
            mensaje.Text = "";
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            try
                {
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    int rowIndex = gvr.RowIndex;

                    var a = GridView1.Rows[rowIndex].Cells[2].Text;
                    var f = GridView1.Rows[rowIndex].Cells[3].Text;
                    mensaje.Text = a + ' '+ f;

                    Session.Add("numreloj", a);
                    Session.Add("escu", f);
                    Response.Redirect("Reembolso_escolar_detalle");
                   
                   }
                catch
                {
                }
        }

        protected void Rechazados_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Reembolso_rechazados.aspx");

        }

        protected void Aceptados_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Reembolso_aprobados.aspx");

        }

        protected void PorYPro_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Porcentajes_promedios.aspx");
        }

        protected void PeryEsc_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Periodos_escuelas.aspx");
        }

    }
}