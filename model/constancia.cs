//-- =============================================
//--Author:		< >
//-- =====================================================================================================================
//--CODE        | NAME                                | MODIFIED DATE       | DESCRIPTION
//-- =====================================================================================================================
//-NA           Daniel Omar Mendoza Rodriguez 51105		05/11/2023			 added file copy validation


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Novacode;
using System.Data;
using MsBarco;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Word;
using System.Web.Providers.Entities;

namespace SIE_KEY_USER.model
{
    public class constancia
    {

        //Si fallan las impresiones:
        //                                    
        //    1. Hacer ping a la impresora de RH
        //    2. Verificar que cree las cartas en \\mxjrznas01\Reports\SIE\Cartas
        //--------------------------------------------------------------------------

        static string printFolderPath = @"C:\SIEPrintCartas";
        /*
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
        }*/

        public static bool constanciaTrabajo()
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                String MyVarFecha = HttpContext.Current.Session["fecha"].ToString();
                String MyVarNum = HttpContext.Current.Session["codigo"].ToString();
                String MyVarSalario = HttpContext.Current.Session["salario"].ToString(); 
                String MyVarRoute = HttpContext.Current.Session["route"].ToString();


                string fecha = MyVarFecha;
                string salario = MyVarSalario;
                string route = MyVarRoute;


