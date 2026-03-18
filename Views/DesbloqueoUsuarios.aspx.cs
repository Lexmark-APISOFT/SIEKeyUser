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
    public partial class DesbloqueoUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                if (!IsPostBack)
                {
                    String MyVarNum = Session["numero"].ToString();
                    String MyVarNom = Session["nombre"].ToString();

                    TextBox1.Focus();

                    nombre.Text = MyVarNom;

                    GetBloqueados();
                }
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }
        }

        public void GetBloqueados()
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                String MyVarNum = Session["numero"].ToString();

                var res = DbUtil.GetCursor("sp_getBloqueados");

                GridView1.DataSource = res;
                GridView1.DataBind();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetBloqueados();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("MenuKey.aspx");
        }

        protected void des_buscar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                System.Threading.Thread.Sleep(500);

                String codigo = TextBox1.Text;

                var res = DbUtil.GetCursor("sp_buscarBloqueados",
                    new SqlParameter("@codigo", codigo)
                    );

                GridView1.DataSource = res;
                GridView1.DataBind();

                TextBox1.Focus();
                mensaje.Text = "";
            }
        }

        protected void des_desbloquear_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            bool userbloq = false;

            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("seleccionar") as CheckBox);

                    if (chkRow.Checked)
                    {
                        string numeros = row.Cells[1].Text;

                        var res = DbUtil.ExecuteProc("sp_desbloquearUsuarios",
                            new SqlParameter("@codigo", numeros)
                            );

                        userbloq = true;
                    }
                }
            }

            GetBloqueados();

            if (userbloq == true)
            {
                mensaje.Text = "Usuarios desbloqueados.";
            }
            else
            {
                mensaje.Text = "No hay usuarios seleccionados.";
            }
        }

        protected void des_reestablecer_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            bool userReestablecido = false;

            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("seleccionar") as CheckBox);

                    if (chkRow.Checked)
                    {
                        string numeros = row.Cells[1].Text;

                        var res = DbUtil.ExecuteProc("sp_usersRestablecer",
                            new SqlParameter("@codigo", numeros)
                            );

                        userReestablecido = true;
                    }
                }
            }

            GetBloqueados();

            if (userReestablecido == true)
            {
                mensaje.Text = "Contraseña restablecida exitosamente.";
            }
            else
            {
                mensaje.Text = "No hay usuarios seleccionados.";
            }
        }
    }
}