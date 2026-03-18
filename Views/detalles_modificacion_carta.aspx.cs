 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Reflection;
using System.Net;
using System.Net.Http;

namespace SIE_KEY_USER.Views
{
    public partial class detalles_modificacion_carta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                string folder = Session["Path"].ToString();
                String MyVarNom = Session["nombre"].ToString();
                String MyVarNum = Session["numero"].ToString();
                
                nombre.Text = MyVarNom;
                lbl_modmsg.Text = Session["MSJ"].ToString();
                /*if (folder == "constanciaTrabajo")
                {
                    string filesPath = HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\constanciaTrabajo\history\");
                    lbl_modmsg.Text = "Constancia de Trabajo";
                    if (Directory.Exists(filesPath))
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(filesPath);
                        //FileInfo [] fileInfos = dirInfo.GetFiles("*.docx");

                        /*DG_HistorialCarta.DataSource = dirInfo.GetFiles("*.docx");
                        DG_HistorialCarta.DataBind();*/
                       /* GridView1.DataSource = dirInfo.GetFiles("*.docx");
                        GridView1.DataBind();*/
                   /* }
                    else
                    {
                        lbl_statusMsg.Text = "No se encontró directorio: " + filesPath;
                    }

                }

                if (folder == "altaimss")
                {
                    string filesPath = HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\altaimss\history\");
                    lbl_modmsg.Text = "Carta alta IMSS";
                    if (Directory.Exists(filesPath))
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(filesPath);
                        //FileInfo [] fileInfos = dirInfo.GetFiles("*.docx");
                       
                        /*DG_HistorialCarta.DataSource = dirInfo.GetFiles("*.docx");
                        DG_HistorialCarta.DataBind();
                        GridView1.DataSource = dirInfo.GetFiles("*.docx");
                        GridView1.DataBind();*/
                   /* }
                    else
                    {
                        lbl_statusMsg.Text = "No se encontró directorio: " + filesPath;
                    }
                }
                if (folder == "cambioTurno")
                {
                    string filesPath = HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\cambioTurno\history\");
                    lbl_modmsg.Text = "Cambio horario IMSS";
                    if (Directory.Exists(filesPath))
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(filesPath);
                        //FileInfo [] fileInfos = dirInfo.GetFiles("*.docx");

                        /*DG_HistorialCarta.DataSource = dirInfo.GetFiles("*.docx");
                        DG_HistorialCarta.DataBind();*/
                        /*GridView1.DataSource = dirInfo.GetFiles("*.docx");
                        GridView1.DataBind();*/
                   /* }
                    else
                    {
                        lbl_statusMsg.Text = "No se encontró directorio: " + filesPath;
                    }
                }
                if (folder == "cambioClinica")
                {
                    string filesPath = HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\cambioClinica\history\");
                    lbl_modmsg.Text = "Cambio clinica IMSS";
                    if (Directory.Exists(filesPath))
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(filesPath);
                        //FileInfo [] fileInfos = dirInfo.GetFiles("*.docx");

                       /* DG_HistorialCarta.DataSource = dirInfo.GetFiles("*.docx");
                        DG_HistorialCarta.DataBind();*/
                       /* GridView1.DataSource = dirInfo.GetFiles("*.docx");
                        GridView1.DataBind();*/
                  /*  }
                    else
                    {
                        lbl_statusMsg.Text = "No se encontró directorio: " + filesPath;
                    }
                }
                if (folder == "ingresoGuarderia")
                {
                    string filesPath = HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\ingresoGuarderia\history\");
                    lbl_modmsg.Text = "Ingreso a Guarderia";
                    if (Directory.Exists(filesPath))
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(filesPath);
                        //FileInfo [] fileInfos = dirInfo.GetFiles("*.docx");

                        /*DG_HistorialCarta.DataSource = dirInfo.GetFiles("*.docx");
                        DG_HistorialCarta.DataBind();*/
                        /*GridView1.DataSource = dirInfo.GetFiles("*.docx");
                        GridView1.DataBind();*/
                   /* }
                    else
                    {
                        lbl_statusMsg.Text = "No se encontró directorio: " + filesPath;
                    }
                }
                if (folder == "vacacionesGuarderia")
                {
                    string filesPath = HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\vacacionesGuarderia\history\");
                    lbl_modmsg.Text = "Vacaciones Guarderia";
                    if (Directory.Exists(filesPath))
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(filesPath);
                        //FileInfo [] fileInfos = dirInfo.GetFiles("*.docx");

                        /*DG_HistorialCarta.DataSource = dirInfo.GetFiles("*.docx");
                        DG_HistorialCarta.DataBind();*/
                       /* GridView1.DataSource = dirInfo.GetFiles("*.docx");
                        GridView1.DataBind();*/
                   /* }
                    else
                    {
                        lbl_statusMsg.Text = "No se encontró directorio: " + filesPath;
                    }
                }
                if (folder == "visaLaser")
                {
                    string filesPath = HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\visaLaser\history\");
                    lbl_modmsg.Text = "Visa Laser";
                    if (Directory.Exists(filesPath))
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(filesPath);
                        //FileInfo [] fileInfos = dirInfo.GetFiles("*.docx");

                       /* DG_HistorialCarta.DataSource = dirInfo.GetFiles("*.docx");
                        DG_HistorialCarta.DataBind();*/
                        /*GridView1.DataSource = dirInfo.GetFiles("*.docx");
                        GridView1.DataBind();*/
                  /*  }
                    else
                    {
                        lbl_statusMsg.Text = "No se encontró directorio: " + filesPath;
                    }
                }
                if (folder == "migracion")
                {

                    string filesPath = HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\migracion\history\");
                    lbl_modmsg.Text = "Migracion";
                    if (Directory.Exists(filesPath))
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(filesPath);
                        //FileInfo [] fileInfos = dirInfo.GetFiles("*.docx");

                        /*DG_HistorialCarta.DataSource = dirInfo.GetFiles("*.docx");
                        DG_HistorialCarta.DataBind();*/
                        /*GridView1.DataSource = dirInfo.GetFiles("*.docx");
                        GridView1.DataBind();*/
                  /*  }
                    else
                    {
                        lbl_statusMsg.Text = "No se encontró directorio: " + filesPath;
                    }
                }*/
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }
        }

        protected void no_fam_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            mensaje.Text = " ";
            Response.Redirect("detalles_modificacion_carta.aspx");

        }
