//-- =============================================
//--Author:	
//-- =====================================================================================================================
//--CODE        | NAME                                | MODIFIED DATE       | DESCRIPTION
//-- =====================================================================================================================
//-NA           Fernando Contreras Contreras 51102		05/11/2023			Added request file list
//-NA           Daniel Omar Mendoza Rodriguez 51105		05/11/2023			Added request file list



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
using Novacode;
using Microsoft.Office.Interop.Word;
using SIE.model;
using System.Runtime.CompilerServices;
using System.Web.Services;

namespace SIE_KEY_USER.Views
{
    public partial class Reimpresion : System.Web.UI.Page
    {
        protected int saldo = 0;
        private string lastZipFilePath;
        private string strCSVFilesPath;
        Random randNum = new Random();


        static string printFolderPath = @"C:\SIEPrintCartas";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                strCSVFilesPath = Server.MapPath(@"~\Virtual\archivos\Reimpresiones\").ToString();
                String MyVarNom = Session["nombre"].ToString();
                String MyVarNum = Session["numero"].ToString();

                nombre.Text = MyVarNom;

                TextBox1.Focus();

                if (!IsPostBack)
                {
                    getCartas(saldo);
                }
                else
                {
                    if (Session["lastZipFile"] != null)
                    {
                        lastZipFilePath = Session["lastZipFile"].ToString();
                        hidden_lastZipFileName.Value = lastZipFilePath;
                    }
                }
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }
        }

        public void getCartas(int saldo)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                String MyVarNum = Session["numero"].ToString();

                var res = DbUtil.GetCursor("sp_getReimpresion_cartas",
                    new SqlParameter("@codigo", TextBox1.Text),
                    new SqlParameter("@saldo", saldo)
                    );

