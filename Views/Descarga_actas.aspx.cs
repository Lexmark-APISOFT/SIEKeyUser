using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIE_KEY_USER.Views
{
    public partial class Descarga_actas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                try
                {
                    
                    string Opc = Session["Desc"].ToString();
                    string FileName = Session["FileName"].ToString();
                    System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                    response.ClearContent();
                    response.Clear();
                    response.Buffer = true;
                    Response.AddHeader("Content-Disposition", string.Format("attachment; filename = \"{0}\"", System.IO.Path.GetFileName(FileName)));
                    response.TransmitFile(FileName);

                    response.Flush();
                    response.End();
                    File.Delete(FileName);
                    if (Opc == "Acta")
                    {
                        Response.Redirect("Agregar_familiares.aspx");
                    }
                    else
                    {
                        Response.Redirect("Vacaciones.aspx");
                    }
                }
                catch
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
    }
}