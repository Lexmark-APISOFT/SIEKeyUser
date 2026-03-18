//-- =============================================
//--Author:	
//-- =====================================================================================================================
//--CODE        | NAME                                | MODIFIED DATE       | DESCRIPTION
//-- =====================================================================================================================
//-NA           Fernando Contreras Contreras 51102		05/11/2023			Restructured class
//-NA           Daniel Omar Mendoza Rodriguez 51105		05/11/2023			Added retuned log message on printing method



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics; // Referencia para process start info
using System.Data;
using System.Data.SqlClient;
using MsBarco;

//using Spire.Doc;
//using Spire.Doc.Documents;

using Microsoft.Office.Interop.Word;
using Microsoft.Office.Core;

using System.IO;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq.Expressions;
using System.Web.Services;
using SIE_KEY_USER.model;
using Microsoft.Ajax.Utilities;

namespace SIE.model
{
    public class SendPrinter
    {
        private readonly string printerName = "";
        private readonly string printerAddress = "";

        //main method to print form the print queue

        //NOT USED
        public static async void Print()
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                string filePath = getFilesFromQueue();
                System.Threading.Tasks.Task printFile = PrintDocumentAsync(@filePath);
                await printFile;

                //executing after finishing printFile task
                CloseOpenWordDocuments();
            }
        }

        //method to print individually given a filepath 
        //NOT USED
        public static System.Threading.Tasks.Task PrintDocumentAsync(string filePath)
        {
            var tcs = new System.Threading.Tasks.TaskCompletionSource<object>();
            //set the printer             //runs the task of printing as an asynchronus task
            System.Threading.Tasks.Task.Run(() => {
                try
                {
                    Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                    Microsoft.Office.Interop.Word.Document wordFile = wordApp.Documents.Open(filePath);
                    wordFile.PrintOut();
                    wordFile.Close();
                    wordApp.Quit();
                    tcs.SetResult(null);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("El archivo no se encontro " + ex);
                }
            });
            return tcs.Task;
        }

        ////print an array of given file paths
        [WebMethod]
        public static string PrintDocumentsAsync(SIE_KEY_USER.model.RequestFile[] filePaths)
        {
            string log = "";
            //var printTasks = new System.Threading.Tasks.Task[filePaths.Length];
            for (int i = 0; i < filePaths.Length; i++)
            {

                //printTasks[i] = System.Threading.Tasks.Task.Run(() => log = log + PrintDocument(filePaths[i]));
                log += PrintDocument(filePaths[i]);
                CloseOpenWordDocuments();
                //System.Threading.Thread.Sleep(500);

            }
            //await System.Threading.Tasks.Task.WhenAll(printTasks);
            return log;
        }
        ////print an array of given file paths
        //private static System.Threading.Tasks.Task<bool> PrintDocumentsAsync(string[] filePaths)
        //{
        //    try
        //    {
        //        //set the printer 
        //        //wordApp.ActivePrinter = $@"\\10.150.90.51\MyPrinter";
        //        return System.Threading.Tasks.Task.Run<bool>(() => {
        //            foreach (string filePath in filePaths)
        //            {
        //                Application wordApp = new Application();
        //                Document wordFile = wordApp.Documents.Open(filePath.ToString());
        //                wordFile.PrintOut();
        //                wordFile.Close();
        //                wordApp.Quit();
        //            }
        //            return true;
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //From a stored procedure checks for the next file to print
        private static string getFilesFromQueue()
        {
            int requestID = Int32.Parse(HttpContext.Current.Session["folio"].ToString());
            //getting files in the print queue
            var filePath = DbUtil.ExecuteProc("sp_getFilesFromQueue",
                         new SqlParameter("@folio", requestID),
                        MsBarco.DbUtil.NewSqlParam("@path", null, SqlDbType.NVarChar, ParameterDirection.Output, 500)
                         );
            return filePath["@path"].ToString(); //returns the result of the stored procedure excecution that is the path 
        }

        //get an specific request file path given the request id
        public static string GetFileByRequestID(int requestID)
        {
            //getting files in the print queue calling a stored procedure 
            var filePath = DbUtil.ExecuteProc("sp_getFilesFromQueue",
                         new SqlParameter("@folio", requestID),// passing request id as an argument 
                        MsBarco.DbUtil.NewSqlParam("@path", null, SqlDbType.NVarChar, ParameterDirection.Output, 500)
                         );
            return filePath["@path"].ToString(); //and returning the resulting path
        }
        //NOT USED
        //Kill every Word Document process that is running to avoid errors
        public static void CloseOpenWordDocuments()
        {
            Process[] processes = Process.GetProcessesByName("WINWORD"); //look up all winword process'

            foreach (var process in processes)
            {
                process.Kill(); //killing every process
            }
        }

        //public static void imprimir()
        //{
        //    if (!string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
        //    {
        //        String MyVarNum = HttpContext.Current.Session["numero"].ToString();
        //        String MyVarPath = HttpContext.Current.Session["path"].ToString();

        //        //try
        //        //{

        //        Application app = new Application();
        //        app.DisplayAlerts = WdAlertLevel.wdAlertsNone;
        //        //app.Visible = true;

        //        var objPresSet = app.Documents;
        //        var objPres = objPresSet.Open(@"\\mxjrzapp04\Applications\SIE\cartas\History\{0}\{1}.docx", MyVarPath, MyVarNum);

        //        //    //var pdfFileName = Path.ChangeExtension(string.Format(HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\{0}\carta_{1}"), MyVarPath, MyVarNum), ".pdf");
        //        //    //var pdfPath = string.Format(HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\{0}\carta_{1}.pdf"), MyVarPath, MyVarNum);
        //        //    //FileInfo wordFile = new FileInfo(string.Format(HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\{0}\carta_{1}.docx"), MyVarPath, MyVarNum));
        //        //    //object fileObject = wordFile.FullName;
        //        //    //object oMissing = System.Reflection.Missing.Value;

        //        //    //Microsoft.Office.Interop.Word.Document doc = app.Documents.Open(string.Format(HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\{0}\carta_{1}.docx"), MyVarPath, MyVarNum));
        //        //    //Microsoft.Office.Interop.Word.Document doc = app.Documents.Open(ref fileObject, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

        //        //    /*
        //        //    var res = DbUtil.ExecuteProc("sp_getImpresora",
        //        //    MsBarco.DbUtil.NewSqlParam("@servidor", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
        //        //    MsBarco.DbUtil.NewSqlParam("@impresora", null, SqlDbType.VarChar, ParameterDirection.Output, 40)
        //        //    );
        //        //    var nombreImp = res["@servidor"].ToString();
        //        //    var ipImp = res["@impresora"].ToString();
        //        //    objPres.Application.ActivePrinter = nombreImp + " on " + ipImp;*/
        //        //    /*
        //        //    String installedPrinters;
        //        //    for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
        //        //    {
        //        //        installedPrinters = PrinterSettings.InstalledPrinters[i];
        //        //        System.Console.WriteLine(installedPrinters);
        //        //    }*/
        //        //    objPres.Application.ActivePrinter = "MXJRZCO2PCM01 on 10.190.10.51";
        //        //    //objPres.Application.ActivePrinter = "Lexmark Universal v2 XL on 10.190.10.51";
        //        //    //objPres.Application.ActivePrinter = "LPM Cloud - PCLXL on LPM Server Port";
        //        //    //objPres.Application.ActivePrinter = "Microsoft XPS Document Writer, Microsoft Print to PDF on PORTPROMPT:";
        //        //    //objPres.Activate();
        //        //    objPres.PrintOut();
        //        //    //objPres.PrintOut(oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
        //        //    try
        //        //    {
        //        //        //objPres.ExportAsFixedFormat(pdfPath, WdExportFormat.wdExportFormatPDF, false, WdExportOptimizeFor.wdExportOptimizeForPrint, WdExportRange.wdExportAllDocument);
        //        //    }
        //        //    catch
        //        //    {
        //        //        //pdfPath = null;
        //        //    }
        //        //    finally
        //        //    {
        //        //        objPres.Close();
        //        //        //app.Visible = false;
        //        //    }
        //        //}
        //        //catch (Exception ex)
        //        //{ throw ex; }

        //        try
        //        {
        //            objPres.Application.ActivePrinter = "MXJRZCO2PCM01 on 10.190.10.51";
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("" + ex);
        //            //throw new InvalidOperationException("",ex);
        //        }
        //        objPres.PrintOut();
        //        objPres.Close();
        //        CloseOpenWordDocuments();
        //    }
        //}

        // main method of printig based on the print queue 
        //method to print individually given a filepath sinchronouslly 

        //not used
        public static string PrintDocument(RequestFile file)
        {

            try
            {
                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document wordFile = wordApp.Documents.Open(file.filePath);
                wordFile.PrintOut();
                wordFile.Close();
                wordApp.Quit();
                return "El archivo con ID de solicitud : " + file.requestId + " fue impreso con exito , carta de tipo " + file.tipoCarta + " \n";
            }
            catch (Exception ex)
            {
                return "El archivo con ID de solicitud : " + file.requestId + " no se pudo imprimir debido a \n" + ex.Message;
            }

        }



        //Async method to print an array of files that returns a log message of each printed file
        public static async System.Threading.Tasks.Task<string> PrintDocumentAsync(RequestFile[] files)
        {
            string log = "";     
            //runs the task of printing as an asynchronus task
            foreach (RequestFile file in files)
            {
                //Creates a new task of an word instance to print for every Request File in the array
                await System.Threading.Tasks.Task.Run(() => {
                    try
                    {
                        Application wordApp = new Application();
                        Document wordFile = wordApp.Documents.Open(file.filePath);
                        wordFile.PrintOut();
                        wordFile.Close();
                        wordApp.Quit();

                        //Print a final message about state of each task
                        log += "El archivo con ID de solicitud : " + file.requestId + " fue impreso con exito , carta de tipo " + file.tipoCarta + " \n";
                    }
                    catch (Exception ex)
                    {
                        //Print a final message about state of each task
                        log += "El archivo con ID de solicitud : " + file.requestId + " no se pudo imprimir debido a " + ex.Message;

                    }
                });
            }
            return log;
        }

    }
}




