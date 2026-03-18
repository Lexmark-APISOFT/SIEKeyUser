using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SIE_KEY_USER.model;
using MsBarco;

namespace SIE_KEY_USER.Views
{
    public partial class Agregar_familiares : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                if (!IsPostBack)
                {
                    String MyVarNom = Session["nombre"].ToString();
                    String MyVarNum = Session["numero"].ToString();

                    nombre.Text = MyVarNom;

                    GetAgregarfam();
                }
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetAgregarfam();
        }
        

        public void GetAgregarfam()
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                //var famCodigo = Session["FamCodigo"].ToString();
                try
                {
                    var res = DbUtil.GetCursor("sp_getAgregarfam",
                        new SqlParameter("@codigo", ""),
                        MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 40)
                        );
                  
                            GridView1.DataSource = res;
                            GridView1.DataBind();
                    
                    /*var res1 = DbUtil.ExecuteProc("sp_getAgregarfam",
                        new SqlParameter("@codigo", ""),
                        MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 40)
                        );*/
                }
                catch (Exception ex)
                {
                    mensaje.Text = ex.Message;
                    //Response.Write(ex);
                }
                //prettyName.Text = res1["@nombre"].ToString();
            }
        }
        protected void des_buscar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);

            var res = DbUtil.GetCursor("sp_getAgregarfam",
                    new SqlParameter("@codigo", TextBox1.Text),
                    MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 40)
                    );

            GridView1.DataSource = res;
            GridView1.DataBind();
            //GetAgregarfam();

            TextBox1.Focus();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("MenuKey.aspx");
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;
                

                var a = GridView1.Rows[rowIndex].Cells[1].Text;
                var d = GridView1.Rows[rowIndex].Cells[2].Text;

                var res = DbUtil.ExecuteProc("sp_getActa_Agreg",
                    new SqlParameter("@codigo", a),
                    new SqlParameter("@fecha", d),
                    MsBarco.DbUtil.NewSqlParam("@acta", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                    MsBarco.DbUtil.NewSqlParam("@acta1", null, SqlDbType.VarChar, ParameterDirection.Output, 40)
                    );

                var acta = res["@acta"].ToString();
                var acta1 = res["@acta1"].ToString();

                var actan = Server.MapPath(@"~\Virtual\AcNac\" + acta);
                var actam = Server.MapPath(@"~\Virtual\AcMat\" + acta1);

                //var hr = GridView1.HeaderRow.Cells[7].Text;
               
                   
                
               
                 
                //mensaje.Text = actan + "<br /> " + actam + "<br/>"+hr+"<br/>"+e.CommandName;
              
                if (e.CommandName == "selectn")
                {
                    mensaje.Text = acta+" "+acta1;
                    if (System.IO.File.Exists(actan))
                    {
                       // System.Diagnostics.Process.Start(actan);
                       try
                       {
                            mensaje.Text = "";  
                            string FileName = Path.Combine(Server.MapPath(@"~\Virtual\AcNac\"), acta);
                            base.Session.Add("FileName", FileName);
                            base.Session.Add("Desc", "Acta");
                            Response.Redirect("Descarga_actas.aspx");
                            /*System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                            response.ClearContent();
                            response.Clear();
                            response.Buffer = true;
                            Response.AddHeader("Content-Disposition", string.Format("attachment; filename = \"{0}\"", System.IO.Path.GetFileName(FileName)));
                            response.TransmitFile(FileName);
                            
                            response.Flush();
                            response.End();*/
                            mensaje.Text = "No se encontró el archivo " + FileName;
                        }
                        catch
                        {
                        }
                        
                    }
                    else
                    {
                        mensaje.Text = "No existe ningun archivo.";
                    }
                }
                else if (e.CommandName == "selectm")
                {
                    mensaje.Text = acta + " " + acta1;
                    if (System.IO.File.Exists(actam))
                    {
                        //System.Diagnostics.Process.Start(actam);

                        try
                        {

                            string FileName = Path.Combine(Server.MapPath(@"~\Virtual\AcMat\"), acta1);
                            base.Session.Add("FileName", FileName);
                            base.Session.Add("Desc", "Acta");
                            Response.Redirect("Descarga_actas.aspx");
                            //mensaje.Text = "No se encontró el archivo " + FileName;
                        }
                        catch
                        {
                        }
                        mensaje.Text = "";
                    }
                    else
                    {
                        mensaje.Text = "No existe ningun archivo.";
                    }
                }
                else
                {
                    mensaje.Text = "No existe ningun archivo.";
                }
            }
            catch 
            {
                
            }
        }

        protected void aceptar_fam_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            bool userCheckbox = false;

            foreach(GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("seleccionar") as CheckBox);

                    if (chkRow.Checked)
                    {
                        string numeros = row.Cells[1].Text;
                        string fechas = row.Cells[2].Text;

                        var res = DbUtil.ExecuteProc("sp_AceptarFam_keyuser",
                            new SqlParameter("@codigo", numeros),
                            new SqlParameter("@fecha", fechas)
                            );

                        userCheckbox = true;
                    }
                }
            }

            GetAgregarfam();

            if (userCheckbox == true)
            {
                mensaje.Text = "Usuarios aceptados.";
            }
            else
            {
                mensaje.Text = "No hay usuarios seleccionados.";
            }
        }

        protected void aceptar_ac_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            if (area_txtAgreg.Text != "")
            {
                bool userCheckbox = false;

                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("seleccionar") as CheckBox);

                        if (chkRow.Checked)
                        {
                            string numeros = row.Cells[1].Text;
                            string fechas = row.Cells[2].Text;
                            string motivo = area_txtAgreg.Text;

                            var res = DbUtil.ExecuteProc("sp_rechazarFam_keyuser",
                                new SqlParameter("@codigo", numeros),
                                new SqlParameter("@fecha", fechas),
                                new SqlParameter("@motivo", motivo)
                                );

                            userCheckbox = true;
                        }
                    }
                }

                GetAgregarfam();

                if (userCheckbox == true)
                {
                    mensaje.Text = "Usuarios rechazados.";
                }
                else
                {
                    mensaje.Text = "No hay usuarios seleccionados.";
                }
            }
            else
            {
                mensaje.Text = "Escribe un motivo de rechazo.";
            }
        }

        protected void FamAcept_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("ver_aceptados.aspx");
        }

        protected void FamRech_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("ver_rechazados.aspx");
        }

        protected void ok_modal_Click(object sender, EventArgs e)
        {

        }
    }
}