                var info = MsBarco.DbUtil.ExecuteProc("sp_getInfo_cartasSalario",
                   new SqlParameter("@codigo", MyVarNum),
                   MsBarco.DbUtil.NewSqlParam("@nombres", null, SqlDbType.VarChar, ParameterDirection.Output, 93),
                   MsBarco.DbUtil.NewSqlParam("@fecha_ing", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                   MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                   MsBarco.DbUtil.NewSqlParam("@apellido", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                   MsBarco.DbUtil.NewSqlParam("@imss", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                   MsBarco.DbUtil.NewSqlParam("@rfc", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                   MsBarco.DbUtil.NewSqlParam("@horario", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                   MsBarco.DbUtil.NewSqlParam("@clasificacion", null, SqlDbType.Int, ParameterDirection.Output, 4)
                   );

                try
                {
                    string TemplateFileName = (MyVarRoute);
                    string TemplateFileNameDir = Path.GetDirectoryName(TemplateFileName);
                    string TemplateFileNameR = Path.GetFileNameWithoutExtension(TemplateFileName);
                    //New path to specify that salary has been replaced
                    string reprintFile = TemplateFileNameDir +"\\"+ TemplateFileNameR + "_Replaced.docx";

                    
                    //Saves a new copy of the file with employee's information and the empty field of [salario] with the salary entered by user.
                    if (File.Exists(reprintFile)) {
                        File.Delete(reprintFile);
                    }
                    File.Copy(TemplateFileName, reprintFile);


                    DocX template = DocX.Load(reprintFile);
                    template.ReplaceText("[SALARIO]", salario, false, System.Text.RegularExpressions.RegexOptions.Singleline, null, null, MatchFormattingOptions.ExactMatch);
                    template.ReplaceText("[FECHA]", fecha, false, System.Text.RegularExpressions.RegexOptions.Singleline, null, null, MatchFormattingOptions.ExactMatch);
                    //TemplateFileName = HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\constanciaTrabajo\carta_" + MyVarNum + "_final.docx");
                    template.SaveAs(reprintFile);

                    ////Guardar el archivo en carpeta de impresion
                    //Random randNum = new Random();

                    ////Guardar el archivo en carpeta de impresion
                    //template.SaveAs(printFolderPath + @"\carta_" + MyVarNum + "_constanciaTrabajo_final_#" + randNum.Next(10000, 99999) + ".docx");

                    //Conversion de archivo .docx a .pdf
                    //conversionPDF(MyVarNum, "constanciaTrabajo_final");

                    //Guardar el archivo en carpeta temporal de impresion
                    //string newprint = HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\SIEPrintCartas\carta_" + MyVarNum + "_constanciaTrabajo_final.docx");
                    //template.SaveAs(newprint);
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void DelconstanciaTrabajo()
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                String MyVarFecha = HttpContext.Current.Session["fecha"].ToString();
                String MyVarNum = HttpContext.Current.Session["codigo"].ToString();
                String MyVarSalario = HttpContext.Current.Session["salario"].ToString();

                string fecha = MyVarFecha;
                string salario = MyVarSalario;

                try
                {
                   
                    string delFile = HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\constanciaTrabajo\carta_" + MyVarNum + ".docx");
                    System.IO.File.Delete(delFile);
                }
                catch (Exception ex)
                {
                    
                }
                
            }
        }
        public static bool DelconstanciaTrabajo(string filePath) {
            try
            {
                if (!File.Exists(filePath))
                {
                    return false;
                }
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }
        public static bool migracion(int rowId)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                String MyVarFecha = HttpContext.Current.Session["fecha"].ToString();
                //String MyVarNum = HttpContext.Current.Session["numero"].ToString();
                String MyVarNum = HttpContext.Current.Session["codigo"].ToString();
                String MyVarSalario = HttpContext.Current.Session["salario"].ToString();
                String MyVarRoute = HttpContext.Current.Session["route"].ToString();

                // Obetener los parametros
                string fecha = MyVarFecha;
                string salario = MyVarSalario;
                string route = MyVarRoute;


                var destino = MsBarco.DbUtil.ExecuteProc("sp_getDestino",
                   new SqlParameter("@rowId", rowId),
                   MsBarco.DbUtil.NewSqlParam("@Destino", null, SqlDbType.VarChar, ParameterDirection.Output, 100));




                var info = MsBarco.DbUtil.ExecuteProc("sp_getInfo_cartasSalario",
                   new SqlParameter("@codigo", MyVarNum),
                   MsBarco.DbUtil.NewSqlParam("@nombres", null, SqlDbType.VarChar, ParameterDirection.Output, 93),
                   MsBarco.DbUtil.NewSqlParam("@fecha_ing", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                   MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                   MsBarco.DbUtil.NewSqlParam("@apellido", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                   MsBarco.DbUtil.NewSqlParam("@imss", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                   MsBarco.DbUtil.NewSqlParam("@rfc", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                   MsBarco.DbUtil.NewSqlParam("@horario", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                   MsBarco.DbUtil.NewSqlParam("@clasificacion", null, SqlDbType.Int, ParameterDirection.Output, 4)
                   );


                
                // Obtener el archivo o la plantilla
                //string TemplateFileName = @"\\mxjrznas01\Reports\SIE\cartas\cambioTurno\carta.docx";
                //string TemplateFileName = HttpContext.Current.Server.MapPath("/Virtual") + @"\cartas\cambioTurno\carta.docx";
                try
                {
                    string TemplateFileName = (MyVarRoute);
                    string TemplateFileNameDir = Path.GetDirectoryName(TemplateFileName);
                    string TemplateFileNameR = Path.GetFileNameWithoutExtension(TemplateFileName);
                    //New path to specify that salary has been replaced

                    string reprintFile = TemplateFileNameDir + "\\" + TemplateFileNameR + "_Replaced.docx";

                    //Saves a new copy of the file with employee's information and the empty field of [salario] with the salary entered by user.

                    if (File.Exists(reprintFile))
                    {
                        File.Delete(reprintFile);
                    }
                    File.Copy(TemplateFileName, reprintFile);
                    DocX template = DocX.Load(TemplateFileName);

                    // Reemplazar macros con los parametros

                    template.ReplaceText("[SALARIO]", salario, false, System.Text.RegularExpressions.RegexOptions.Singleline, null, null, MatchFormattingOptions.ExactMatch);
                    template.ReplaceText("[FECHA]", fecha, false, System.Text.RegularExpressions.RegexOptions.Singleline, null, null, MatchFormattingOptions.ExactMatch);

                    // Guardar el archivo con un nombre nuevo
                    /********************************* CARGAR CONSTANCIA DIRECTORIO VIRTUAL *******************************/
                    //TemplateFileName = HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\migracion\carta_" + MyVarNum + "_final.docx");
                    template.SaveAs(reprintFile);

                    ////Guardar el archivo en carpeta de impresion
                    //Random randNum = new Random();

                    ////Guardar el archivo en carpeta de impresion
                    //template.SaveAs(printFolderPath + @"\carta_" + MyVarNum +"_migracion_final_#" + randNum.Next(10000, 99999) + ".docx");

                    //Conversion de archivo .docx a .pdf
                    //conversionPDF(MyVarNum, "migracion_final");

                    //Guardar el archivo en carpeta temporal de impresion
                    //string newprint = HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\SIEPrintCartas\carta_" + MyVarNum + "_migracion_final.docx");
                    //template.SaveAs(newprint);

                    return true;
                }
                catch (Exception ex)
                { 
                    return false;
                }
                return true;


                /******************** Acceder a un directorio localhost ******************/
                //string newFile = string.Format(@"\\mxjrznas01\Reports\SIE\cartas\cambioTurno\carta_{0}.docx", MyVarNum);
                //template.SaveAs(newFile);
            }
            else
            {
                return false;
            }
        }

        public static void Delmigracion()
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                String MyVarFecha = HttpContext.Current.Session["fecha"].ToString();
                //String MyVarNum = HttpContext.Current.Session["numero"].ToString();
                String MyVarNum = HttpContext.Current.Session["codigo"].ToString();
                String MyVarSalario = HttpContext.Current.Session["salario"].ToString();

                // Obetener los parametros
                string fecha = MyVarFecha;
                string salario = MyVarSalario;
                // Obtener el archivo o la plantilla
                //string TemplateFileName = @"\\mxjrznas01\Reports\SIE\cartas\cambioTurno\carta.docx";
                //string TemplateFileName = HttpContext.Current.Server.MapPath("/Virtual") + @"\cartas\cambioTurno\carta.docx";
                try
                {
                    string newFile = HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\migracion\carta_" + MyVarNum + ".docx");
                    System.IO.File.Delete(newFile);
                }
                catch (Exception ex)
                {
                   
                }
                


                /******************** Acceder a un directorio localhost ******************/
                //string newFile = string.Format(@"\\mxjrznas01\Reports\SIE\cartas\cambioTurno\carta_{0}.docx", MyVarNum);
                //template.SaveAs(newFile);
            }
            else
            {
                
            }
        }

        public static bool visaLaser()
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                
                String MyVarFecha = HttpContext.Current.Session["fecha"].ToString();
                //String MyVarNum = HttpContext.Current.Session["numero"].ToString();
                String MyVarNum = HttpContext.Current.Session["codigo"].ToString();
                String MyVarSalario = HttpContext.Current.Session["salario"].ToString();
                String MyVarRoute = HttpContext.Current.Session["route"].ToString();

                // Obetener los parametros
                string fecha = MyVarFecha;
                string salario = MyVarSalario;
                string route = MyVarRoute;

                var info = MsBarco.DbUtil.ExecuteProc("sp_getInfo_cartasSalario",
                   new SqlParameter("@codigo", MyVarNum),
                   MsBarco.DbUtil.NewSqlParam("@nombres", null, SqlDbType.VarChar, ParameterDirection.Output, 93),
                   MsBarco.DbUtil.NewSqlParam("@fecha_ing", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                   MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                   MsBarco.DbUtil.NewSqlParam("@apellido", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                   MsBarco.DbUtil.NewSqlParam("@imss", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                   MsBarco.DbUtil.NewSqlParam("@rfc", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                   MsBarco.DbUtil.NewSqlParam("@horario", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                   MsBarco.DbUtil.NewSqlParam("@clasificacion", null, SqlDbType.Int, ParameterDirection.Output, 4)
                   );



                try
                {
                    //string TemplateFileName = HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\visaLaser\carta_" + MyVarNum + ".docx");
                    string TemplateFileName = (MyVarRoute);
                    string TemplateFileNameDir = Path.GetDirectoryName(TemplateFileName);
                    string TemplateFileNameR = Path.GetFileNameWithoutExtension(TemplateFileName);
                    //New path to specify that salary has been replaced
                    string reprintFile = TemplateFileNameDir + "\\" + TemplateFileNameR + "_Replaced.docx";
                    //Saves a new copy of the file with employee's information and the empty field of [salario] with the salary entered by user.
                    if (File.Exists(reprintFile))
                    {
                        File.Delete(reprintFile);
                    }
                    File.Copy(TemplateFileName, reprintFile);
                    DocX template = DocX.Load(TemplateFileName);

                    // Reemplazar macros con los parametros
                    template.ReplaceText("[SALARIO]", salario, false, System.Text.RegularExpressions.RegexOptions.Singleline, null, null, MatchFormattingOptions.ExactMatch);
                    template.ReplaceText("[FECHA]", fecha, false, System.Text.RegularExpressions.RegexOptions.Singleline, null, null, MatchFormattingOptions.ExactMatch);
                    // Guardar el archivo con un nombre nuevo
                    /********************************* CARGAR CONSTANCIA DIRECTORIO VIRTUAL *******************************/
                    template.SaveAs(reprintFile);

                    //Guardar el archivo en carpeta de impresion
                    //Random randNum = new Random();

                    ////Guardar el archivo en carpeta de impresion
                    //template.SaveAs(printFolderPath + @"\carta_" + MyVarNum + "_visaLaser_final_#" + randNum.Next(10000, 99999) + ".docx");

                    ////Conversion de archivo .docx a .pdf
                    ////conversionPDF(MyVarNum, "visaLaser_final");

                    //Guardar el archivo en carpeta temporal de impresion
                    //string newprint = HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\SIEPrintCartas\carta_" + MyVarNum + "_visaLaser_final.docx");
                    //template.SaveAs(newprint);
                }
                catch (Exception ex)
                { return false; }
                return true;

                /******************** Acceder a un directorio localhost ******************/
                //string newFile = string.Format(@"\\mxjrznas01\Reports\SIE\cartas\cambioTurno\carta_{0}.docx", MyVarNum);
                //template.SaveAs(newFile);
            }
            else
            { return false; }
        }

        public static void DelvisaLaser()
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                String MyVarFecha = HttpContext.Current.Session["fecha"].ToString();
                //String MyVarNum = HttpContext.Current.Session["numero"].ToString();
                String MyVarNum = HttpContext.Current.Session["codigo"].ToString();
                String MyVarSalario = HttpContext.Current.Session["salario"].ToString();

                // Obetener los parametros
                string fecha = MyVarFecha;
                string salario = MyVarSalario;

                // Obtener el archivo o la plantilla
                //string TemplateFileName = @"\\mxjrznas01\Reports\SIE\cartas\cambioTurno\carta.docx";
                //string TemplateFileName = HttpContext.Current.Server.MapPath("/Virtual") + @"\cartas\cambioTurno\carta.docx";

                try
                {
                   
                    // Guardar el archivo con un nombre nuevo
                    /********************************* CARGAR CONSTANCIA DIRECTORIO VIRTUAL *******************************/
                    string newFile = HttpContext.Current.Server.MapPath(@"~\Virtual\cartas\visaLaser\carta_" + MyVarNum + ".docx");
                    System.IO.File.Delete(newFile);
                }
                catch (Exception ex)
                {  }
                

                /******************** Acceder a un directorio localhost ******************/
                //string newFile = string.Format(@"\\mxjrznas01\Reports\SIE\cartas\cambioTurno\carta_{0}.docx", MyVarNum);
                //template.SaveAs(newFile);
            }
            else
            {  }
        }
    }
}