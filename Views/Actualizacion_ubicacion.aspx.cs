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
    public partial class Actualizacion_ubicacion : System.Web.UI.Page
    {
        private string lastZipFilePath;
        private string strCSVFilesPath;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                strCSVFilesPath = Server.MapPath(@"~\Virtual\archivos\Ubicaciones\").ToString();
                String MyVarNom = Session["nombre"].ToString();
                String MyVarNum = Session["numero"].ToString();

                nombre.Text = MyVarNom;

                GetUbis();
                if (IsPostBack)
                {
                    if (Session["lastZipFile"] != null)
                    {
                        lastZipFilePath = Session["lastZipFile"].ToString();
                        hidden_lastZipFileName.Value = lastZipFilePath;
                    }
                }
                else
                {
                    GenerateCSVFiles();
                    ZipFilesCSV(strCSVFilesPath);
                }
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }
            
        }
        protected void GetUbis()
        {
            //var res = DbUtil.GetCursor("sp_GetUbicacionesKU",
            //        new SqlParameter("@Status", 1)
            //        );

            //GridView1.DataSource = res;
            //GridView1.DataBind();
            var Ubi = DbUtil.GetCursor("sp_GetUbicacionesKU");
            GridView1.DataSource = Ubi;
            GridView1.DataBind();

            if (GridView1.Rows.Count > 0)
                ok_modal.Visible = true;
            else
                ok_modal.Visible = false;
        }
        private void GenerateCSVFiles()
        {
           
            try
            {
                var dirInfo = new DirectoryInfo(strCSVFilesPath);

                // deleting existing files in directory
                foreach (var csvFile in dirInfo.GetFiles("*.csv"))
                {
                    if (System.IO.File.Exists(csvFile.FullName))
                    {
                        System.IO.File.Delete(csvFile.FullName);
                    }
                }
                
                // generating new csv files
                TextWriter Ubicaciones = new StreamWriter(strCSVFilesPath + @"Ubicaciones.csv");

                string strUbi = "";


                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        string strCodigoEmpleado = row.Cells[0].Text.ToString().Trim();
                        string strPlanta = row.Cells[1].Text.ToString().Trim();
                        string strPiso = row.Cells[2].Text.ToString().Trim();
                        string strCons = row.Cells[3].Text.ToString().Trim();
                        string strTipo = row.Cells[4].Text.ToString().Trim();
                        string strUbicacion = row.Cells[5].Text.ToString().Trim();

                        strUbi = !string.IsNullOrEmpty(row.Cells[0].Text.Replace("&nbsp;", "").Trim()) ? strUbi + string.Format(strCodigoEmpleado + ", "+strPlanta+", "+strPiso+", "+strCons+", "+strTipo+", "+strUbicacion+", ", Environment.NewLine) : strUbi;
                    }

                }
                
                    if (!string.IsNullOrEmpty(strUbi)) Ubicaciones.WriteLine(strUbi.TrimEnd('\r', '\n'));
                
                


                Ubicaciones.Close();

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
               // lbl_error.Text = ex2.Message;
            }
        }
        
        private void ZipFilesCSV(string dirPath)
        {
            var zipFileName = string.Format("Ubicacion-{0}.zip", DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss"));

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
        protected void aceptar_ac_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // execute sp of data updated
                    var codigo = row.Cells[0].Text.ToString();

                    var res = DbUtil.ExecuteProc("sp_UpdateUbiKU",
                        new SqlParameter("@codigo", codigo)
                        );
                }
            }
            GetUbis();
        }
       
        private void redirect()
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Actualizacion_ubicacion.aspx");
        }
        protected void btn_MainMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuKey.aspx");
        }
    }
}