                GridView1.DataSource = res;
                GridView1.DataBind();
            }
        }
        private void GenerateCSVFiles(string btn_tipo)
        {
            string opcion_carta = "";
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
                if (btn_tipo == "constancia_trabajo")
                {
                    opcion_carta = "Constancia de trabajo";
                }
                if (btn_tipo == "constancia_visa")
                {
                    opcion_carta = "Visa l&#225;ser";
                }
                if (btn_tipo == "constancia_migracion")
                {
                    opcion_carta = "Permiso migraci&#243;n";
                }
                // generating new csv files
                TextWriter ConstTrab = new StreamWriter(strCSVFilesPath + @"ConstanciaTrabajo.csv");
                TextWriter CVisa = new StreamWriter(strCSVFilesPath + @"CartaVisa.csv");
                TextWriter Cmigracion = new StreamWriter(strCSVFilesPath + @"CartaMigracion.csv");


                string strCT = "";


                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        // CheckBox chkRow = (row.Cells[0].FindControl("seleccionar") as CheckBox);
                        string tipocarta = row.Cells[5].Text;
                        //mensaje.Text = tipocarta;
                        if (tipocarta == "Visa l&#225;ser")
                            if (tipocarta == opcion_carta)
                            {
                                string strCodigoEmpleado = row.Cells[2].Text.ToString().Trim();

                                strCT = !string.IsNullOrEmpty(row.Cells[2].Text.Replace("&nbsp;", "").Trim()) ? strCT + string.Format(strCodigoEmpleado + ", ", Environment.NewLine) : strCT;
                            }

                    }

                }
                if (btn_tipo == "constancia_trabajo")
                {
                    if (!string.IsNullOrEmpty(strCT)) ConstTrab.WriteLine(strCT.TrimEnd('\r', '\n'));
                }
                if (btn_tipo == "constancia_visa")
                {
                    if (!string.IsNullOrEmpty(strCT)) CVisa.WriteLine(strCT.TrimEnd('\r', '\n'));
                }
                if (btn_tipo == "constancia_migracion")
                {
                    if (!string.IsNullOrEmpty(strCT)) Cmigracion.WriteLine(strCT.TrimEnd('\r', '\n'));
                }


                ConstTrab.Close();
                CVisa.Close();
                Cmigracion.Close();

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

        private void ZipFilesCSV(string dirPath, string fName)
        {
            var zipFileName = string.Format("{0}-{1}.zip", fName, DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss"));

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
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            getCartas(saldo);
            TextBox1.Focus();
        }

        protected void des_buscar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(500);
            getCartas(saldo);
            //CheckBox2.Checked = false;
            TextBox1.Focus();
            mensaje.Text = "";
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Hiding columns from Data Source   
                GridView1.HeaderRow.Cells[6].CssClass = "boundAgreg";
                e.Row.Cells[6].CssClass = "boundAgreg";
                GridView1.HeaderRow.Cells[7].CssClass = "boundAgreg";
                e.Row.Cells[7].CssClass = "boundAgreg";
                GridView1.HeaderRow.Cells[10].CssClass = "boundAgreg";
                e.Row.Cells[10].CssClass = "boundAgreg";
                GridView1.HeaderRow.Cells[11].CssClass = "boundAgreg";
                e.Row.Cells[11].CssClass = "boundAgreg";  //row_id column



                if (e.Row.Cells[6].Text.Contains("4") || e.Row.Cells[6].Text.Contains("5") || e.Row.Cells[6].Text.Contains("6") || e.Row.Cells[6].Text.Contains("7") || e.Row.Cells[6].Text.Contains("8"))
                {
                    TextBox tb = (TextBox)e.Row.FindControl("TextBox2");
                    tb.Visible = false;
                    Label lb = (Label)e.Row.FindControl("no_aplica");
                    lb.Visible = true;
                    lb.Text = "No aplica";
                    lb.CssClass = "no_aplica";
                }
            }
        }
        protected void btn_del_Click(object sender, EventArgs e)
        {
            bool userCheckbox = false;

            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    System.Web.UI.WebControls.CheckBox chkRow = (row.Cells[0].FindControl("seleccionar") as System.Web.UI.WebControls.CheckBox);

                    if (chkRow.Checked)
                    {
                        string row_id = row.Cells[11].Text;
                        var res = DbUtil.ExecuteProc("sp_delReiCartRowId",
                                       new SqlParameter("@row_id", row_id)
                                   );

                        userCheckbox = true;
                    }
                }
            }

            getCartas(saldo);

            if (userCheckbox == true)
            {
                mensaje.Text = "registros de cartas eliminados.";

            }
            else
            {
                mensaje.Text = "No hay registros de cartas seleccionados.";
            }
        }

        [WebMethod]
        public async void imprimir_cartas_Click(object sender, EventArgs e)
        {
            var isPrintSuccessful = false;
            bool isTxtVacio = false;
            bool isRecordSelected = false;

            string log_of_files_printed = "";
          
            List<SIE_KEY_USER.model.RequestFile> fileList = new List<SIE_KEY_USER.model.RequestFile>();


            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    System.Web.UI.WebControls.CheckBox chkRow = (row.Cells[0].FindControl("seleccionar") as System.Web.UI.WebControls.CheckBox);

                    TextBox txtbox = (row.Cells[1].FindControl("TextBox2") as TextBox);
                    Label lblSalario = (row.Cells[1].FindControl("no_aplica") as Label);
                    //System.Web.UI.WebControls.CheckBox checkBox = (System.Web.UI.WebControls.CheckBox)GridView1.FindControl("checkbox");



                    if (chkRow.Checked)
                    {
                        isRecordSelected = true;
                        var codigo = row.Cells[2].Text;

                        if (lblSalario.Text == "No aplica" || !(string.IsNullOrEmpty(txtbox.Text.Trim())))
                        {
                            var tipo_carta = row.Cells[5].Text;
                            var FecImp = row.Cells[7].Text;
                            var salario = txtbox.Text;
                            var requestID = Int32.Parse(row.Cells[11].Text);

                            string filePath = SendPrinter.GetFileByRequestID(requestID);
                            string fileName = Path.GetFileName(filePath);

                            //instanciatses a new Request file object and adds it to the list
                            SIE_KEY_USER.model.RequestFile file = new SIE_KEY_USER.model.RequestFile(requestID,tipo_carta,filePath,fileName,FecImp);
                            fileList.Add(file);
                            
                        }
                        else
                        {
                            isTxtVacio = true;
                            break;
                        }
                    }
                }
            }


            //printing files in SendPrinter.cs
            log_of_files_printed = await SendPrinter.PrintDocumentAsync(fileList.ToArray());




            panel_print_log.Visible = true;
            txtPrintLog.Visible = true; txtPrintLog.Style.Add("resize", "none");
            txtPrintLog.Text = log_of_files_printed;

            getCartas(saldo);
            TextBox1.Focus();

            if (isPrintSuccessful == true)
            {
                // CheckBox2.Checked = false;
                mensaje.Text = "Se mandaron impresiones.";
                mensaje.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                mensaje.ForeColor = System.Drawing.Color.Red;
                if (!isRecordSelected)
                {
                    // CheckBox2.Checked = false;
                    mensaje.Text = "No hay usuarios seleccionados.";
                }
                else if (isTxtVacio == true)
                {
                    // CheckBox2.Checked = false;
                    mensaje.Text = "Por favor completa el salario.";
                }
            }
        }
        
        public static void conversionPDF(string MyVarNum, string tipoCarta)
        {
            Random randNum = new Random();

            //Conversion de documento .docx a .pdf
            Application app = new Application();
            app.DisplayAlerts = WdAlertLevel.wdAlertsNone;
            var objPresSet = app.Documents;

            var objPres = objPresSet.Open(printFolderPath+@"\carta_" + MyVarNum + "_"+tipoCarta+".docx", Microsoft.Office.Core.MsoTriState.msoTrue, Microsoft.Office.Core.MsoTriState.msoTrue, Microsoft.Office.Core.MsoTriState.msoFalse);

            var pdfPath = string.Format(printFolderPath + @"\carta_" +MyVarNum+"_" +tipoCarta+ "_" + randNum.Next(10000, 99999) + ".pdf");

            try
            {
                objPres.ExportAsFixedFormat(pdfPath, WdExportFormat.wdExportFormatPDF, false, WdExportOptimizeFor.wdExportOptimizeForPrint, WdExportRange.wdExportAllDocument);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                objPres.Close();
            }
        }
      
        private bool CallPrintProcess(string TipoCarta, string codigo, string FecImp, string salario)
        {
            Session.Add("codigo", codigo);
            Session.Add("salario", salario);

            bool temp_return_result = false;
            bool existsConst = false;

            if (TipoCarta == "1")
            {
                Session.Add("path", "constanciaTrabajo");   
                fecha_cartas.GetFechaCartas();
                existsConst=constancia.constanciaTrabajo();
                if (existsConst)
                {
                    temp_return_result = true;//SendPrinter.imprimir(1);

                    if (temp_return_result)
                    {
                        var res = DbUtil.ExecuteProc("sp_cartasUpdate", new SqlParameter("@codigo", codigo), new SqlParameter("@fecha", FecImp));
                        //constancia.DelconstanciaTrabajo();
                    }
                }
               
            }

            if (TipoCarta == "2")
            {
                Session.Add("path", "migracion");
                fecha_cartas.GetFechaCartas();
                existsConst = constancia.migracion(0);
                if (existsConst)
                {
                    temp_return_result = true;//SendPrinter.imprimir(2);
                    if (temp_return_result)
                    {
                        var res = DbUtil.ExecuteProc("sp_cartasUpdate", new SqlParameter("@codigo", codigo), new SqlParameter("@fecha", FecImp));
                        //constancia.Delmigracion();
                    }
                }
            }

            if (TipoCarta == "3")
            {
                Session.Add("path", "visaLaser");
                fecha_cartas.GetFechaCartas();
                existsConst = constancia.visaLaser();
                if (existsConst)
                {
                    temp_return_result = true;//SendPrinter.imprimir(3);
                    if (temp_return_result)
                    {
                        var res = DbUtil.ExecuteProc("sp_cartasUpdate", new SqlParameter("@codigo", codigo), new SqlParameter("@fecha", FecImp));
                        //constancia.DelvisaLaser();
                    }
                }
            }

            if (TipoCarta == "4")
            {
                Session.Add("path", "altaimss");

                String Codigo = HttpContext.Current.Session["codigo"].ToString();
                string FileName =  string.Format(HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\altaimss\carta_" + Codigo + ".docx"));
                DocX template = DocX.Load(FileName);
                
                //Guardar el archivo en carpeta de impresion
                template.SaveAs(printFolderPath + @"\carta_" + Codigo  + "_altaImss_#" + randNum.Next(10000, 99999)+".docx");

                //Conversion de archivo .docx a .pdf
                //conversionPDF(Codigo, "altaImss");

                temp_return_result = true;//SendPrinter.imprimir(4);

                if (temp_return_result)
                {
                    var res = DbUtil.ExecuteProc("sp_cartasUpdate", new SqlParameter("@codigo", codigo), new SqlParameter("@fecha", FecImp));
                }
            }

            if (TipoCarta == "5")
            {
                Session.Add("path", "cambioTurno");

                String Codigo = HttpContext.Current.Session["codigo"].ToString();
                string FileName = string.Format(HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\cambioTurno\carta_" + Codigo + ".docx"));
                DocX template = DocX.Load(FileName);

                //Guardar el archivo en carpeta de impresion
                template.SaveAs(printFolderPath + @"\carta_" + Codigo +  "_cambioTurno_#" + randNum.Next(10000, 99999) + ".docx");

                //Conversion de archivo .docx a .pdf
                //conversionPDF(Codigo, "cambioTurno");

                temp_return_result = true;//SendPrinter.imprimir(5);
                if (temp_return_result)
                {
                    var res = DbUtil.ExecuteProc("sp_cartasUpdate", new SqlParameter("@codigo", codigo), new SqlParameter("@fecha", FecImp));
                }
            }

            if (TipoCarta == "6")
            {
                Session.Add("path", "cambioClinica");

                String Codigo = HttpContext.Current.Session["codigo"].ToString();
                string FileName = string.Format(HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\cambioClinica\carta_" + Codigo + ".docx"));
                DocX template = DocX.Load(FileName);

                //Guardar el archivo en carpeta de impresion
                template.SaveAs(printFolderPath + @"\carta_" + Codigo + "_cambioClinica_#" + randNum.Next(10000, 99999) + ".docx");

                //Conversion de archivo .docx a .pdf
                //conversionPDF(Codigo, "cambioClinica");

                temp_return_result = true;//SendPrinter.imprimir(6);
                if (temp_return_result)
                {
                    var res = DbUtil.ExecuteProc("sp_cartasUpdate", new SqlParameter("@codigo", codigo), new SqlParameter("@fecha", FecImp));
                }
            }

            if (TipoCarta == "7")
            {
                Session.Add("path", "ingresoGuarderia");

                String Codigo = HttpContext.Current.Session["codigo"].ToString();
                string FileName = string.Format(HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\ingresoGuarderia\carta_" + Codigo + ".docx"));
                DocX template = DocX.Load(FileName);

                //Guardar el archivo en carpeta de impresion
                template.SaveAs(printFolderPath + @"\carta_" + Codigo + "_ingresoGuarderia_#" + randNum.Next(10000, 99999) + ".docx");
                
                //Conversion de archivo .docx a .pdf
                //conversionPDF(Codigo, "ingresoGuarderia");

                temp_return_result = true;//SendPrinter.imprimir(7);
                if (temp_return_result)
                {
                    var res = DbUtil.ExecuteProc("sp_cartasUpdate", new SqlParameter("@codigo", codigo), new SqlParameter("@fecha", FecImp));
                }
            }

            if (TipoCarta == "8")
            {
                Session.Add("path", "vacacionesGuarderia");

                String Codigo = HttpContext.Current.Session["codigo"].ToString();
                string FileName = string.Format(HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\vacacionesGuarderia\carta_" + Codigo + ".docx"));
                DocX template = DocX.Load(FileName);

                //Guardar el archivo en carpeta de impresion
                template.SaveAs(printFolderPath + @"\carta_" + Codigo +"_vacacionesGuarderia_#" + randNum.Next(10000, 99999) + ".docx");

                //Conversion de archivo .docx a .pdf
                //conversionPDF(Codigo, "vacacionesGuarderia");

                temp_return_result = true;//SendPrinter.imprimir(8);
                if (temp_return_result)
                {
                    var res = DbUtil.ExecuteProc("sp_cartasUpdate", new SqlParameter("@codigo", codigo), new SqlParameter("@fecha", FecImp));
                }
            }

            return temp_return_result;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuKey.aspx");
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            //if (CheckBox2.Checked)
            //{
            //    saldo = 1;
            //    getCartas(saldo);
            //    //constancia_trabajo.Visible = true;
            //    //constancia_migracion.Visible = true;
            //    //constancia_visa.Visible = true;

            //}
            //else
            //{
            //    saldo = 0;
            //    getCartas(saldo);
            //    //constancia_trabajo.Visible = false;
            //    //constancia_migracion.Visible = false;
            //    //constancia_visa.Visible = false;
            //}
        }
        private void Download(string zipFilePath,string downloadFileName)
        {
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
                mensaje.Text = ex1.Message;
            }
           
            Response.End();
            
        }
        private void redirect()
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Reimpresion.aspx");  
        }
        protected void generar_Click(object sender, EventArgs e)
        {

            System.Threading.Thread.Sleep(200);
            Button button = (Button)sender;
            string ID = button.ID;
            GenerateCSVFiles(ID);
            ZipFilesCSV(strCSVFilesPath,ID);
            try
            {
                Download(hidden_lastZipFilePath.Value, hidden_lastZipFileName.Value);
            }
            catch (Exception ex)
            {
                mensaje.Text = ex.Message;
            }
            redirect();
            
           // //if (Session["lastZipFile"] != null)
           //// {
           //     lastZipFilePath = Session["lastZipFile"].ToString();
           //     hidden_lastZipFileName.Value = lastZipFilePath;
           // //}

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "download();", true);

           
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reimpresion_salario.aspx");  

        }

        //private static  void Print(int requestID) {
        //    var filePath = SendPrinter.GetFileByRequestID(requestID);

        //    SendPrinter.PrintDocument(@filePath);
           
        //    SendPrinter.CloseOpenWordDocuments();


        //}


    }
}