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
    public partial class Mantenimiento_Catalogos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                String MyVarNom = Session["nombre"].ToString();
                String MyVarNum = Session["numero"].ToString();

                nombre.Text = MyVarNom;

               // getClinicas();
                getCorreos();
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }
        }
        public void getClinicas()
        {
            var res = DbUtil.GetCursor("sp_verClinicas");
            Grid_Clinica.DataSource = res;
            Grid_Clinica.DataBind();
        }
        public void getCorreos()
        {
            var res = DbUtil.GetCursor("sp_verDominios");
            Grid_Correo.DataSource = res;
            Grid_Correo.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("MenuKey.aspx");
        }
        protected void Grid_Clinica_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grid_Clinica.PageIndex = e.NewPageIndex;
            getClinicas();
           
        }
        protected void Grid_Correo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grid_Correo.PageIndex = e.NewPageIndex;
            getCorreos();
            
        }
        /*protected void Grid_Clinica_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
           {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;
                mensaje.Text = "";

                string NumClin = Grid_Clinica.Rows[rowIndex].Cells[0].Text;
                string RefUbi = Grid_Clinica.Rows[rowIndex].Cells[1].Text;

                //mensaje.Text = PMI + "<br/> " + PMI + "<br/> " + PCN;
                // mensaje.Text = actan + "<br /> " + actam + "<br/>" + hr + "<br/>" + e.CommandName;
                Session.Add("X", 1);
                if (e.CommandName == "mod")
                {
                    Tclinica.Text = NumClin;
                    Tclinica.Enabled = true;
                    Rubicacion.Text = RefUbi;
                    Rubicacion.Enabled = true;
                    Guardar.Enabled = true;
                    Cancelar.Enabled = true;

                    //Session.Add("Proceso", "mod");
                    Session.Add("NumClinDef", NumClin);
                    Session.Add("RefUbiDef", RefUbi);

                }
                else if (e.CommandName == "del")
                {

                    
                    mensaje.Text = "Eliminado exitosamente";
                    
                    var res = DbUtil.ExecuteProc("sp_insertupdateClinica",
                       new SqlParameter("@NumClin", int.Parse(NumClin)),
                       new SqlParameter("@RefUbi", RefUbi),
                       new SqlParameter("@borrar", 2),
                       new SqlParameter("@NumClinDef", int.Parse(NumClin)),
                       new SqlParameter("@RefUbiDef", RefUbi)
                   );
                    getClinicas();
                   // Session.Add("Proceso", "del");
                    /*Session.Add("TipoP", PMI);
                    Session.Add("tipoDef", PMI);
                    getPeriodos();
                    Response.Redirect("confirmacion_periodos_escuelas.aspx");*/
               /*}

            }
            catch
            {

            }

        }*/
        protected void Grid_Correo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;
                mensaje.Text = "";

                string DescDom = Grid_Correo.Rows[rowIndex].Cells[0].Text;

                //mensaje.Text = PMI + "<br/> " + PMI + "<br/> " + PCN;
                // mensaje.Text = actan + "<br /> " + actam + "<br/>" + hr + "<br/>" + e.CommandName;
                Session.Add("X", 2);
                if (e.CommandName == "mod")
                {
                    Ddominio.Text = DescDom;
                    Ddominio.Enabled = true;
                    Guardar.Enabled = true;
                    Cancelar.Enabled = true;
                    Session.Add("DescDomDef", DescDom);

                    /*Session.Add("Proceso", "mod");
                    Session.Add("tipoDef", tipoDef);*/

                }
                else if (e.CommandName == "del")
                {

                    var res = DbUtil.ExecuteProc("sp_insertupdateDominio",
                       new SqlParameter("@DescDom",DescDom),
                       new SqlParameter("@DescDomDef", DescDom),
                       new SqlParameter("@borrar", 2)
                       );
                    mensaje.Text = "Eliminado exitosamente";
                    getCorreos();
                    /*
                    Session.Add("Proceso", "del");
                    Session.Add("TipoP", PMI);
                    Session.Add("tipoDef", PMI);
                    getPeriodos();
                    Response.Redirect("confirmacion_periodos_escuelas.aspx");*/
                }

            }
            catch
            {

            }

        }

       /* protected void agregar_clinica_Click(object sender, EventArgs e)
        {
           
            Guardar.Enabled = true;
            Cancelar.Enabled = true;
            Session.Add("NumClinDef", "1");
            Session.Add("RefUbiDef", "qw");
            Session.Add("X", 1);
        }*/

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            
            Ddominio.Text = "";
            Ddominio.Enabled = false;
            Guardar.Enabled = false;
            Cancelar.Enabled = false;
        }

        protected void agregar_correo_Click(object sender, EventArgs e)
        {
            Ddominio.Text = "";
            Ddominio.Enabled = true;
            Guardar.Enabled = true;
            Cancelar.Enabled = true;
            Session.Add("DescDomDef", "");
            Session.Add("X", 2);
        }

        protected void Guardar_Click(object sender, EventArgs e)
        {
            var x = Session["X"].ToString();
           
           /* if (x == "1")
            { 
               
                string NumClinDef = Session["NumClinDef"].ToString();
                string RefUbiDef = Session["RefUbiDef"].ToString();
                if (NumClin != "" && RefUbi != "")
                {
                    var res = DbUtil.ExecuteProc("sp_insertupdateClinica",
                           new SqlParameter("@NumClin", int.Parse(NumClin)),
                           new SqlParameter("@RefUbi", RefUbi),
                           new SqlParameter("@borrar", 1),
                           new SqlParameter("@NumClinDef", int.Parse(NumClinDef)),
                           new SqlParameter("@RefUbiDef", RefUbiDef)
                       );
                    mensaje.Text = "Cambios guardados exitosamente";
                    getClinicas();
                }
                else
                {
                    mensaje.Text = "Favor de llenar todos los campos";
                }
            }
            else
            {*/
                string DescDom = Ddominio.Text;
                string DescDomDef = Session["DescDomDef"].ToString();
                if (DescDom != "")
                {
                    var res = DbUtil.ExecuteProc("sp_insertupdateDominio",
                           new SqlParameter("@DescDom", DescDom),
                           new SqlParameter("@DescDomDef", DescDomDef),
                           new SqlParameter("@borrar", 1)
                           );
                    mensaje.Text = "Cambios guardados exitosamente";
                    getCorreos();
                }
                else
                {
                    mensaje.Text = "Favor de llenar todos los campos";
                }


            //}
            
            Ddominio.Text = "";
            Ddominio.Enabled = false;
            Guardar.Enabled = false;
            Cancelar.Enabled = false;
        }

        protected void cat_sig_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Mantenimiento_Catalogos_Sig.aspx");
        }

        protected void cat_tress_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Catalogos_TRESS.aspx");
        }
    }
}