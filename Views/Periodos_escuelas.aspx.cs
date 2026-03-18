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
    public partial class Periodos_escuelas : System.Web.UI.Page
    {
        public string x;
        public string tipoDef;
        public string DDtipoDef;
        public string DelmntDef;
        public string NomEscDef;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                String MyVarNom = Session["nombre"].ToString();
                String MyVarNum = Session["numero"].ToString();

                nombre.Text = MyVarNom;
                getPeriodos();  
                getEperiodos();
                getEscuelas();
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }
        }
        protected void Grid_Periodo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grid_Periodo.PageIndex = e.NewPageIndex;
            getPeriodos();
            //getEperiodos();
        }
        protected void Grid_Eperiodo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grid_Eperiodo.PageIndex = e.NewPageIndex;
            //getPeriodos();
            getEperiodos();
        }
        protected void Grid_Escuelas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grid_Escuelas.PageIndex = e.NewPageIndex;
            //getPeriodos();
            //getEperiodos();
            getEscuelas();
        }
        
        public void getPeriodos()
        {
            String MyVarNum = Session["numero"].ToString();


            var res = DbUtil.GetCursor("sp_verPeriodos");

            Grid_Periodo.DataSource = res;
            Grid_Periodo.DataBind();

            DropDownList1.DataSource = res;
            DropDownList1.DataBind();
        }
        public void getEperiodos()
        {
            var res = DbUtil.GetCursor("sp_verPeriodos",
                new SqlParameter("@mot","1")
                );
            Grid_Eperiodo.DataSource = res;
            Grid_Eperiodo.DataBind();
        }
        public void getEscuelas()
        {
            var res = DbUtil.GetCursor("sp_verPeriodoEscuela");
            Grid_Escuelas.DataSource = res;
            Grid_Escuelas.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Reembolso_escolar.aspx");
        }
        protected void Grid_Periodo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;
                mensaje.Text = "";

                string PMI = Grid_Periodo.Rows[rowIndex].Cells[0].Text;
               
                //mensaje.Text = PMI + "<br/> " + PMI + "<br/> " + PCN;
                // mensaje.Text = actan + "<br /> " + actam + "<br/>" + hr + "<br/>" + e.CommandName;
                Session.Add("X", 1);
                if (e.CommandName == "mod")
                {
                    Tperiodo.Text = PMI;
                    tipoDef = PMI;
                    Tperiodo.Enabled = true;                   
                    Guardar.Enabled = true;
                    Cancelar.Enabled = true;
                    
                    Session.Add("Proceso", "mod");
                    Session.Add("tipoDef", tipoDef);
                    
                }
                else if (e.CommandName == "del")
                {
                    mensaje.Text = "Eliminado exitosamente";
                    Session.Add("Proceso", "del");
                    Session.Add("TipoP", PMI);
                    Session.Add("tipoDef", PMI);
                    getPeriodos();
                    Response.Redirect("confirmacion_periodos_escuelas.aspx");
                }

            }
            catch
            {

            }

        }
        protected void Grid_Eperiodo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;
                mensaje.Text = "";

                string PMI = Grid_Eperiodo.Rows[rowIndex].Cells[0].Text;
                string PMA = Grid_Eperiodo.Rows[rowIndex].Cells[1].Text;
                Session.Add("X", 2);
                //mensaje.Text = PMI + "<br/> " + PMI + "<br/> " + PCN;
                // mensaje.Text = actan + "<br /> " + actam + "<br/>" + hr + "<br/>" + e.CommandName;
                if (e.CommandName == "mod")
                {
                    Delemento.Text = PMI;
                    DropDownList1.Text = PMA;
                    DelmntDef = PMI;
                    DDtipoDef = PMA;
                    DropDownList1.Enabled=true;
                    Delemento.Enabled = true;
                    Guardar.Enabled = true;
                    Cancelar.Enabled = true;
                    
                    Session.Add("Proceso", "mod");
                    Session.Add("DelmntDef", DelmntDef);
                    Session.Add("DDtipoDef", DDtipoDef);
                }
                else if (e.CommandName == "del")
                {
                    mensaje.Text = "Eliminado exitosamente";
                    Session.Add("Proceso", "del");
                    Session.Add("Delmnt", PMI);
                    Session.Add("DDtipo", PMA);
                    Session.Add("DelmntDef", PMI);
                    Session.Add("DDtipoDef", PMA);
                    getEperiodos();
                    Response.Redirect("confirmacion_periodos_escuelas.aspx");
                }

            }
            catch
            {

            }

        }
        protected void Grid_Escuelas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;
                mensaje.Text = "";

                string PMI = Grid_Escuelas.Rows[rowIndex].Cells[0].Text;
                string PMA = Grid_Escuelas.Rows[rowIndex].Cells[1].Text;
                Session.Add("X", 3);
                //mensaje.Text = PMI + "<br/> " + PMI + "<br/> " + PCN;
                // mensaje.Text = actan + "<br /> " + actam + "<br/>" + hr + "<br/>" + e.CommandName;
                if (e.CommandName == "mod")
                {
                    Nescuela.Text = PMI;
                    DropDownList1.Text = PMA;
                    NomEscDef = PMI;
                    DDtipoDef = PMA;
                    DropDownList1.Enabled = true;
                    Nescuela.Enabled = true;
                    Guardar.Enabled = true;
                    Cancelar.Enabled = true;
                    
                    Session.Add("Proceso", "mod");
                    Session.Add("NomEscDef", NomEscDef);
                    Session.Add("DDtipoDef", DDtipoDef);
                }
                else if (e.CommandName == "del")
                {
                    mensaje.Text = "Eliminado exitosamente";
                    Session.Add("Proceso", "del");
                    Session.Add("DDtipo", PMA);
                    Session.Add("NomEsc", PMI);
                    Session.Add("NomEscDef", PMI);
                    Session.Add("DDtipoDef", PMA);
                    getEscuelas();
                    Response.Redirect("confirmacion_periodos_escuelas.aspx");

                }

            }
            catch
            {

            }

        }
        protected void agregar_Click(object sender, EventArgs e)
        {
            Tperiodo.Text = "";
            Tperiodo.Enabled = true;
            Guardar.Enabled = true;
            Cancelar.Enabled = true;
            mensaje.Text = "";
            Session.Add("Proceso", "add");
        }

        protected void agregar_elemento_Click(object sender, EventArgs e)
        {
            Delemento.Enabled = true;
            DropDownList1.Enabled = true;
            Guardar.Enabled = true;
            Cancelar.Enabled = true;
            mensaje.Text = "";
            Session.Add("Proceso", "add");
        }

        protected void agregar_escuela_Click(object sender, EventArgs e)
        {
            DropDownList1.Enabled = true;
            Nescuela.Enabled = true;
            Guardar.Enabled = true;
            Cancelar.Enabled = true;
            mensaje.Text = "";
            Session.Add("Proceso", "add");
        }
        protected void Cancelar_Click(object sender, EventArgs e)
        {
            Tperiodo.Text = "";
            Delemento.Text = "";
            Delemento.Enabled = false;
            Nescuela.Text = "";
            Nescuela.Enabled = false;
            DropDownList1.Enabled = false;
            Tperiodo.Enabled = false;           
            Guardar.Enabled = false;
            Cancelar.Enabled = false;
            mensaje.Text = "";
            getPeriodos();
            getEperiodos();
            getEscuelas();
        }
        protected void Guardar_Click(object sender, EventArgs e)
        {
            string tipo = Tperiodo.Text;
            string DDtipo = DropDownList1.Text;
            string Delmnt = Delemento.Text;
            string NomEsc = Nescuela.Text;
            
            x = Session["X"].ToString();
            Session.Add("X", x);
            if (x == "1")
            {
                if (tipo != "")
                {
                    Session.Add("TipoP",tipo);
                    //Session.Add("tipoDef", tipoDef);
                    getPeriodos();
                    mensaje.Text = "Guardado exitosamente";
                }
                else
                {
                    mensaje.Text = "Favor de llenar los campos";
                }
            }
            if (x == "2")
            {
                if (Delmnt != "" && DDtipo != "")
                {
                    Session.Add("Delmnt", Delmnt);
                    Session.Add("DDtipo", DDtipo);
                    //Session.Add("DelmntDef", DelmntDef);
                    //Session.Add("DDtipoDef", DDtipoDef);
                    getEperiodos();
                    mensaje.Text = "Guardado exitosamente";
                }
                else
                {
                    mensaje.Text = "Favor de llenar los campos";
                }
            }
            if( x == "3")
            {
                if (DDtipo != "" && NomEsc != "")
                {
                    Session.Add("DDtipo",DDtipo);
                    Session.Add("NomEsc", NomEsc);
                    //Session.Add("NomEscDef", NomEscDef);
                    //Session.Add("DDt+ipoDef", DDtipoDef);
                    getEscuelas();
                    mensaje.Text = "Guardado exitosamente";
                }
                else
                {
                    mensaje.Text = "Favor de llenar los campos";
                }
            }
            Tperiodo.Text = "";
            Delemento.Text = "";
            Delemento.Enabled = false;
            Nescuela.Text = "";
            Nescuela.Enabled = false;
            DropDownList1.Enabled = false;
            Tperiodo.Enabled = false;
            Guardar.Enabled = false;
            Cancelar.Enabled = false;
            Response.Redirect("confirmacion_periodos_escuelas.aspx");
           
        }
        
    }
}