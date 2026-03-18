using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using MsBarco;
using System.IO;

namespace SIE_KEY_USER.Views
{
    public partial class Reembolso_aprobacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                if (!IsPostBack)
                {
                    String MyVarNom = Session["nombre"].ToString();
                    String MyVarNum = Session["numero"].ToString();
                    string periodo = Session["periodo"].ToString();
                    string promedio = Session["promedio"].ToString();
                    string porcentaje = Session["porcentaje"].ToString();
                    Observaciones.Text = "En el periodo: " + periodo + " obtuvo un promedio de: " + promedio + " y se reembolsará el " + porcentaje;
                    nombre.Text = MyVarNom;
                    
                    //mensaje.Text = a + ' ' + f;
                }

            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }
        }

        protected void aceptar_fam_Click(object sender, EventArgs e)
        {
            
            String a = Session["numreloj"].ToString();
            String f = Session["escu"].ToString();
            string obser = Observaciones.Text;
            string monto = Monto.Text;
            if (monto != "")
            {
                var res = DbUtil.ExecuteProc("sp_UpdateReembolso",
                    new SqlParameter("@codigo", int.Parse(a)),
                    new SqlParameter("@escuela", f),
                    new SqlParameter("@status", 1),
                    new SqlParameter("@obser", obser),
                    new SqlParameter("@monto", float.Parse(monto))
                    //new SqlParameter("@periodo",Session["periodo"].ToString())
                    );
                mensaje.Text = "Reembolso aceptado exitosamente";
                System.Threading.Thread.Sleep(200);
                Response.Redirect("confirmar_reembolso.aspx");
            }
            else
            {
                mensaje.Text = "Especificar monto";
            }
        }

        protected void rechazar_fam_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Reembolso_escolar.aspx");
        }
    }
}