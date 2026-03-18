using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MsBarco;
using System.Configuration;
using System.Web.UI.HtmlControls;

namespace SIE_KEY_USER.Views
{
    public partial class actualizar_dias_vacaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         
                

            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                if (!IsPostBack)
                {

                    var res = DbUtil.GetCursor("get_cant_Dias_Max");
                    CantidadDiasMax.Text = res.Rows[0].ItemArray.GetValue(0).ToString();
                    String nombreEmpleado = Session["nombre"].ToString();
                    lblNombreEmpleado.Text = nombreEmpleado;
                }
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }


        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("solicitud_vacaciones.aspx");

        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            int cantidad = int.Parse(txtActualizar.Text);
            getCursos(cantidad);


        }

        private void getCursos(int cantidad)
        {
            var res = DbUtil.ExecuteProc("sp_u_cantidad_dias_vac",
                new System.Data.SqlClient.SqlParameter("@cantidad", cantidad)
                );

            var cant_dias = DbUtil.GetCursor("get_cant_Dias_Max");
            CantidadDiasMax.Text = cant_dias.Rows[0].ItemArray.GetValue(0).ToString();
        }

    }
}