using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIE.Views
{
    public partial class confirmacion_cartas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("Default.aspx");
            }
        }

        protected void confir_aceptar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("MenuUser.aspx");
        }
    }
}