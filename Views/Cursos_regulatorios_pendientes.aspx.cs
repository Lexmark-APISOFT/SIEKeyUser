using MsBarco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIE_KEY_USER.Views
{
    public partial class Cursos_regulatorios_pendientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getPendientes();
        }

        public void getPendientes() {
            var res = DbUtil.ExecuteQuery("Select * from vw_cursos_por_empleado order by supervisor");
            gvPendientes.DataSource = res;
            gvPendientes.DataBind();
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cursos_regulatorios.aspx");
        }
    }
}