using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using MsBarco;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using System.Web.Services;
using Microsoft.Ajax.Utilities;
using System.Data;
using System.Configuration;
using Microsoft.Office.Interop.Word;
using System.Web.Providers.Entities;
using System.IO;
using System.IO.Ports;
using System.Security.Cryptography;
using static DotNetOpenAuth.OpenId.Extensions.AttributeExchange.WellKnownAttributes.Contact;

namespace SIE_KEY_USER.Views
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        string[] folios { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {

                if (IsPostBack)
                {
                    String nombreEmpleado = Session["nombre"].ToString();
                    lblNombreEmpleado.Text = nombreEmpleado;
                    String MyvarNum = Session["numero"].ToString();
                    fsFolios.Visible = false;
                }

            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }
        }

        [WebMethod]
        public static string generarArchivo(string folios)
        {
            string[] foliosS = folios.Split(',');
            string filePath = generarArchivo_Click(foliosS);

            return filePath;
        }
        protected static string generarArchivo_Click(string[] folios)
        {
            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition",
            // "attachment;filename=ArchivoVacacionesAceptadas.csv");
            //Response.Charset = "";
            //Response.ContentType = "application/text";
            var rowsCSV = new List<string>();

            string SqlconString = ConfigurationManager.ConnectionStrings["db"].ToString();
            using (SqlConnection conn = new SqlConnection(SqlconString))
            using (SqlCommand cmd = new SqlCommand("sp_fastScan", conn))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_sol_vac", SqlDbType.Int);
                cmd.Parameters.Add("@accepted", SqlDbType.Int);
                cmd.Parameters.Add("@rowCSV", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;
                conn.Open();

                int solicitud_num;

                for (int i = 0; i < folios.Length; i++)
                {

                    bool parsed = int.TryParse(folios[i], out solicitud_num);

                    if (parsed)
                    {

                        cmd.Parameters["@id_sol_vac"].Value = solicitud_num;
                        cmd.Parameters["@accepted"].Value = 0;
                        cmd.ExecuteNonQuery();
                        rowsCSV.Add(Convert.ToString(cmd.Parameters["@rowCSV"].Value));

                    }

                    //var res = DbUtil.ExecuteProc("sp_fastScan_Acceptation",
                    //new SqlParameter("@id_sol_vac", solicitud_num));

                }
                conn.Close();
            }

            StringBuilder sb = new StringBuilder();

            //sacar los headers de la tabla
            sb.Append("# Reloj,Fecha inicio,Fecha final,Turno,Duracion,ID solicitud,Periodo de nómina,Fecha inicio-final,Fecha de pago");
            sb.Append("\r\n");
            //sacar los contenidos de las columnas donde esta ese folio
            for (int j = 0; j < rowsCSV.Count; j++)
            {
                //add separator
                sb.Append(rowsCSV[j]);
                sb.Append("\r\n");
            }

            string pathCSV = @"\\mxjrzapp11\Encryption\work\" + "vacaciones" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm") + ".csv";
            string pathCSVpruebas = "C:\\Users\\dmendozarodr\\Documents\\SIE Y KEYUSER\\" + "vacaciones" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm") + ".csv";

            System.IO.File.WriteAllText(pathCSV, sb.ToString());


            return pathCSV;

        }

        [WebMethod]
        public static string downloadCSV(string path)
        {

            //Read the File as Byte Array.
            byte[] bytes = File.ReadAllBytes(path);

            //Convert File to Base64 string and send to Client.
            return Convert.ToBase64String(bytes, 0, bytes.Length);

        }
    }
}