//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Diagnostics; // Referencia para process start info
//using System.Data;
//using System.Data.SqlClient;
//using MsBarco;
//using Microsoft.Office.Interop.Word;


//using System.IO;
//using System.Drawing;
//using System.Drawing.Printing;
//using Microsoft.Office.Core;

//namespace SIE_KEY_USER.model
//{
//    public class SendPrinter
//    {
//        private string printerServer;
//        private string printerName;

//        // setting up Constructor
//        public SendPrinter()
//        {
//            try
//            {
//                var res = DbUtil.ExecuteProc("sp_getImpresora",
//                    MsBarco.DbUtil.NewSqlParam("@servidor", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
//                    MsBarco.DbUtil.NewSqlParam("@impresora", null, SqlDbType.VarChar, ParameterDirection.Output, 40)
//                    );

//                printerServer = res["@servidor"].ToString();
//                printerName = res["@impresora"].ToString();
//            }
//            catch (Exception)
//            {

//                //throw;
//            }


//        }

//        public static bool imprimir(int tipo_carta)
//        {
//            if (!string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
//            {
//                String Codigo = HttpContext.Current.Session["codigo"].ToString();
//                String Path = HttpContext.Current.Session["path"].ToString();
//                try
//                {

//                    Application app = new Application();
//                    app.DisplayAlerts = WdAlertLevel.wdAlertsNone;
//                    //app.Visible = true;

