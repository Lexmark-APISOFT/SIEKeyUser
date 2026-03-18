using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SIE_KEY_USER.model;
using MsBarco;
using System.Text;
using Ionic.Zip;

namespace SIE_KEY_USER.Views
{
    public partial class Actualizacion_datos_PS : System.Web.UI.Page
    {
        private string lastZipFilePath;
        private string strCSVFilesPath; 
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                String MyvarNom = Session["nombre"].ToString();
                String MyVarNum = Session["numero"].ToString();

                nombre.Text = MyvarNom;

                strCSVFilesPath = Server.MapPath(@"~\VirtEncr\").ToString();

                GetGenerarDatosPS();

                //if (IsPostBack)
                //{
                //    if (Session["lastZipFile"] != null)
                //    {
                //        lastZipFilePath = Session["lastZipFile"].ToString();
                //        hidden_lastZipFileName.Value = lastZipFilePath;
                //    }
                //}
                //else
                //{

                //   // ZipFilesCSV(strCSVFilesPath);
                //}
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("Default.aspx");
            }
        }

        private void GenerateCSVPSFiles()
        {
            try
            {
                var dirInfo = new DirectoryInfo(strCSVFilesPath);

                // deleting existing files in directory
                //foreach (var csvFile in dirInfo.GetFiles("*.csv"))
                //{
                //    if (System.IO.File.Exists(csvFile.FullName))
                //    {
                //        File.Delete(csvFile.FullName);
                //    }
                //}

                //generating new csv files
                TextWriter pSoft = new StreamWriter(strCSVFilesPath + @"PeopleSoft_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".csv");


                string strpSoft = "";
                string[] peopSoft = new string[30];

                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        //DateTime dt = DateTime.Parse(row.Cells[15].Text.ToString());
                        //string strCodigoEmpleado = row.Cells[0].Text.ToString().Trim();

                        strpSoft = !string.IsNullOrEmpty(row.Cells[1].Text.Replace("&nbsp;", "").Trim()) ? strpSoft + string.Format(row.Cells[0].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[1].Text.Replace("&nbsp;", "").ToString() + "|" + DateTime.Now.ToString("u").Substring(0, 10) + "|" + row.Cells[2].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[3].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[4].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[5].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[6].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[7].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[8].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[9].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[10].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[11].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[12].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[13].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[14].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[15].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[16].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[17].Text.Replace("&nbsp;", "").ToString() + "|" + DateTime.Now.ToString("u").Substring(0, 10) + "|" + row.Cells[18].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[19].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[20].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[21].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[22].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[23].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[24].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[25].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[26].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[27].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[28].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[29].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[30].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[31].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[32].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[33].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[34].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[35].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[36].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[37].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[38].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[39].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[40].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[41].Text.Replace("&nbsp;", "").ToString() + "|" + row.Cells[42].Text.Replace("&nbsp;", "").ToString() + "{0}", Environment.NewLine) : strpSoft;
                    }

                }


                if (!string.IsNullOrEmpty(strpSoft)) pSoft.WriteLine(strpSoft.TrimEnd('\r', '\n'));


                pSoft.Close();


                // deleting files of 0k size
                foreach (var csvFile in dirInfo.GetFiles("*.csv"))
                {
                    if (csvFile.Length == 0)
                    {
                        if (System.IO.File.Exists(csvFile.FullName))
                        {
                            System.IO.File.Delete(csvFile.FullName);
                        }
                    }
                }
            }
            catch (Exception ex2)
            {
                lblErrMsg.Text = ex2.Message + " " + strCSVFilesPath;
            }
        }

        public void GetGenerarDatosPS()
        {
            try
            {
                var res = DbUtil.GetCursor("sp_verGenerarDatos",
                           new SqlParameter("@tipo", "PS"));
                GridView1.DataSource = res;
                GridView1.DataBind();
                lblErrMsg.Text = "";
            }
            catch (Exception ex)
            {
                lblErrMsg.Text = ex.Message.ToString();
            }

            if (GridView1.Rows.Count > 0)
                ok_modal.Visible = true;
            else
                ok_modal.Visible = false;
        }

        //protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    GridView1.PageIndex = e.NewPageIndex;
        //    GetGenerarDatos();
        //}


        protected void aceptar_ac_Click(object sender, EventArgs e)
        {
           
                GenerateCSVPSFiles();
                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        // execute sp of data updated
                        var codigo = row.Cells[6].Text.ToString().Substring(3, 5);
                        var fecha = row.Cells[43].Text.ToString();

                        var res = DbUtil.ExecuteProc("sp_updateDatos",
                            new SqlParameter("@codigo", codigo),
                            new SqlParameter("@tipo", "PS"),
                            new SqlParameter("@fecha", fecha)
                            );
                    }
                }
                GetGenerarDatosPS();
            
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //GridView1.HeaderRow.Cells[15].CssClass = "boundAgreg";
                //e.Row.Cells[15].CssClass = "boundAgreg";
            }
        }

        private void ZipFilesCSV(string dirPath)
        {
            var zipFileName = string.Format("Act_Datos-{0}.zip", DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss"));

            try
            {
                var dirInfo = new DirectoryInfo(dirPath);


                using (var zip = new ZipFile())
                {
                    foreach (var csvFile in dirInfo.GetFiles("*.csv"))
                    {
                        zip.AddFile(csvFile.FullName, "");
                    }

                    zip.Save(dirPath + zipFileName);
                    Session["lastZipFile"] = dirPath + zipFileName;
                    hidden_lastZipFileName.Value = zipFileName;
                    hidden_lastZipFilePath.Value = dirPath;
                }
            }
            catch (Exception ex1)
            {
                lblErrMsg.Text = ex1.Message;
            }
        }

        protected void irclinica_Click(object sender, EventArgs e)
        {
            Response.Redirect("actualizar_clinica.aspx");
        }

        protected void btn_MainMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("Actualizacion_datos.aspx");
        }

       
    }
}