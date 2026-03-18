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
    public partial class prestamos : System.Web.UI.Page
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

                GetPrestamos();
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("Default.aspx");
            }
        }

        public void GetPrestamos()
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                var res = DbUtil.GetCursor("sp_Prestamos_KeyUser");

                GridView1.DataSource = res;
                GridView1.DataBind();
                var res1 = DbUtil.GetCursor("sp_prestamo_generarArchivo");
                gridCSV.DataSource = res1;
                gridCSV.DataBind();
            }
            if (GridView1.Rows.Count > 0)
                ok_modal.Visible = true;
            else
                ok_modal.Visible = false;
        }

       

        private void ZipFilesCSV(string dirPath)
        {
            var zipFileName = string.Format("Act-prest-{0}.zip", DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss"));

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

        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("MenuKey.aspx");
        }
        protected void aceptar_ac_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // execute sp of data updated
                    var codigo = row.Cells[0].Text.ToString();
                    //var fecha = row.Cells[8].Text.ToString();
                    var res = DbUtil.ExecuteProc("sp_UpdatePrestamo",
                                new SqlParameter("@codigo", codigo)
                              );
                }
            }
            crearCSV();
            GetPrestamos();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetPrestamos();
        }
        protected void boton_prestamos_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("configurar_prestamo.aspx");
        }

        protected void boton_prestamos1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("ver_prestamos.aspx");
        }

        
        protected void crearCSV()
        {
            string strValue = string.Empty;
            for (int i = 0; i < gridCSV.Columns.Count; i++)
            {
                strValue = strValue + gridCSV.Columns[i].HeaderText;
                if (i != gridCSV.Columns.Count)
                {
                    strValue = strValue + ",";
                }
            }

            for (int i = 0; i < gridCSV.Rows.Count; i++)
            {
                for (int j = 0; j < gridCSV.Rows[i].Cells.Count; j++)
                {
                    if (!string.IsNullOrEmpty(gridCSV.Rows[i].Cells[j].Text.ToString()))
                    {
                        if (j > 0)
                            strValue = strValue + "," + gridCSV.Rows[i].Cells[j].Text.ToString();
                        else
                        {
                            if (string.IsNullOrEmpty(strValue))
                                strValue = gridCSV.Rows[i].Cells[j].Text.ToString();
                            else
                                strValue = strValue + Environment.NewLine + gridCSV.Rows[i].Cells[j].Text.ToString();
                        }
                    }
                }
            }
            System.IO.File.WriteAllText(strCSVFilesPath + "Solicitud_prestamos_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".csv", strValue);

            }
    }
}
