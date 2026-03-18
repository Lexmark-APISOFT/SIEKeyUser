using MsBarco;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIE_KEY_USER.Views
{
    public partial class listaReprogramaciones : System.Web.UI.Page
    {
        private string strCSVFilesPath;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                strCSVFilesPath = Server.MapPath(@"~\Virtual\archivos\asistencia_perfecta\").ToString();
                lblErrMsg.Text = "";
                //strCSVFilesPath = Server.MapPath(@"~\Virtual\archivos\VirtEncr\").ToString();
                //strCSVFilesPath = Server.MapPath(@"~\VirtEncr\").ToString();

                if (!IsPostBack)
                {
                    getPeriodo();
                    String nombreEmpleado = Session["nombre"].ToString();
                    String cb_codigo = Session["numero"].ToString();
                    String MyvarNumS = Session["numero"].ToString();

                    lblNombreEmpleado.Text = nombreEmpleado;
                }
            }
        }

        private void getPeriodo()
        {
            var periodo = DbUtil.ExecuteQuery("SELECT [tipo_Periodo],[fecha] FROM [SIE].[dbo].[periodo_asistencia_perfecta_cursos]");
            if (periodo.Rows.Count > 0)
            {
                string inicio = periodo.Rows[0]["fecha"].ToString().Split(' ')[0];
                string fin = periodo.Rows[1]["fecha"].ToString().Split(' ')[0];

                txtInicioCiclo.Text = inicio;

                /*txtInicioCiclo.Text = ((Int32.Parse(inicio.Split('/')[1]) < 10)? "0"+inicio.Split('/')[1] : inicio.Split('/')[1]) 
                                        +"/"+ ((Int32.Parse(inicio.Split('/')[0]) < 10) ? "0" + inicio.Split('/')[0] : inicio.Split('/')[0])
                                        + "/"+(inicio.Split('/')[2]);*/

                txtFinCiclo.Text = fin;

                /*txtFinCiclo.Text = ((Int32.Parse(fin.Split('/')[1]) < 10) ? "0" + fin.Split('/')[1] : fin.Split('/')[1])
                                        + "/" + ((Int32.Parse(fin.Split('/')[0]) < 10) ? "0" + fin.Split('/')[0] : fin.Split('/')[0])
                                        + "/" + (fin.Split('/')[2]);*/
            }
        }

        private void getListaReprogramaciones(string inicio, string fin)
        {
            var res = MsBarco.DbUtil.GetCursor("sp_s_lista_reprogramaciones_cursos",
                new System.Data.SqlClient.SqlParameter("@inicioCiclo", inicio),
                new System.Data.SqlClient.SqlParameter("@finCiclo", fin));

            gv_Lista_de_asistencia_perfecta.DataSource = res;
            gv_Lista_de_asistencia_perfecta.DataBind();
        }

        protected void gv_lista_reprogramaciones_PreRender(object sender, EventArgs e)
        {
            getListaReprogramaciones(txtInicioCiclo.Text, txtFinCiclo.Text);
            var myGrid = sender as GridView;
            if (myGrid.Rows.Count > 0)
            {
                myGrid.UseAccessibleHeader = true;

                // This will add the <thead> and <tbody> elements
                myGrid.HeaderRow.TableSection = TableRowSection.TableHeader;

                //This adds the <tfoot> element. 
                myGrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            GenerateCSVFiles();

            Response.Redirect("Cursos_regulatorios.aspx");
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cursos_regulatorios.aspx");
        }

        protected bool verificacion_de_fechas()
        {
            string fechaFin = txtFinCiclo.Text.Split(' ')[0];
            string fechaInicio = txtInicioCiclo.Text.Split(' ')[0];
        
            if (Int32.Parse(fechaInicio.Split('/')[2]) <= Int32.Parse(fechaFin.Split('/')[2])) // año
            {
                if ((Int32.Parse(fechaInicio.Split('/')[0]) <= Int32.Parse(fechaFin.Split('/')[0]))) // mes
                {
                    if ((Int32.Parse(fechaInicio.Split('/')[2]) == Int32.Parse(fechaFin.Split('/')[2]) && Int32.Parse(fechaInicio.Split('/')[0]) == Int32.Parse(fechaFin.Split('/')[0]))) // dia
                    {
                        if((Int32.Parse(fechaInicio.Split('/')[1]) < Int32.Parse(fechaFin.Split('/')[1])))
                        {
                            return true;
                        }
                        else
                        {
                            lblErrMsg.Text = "Error: La fecha de inicio y fin del periodo deben ser diferentes.";
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                    //lblErrMsg.Text = "Error: Introdusca un rango de fechas correcto, la fecha de inicio debe ser menor a la fecha final.";
                    //return false;
                }
            }
            else
            {
                lblErrMsg.Text = "Error: Introdusca un rango de fechas correcto, la fecha de inicio debe ser menor a la fecha final.";
                return false;
            }
        }

        protected void calFechaInicio_SelectionChanged(object sender, EventArgs e)
        {
            isCalInicioOn.Value = "0";
            var dateInicio = calFechaInicio.SelectedDate;
            string fechaInicio = dateInicio.ToString().Split(' ')[0];
            
            if (verificacion_de_fechas())
            {
                txtInicioCiclo.Text = fechaInicio;

                /*txtInicioCiclo.Text = ((Int32.Parse(fechaInicio.Split('/')[1]) < 10) ? "0" + fechaInicio.Split('/')[1] : fechaInicio.Split('/')[1])
                                        + "/" + ((Int32.Parse(fechaInicio.Split('/')[0]) < 10) ? "0" + fechaInicio.Split('/')[0] : fechaInicio.Split('/')[0])
                                        + "/" + (fechaInicio.Split('/')[2]);*/
            }

        }

        protected void calFechaFin_SelectionChanged(object sender, EventArgs e)
        {
            isCalFinOn.Value = "0";
            var dateFin = calFechaFin.SelectedDate;
            string fechaFin = dateFin.ToString().Split(' ')[0];
            
            if (verificacion_de_fechas())
            {

                txtFinCiclo.Text = fechaFin;
                
                /*txtFinCiclo.Text = ((Int32.Parse(fechaFin.Split('/')[1]) < 10) ? "0" + fechaFin.Split('/')[1] : fechaFin.Split('/')[1])
                                        + "/" + ((Int32.Parse(fechaFin.Split('/')[0]) < 10) ? "0" + fechaFin.Split('/')[0] : fechaFin.Split('/')[0])
                                        + "/" + (fechaFin.Split('/')[2]);*/
            }
        }

        protected void btnAplicarCiclo_Click(object sender, EventArgs e)
        {
            string fechaInicio = txtInicioCiclo.Text;
            string fechaFin = txtFinCiclo.Text;

            if (verificacion_de_fechas())
            {
                var res = MsBarco.DbUtil.GetCursor("sp_u_periodo_asis_perfecta_cursos_r",
                            new System.Data.SqlClient.SqlParameter("@inicio", txtInicioCiclo.Text),
                            new System.Data.SqlClient.SqlParameter("@fin", txtFinCiclo.Text));
            }

            isCalInicioOn.Value = "0";
            isCalFinOn.Value = "0";
        }

        private void GenerateCSVFiles()
        {
            try
            {
                //var dirInfo = new DirectoryInfo(strCSVFilesPath);

                // deleting existing files in directory
                /*foreach (var csvFile in dirInfo.GetFiles("*.csv"))
                {
                    if (System.IO.File.Exists(csvFile.FullName))
                    {
                        File.Delete(csvFile.FullName);
                    }
                }*/

                string [] fechaInicio = txtInicioCiclo.Text.Split('/');
                string [] fechaFin = txtFinCiclo.Text.Split('/');

                TextWriter docListaAsistenciaPer = new StreamWriter(strCSVFilesPath + @"Asistencia_Perfecta" + fechaInicio[1] + "-" + fechaInicio[0] + "-" + fechaInicio[2]
                                                                             + " - " + fechaFin[1] + "-" + fechaFin[0] + "-" + fechaFin[2] + ".csv");

                string strAsistenciaPerfecta = "";
                strAsistenciaPerfecta = strAsistenciaPerfecta + string.Format("Numero de empleado" + ", " + "Nombre" + ", " 
                                        + "Supervisor" + ", " + "Planta" + ".\r \n ", Environment.NewLine);

                foreach (GridViewRow row in gv_Lista_de_asistencia_perfecta.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        string strCodigoEmpleado = row.Cells[0].Text.ToString().Trim();
                        string nombre = row.Cells[1].Text.ToString().Trim();
                        string supervisor = row.Cells[2].Text.ToString().Trim();
                        string planta = row.Cells[3].Text.ToString().Trim();
                        
                        strAsistenciaPerfecta = !string.IsNullOrEmpty(row.Cells[0].Text.Replace("&nbsp;", "").Trim()) ? strAsistenciaPerfecta + string.Format(strCodigoEmpleado + ", " + nombre + ", " 
                                    + supervisor + ", " + planta + ".\n", Environment.NewLine) : strAsistenciaPerfecta;
                    }
                }

                if (!string.IsNullOrEmpty(strAsistenciaPerfecta)) docListaAsistenciaPer.WriteLine(strAsistenciaPerfecta.TrimEnd('\r', '\n'));

                docListaAsistenciaPer.Close();

                // deleting files of 0k size
                /*foreach (var csvFile in dirInfo.GetFiles("*.csv"))
                {
                    if (csvFile.Length == 0)
                    {
                        if (System.IO.File.Exists(csvFile.FullName))
                        {
                            File.Delete(csvFile.FullName);
                        }
                    }
                }*/
            }
            catch (Exception ex2)
            {
                lblErrMsg.Text = ex2.Message;
            }
        }
    }
}