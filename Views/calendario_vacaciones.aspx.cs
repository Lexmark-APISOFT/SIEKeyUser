using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using SIE.model;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Data;
using MsBarco;
using System.Globalization;

namespace SIE.Views
{
    public partial class calendario_vacaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
       
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                String MyvarNum = Session["numero"].ToString();
                getHabiles(MyvarNum);
            }
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as String))
            {
                Get_nombre_numero_short n = new Get_nombre_numero_short();
                lblNombreEmpleado.Text = n.NombreS.ToString();

            }


        }
        private void getHabiles(String numero)
        {
            var res = DbUtil.GetCursor("sp_S_Dias_vacaciones",
           new System.Data.SqlClient.SqlParameter("@codigo", numero)
           );


            lblDiasHabiles.Text = res.Rows[0].ItemArray.GetValue(0).ToString();
        }

    }
}