using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using MsBarco;

namespace SIE_KEY_USER.Views
{
    public partial class Catalogos_TRESS : System.Web.UI.Page
    {
        protected static string element;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                String MyVarNom = Session["nombre"].ToString();
                String MyVarNum = Session["numero"].ToString();
                //Session.Add("Text", "");
                nombre.Text = MyVarNom;
                
                //Session.Add("elementoTRESS", "");
                //getTRESS("");

            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }
        }
        public void getTRESS(string elemento)
        {
            var res = DbUtil.GetCursor("sp_getTRESS",
                new SqlParameter("@elemento", elemento)
                );

            Grid_TRESS.DataSource = res;
            Grid_TRESS.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("MenuKey.aspx");
        }
        protected void Grid_TRESS_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           
                Grid_TRESS.PageIndex = e.NewPageIndex;
                getTRESS(element);

        }
       
        protected void Grid_TRESS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;
                mensaje.Text = "";
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }

        }

        protected void Grid_TRESS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Grid_TRESS.HeaderRow.Cells[2].CssClass = "boundAgreg";
                e.Row.Cells[2].CssClass = "boundAgreg";

                DropDownList ddl = (e.Row.Cells[0].FindControl("seleccionar") as DropDownList);

               // string elemento = Session["elementoTRESS"].ToString();
                var res = DbUtil.ExecuteProc("sp_getStatusTRESS",
                    new SqlParameter("@elemento", element),
                    MsBarco.DbUtil.NewSqlParam("@id_opcion", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                    );
             

              
                    if (e.Row.Cells[2].Text.Contains("1"))
                    {
                        ddl.SelectedValue = res["@id_opcion"].ToString();
                    }
               
            }
        }
        protected void Button_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Button button = (Button)sender;
            string ID = button.ID;
            //string Text = button.Text;
            //Session.Add("elementoTRESS", ID);
           // Session.Add("Text", Text);
            element = ID;
            //GuardarTRESS.Text = ID;
            getTRESS(element);
            GuardarTRESS.Visible = true;
        }
        /*protected void talla_Click(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(200);
            getTRESS("talla");
            //Session.Add("elementoTRESS", "talla");
            element = "talla";
            GuardarTRESS.Visible = true;
            actualizarTRESS.Visible = true;
        }

        protected void vive_con_Click(object sender, EventArgs e)
        {
            Session.Add("elementoTRESS", "vive_con");
            getTRESS("vive_con");
            string elemento = Session["elementoTRESS"].ToString();
            mensaje.Text = elemento;
            GuardarTRESS.Visible = true;
            actualizarTRESS.Visible = true;
        }

        protected void vive_en_Click(object sender, EventArgs e)
        {
            Session.Add("elementoTRESS", "vive_en");
            getTRESS("vive_en");
            string elemento = Session["elementoTRESS"].ToString();
            mensaje.Text = elemento;
            GuardarTRESS.Visible = true;
            actualizarTRESS.Visible = true;
        }

        protected void transpor_Click(object sender, EventArgs e)
        {
            Session.Add("elementoTRESS", "transpor");
            getTRESS("transpor");
            string elemento = Session["elementoTRESS"].ToString();
            mensaje.Text = elemento;
            GuardarTRESS.Visible = true;
            actualizarTRESS.Visible = true;
        }*/

        protected void GuardarTRESS_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in Grid_TRESS.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList Ddl = (row.Cells[0].FindControl("seleccionar") as DropDownList);
                    //string elemento = Session["elementoTRESS"].ToString();
                    if (Ddl.SelectedItem.Text == "visible")
                    {
                        var modo = "visible";
                        var descripcion = row.Cells[1].Text;

                        var res = DbUtil.ExecuteProc("sp_updateVisible",
                            new SqlParameter("@modo", modo),
                            new SqlParameter("@elemento", element),
                            new SqlParameter("@descripcion", descripcion)
                            );
                    }

                    if (Ddl.SelectedItem.Text == "no visible")
                    {
                        var modo = "no_visible";
                        var descripcion = row.Cells[1].Text;

                        var res = DbUtil.ExecuteProc("sp_updateVisible",
                            new SqlParameter("@modo", modo),
                            new SqlParameter("@elemento", element),
                            new SqlParameter("@descripcion", descripcion)
                            );
                    }
                }
            }
            Response.Redirect("Confirmar_opciones.aspx");
        }

        protected void actualizarTRESS_Click(object sender, EventArgs e)
        {
            var res = DbUtil.ExecuteProc("sp_ActualizarElementos");
            Response.Redirect("Confirmar_opciones.aspx");
        }

       /* protected void relacion_Click(object sender, EventArgs e)
        {
            Session.Add("elementoTRESS", "relacion");
            getTRESS("relacion");
            string elemento = Session["elementoTRESS"].ToString();
            mensaje.Text = elemento;
            GuardarTRESS.Visible = true;
            actualizarTRESS.Visible = true;
        }

        protected void adj_Click(object sender, EventArgs e)
        {
            Session.Add("elementoTRESS", "adj");
            getTRESS("adj");
            string elemento = Session["elementoTRESS"].ToString();
            mensaje.Text = elemento;
            GuardarTRESS.Visible = true;
            actualizarTRESS.Visible = true;
        }*/
       

       
           

       
    }
}