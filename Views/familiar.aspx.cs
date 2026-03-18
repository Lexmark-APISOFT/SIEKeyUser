using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SIE_KEY_USER.model;
using MsBarco;

namespace SIE_KEY_USER.Views
{
    public partial class familiar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                String MyVarNom = Session["nombre"].ToString();
                String MyVarNum = Session["numero"].ToString();

                nombre.Text = MyVarNom;
                TextBox1.Focus();

                VerNumero();
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }
        }

        public void VerNumero()
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                var a = TextBox1.Text;

                var res = DbUtil.GetCursor("sp_KeyUser_FamiliarNumero",
                    new SqlParameter("@codFam", a),
                    MsBarco.DbUtil.NewSqlParam("@nombre", null,SqlDbType.VarChar,ParameterDirection.Output, 15)
                    );

                GridView1.DataSource = res;
                GridView1.DataBind();

                var res1 = DbUtil.ExecuteProc("sp_KeyUser_FamiliarNumero",
                    new SqlParameter("@codFam", a),
                    MsBarco.DbUtil.NewSqlParam("@nombre", null,SqlDbType.VarChar,ParameterDirection.Output, 40)
                    );

                prettyName.Text = res1["@nombre"].ToString();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            VerNumero();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;

                if (e.CommandName == "select")
                {
                    var a = GridView1.Rows[rowIndex].Cells[1].Text;
                    Session.Add("FamCodigo", a);

                    Click.playSimpleSound();
                    Response.Redirect("Agregar_familiares.aspx");
                }
            }
            catch
            {

            }
        }

        protected void des_buscar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            VerNumero();

            TextBox1.Focus();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("MenuKey.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("ver_aceptados.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("ver_rechazados.aspx");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Agregar_familiares");
        }
    }
}