//                    string docPath="";

//                    if (tipo_carta > 0 && tipo_carta < 4)
//                    {
//                        docPath = string.Format(HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\{0}\carta_{1}_final.docx"), Path, Codigo);
//                    }
//                    else
//                    {
//                        docPath = string.Format(HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\{0}\carta_{1}.docx"), Path, Codigo);
//                    }

//                    var objPresSet = app.Documents;
//                    var objPres = objPresSet.Open(docPath, MsoTriState.msoTrue, MsoTriState.msoTrue, MsoTriState.msoFalse);

//                    //var pdfFileName = System.IO.Path.ChangeExtension(string.Format(HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\{0}\carta_{1}"), Path, Codigo), ".pdf");
//                    //var pdfPath = string.Format(HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\{0}\carta_{1}.pdf"), Path, Codigo);


//                    //FileInfo wordFile = new FileInfo(string.Format(HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\{0}\carta_{1}.docx"), MyVarPath, MyVarNum));
//                    //object fileObject = wordFile.FullName;
//                    //object oMissing = System.Reflection.Missing.Value;


//                    //Microsoft.Office.Interop.Word.Document doc = app.Documents.Open(string.Format(HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\{0}\carta_{1}.docx"), MyVarPath, MyVarNum));
//                    //Microsoft.Office.Interop.Word.Document doc = app.Documents.Open(ref fileObject, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

//                    /*var res = DbUtil.ExecuteProc("sp_getImpresora",
//                   MsBarco.DbUtil.NewSqlParam("@servidor", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
//                   MsBarco.DbUtil.NewSqlParam("@impresora", null, SqlDbType.VarChar, ParameterDirection.Output, 40)
//                   );

//                    var servidor = res["@servidor"].ToString();
//                    var impresora = res["@impresora"].ToString();

//                    objPres.Application.ActivePrinter = servidor + " on " + impresora;*/

//                    objPres.Application.ActivePrinter = "MXJRZCO2PCM01 on 10.190.10.51";
//                    //objPres.Application.ActivePrinter = "Lexmark Universal v2 XL on 10.190.10.51";
//                    //objPres.Application.ActivePrinter = "MXJRZCO2PCM01 on 10.190.10.51";
//                    //objPres.Application.ActivePrinter = "LPM Cloud - PCLXL on LPM Server Port";
//                    //objPres.Application.ActivePrinter = "Microsoft XPS Document Writer, Microsoft Print to PDF on PORTPROMPT:";

//                    //objPres.Activate();
//                    objPres.PrintOut();
//                    //objPres.PrintOut(oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);


//                    try
//                    {
//                        //objPres.ExportAsFixedFormat(pdfPath, WdExportFormat.wdExportFormatPDF, false, WdExportOptimizeFor.wdExportOptimizeForPrint, WdExportRange.wdExportAllDocument);
//                    }
//                    catch
//                    {
//                        //pdfPath = null;
//                    }
//                    finally
//                    {
//                        objPres.Close();
//                        //app.Visible = false;
//                    }

//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                    return false;
//                }

//                return true;
//            }
//            else
//                return false;
//        }

//        internal static bool imprimir()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
