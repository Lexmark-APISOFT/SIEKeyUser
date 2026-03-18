using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIE_KEY_USER.model;
using Ionic.Zip;
using System.IO;

namespace SIE_KEY_USER.Views
{
    public partial class confirmar_datos : System.Web.UI.Page
    {
        private string zipFilePath;
        private string downloadFileName;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                zipFilePath = Request.QueryString["ZipFilePath"];
                downloadFileName = Request.QueryString["ZipFileName"];
                System.Threading.Thread.Sleep(3000);
                if (downloadFileName != null && downloadFileName != null)
                {
                    DownloadZipFileCSV();
                }
                else
                {
                    Response.Redirect("MenuKey.aspx", false);
                }
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("Default.aspx");
            }            
        }

        protected void guardar_configurar_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuKey.aspx", false);   
        }

        private void DownloadZipFileCSV()
        {
            // Tell the browser we're sending a ZIP file!
            //var downloadFileName = string.Format("Act_Datos-{0}.zip", DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss"));
            Response.ContentType = "application/zip";
            Response.AddHeader("Content-Disposition", "filename=" + downloadFileName);
            
            try
            {
                string FullfileName = zipFilePath + downloadFileName;
                FileInfo zipFile = new FileInfo(FullfileName);
                if (zipFile.Exists)
                {
                    //Response.Flush();
                    Response.TransmitFile(zipFile.FullName);                    
                }
            }
            catch (Exception ex1)
            {
                lblErrMsg.Text = ex1.Message;
            }

            Response.End();
        }
    }
}