//------------------------------------------------------------------------------------------------------------REEMPLAZAR

  protected void aceptar_ac_Click(object sender, EventArgs e)
        {
        string folder = Session["Path"].ToString();

        if (FileUpload1.FileName != "")
        {
            string sourceFilePath = (@"\\mxjrzapp04\Applications\SIE\cartas\Templates\"+folder);

            try
            {
                FileUpload1.PostedFile.SaveAs(sourceFilePath + "\\carta.docx");
                mensaje.Text = "Carta reemplazada exitosamente.";
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }
        else
        {
            mensaje.Text = "Debe seleccionar un archivo de reemplazo";
        }
            
  }
        /* protected void btn_ReemplazarC_Click(object sender, EventArgs e)
            {
                string folder = Session["Path"].ToString();

           if (FileUpload1.FileName != "")
                    {
                        string sourceFilePath = HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\"+folder+"\\");
                        string targetFilePath = HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\"+folder+"\\history\\");

                        string date_pattern = "yyyy-MM-dd";
                        DateTime newDate = DateTime.Now;

                        newDate.ToString(date_pattern);
                        try
                        {
                            if (File.Exists(sourceFilePath + "carta.docx") && Directory.Exists(targetFilePath))
                               {
                                   string date_for_fileName = string.Format("{0:yyyy-MM-dd_HH-mm-ss}", DateTime.Now);

                                   File.Copy(sourceFilePath + "carta.docx", string.Format("{0}carta.docx", sourceFilePath));

                               }

                                FileUpload1.PostedFile.SaveAs(sourceFilePath + "carta.docx");
                                //Response.Write("The file has been uploaded.");
                                //archivo.Text = FileUpload1.FileName;
                            }
                            catch (Exception ex)
                            {
                                Response.Write("Error: " + ex.Message);
                                //Note: Exception.Message returns detailed message that describes the current exception. 
                                //For security reasons, we do not recommend you return Exception.Message to end users in 
                                //production environments. It would be better just to put a generic error message. 
                            }
                            }
                            else
                            {
                            mensaje.Text = "Debe seleccionar un archivo de reemplazo";
                            }
         }*/

        protected void btn_ModificarC_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            string folder = Session["Path"].ToString();

            string filePath = @"\\mxjrzapp04\Applications\SIE\cartas\Templates\"+folder+"\\carta.docx";
            string fileName = Path.GetFileName(filePath);
            if (System.IO.File.Exists(filePath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.BinaryWrite(fileBytes);
                Response.Flush();
                Response.End();
            }

        }
         protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("modificar_cartas");
        }
        /* protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
         {
             GridView1.PageIndex = e.NewPageIndex;
           
         }
         protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
         {

             System.Threading.Thread.Sleep(200);
             string folder = Session["Path"].ToString();
          
          try
                 {
                     GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                     int rowIndex = gvr.RowIndex;

                     var a = GridView1.Rows[rowIndex].Cells[3].Text;
                     string FileName = Path.Combine(Server.MapPath(@"~\Virtual\cartas\"+folder+"\\"),a);
                     System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                     response.ClearContent();
                     response.Clear();

                     Response.AddHeader("Content-Disposition", string.Format("attachment; filename = \"{0}\"", System.IO.Path.GetFileName(FileName)));
                     response.TransmitFile(FileName);
                     response.Flush();
                     response.End();
                     mensaje.Text = "No se encontró el archivo " + FileName;
                    
                 }
                 catch
                 {
                 }
         }*/
            
    }
    
}