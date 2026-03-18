using MsBarco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIE_KEY_USER.Views
{
    public partial class Cursos_regulatorios_programacion_manual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                if (!IsPostBack)
                {
                    
                }
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("Default.aspx");
            }
        }
        private void getCursos(int codigo)
        {
            var res = DbUtil.GetCursor("sp_get_cursos_regulatorios",
                new System.Data.SqlClient.SqlParameter("@codigo", codigo)
                );
            gvCursos.DataSource = res;
            gvCursos.DataBind();

            var info = MsBarco.DbUtil.ExecuteProc("sp_getInfo_cartasSalario",
                   new SqlParameter("@codigo", codigo),
                   MsBarco.DbUtil.NewSqlParam("@nombres", null, SqlDbType.VarChar, ParameterDirection.Output, 93),
                   MsBarco.DbUtil.NewSqlParam("@fecha_ing", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                   MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                   MsBarco.DbUtil.NewSqlParam("@apellido", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                   MsBarco.DbUtil.NewSqlParam("@imss", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                   MsBarco.DbUtil.NewSqlParam("@rfc", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                   MsBarco.DbUtil.NewSqlParam("@horario", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                   MsBarco.DbUtil.NewSqlParam("@clasificacion", null, SqlDbType.Int, ParameterDirection.Output, 4)
                   );

            lblNombre.Text = info["@nombres"].ToString();
            lblPuesto.Text = info["@puesto"].ToString();
            lblHorario.Text = info["@horario"].ToString();

            if (lblNombre.Text == "") {
                lblNombre.Text = "No se encontró al empleado";
            }
        }

        private void getCursosByName(String prettyname)
        {
            var res = DbUtil.GetCursor("sp_get_cursos_regulatorios_byPrettyName",
                new System.Data.SqlClient.SqlParameter("@prettyname", prettyname)
                );
            if (res.Rows.Count == 0) { 
            gvCursos.DataSource = res;
            gvCursos.DataBind();
            var res2 = MsBarco.DbUtil.ExecuteQuery("SELECT TOP 1 CB_CODIGO FROM CommonDB.dbo.ColaboraV2 Where PRETTYNAME = " + "'"+prettyname+"'");
                if(res2.Rows.Count != 0) {
                        var codigo = res2.Rows[0].ItemArray;

                    Session.Add("codigo_empleado", Convert.ToInt32(codigo[0].ToString()));
                    var info = MsBarco.DbUtil.ExecuteProc("sp_getInfo_cartasSalario",
                           new SqlParameter("@codigo", Convert.ToInt32(codigo[0].ToString())),
                           MsBarco.DbUtil.NewSqlParam("@nombres", null, SqlDbType.VarChar, ParameterDirection.Output, 93),
                           MsBarco.DbUtil.NewSqlParam("@fecha_ing", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                           MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                           MsBarco.DbUtil.NewSqlParam("@apellido", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                           MsBarco.DbUtil.NewSqlParam("@imss", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                           MsBarco.DbUtil.NewSqlParam("@rfc", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                           MsBarco.DbUtil.NewSqlParam("@horario", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                           MsBarco.DbUtil.NewSqlParam("@clasificacion", null, SqlDbType.Int, ParameterDirection.Output, 4)
                           );

                    lblNombre.Text = info["@nombres"].ToString();
                    lblPuesto.Text = info["@puesto"].ToString();
                    lblHorario.Text = info["@horario"].ToString();

                    txtAviso.Text = "";
                    if (lblNombre.Text == "")
                    {
                        lblNombre.Text = "No se encontró al empleado";
                    }
                }else if(res2.Rows.Count == 0)
                {
                    txtAviso.Text = "No existe el empleado";
                }
            }
           
        }

        protected void gvCursos_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvCursos.Rows)
            {

                LinkButton lkbSeleccionar = (LinkButton)row.FindControl("");
                switch (row.Cells[3].Text)
                {
                    case "INSCRITO":
                        lkbSeleccionar = (LinkButton)row.FindControl("lkbSeleccionar");
                        lkbSeleccionar.Text = "Reprogramar";
                        break;
                    case "CURSO TOMADO":
                        row.Cells[0].Text = "";
                        break;

                    case "FALTA":
                        lkbSeleccionar = (LinkButton)row.FindControl("lkbSeleccionar");
                        lkbSeleccionar.Text = "Reprogramar";
                        break;
                    case "NO INSCRITO":
                        lkbSeleccionar = (LinkButton)row.FindControl("lkbSeleccionar");
                        lkbSeleccionar.Text = "Programar";
                        break;
                    default:
                        lkbSeleccionar = (LinkButton)row.FindControl("lkbSeleccionar");
                        lkbSeleccionar.Text = "Reprogramar";
                        break;

                }
            }


        }

        protected void gvCursos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                //Determine the RowIndex of the Row whose LinkButton was clicked.
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvCursos.Rows[rowIndex];

                string cu_codigo = row.Cells[1].Text;
                String se_folio = row.Cells[5].Text;


                if (se_folio.Equals("&nbsp;"))
                {
                    Session.Add("se_folio_previo", "reinscripcion");
                }
                else
                {
                    Session.Add("se_folio_previo", se_folio);
                }


                Session.Add("cu_codigo", cu_codigo);
                Response.Redirect("Cursos_regulatorios_sesiones_cursos.aspx");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            int cb_codigo = int.Parse(txtNumReloj.Text);
            getCursos(cb_codigo);
            Session.Add("codigo_empleado", cb_codigo);
        }

        protected void btnBuscar2_Click(object sender, EventArgs e)
        {

            var prettyName = txtPrettyName.Text;
            getCursosByName(prettyName);
           
        }
        protected void btnRegresar_Click(object sender, EventArgs e) {
            Response.Redirect("Cursos_regulatorios.aspx");
        }
    }
}