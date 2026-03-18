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
    public partial class Opciones : System.Web.UI.Page
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

                    GetLista();
                }
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }
        }

        public void GetLista()
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                var res = DbUtil.GetCursor("sp_getListaMenu");

                GridView1.DataSource = res;
                GridView1.DataBind();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetLista();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("MenuKey.aspx");
        }

        protected void opciones_guardar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);

            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList Ddl = (row.Cells[0].FindControl("seleccionar") as DropDownList);

                    if (Ddl.SelectedItem.Text == "Deshabilitado")
                    {
                        var modo = "deshabilitado";
                        var descripcion = row.Cells[2].Text;

                        var res = DbUtil.ExecuteProc("sp_UpdateMenu",
                            new SqlParameter("@modo", modo),
                            new SqlParameter("@descripcion", descripcion)
                            );
                    }

                    if (Ddl.SelectedItem.Text == "Habilitado")
                    {
                        var modo = "habilitado";
                        var descripcion = row.Cells[2].Text;

                        var res = DbUtil.ExecuteProc("sp_UpdateMenu",
                            new SqlParameter("@modo", modo),
                            new SqlParameter("@descripcion", descripcion)
                            );
                    }

                    if (Ddl.SelectedItem.Text == "Próximamente")
                    {
                        var modo = "proximamente";
                        var descripcion = row.Cells[2].Text;

                        var res = DbUtil.ExecuteProc("sp_UpdateMenu",
                            new SqlParameter("@modo", modo),
                            new SqlParameter("@descripcion", descripcion)
                            );
                    }
                    if (Ddl.SelectedItem.Text == "Oculto")
                    {
                        var modo = "oculto";
                        var descripcion = row.Cells[2].Text;

                        var res = DbUtil.ExecuteProc("sp_UpdateMenu",
                            new SqlParameter("@modo", modo),
                            new SqlParameter("@descripcion", descripcion)
                            );
                    }
                }
            }

            Response.Redirect("Confirmar_opciones.aspx");
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView1.HeaderRow.Cells[2].CssClass = "boundAgreg";
                e.Row.Cells[2].CssClass = "boundAgreg";

                DropDownList ddl = (e.Row.Cells[0].FindControl("seleccionar") as DropDownList);

                var res = DbUtil.ExecuteProc("sp_getMenu_KeyUser",
                    MsBarco.DbUtil.NewSqlParam("@button1", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@button2", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@button3", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@button4", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@button5", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@button6", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@button7", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@button8", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@button9", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@button10", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@button11", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@button12", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@button13", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@button14", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                    );

                if (e.Row.Cells[2].Text.Contains("Identificacion"))
                {
                    ddl.SelectedValue = res["@button1"].ToString();
                }
                if (e.Row.Cells[2].Text.Contains("Datos personales"))
                {
                    ddl.SelectedValue = res["@button2"].ToString();
                }
                if (e.Row.Cells[2].Text.Contains("Familiares"))
                {
                    ddl.SelectedValue = res["@button3"].ToString();
                }
                if (e.Row.Cells[2].Text.Contains("Experiencia"))
                {
                    ddl.SelectedValue = res["@button4"].ToString();
                }
                if (e.Row.Cells[2].Text.Contains("Contratacion"))
                {
                    ddl.SelectedValue = res["@button5"].ToString();
                }
                if (e.Row.Cells[2].Text.Contains("Area de trabajo"))
                {
                    ddl.SelectedValue = res["@button6"].ToString();
                }
                if (e.Row.Cells[2].Text.Contains("Vacaciones"))
                {
                    ddl.SelectedValue = res["@button7"].ToString();
                }
                if (e.Row.Cells[2].Text.Contains("Cartas"))
                {
                    ddl.SelectedValue = res["@button8"].ToString();
                }
                if (e.Row.Cells[2].Text.Contains("Prestamos"))
                {
                    ddl.SelectedValue = res["@button9"].ToString();
                }
                if (e.Row.Cells[2].Text.Contains("Reembolso escolar"))
                {
                    ddl.SelectedValue = res["@button10"].ToString();
                }
                if (e.Row.Cells[2].Text.Contains("Cursos"))
                {
                    ddl.SelectedValue = res["@button11"].ToString();
                }
                if (e.Row.Cells[2].Text.Contains("Ubicacion"))
                {
                    ddl.SelectedValue = res["@button12"].ToString();
                }
                if (e.Row.Cells[2].Text.Contains("Mis certificaciones"))
                {
                    ddl.SelectedValue = res["@button13"].ToString();
                }
                if (e.Row.Cells[2].Text.Contains("Cursos regulatorios"))
                {
                    ddl.SelectedValue = res["@button14"].ToString();
                }
            }
        }
    }
}