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
    public partial class ver_prestamos : System.Web.UI.Page
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

                    getPrestamo();

                    TextBox1.Focus();
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
            getPrestamo();
        }
        public void getPrestamo()
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                var codigo = TextBox1.Text;

                var res = DbUtil.GetCursor("sp_verprestamo",
                    new SqlParameter("@codigo", codigo)
                    );

                GridView1.DataSource = res;
                GridView1.DataBind();
            }
        }

        protected void aceptar_ac_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);

            bool quitaruser = false;

            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("seleccionar") as CheckBox);

                    if (chkRow.Checked)
                    {
                        string codigo = row.Cells[1].Text;
                        string fecha = row.Cells[5].Text;

                        var res = DbUtil.ExecuteProc("sp_Quitarprestamo",
                            new SqlParameter("@codigo", codigo),
                            new SqlParameter("@fec_sol", fecha)
                            );

                        quitaruser = true;
                    }
                }
            }

            getPrestamo();
            TextBox1.Focus();

            if (quitaruser == true)
            {
                mensaje.Text = "Las solicitudes fueron eliminadas exitosamente.";
            }
            else
            {
                mensaje.Text = "No hay solicitudes seleccionadas.";
            }
        }

        protected void des_buscar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            getPrestamo();
            mensaje.Text = "";
            TextBox1.Focus();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("prestamos.aspx");
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView1.HeaderRow.Cells[5].CssClass = "boundAgreg";
                e.Row.Cells[5].CssClass = "boundAgreg";
            }
        }

        protected void reiniciar_solicitud_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);

            bool reiniciaruser = false;

            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("seleccionar") as CheckBox);

                    if (chkRow.Checked)
                    {
                        string codigo = row.Cells[1].Text;
                        string fecha = row.Cells[5].Text;
                        
                        var res = DbUtil.ExecuteProc("sp_reiniciar_solicitud_prestamo",
                            new SqlParameter("@codigo", codigo),
                            new SqlParameter("@fec_sol", fecha)
                            );

                        reiniciaruser = true;
                    }
                }
            }

            getPrestamo();
            TextBox1.Focus();

            if (reiniciaruser == true)
            {
                mensaje.Text = "Las solicitudes fueron reiniciadas exitosamente.";
            }
            else
            {
                mensaje.Text = "No hay solicitudes seleccionadas.";
            }
        }
    }
}