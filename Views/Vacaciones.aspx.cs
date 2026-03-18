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
    public partial class Vacaciones : System.Web.UI.Page
    {
        private string lastZipFilePath;
        private string strCSVFilesPath;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                strCSVFilesPath = Server.MapPath(@"~\VirtEncr\").ToString();
                
                if (!IsPostBack)
                {
                    String MyVarNom = Session["nombre"].ToString();
                    String MyVarNum = Session["numero"].ToString();
                    nombre.Text = MyVarNom;
                    Getsolvacaciones();
                }
                //else
                //{
                //    if (Session["lastZipFile"] != null)
                //    {
                //        lastZipFilePath = Session["lastZipFile"].ToString();
                //        hidden_lastZipFileName.Value = lastZipFilePath;
                //    }
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
                TextWriter vacacion = new StreamWriter(strCSVFilesPath + @"vacaciones_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".csv");

                string strVacacion = "";
                strVacacion = strVacacion + string.Format("Numero de empleado" + ", " + "Fecha de inicio" + ", " +
                            "Dias" + ", " + "Fecha de regreso" + ", " + "Fecha de pago" + ","+"Observaciones"+","+"Fecha de solicitud"+".\r \n ", Environment.NewLine);

                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("seleccionar") as CheckBox);
                        if (chkRow.Checked)
                        {
                            string strCodigoEmpleado = row.Cells[1].Text.ToString().Trim();
                            string fechaini = row.Cells[2].Text.ToString().Trim();
                            string Dias = row.Cells[3].Text.ToString().Trim();
                            string fechareg = row.Cells[4].Text.ToString().Trim();
                            string fechapag = row.Cells[5].Text.ToString().Trim();
                            string observ = row.Cells[6].Text.ToString().Trim();
                            string fechasol = row.Cells[7].Text.ToString().Trim();

                            strVacacion = !string.IsNullOrEmpty(row.Cells[1].Text.Replace("&nbsp;", "").Trim()) ? strVacacion + string.Format(strCodigoEmpleado + ", " + fechaini + ", " +
                                Dias + ", " + fechareg + ", " + fechapag + ", " + observ + ", " + fechasol + ".\r \n ", Environment.NewLine) : strVacacion; 
                        }

                    }

                }
               
                if (!string.IsNullOrEmpty(strVacacion)) vacacion.WriteLine(strVacacion.TrimEnd('\r', '\n'));

                vacacion.Close();

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
            var zipFileName = string.Format("Vacaciones-{0}.zip", DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss"));

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
        protected void des_buscar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(500);
            Getsolvacaciones();

            TextBox1.Focus();
            mensaje.Text = "";
        }
        public void Getsolvacaciones()
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                var codigo = TextBox1.Text;
                var res = DbUtil.GetCursor("sp_Getsolvacaciones",
                    new SqlParameter("@codigo", codigo)
                    );

                GridView1.DataSource = res;
                GridView1.DataBind();

                if (GridView1.Rows.Count > 0)
                    ok_modal.Visible = true;
                else
                    ok_modal.Visible = false;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            Getsolvacaciones();
        }

        protected void vaca_generar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            bool vacaciones = false;

            GenerateCSVFiles();
            //ZipFilesCSV(strCSVFilesPath);

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "download();", true);

            foreach(GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                   CheckBox chkRow = (row.Cells[0].FindControl("seleccionar") as CheckBox);

                    if (chkRow.Checked)
                    {
                        string codigo = row.Cells[1].Text;
                        //string fecini = row.Cells[2].Text;
                        //string dias = row.Cells[3].Text;
                        //string fecfin = row.Cells[4].Text;
                        //string obser = row.Cells[6].Text;
                        string fecha = row.Cells[7].Text;

                        var res = DbUtil.ExecuteProc("sp_CapturarVacaciones",
                            new SqlParameter("@codigo", codigo),
                            new SqlParameter("@fecha", fecha)
                            );
                        //var res1 = DbUtil.ExecuteProc("sp_insertVaca",
                        //    new SqlParameter("@codigo", codigo),
                        //    new SqlParameter("@fecini", fecini),
                        //    new SqlParameter("@fecfin",fecfin),
                        //    new SqlParameter("@obser",obser),
                        //    new SqlParameter("@dias",dias)
                        //    );

                        vacaciones = true;
                    }
                }
            }
            
            Getsolvacaciones();

            if (vacaciones == true)
            {
                mensaje.Text = "Datos capturados exitosamente.";
            }
            else
            {
                mensaje.Text = "No hay solicitudes seleccionadas";
            }
            //System.Threading.Thread.Sleep(2000);
           // Response.Redirect("Vacaciones.aspx");

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("MenuKey.aspx");
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView1.HeaderRow.Cells[7].CssClass = "boundAgreg";
                e.Row.Cells[7].CssClass = "boundAgreg";
            }
        }
    }
}