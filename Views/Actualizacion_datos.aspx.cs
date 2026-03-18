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
    public partial class actualizacion_datos : System.Web.UI.Page
    {
        private string lastZipFilePath;
        private string strCSVFilesPath;
        private string strTemp="";

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                String MyvarNom = Session["nombre"].ToString();
                String MyVarNum = Session["numero"].ToString();

                nombre.Text = MyvarNom;

                strCSVFilesPath = Server.MapPath(@"~\VirtEncr\").ToString();

                GetGenerarDatos();

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
            

            
            /* running directly, not logging in */
            ////String MyvarNom = Session["nombre"].ToString();
            ////String MyVarNum = Session["numero"].ToString();
            //String MyvarNom = "Ruuul2";
            //String MyVarNum = "41959";

            //nombre.Text = MyvarNom;

            //strCSVFilesPath = Server.MapPath(@"~\VirtEncr\").ToString();

            //GetGenerarDatos();


        }

        private string onlyNumbers(string str)
        {
            string res="";

            for(int i=0; i < str.Length; i++)
            {
                if (((int)str[i]>47) && ((int)str[i] < 58))
                {
                    res = res + str[i].ToString();
                }
            }
            
            return res;
        }

        private void GenerateCSVFiles()
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

                // generating new csv files
                //TextWriter clinica = new StreamWriter(strCSVFilesPath + @"clinica.csv");
                TextWriter calle = new StreamWriter(strCSVFilesPath + @"calle_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".csv");
                TextWriter num_int = new StreamWriter(strCSVFilesPath + @"num_int_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".csv");
                TextWriter num_ext = new StreamWriter(strCSVFilesPath + @"num_ext_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".csv");
                TextWriter colonia = new StreamWriter(strCSVFilesPath + @"colonia_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".csv");
                TextWriter telefono = new StreamWriter(strCSVFilesPath + @"telefono_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".csv");
                TextWriter celular = new StreamWriter(strCSVFilesPath + @"celular_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".csv");
                TextWriter celular_Emp = new StreamWriter(strCSVFilesPath + @"celular_Emp_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".csv");
                TextWriter correo = new StreamWriter(strCSVFilesPath + @"correo_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".csv");
                TextWriter vivecon = new StreamWriter(strCSVFilesPath + @"vivecon_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".csv");
                TextWriter viveen = new StreamWriter(strCSVFilesPath + @"viveen_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".csv");
                TextWriter transporte = new StreamWriter(strCSVFilesPath + @"transporte_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".csv");
                TextWriter talla = new StreamWriter(strCSVFilesPath + @"talla_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".csv");
                TextWriter contacto = new StreamWriter(strCSVFilesPath + @"contacto_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".csv");
                TextWriter relacion = new StreamWriter(strCSVFilesPath + @"relacion_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".csv");
                TextWriter telcontacto = new StreamWriter(strCSVFilesPath + @"telcontacto_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".csv");
                TextWriter telemp = new StreamWriter(strCSVFilesPath + @"telemp_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".csv");
                TextWriter extension = new StreamWriter(strCSVFilesPath + @"extension_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".csv");
                TextWriter paisresiden = new StreamWriter(strCSVFilesPath + @"paisresiden_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".csv");

                string  strCalle = "", strNum_ext = "", strNum_int = "", strColonia = "", strTelefono = "", strCelular = "", strCorreo = "", strVivecon = "",
                    strViveen = "", strTransporte = "", strTalla = "", strContacto = "", strRelacion = "", strTelcontacto = "", strCelEmp="",strTelemp="",strExtension="",strPaisresiden="";

                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        DateTime dt = DateTime.Parse(row.Cells[15].Text.ToString());
                        string strCodigoEmpleado = row.Cells[0].Text.ToString().Trim();

                        //strClinica = !string.IsNullOrEmpty(row.Cells[1].Text.Replace("&nbsp;", "").Trim()) ? strClinica + string.Format(strCodigoEmpleado + ",10," + "\"" +
                        //    dt.ToString("dd/MM/yyyy") + "\",\"CB_CLINICA\"," + row.Cells[1].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act clinica\"{0}", Environment.NewLine) : strClinica;

                        //strCalle = !string.IsNullOrEmpty(row.Cells[2].Text.Replace("&nbsp;", "").Trim()) ? strCalle + string.Format(strCodigoEmpleado + ",10," + "\"" +
                        //    dt.ToString("dd/MM/yyyy") + "\",\"CB_CALLE\"," + row.Cells[2].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act calle\"{0}", Environment.NewLine) : strCalle;

                        //strNum_ext = !string.IsNullOrEmpty(row.Cells[3].Text.Replace("&nbsp;", "").Trim()) ? strNum_ext + string.Format(strCodigoEmpleado + ",10," + "\"" +
                        //    dt.ToString("dd/MM/yyyy") + "\",\"CB_NUM_EXT\"," + row.Cells[3].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act exterior\"{0}", Environment.NewLine) : strNum_ext;

                        //strNum_int = !string.IsNullOrEmpty(row.Cells[4].Text.Replace("&nbsp;", "").Trim()) ? strNum_int + string.Format(strCodigoEmpleado + ",10," + "\"" +
                        //    dt.ToString("dd/MM/yyyy") + "\",\"CB_NUM_INT\"," + row.Cells[4].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act interior\"{0}", Environment.NewLine) : strNum_int;

                        //strColonia = !string.IsNullOrEmpty(row.Cells[5].Text.Replace("&nbsp;", "").Trim()) ? strColonia + string.Format(strCodigoEmpleado + ",10," + "\"" +
                        //    dt.ToString("dd/MM/yyyy") + "\",\"CB_COLONIA\"," + row.Cells[5].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act colonia\"{0}", Environment.NewLine) : strColonia;

                        //strTelefono = !string.IsNullOrEmpty(row.Cells[6].Text.Replace("&nbsp;", "").Trim()) ? strTelefono + string.Format(strCodigoEmpleado + ",10," + "\"" +
                        //    dt.ToString("dd/MM/yyyy") + "\",\"CB_TEL\"," + row.Cells[6].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act telefono\"{0}", Environment.NewLine) : strTelefono;

                        //strCelular = !string.IsNullOrEmpty(row.Cells[7].Text.Replace("&nbsp;", "").Trim()) ? strCelular + string.Format(strCodigoEmpleado + ",10," + "\"" +
                        //    dt.ToString("dd/MM/yyyy") + "\",\"CB_G_NUM_8\"," + row.Cells[7].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act celular\"{0}", Environment.NewLine) : strCelular;

                        //strCorreo = !string.IsNullOrEmpty(row.Cells[8].Text.Replace("&nbsp;", "").Trim()) ? strCorreo + string.Format(strCodigoEmpleado + ",10," + "\"" +
                        //    dt.ToString("dd/MM/yyyy") + "\",\"CB_E_MAIL\"," + row.Cells[8].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act correo electronico personal\"{0}", Environment.NewLine) : strCorreo;

                        //strVivecon = !string.IsNullOrEmpty(row.Cells[9].Text.Replace("&nbsp;", "").Trim()) ? strVivecon + string.Format(strCodigoEmpleado + ",10," + "\"" +
                        //    dt.ToString("dd/MM/yyyy") + "\",\"CB_VIVECON\"," + row.Cells[9].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act vive_con\"{0}", Environment.NewLine) : strVivecon;

                        //strViveen = !string.IsNullOrEmpty(row.Cells[10].Text.Replace("&nbsp;", "").Trim()) ? strViveen + string.Format(strCodigoEmpleado + ",10," + "\"" +
                        //    dt.ToString("dd/MM/yyyy") + "\",\"CB_VIVEEN\"," + row.Cells[10].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act vive_en\"{0}", Environment.NewLine) : strViveen;

                        //strTransporte = !string.IsNullOrEmpty(row.Cells[11].Text.Replace("&nbsp;", "").Trim()) ? strTransporte + string.Format(strCodigoEmpleado + ",10," + "\"" +
                        //    dt.ToString("dd/MM/yyyy") + "\",\"CB_TRANSPORT\"," + row.Cells[11].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act transporte\"{0}", Environment.NewLine) : strTransporte;

                        //strTalla = !string.IsNullOrEmpty(row.Cells[12].Text.Replace("&nbsp;", "").Trim()) ? strTalla + string.Format(strCodigoEmpleado + ",10," + "\"" +
                        //    dt.ToString("dd/MM/yyyy") + "\",\"CB_TALLA_CAM\"," + row.Cells[12].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act talla de camisa\"{0}", Environment.NewLine) : strTalla;

                        //strContacto = !string.IsNullOrEmpty(row.Cells[13].Text.Replace("&nbsp;", "").Trim()) ? strContacto + string.Format(strCodigoEmpleado + ",10," + "\"" +
                        //    dt.ToString("dd/MM/yyyy") + "\",\"CB_G_TEX_9\"," + row.Cells[13].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act contacto\"{0}", Environment.NewLine) : strContacto;

                        //strRelacion = !string.IsNullOrEmpty(row.Cells[14].Text.Replace("&nbsp;", "").Trim()) ? strRelacion + string.Format(strCodigoEmpleado + ",10," + "\"" +
                        //    dt.ToString("dd/MM/yyyy") + "\",\"CB_G_TAB_6\"," + row.Cells[14].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act relacion\"{0}", Environment.NewLine) : strRelacion;

                        //strTelcontacto = !string.IsNullOrEmpty(row.Cells[15].Text.Replace("&nbsp;", "").Trim()) ? strTelcontacto + string.Format(strCodigoEmpleado + ",10," + "\"" +
                        //    dt.ToString("dd/MM/yyyy") + "\",\"CB_TEX12\"," + row.Cells[15].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act telefono de contacto\"{0}", Environment.NewLine) : strTelcontacto;

                       // strClinica = !string.IsNullOrEmpty(row.Cells[1].Text.Replace("&nbsp;", "").Trim()) ? strClinica + string.Format(strCodigoEmpleado + ",10," +
                          //  dt.ToString("dd/MM/yyyy") + ",CB_CLINICA,\"" + row.Cells[1].Text.Replace("&nbsp;", "").ToString().ToUpperInvariant() + "\"{0}", Environment.NewLine) : strClinica;

                        strCalle = !string.IsNullOrEmpty(row.Cells[1].Text.Replace("&nbsp;", "").Trim()) ? strCalle + string.Format(strCodigoEmpleado + ",10," +
                            dt.ToString("dd/MM/yyyy") + ",CB_CALLE,\"" + row.Cells[1].Text.Replace("&nbsp;", "").ToString().ToUpperInvariant() + "\"{0}", Environment.NewLine) : strCalle;

                        strNum_ext = !string.IsNullOrEmpty(row.Cells[2].Text.Replace("&nbsp;", "").Trim()) ? strNum_ext + string.Format(strCodigoEmpleado + ",10," +
                            dt.ToString("dd/MM/yyyy") + ",CB_NUM_EXT,\"" + row.Cells[2].Text.Replace("&nbsp;", "").ToString().ToUpperInvariant() + "\"{0}", Environment.NewLine) : strNum_ext;

                        strNum_int = !string.IsNullOrEmpty(row.Cells[3].Text.Replace("&nbsp;", "").Trim()) ? strNum_int + string.Format(strCodigoEmpleado + ",10," +
                            dt.ToString("dd/MM/yyyy") + ",CB_NUM_INT,\"" + row.Cells[3].Text.Replace("&nbsp;", "").ToString().ToUpperInvariant() + "\"{0}", Environment.NewLine) : strNum_int;

                        strColonia = !string.IsNullOrEmpty(row.Cells[4].Text.Replace("&nbsp;", "").Trim()) ? strColonia + string.Format(strCodigoEmpleado + ",10," +
                            dt.ToString("dd/MM/yyyy") + ",CB_COD_COL,\"" + row.Cells[4].Text.Replace("&nbsp;", "").ToString().ToUpperInvariant() + "\"{0}", Environment.NewLine) : strColonia;

                         strTemp= onlyNumbers(row.Cells[5].Text.Replace("&nbsp;", "").ToString().ToUpperInvariant());

                        strTelefono = !string.IsNullOrEmpty(row.Cells[5].Text.Replace("&nbsp;", "").Trim()) ? strTelefono + string.Format(strCodigoEmpleado + ",10," +
                            dt.ToString("dd/MM/yyyy") + ",CB_TEL,\"" + strTemp + "\"{0}", Environment.NewLine) : strTelefono;

                        strTemp = onlyNumbers(row.Cells[6].Text.Replace("&nbsp;", "").ToString().ToUpperInvariant());

                        strCelular = !string.IsNullOrEmpty(row.Cells[6].Text.Replace("&nbsp;", "").Trim()) ? strCelular + string.Format(strCodigoEmpleado + ",10," +
                            dt.ToString("dd/MM/yyyy") + ",CB_G_NUM_8,\"" + strTemp + "\"{0}", Environment.NewLine) : strCelular;

                        strCorreo = !string.IsNullOrEmpty(row.Cells[7].Text.Replace("&nbsp;", "").Trim()) ? strCorreo + string.Format(strCodigoEmpleado + ",10," +
                            dt.ToString("dd/MM/yyyy") + ",CB_E_MAIL,\"" + row.Cells[7].Text.Replace("&nbsp;", "").ToString() + "\"{0}", Environment.NewLine) : strCorreo;

                        strVivecon = !string.IsNullOrEmpty(row.Cells[8].Text.Replace("&nbsp;", "").Trim()) ? strVivecon + string.Format(strCodigoEmpleado + ",10," +
                            dt.ToString("dd/MM/yyyy") + ",CB_VIVECON,\"" + row.Cells[8].Text.Replace("&nbsp;", "").ToString().ToUpperInvariant() + "\"{0}", Environment.NewLine) : strVivecon;

                        strViveen = !string.IsNullOrEmpty(row.Cells[9].Text.Replace("&nbsp;", "").Trim()) ? strViveen + string.Format(strCodigoEmpleado + ",10," +
                            dt.ToString("dd/MM/yyyy") + ",CB_VIVEEN,\"" + row.Cells[9].Text.Replace("&nbsp;", "").ToString().ToUpperInvariant() + "\"{0}", Environment.NewLine) : strViveen;

                        strTransporte = !string.IsNullOrEmpty(row.Cells[10].Text.Replace("&nbsp;", "").Trim()) ? strTransporte + string.Format(strCodigoEmpleado + ",10," +
                            dt.ToString("dd/MM/yyyy") + ",CB_MED_TRA,\"" + row.Cells[10].Text.Replace("&nbsp;", "").ToString().ToUpperInvariant() + "\"{0}", Environment.NewLine) : strTransporte;

                        strTalla = !string.IsNullOrEmpty(row.Cells[11].Text.Replace("&nbsp;", "").Trim()) ? strTalla + string.Format(strCodigoEmpleado + ",10," +
                            dt.ToString("dd/MM/yyyy") + ",CB_G_TAB_2,\"" + row.Cells[11].Text.Replace("&nbsp;", "").ToString().ToUpperInvariant() + "\"{0}", Environment.NewLine) : strTalla;

                        strContacto = !string.IsNullOrEmpty(row.Cells[12].Text.Replace("&nbsp;", "").Trim()) ? strContacto + string.Format(strCodigoEmpleado + ",10," +
                            dt.ToString("dd/MM/yyyy") + ",CB_G_TEX_9,\"" + row.Cells[12].Text.Replace("&nbsp;", "").ToString().ToUpperInvariant() + "\"{0}", Environment.NewLine) : strContacto;

                        strRelacion = !string.IsNullOrEmpty(row.Cells[13].Text.Replace("&nbsp;", "").Trim()) ? strRelacion + string.Format(strCodigoEmpleado + ",10," +
                            dt.ToString("dd/MM/yyyy") + ",CB_G_TAB_6,\"" + row.Cells[13].Text.Replace("&nbsp;", "").ToString().ToUpperInvariant() + "\"{0}", Environment.NewLine) : strRelacion;

                        strTemp = onlyNumbers(row.Cells[14].Text.Replace("&nbsp;", "").ToString().ToUpperInvariant());

                        strTelcontacto = !string.IsNullOrEmpty(row.Cells[14].Text.Replace("&nbsp;", "").Trim()) ? strTelcontacto + string.Format(strCodigoEmpleado + ",10," +
                            dt.ToString("dd/MM/yyyy") + ",CB_G_TEX12,\"" + strTemp + "\"{0}", Environment.NewLine) : strTelcontacto;

                        strTemp = onlyNumbers(row.Cells[16].Text.Replace("&nbsp;", "").ToString().ToUpperInvariant());

                        strCelEmp = !string.IsNullOrEmpty(row.Cells[16].Text.Replace("&nbsp;", "").Trim()) ? strCelEmp + string.Format(strCodigoEmpleado + ",10," +
                            dt.ToString("dd/MM/yyyy") + ",CB_G_NUM_6,\"" + strTemp + "\"{0}", Environment.NewLine) : strCelEmp;

                        strTemp = onlyNumbers(row.Cells[17].Text.Replace("&nbsp;", "").ToString().ToUpperInvariant());

                        strTelemp = !string.IsNullOrEmpty(row.Cells[17].Text.Replace("&nbsp;", "").Trim()) ? strTelemp + string.Format(strCodigoEmpleado + ",10," +
                            dt.ToString("dd/MM/yyyy") + ",CB_G_NUM_4,\"" + strTemp + "\"{0}", Environment.NewLine) : strTelemp;

                        strExtension = !string.IsNullOrEmpty(row.Cells[18].Text.Replace("&nbsp;", "").Trim()) ? strExtension + string.Format(strCodigoEmpleado + ",10," +
                            dt.ToString("dd/MM/yyyy") + ",CB_G_NUM_7,\"" + row.Cells[18].Text.Replace("&nbsp;", "").ToString().ToUpperInvariant() + "\"{0}", Environment.NewLine) : strExtension;

                        strPaisresiden = !string.IsNullOrEmpty(row.Cells[19].Text.Replace("&nbsp;", "").Trim()) ? strPaisresiden + string.Format(strCodigoEmpleado + ",10," +
                            dt.ToString("dd/MM/yyyy") + ",CB_G_TEX13,\"" + row.Cells[19].Text.Replace("&nbsp;", "").ToString().ToUpperInvariant() + "\"{0}", Environment.NewLine) : strPaisresiden;

                        //clinica.WriteLine(row.Cells[0].Text.ToString() + ",10," + "\"" + dt.ToString("dd/MM/yyyy") + "\",\"CB_CLINICA\"," + row.Cells[1].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act clinica\"");
                        //calle.WriteLine(row.Cells[0].Text.ToString() + ",10," + "\"" + dt.ToString("dd/MM/yyyy") + "\",\"CB_CALLE\"," + row.Cells[2].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act calle\"");
                        //num_ext.WriteLine(row.Cells[0].Text.ToString() + ",10," + "\"" + dt.ToString("dd/MM/yyyy") + "\",\"CB_NUM_EXT\"," + row.Cells[3].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act exterior\"");
                        //num_int.WriteLine(row.Cells[0].Text.ToString() + ",10," + "\"" + dt.ToString("dd/MM/yyyy") + "\",\"CB_NUM_INT\"," + row.Cells[4].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act interior\"");
                        //colonia.WriteLine(row.Cells[0].Text.ToString() + ",10," + "\"" + dt.ToString("dd/MM/yyyy") + "\",\"CB_COLONIA\"," + row.Cells[5].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act colonia\"");
                        //telefono.WriteLine(row.Cells[0].Text.ToString() + ",10," + "\"" + dt.ToString("dd/MM/yyyy") + "\",\"CB_TEL\"," + row.Cells[6].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act telefono\"");
                        //celular.WriteLine(row.Cells[0].Text.ToString() + ",10," + "\"" + dt.ToString("dd/MM/yyyy") + "\",\"CB_G_NUM_8\"," + row.Cells[7].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act celular\"");
                        //correo.WriteLine(row.Cells[0].Text.ToString() + ",10," + "\"" + dt.ToString("dd/MM/yyyy") + "\",\"CB_E_MAIL\"," + row.Cells[8].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act correo electronico personal\"");
                        //vivecon.WriteLine(row.Cells[0].Text.ToString() + ",10," + "\"" + dt.ToString("dd/MM/yyyy") + "\",\"CB_VIVECON\"," + row.Cells[9].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act vive_con\"");
                        //viveen.WriteLine(row.Cells[0].Text.ToString() + ",10," + "\"" + dt.ToString("dd/MM/yyyy") + "\",\"CB_VIVEEN\"," + row.Cells[10].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act vive_en\"");
                        //transporte.WriteLine(row.Cells[0].Text.ToString() + ",10," + "\"" + dt.ToString("dd/MM/yyyy") + "\",\"CB_TRANSPORT\"," + row.Cells[11].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act transporte\"");
                        //talla.WriteLine(row.Cells[0].Text.ToString() + ",10," + "\"" + dt.ToString("dd/MM/yyyy") + "\",\"CB_TALLA_CAM\"," + row.Cells[12].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act talla de camisa\"");
                        //contacto.WriteLine(row.Cells[0].Text.ToString() + ",10," + "\"" + dt.ToString("dd/MM/yyyy") + "\",\"CB_G_TEX_9\"," + row.Cells[13].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act contacto\"");
                        //relacion.WriteLine(row.Cells[0].Text.ToString() + ",10," + "\"" + dt.ToString("dd/MM/yyyy") + "\",\"CB_G_TAB_6\"," + row.Cells[14].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act relacion\"");
                        //telcontacto.WriteLine(row.Cells[0].Text.ToString() + ",10," + "\"" + dt.ToString("dd/MM/yyyy") + "\",\"CB_TEX12\"," + row.Cells[15].Text.Replace("&nbsp;", "").ToString() + ",\"\"," + "\"Act telefono de contacto\"");

                    }

                }

                //if (!string.IsNullOrEmpty(strClinica)) clinica.WriteLine(strClinica.TrimEnd('\r', '\n'));
                if (!string.IsNullOrEmpty(strCalle)) calle.WriteLine(strCalle.TrimEnd('\r', '\n'));
                if (!string.IsNullOrEmpty(strNum_ext)) num_ext.WriteLine(strNum_ext.TrimEnd('\r', '\n'));
                if (!string.IsNullOrEmpty(strNum_int)) num_int.WriteLine(strNum_int.TrimEnd('\r', '\n'));
                if (!string.IsNullOrEmpty(strColonia)) colonia.WriteLine(strColonia.TrimEnd('\r', '\n'));
                if (!string.IsNullOrEmpty(strTelefono)) telefono.WriteLine(strTelefono.TrimEnd('\r', '\n'));
                if (!string.IsNullOrEmpty(strCelular)) celular.WriteLine(strCelular.TrimEnd('\r', '\n'));
                if (!string.IsNullOrEmpty(strCorreo)) correo.WriteLine(strCorreo.TrimEnd('\r', '\n'));
                if (!string.IsNullOrEmpty(strVivecon)) vivecon.WriteLine(strVivecon.TrimEnd('\r', '\n'));
                if (!string.IsNullOrEmpty(strViveen)) viveen.WriteLine(strViveen.TrimEnd('\r', '\n'));
                if (!string.IsNullOrEmpty(strTransporte)) transporte.WriteLine(strTransporte.TrimEnd('\r', '\n'));
                if (!string.IsNullOrEmpty(strTalla)) talla.WriteLine(strTalla.TrimEnd('\r', '\n'));
                if (!string.IsNullOrEmpty(strContacto)) contacto.WriteLine(strContacto.TrimEnd('\r', '\n'));
                if (!string.IsNullOrEmpty(strRelacion)) relacion.WriteLine(strRelacion.TrimEnd('\r', '\n'));
                if (!string.IsNullOrEmpty(strTelcontacto)) telcontacto.WriteLine(strTelcontacto.TrimEnd('\r', '\n'));
                if (!string.IsNullOrEmpty(strCelEmp)) celular_Emp.WriteLine(strTelemp.TrimEnd('\r', '\n'));
                if (!string.IsNullOrEmpty(strTelemp)) celular_Emp.WriteLine(strCelEmp.TrimEnd('\r', '\n'));
                if (!string.IsNullOrEmpty(strExtension)) celular_Emp.WriteLine(strExtension.TrimEnd('\r', '\n'));
                if (!string.IsNullOrEmpty(strPaisresiden)) celular_Emp.WriteLine(strPaisresiden.TrimEnd('\r', '\n'));

               // clinica.Close();
                calle.Close();
                num_int.Close();
                num_ext.Close();
                colonia.Close();
                telefono.Close();
                celular.Close();
                correo.Close();
                vivecon.Close();
                viveen.Close();
                transporte.Close();
                talla.Close();
                contacto.Close();
                relacion.Close();
                telcontacto.Close();
                celular_Emp.Close();
                telemp.Close();
                extension.Close();
                paisresiden.Close();

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
        private void GenerateCSVPSFiles()
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

                //generating new csv files
                TextWriter pSoft = new StreamWriter(strCSVFilesPath + @"PeopleSoft_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".csv");
               

                string strpSoft = "";
                string[] peopSoft=new string[30];

                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        //DateTime dt = DateTime.Parse(row.Cells[15].Text.ToString());
                        //string strCodigoEmpleado = row.Cells[0].Text.ToString().Trim();

                        strpSoft = !string.IsNullOrEmpty(row.Cells[1].Text.Replace("&nbsp;", "").Trim()) ? strpSoft + string.Format(row.Cells[0].Text.ToString() + "," + row.Cells[1].Text.ToString() + "," + DateTime.Now.ToString("u").Substring(0, 10) + "," + row.Cells[2].Text.ToString() + "," + row.Cells[3].Text.ToString() + "," + row.Cells[4].Text.ToString() + "," + row.Cells[5].Text.ToString() + "," + row.Cells[6].Text.ToString() + "," + row.Cells[7].Text.ToString() + "," + row.Cells[8].Text.ToString() + "," + row.Cells[9].Text.ToString() + "," + row.Cells[10].Text.ToString() + "," + row.Cells[11].Text.ToString() + "," + row.Cells[12].Text.ToString() + "," + row.Cells[13].Text.ToString() + "," + row.Cells[14].Text.ToString() + "," + row.Cells[15].Text.ToString() + "," + row.Cells[16].Text.ToString() + "," + row.Cells[17].Text.ToString() + "," + DateTime.Now.ToString("u").Substring(0, 10) + "," + row.Cells[18].Text.ToString() + "," + row.Cells[19].Text.ToString() + "," + row.Cells[20].Text.ToString() + "," + row.Cells[21].Text.ToString() + "," + row.Cells[22].Text.ToString() + "," + row.Cells[23].Text.ToString() + "," + row.Cells[24].Text.ToString() + "," + row.Cells[25].Text.ToString() + "," + row.Cells[26].Text.ToString() + "," + row.Cells[27].Text.ToString() + "," + row.Cells[28].Text.ToString() + "," + row.Cells[29].Text.ToString() + "," + row.Cells[30].Text.ToString() + "," + row.Cells[31].Text.ToString() + "," + row.Cells[32].Text.ToString() + "," + row.Cells[33].Text.ToString() + "," + row.Cells[34].Text.ToString() + "," + row.Cells[35].Text.ToString() + "," + row.Cells[36].Text.ToString() + "," + row.Cells[37].Text.ToString() + "," + row.Cells[38].Text.ToString() + "," + row.Cells[39].Text.ToString() + "," + row.Cells[40].Text.ToString() + "," + row.Cells[41].Text.ToString() + "," + row.Cells[42].Text.ToString() + "\"{0}", Environment.NewLine) : strpSoft;
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

        public void GetGenerarDatos()
        {
            try
            {
                var res = DbUtil.GetCursor("sp_verGenerarDatos",
                            new SqlParameter("@tipo", "TRESS"));
                GridView1.DataSource = res;
                GridView1.DataBind();
                lblErrMsg.Text = "";
            }
            catch (Exception ex)
            {
                lblErrMsg.Text = ex.Message.ToString();
            }
            
            if (GridView1.Rows.Count > 0) {
                printing.Visible = false;
                ok_modal.Visible = true;}
            else {
                printing.Visible = false;
                ok_modal.Visible = false;
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
           
                GenerateCSVFiles();
                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        // execute sp of data updated
                        var codigo = row.Cells[0].Text.ToString();
                        var fecha = row.Cells[15].Text.ToString();

                        var res = DbUtil.ExecuteProc("sp_updateDatos",
                            new SqlParameter("@codigo", codigo),
                            new SqlParameter("@tipo", "TRESS"),
                            new SqlParameter("@fecha", fecha)
                            );
                    }
                }
                GetGenerarDatos();
           
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
            Response.Redirect("Actualizacion_datos_PS.aspx");
        }

        protected void btn_MainMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuKey.aspx");
        }

        //protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (CheckBox1.Checked)
        //    {
        //        GetGenerarDatosPS();
        //    }
        //    else
        //    {
        //        GetGenerarDatos();
        //    }
        //}

      
    }
}