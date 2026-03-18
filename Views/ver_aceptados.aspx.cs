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
    public partial class ver_aceptados : System.Web.UI.Page
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
                GetFamApro_keyuser();
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
                    
                //    ZipFilesCSV(strCSVFilesPath);
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
                TextWriter famacept = new StreamWriter(strCSVFilesPath + @"famacept_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".csv");


                string strFamAcept = "";
                strFamAcept = strFamAcept + string.Format("Numero de empleado" + ", " + "Parentesco"+ ", " +
                            "Nombre" + ", " + "sexo" + ", " + "Fecha de Nacimiento"+ ".\r \n ", Environment.NewLine);

                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        string strCodigoEmpleado = row.Cells[0].Text.ToString().Trim();
                        string parentesco = row.Cells[1].Text.ToString().Trim();
                        string nombre = row.Cells[2].Text.ToString().Trim();
                        string sexo = row.Cells[3].Text.ToString().Trim();
                        string fechasol= row.Cells[4].Text.ToString().Trim();



                        strFamAcept = !string.IsNullOrEmpty(row.Cells[1].Text.Replace("&nbsp;", "").Trim()) ? strFamAcept + string.Format(strCodigoEmpleado + ", " + parentesco + ", " +
                            nombre + ", " + sexo + ", " + fechasol+".\r \n ", Environment.NewLine) : strFamAcept;
                    }

                }

                if (!string.IsNullOrEmpty(strFamAcept)) famacept.WriteLine(strFamAcept.TrimEnd('\r', '\n'));

                famacept.Close();

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
                lblErrMsg.Text = ex2.Message;
            }
        }
        private void ZipFilesCSV(string dirPath)
        {
            var zipFileName = string.Format("Fam-aprob-{0}.zip", DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss"));

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
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetFamApro_keyuser();
        }

        public void GetFamApro_keyuser()
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                var res = DbUtil.GetCursor("sp_veraceptados_Keyuser");
                
                GridView1.DataSource = res;
                GridView1.DataBind();

                int rowCount = GridView1.Rows.Count;

                if (rowCount == 0)
                {
                   ok_modal.Visible = false;
                }
                else
                {
                    ok_modal.Visible = true;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Agregar_familiares.aspx");
        }
       
        protected void aceptar_ac_Click(object sender, EventArgs e)
        {
            GenerateCSVFiles();
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // execute sp of data updated
                    //var codigo = row.Cells[0].Text.ToString();
                    //var fecha = row.Cells[8].Text.ToString();
                    var res = DbUtil.ExecuteProc("sp_delAprob",
                        new SqlParameter("@opcion", "fam")

                        );
                }
            }
            GetFamApro_keyuser();
        }

        
    }
}