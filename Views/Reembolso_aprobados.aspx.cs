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
    public partial class Reembolso_aprobados : System.Web.UI.Page
    {
        private string lastZipFilePath;
        private string strCSVFilesPath;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                String MyVarNom = Session["nombre"].ToString();
                String MyVarNum = Session["numero"].ToString();

                nombre.Text = MyVarNom;
                strCSVFilesPath = Server.MapPath(@"~\VirtEncr\").ToString();
                getReembolsoAprobados();
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
                TextWriter reemacept = new StreamWriter(strCSVFilesPath + @"reemacept_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".csv");
               


                string strreemacept = ""; 
                strreemacept = strreemacept + string.Format("Fecha" + ", " + "Numero de empleado" + ", " +
                            "Nombre empleado" + ", " + "Escuela" + ", " + "Observaciones" + ","+"Monto"+".\r \n ", Environment.NewLine);
                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        //DateTime dt = DateTime.Parse(row.Cells[8].Text.ToString());
                        string fecha = row.Cells[0].Text.ToString().Trim();
                        string codemp = row.Cells[1].Text.ToString().Trim();
                        string nombre = row.Cells[2].Text.ToString().Trim();
                        string esc = row.Cells[3].Text.ToString().Trim();
                        string observ = row.Cells[4].Text.ToString().Trim();
                        string monto = row.Cells[5].Text.ToString().Trim();



                        strreemacept = !string.IsNullOrEmpty(row.Cells[1].Text.Replace("&nbsp;", "").Trim()) ? strreemacept + string.Format(fecha + ", " + codemp + ", " +
                            nombre + ", " + esc + ", " + observ +", "+monto+ ".\r \n ", Environment.NewLine) : strreemacept;

                      

                    }

                }

                if (!string.IsNullOrEmpty(strreemacept)) reemacept.WriteLine(strreemacept.TrimEnd('\r', '\n'));
               


                reemacept.Close();
             


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
                mensaje.Text = ex2.Message;
            }
        }
        private void ZipFilesCSV(string dirPath)
        {
            var zipFileName = string.Format("Reem-aprob-{0}.zip", DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss"));

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
                mensaje.Text = ex1.Message;
            }
        }
        public void getReembolsoAprobados()
        {
            String MyVarNum = Session["numero"].ToString();

            var codigo = TextBox1.Text;
            var res = DbUtil.GetCursor("sp_verReembolsoA",
                new SqlParameter("@codigo", codigo));

            GridView1.DataSource = res;
            GridView1.DataBind();

            if (GridView1.Rows.Count > 0)
                ok_modal.Visible = true;
            else
                ok_modal.Visible = false;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Reembolso_escolar.aspx");
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            getReembolsoAprobados();
        }
        protected void des_buscar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(500);
            getReembolsoAprobados();

            TextBox1.Focus();
            mensaje.Text = "";
        }
        protected void aceptar_ac_Click(object sender, EventArgs e)
        {
            GenerateCSVFiles();
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // execute sp of data updated
                    var codigo = row.Cells[0].Text.ToString();
                    //var fecha = row.Cells[8].Text.ToString();
                    var res = DbUtil.ExecuteProc("sp_delAprob",
                        new SqlParameter("@opcion", "reem")

                        );
                }
            }
            getReembolsoAprobados();
        }
    }
}