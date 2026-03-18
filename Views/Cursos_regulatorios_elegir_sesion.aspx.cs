using MsBarco;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIE_KEY_USER.Views
{
    public partial class Cursos_regulatorios_elegir_sesion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                if (!IsPostBack)
                {
                    getSesiones(HttpContext.Current.Session["se_folio"].ToString());
                }
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("Default.aspx");
            }
        }
        private void getSesiones(String folio)
        {
            var res = DbUtil.GetCursor("sp_get_detalles_sesion",
                new System.Data.SqlClient.SqlParameter("@folio", folio)
                );
            lblFolio.Text = res.Rows[0].ItemArray.GetValue(6).ToString();
            lblCodigo.Text = "Código: " + res.Rows[0].ItemArray.GetValue(0).ToString();
            lblNombre.Text = res.Rows[0].ItemArray.GetValue(1).ToString();
            lblFecha.Text = "Fecha: " + res.Rows[0].ItemArray.GetValue(2).ToString();
            lblHorario.Text = "Horario: " + res.Rows[0].ItemArray.GetValue(3).ToString();
            lblLugar.Text = "Lugar: " + res.Rows[0].ItemArray.GetValue(4).ToString();
            lblCupo.Text = "Cupo: " + res.Rows[0].ItemArray.GetValue(5).ToString();
        }


        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            String folio = lblFolio.Text;
            int numReloj = int.Parse(HttpContext.Current.Session["codigo_empleado"].ToString());

            String prevFolio = HttpContext.Current.Session["se_folio_previo"].ToString();
            String cu_codigo = HttpContext.Current.Session["cu_codigo"].ToString();



            if (prevFolio.Equals("reinscripcion"))
            {
                var res = DbUtil.ExecuteProc("sp_regulatorios_inscribir",
                    new SqlParameter("@se_folio", folio),
                    new SqlParameter("@cb_codigo", numReloj),
                    new SqlParameter("@cu_codigo", cu_codigo)
                    );
            }
            else
            {
                var rep = DbUtil.ExecuteProc("sp_regulatorios_reprogramar",
                    new SqlParameter("@se_folio", folio),
                    new SqlParameter("@cb_codigo", numReloj),
                    new SqlParameter("@re_razon", "Reprogramación"),
                    new SqlParameter("@SE_FOLIO_PREVIO", prevFolio),
                    new SqlParameter("@cu_codigo", cu_codigo)
                    );
            }

            Response.Redirect("Cursos_regulatorios.aspx");
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cursos_regulatorios.aspx");
        }
    }
}