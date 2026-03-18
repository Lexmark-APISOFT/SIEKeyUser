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
    public partial class Mantenimiento_Catalogos_Sig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                String MyVarNom = Session["nombre"].ToString();
                String MyVarNum = Session["numero"].ToString();

                nombre.Text = MyVarNom;
                getHorIMSS();
                getParentesco();
                
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }
        }

        public void getHorIMSS()
        {
            var res = DbUtil.GetCursor("sp_verHorariosIMSS");
            Grid_IMSS.DataSource = res;
            Grid_IMSS.DataBind();
        }
        public void getParentesco()
        {
            var res = DbUtil.GetCursor("sp_verDescParent");
            Grid_Desparent.DataSource = res;
            Grid_Desparent.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("MenuKey.aspx");
        }
        protected void Grid_IMSS_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grid_IMSS.PageIndex = e.NewPageIndex;
            getHorIMSS();

        }
        protected void Grid_Desparent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grid_Desparent.PageIndex = e.NewPageIndex;
            getParentesco();

        }
        protected void Grid_IMSS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;
                mensaje.Text = "";

                string Himss = Grid_IMSS.Rows[rowIndex].Cells[0].Text;

              
                Session.Add("X", 1);
                if (e.CommandName == "mod")
                {
                    Dhorario.Text = Himss;
                    Dparentesco.Text = "";
                    Dparentesco.Enabled = false;
                    Dhorario.Enabled = true;
                    Guardar.Enabled = true;
                    Cancelar.Enabled = true;

                    
                    Session.Add("HimssDef", Himss);
                    Session.Add("UpdateCat", "1");

                }
                else if (e.CommandName == "del")
                {


                    mensaje.Text = "Eliminado exitosamente";

                    var res = DbUtil.ExecuteProc("sp_insertupdateHorIMSS",
                       new SqlParameter("@Himss", Himss),
                       new SqlParameter("@borrar", 2),
                       new SqlParameter("@HimssDef",Himss),
                       new SqlParameter("@UpdateCat","0")
                   );
                    getHorIMSS();
                   
                }

            }
            catch
            {

            }

        }
        protected void Grid_Desparent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;
                mensaje.Text = "";

                string DescParent = Grid_Desparent.Rows[rowIndex].Cells[0].Text;

                
                Session.Add("X", 2);
                if (e.CommandName == "mod")
                {
                    Dhorario.Text = "";
                    Dhorario.Enabled = false;
                    Dparentesco.Text = DescParent;
                    Dparentesco.Enabled = true;
                    Guardar.Enabled = true;
                    Cancelar.Enabled = true;
                    Session.Add("DescParentDef", DescParent);
                    Session.Add("UpdateCat", "1");
                   

                }
                else if (e.CommandName == "del")
                {

                    var res = DbUtil.ExecuteProc("sp_insertupdateDescParent",
                       new SqlParameter("@DescParent", DescParent),
                       new SqlParameter("@DescParentDef", DescParent),
                       new SqlParameter("@borrar", 2),
                       new SqlParameter("@UpdateCat","0")
                       );
                    mensaje.Text = "Eliminado exitosamente";
                    getParentesco();
                   
                }

            }
            catch
            {

            }

        }

        protected void agregar_horario_imss_Click(object sender, EventArgs e)
        {
            Dhorario.Text = "";
            Dhorario.Enabled = true;
            Guardar.Enabled = true;
            Cancelar.Enabled = true;
            Dparentesco.Text = "";
            Dparentesco.Enabled = false;
            Session.Add("HimssDef", "1");
            Session.Add("UpdateCat", "2");
            Session.Add("X", 1);
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            Dhorario.Text = "";
            Dhorario.Enabled = false;
            Dparentesco.Text = "";
            Dparentesco.Enabled = false;
            Guardar.Enabled = false;
            Cancelar.Enabled = false;
        }

        protected void agregar_parentesco_Click(object sender, EventArgs e)
        {
            Dhorario.Text = "";
            Dhorario.Enabled = false;
            Dparentesco.Text = "";
            Dparentesco.Enabled = true;
            Guardar.Enabled = true;
            Cancelar.Enabled = true;
            Session.Add("DescParentDef", "");
            Session.Add("UpdateCat", "2");
            Session.Add("X", 2);
        }

        protected void Guardar_Click(object sender, EventArgs e)
        {
            var x = Session["X"].ToString();

            if (x == "1")
            {
                string Himss = Dhorario.Text;
                string HimssDef = Session["HimssDef"].ToString();
                if (Himss != "" )
                {
                    if (Session["UpdateCat"].ToString() == "2")
                    {
                        var res = DbUtil.ExecuteProc("sp_insertupdateHorIMSS",
                               new SqlParameter("@Himss", Himss),
                               new SqlParameter("@borrar", 1),
                               new SqlParameter("@HimssDef", HimssDef),
                               new SqlParameter("@UpdateCat", "2")
                           );
                        mensaje.Text = "Cambios guardados exitosamente";
                        getHorIMSS();
                    }
                    else
                    {
                        var res = DbUtil.ExecuteProc("sp_insertupdateHorIMSS",
                               new SqlParameter("@Himss", Himss),
                               new SqlParameter("@borrar", 1),
                               new SqlParameter("@HimssDef", HimssDef),
                               new SqlParameter("@UpdateCat", "1")
                           );
                        mensaje.Text = "Cambios guardados exitosamente";
                        getHorIMSS();
                    }
                }
                else
                {
                    mensaje.Text = "Favor de llenar todos los campos";
                }
            }
            else
            {
                string DescParent = Dparentesco.Text;
                string DescParentDef = Session["DescParentDef"].ToString();
                if (DescParent != "")
                {
                    if (Session["UpdateCat"].ToString() == "2")
                    {
                        var res = DbUtil.ExecuteProc("sp_insertupdateDescParent",
                           new SqlParameter("@DescParent", DescParent),
                           new SqlParameter("@DescParentDef", DescParentDef),
                           new SqlParameter("@borrar", 1),
                           new SqlParameter("@UpdateCat", "2")
                           );
                        mensaje.Text = "Cambios guardados exitosamente";
                        getParentesco();
                    }
                    else
                    {
                        var res = DbUtil.ExecuteProc("sp_insertupdateDescParent",
                           new SqlParameter("@DescParent", DescParent),
                           new SqlParameter("@DescParentDef", DescParentDef),
                           new SqlParameter("@borrar", 1),
                           new SqlParameter("@UpdateCat", "1")
                           );
                        mensaje.Text = "Cambios guardados exitosamente";
                        getParentesco();
                    }
                }
                else
                {
                    mensaje.Text = "Favor de llenar todos los campos";
                }


            }
            Dhorario.Text = "";
            Dhorario.Enabled = false;
            Dparentesco.Text = "";
            Dparentesco.Enabled = false;
            Guardar.Enabled = false;
            Cancelar.Enabled = false;
        }

        protected void cat_sig_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Mantenimiento_Catalogos.aspx");
        }

        protected void cat_tress_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Catalogos_TRESS.aspx");
        }
    }
}