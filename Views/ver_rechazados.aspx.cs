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
    public partial class ver_rechazados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                if (!IsPostBack)
                {
                    String MyVarNom = Session["nombre"].ToString();
                    String MyVarNum = Session["numero"].ToString();

                    nombre.Text = MyVarNom;
                    TextBox1.Focus();

                    getVerrechazados_keyuser();
                }
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;

            var res = DbUtil.GetCursor("sp_verrechazados_Keyuser",
                    new SqlParameter("@codigo", codigos.Text)
                    );

            GridView1.DataSource = res;
            GridView1.DataBind();
            //getVerrechazados_keyuser();

            TextBox1.Focus();
        }

        public void getVerrechazados_keyuser()
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                var res = DbUtil.GetCursor("sp_verrechazados_Keyuser",
                    new SqlParameter("@codigo", TextBox1.Text)
                    );

                GridView1.DataSource = res;
                GridView1.DataBind();
            }
        }

        protected void des_buscar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);

            getVerrechazados_keyuser();

            var res = DbUtil.ExecuteProc("sp_buscarrechazados_Keyuser",
                    new SqlParameter("@codigo", TextBox1.Text),
                    MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                    MsBarco.DbUtil.NewSqlParam("@codigos", null, SqlDbType.VarChar, ParameterDirection.Output, 40)
                    );

            nombre_codigo.Text = res["@nombre"].ToString();
            codigos.Text = res["@codigos"].ToString();

            TextBox1.Text = "";
            TextBox1.Focus();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Agregar_familiares.aspx");
        }

        protected void btn_del_Click(object sender, EventArgs e)
        {
            bool userCheckbox = false;

            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("seleccionar") as CheckBox);

                    if (chkRow.Checked)
                    {
                        string numeros = row.Cells[1].Text;
                        string fechas = row.Cells[2].Text;

                        var res = DbUtil.ExecuteProc("sp_DelFamRech_KU",
                           new SqlParameter("@codigo", numeros),
                           new SqlParameter("@fecha", fechas)
                           );

                        userCheckbox = true;
                    }
                }
            }

            getVerrechazados_keyuser();

            if (userCheckbox == true)
            {
                mensaje.Text = "Familiares eliminados.";
            }
            else
            {
                mensaje.Text = "No hay familiares seleccionados.";
            }
        }

        protected void btn_modif_Click(object sender, EventArgs e)
        {
            bool userCheckbox = false;

            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("seleccionar") as CheckBox);

                    if (chkRow.Checked)
                    {
                        string numeros = row.Cells[1].Text;
                        string fechas = row.Cells[2].Text;

                        var res = DbUtil.ExecuteProc("sp_FamRechBack_KU",
                           new SqlParameter("@codigo", numeros),
                           new SqlParameter("@fecha", fechas)
                           );

                        userCheckbox = true;
                    }
                }
            }

            getVerrechazados_keyuser();

            if (userCheckbox == true)
            {
                mensaje.Text = "Registros regresados.";
            }
            else
            {
                mensaje.Text = "No hay registros seleccionados.";
            }
        }
    }
}