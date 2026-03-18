using MsBarco;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIE_KEY_USER.Views
{
    public partial class Certificaciones : System.Web.UI.Page
    {
        private static string codigo = "";
        private static string codigoo = "";
        // private static string palabra = "LCNIA,LCNBA,LCNBC";
        private static string palabra = "";
        //char alas = "alas";

        // private static bool planta = false;
        //int asd = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                if (!IsPostBack)
                {
                    String MyvarNum = Session["numero"].ToString();
                    num_person(MyvarNum);

                }
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }
        }

        private void buscarCertificaciones(String numero,string buscador)
        {
            string path = @"~\Virtual\FoPerfil\" + numero + ".jpg";
            string path1 = @"~\Virtual\FoPerfil\duda.jpg";
            if (System.IO.File.Exists(Server.MapPath(path))){
                imgEmpleado.ImageUrl = path;
            }else{
                imgEmpleado.ImageUrl = path1;

            }
            //-----
            /*
            try{
            var res = DbUtil.GetCursor("sp_S_cursos",
                    new System.Data.SqlClient.SqlParameter("@codigo", numero)
                    //,new System.Data.SqlClient.SqlParameter("@planta", planta)
                    );
                
                grdCertificaciones.DataSource = res;
                grdCertificaciones.DataBind();
                lblMensaje.Text = "";

                if(grdCertificaciones.Rows.Count == 0){
                    lblMensaje.Text = "El empleado no tiene certificaciones registradas";  
                }
                txtCodigo.Text = "";
                txtCodigo.Focus();
                }
                */
            if (buscador.Length >= 3)
            {
                try
                {
                    var res = DbUtil.GetCursor("sp_S_cursoslc",
                            new System.Data.SqlClient.SqlParameter("@codigo", numero)
                            , new System.Data.SqlClient.SqlParameter("@nombre", buscador)
                            );

                    grdCertificaciones.DataSource = res;
                    grdCertificaciones.DataBind();
                    lblMensaje.Text = "";

                    if (grdCertificaciones.Rows.Count == 0)
                    {
                        lblMensaje.Text = "El empleado no tiene certificaciones registradas";
                    }
                    txtCodigo.Text = "";
                    TextBox1.Text = "";
                    txtCodigo.Focus();
                    TextBox1.Focus();
                }
                catch (Exception e)
                {
                    txtCodigo.Text = "";
                    TextBox1.Text = "";
                    txtCodigo.Focus();
                    TextBox1.Focus();
                }
            }
            else if (numero.Length >= 5)
            {
                try
                {
                    var res = DbUtil.GetCursor("sp_S_cursos",
                            new System.Data.SqlClient.SqlParameter("@codigo", numero)
                            );

                    grdCertificaciones.DataSource = res;
                    grdCertificaciones.DataBind();
                    lblMensaje.Text = "";

                    if (grdCertificaciones.Rows.Count == 0)
                    {
                        lblMensaje.Text = "El empleado no tiene certificaciones registradas";
                    }
                    txtCodigo.Text = "";
                    txtCodigo.Focus();
                }
                catch (Exception e)
                {
                    txtCodigo.Text = "";
                    txtCodigo.Focus();
                }
            }
            else {
                lblMensaje.Text = "No se ingreso nada bro";
            }
            //------------
            try { 
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = DbUtil.GetCursor("sp_S_datos_generales",
                    new System.Data.SqlClient.SqlParameter("@codigo", numero)
                    );
                lblNombre.Text = dt.Rows[0].ItemArray[0].ToString();
                lblFechaIngreso.Text = dt.Rows[0].ItemArray[1].ToString();
                lblNoReloj.Text = dt.Rows[0].ItemArray[2].ToString();
                lblTurno.Text = dt.Rows[0].ItemArray[3].ToString();
                lblSupervisor.Text = dt.Rows[0].ItemArray[4].ToString();
                lblArea.Text = dt.Rows[0].ItemArray[5].ToString();
            }
            catch (Exception e)
            {
                lblMensaje.Text = "No se encontró el empleado";
                lblNombre.Text = "";
                lblNoReloj.Text = "";
                lblFechaIngreso.Text = "";
                lblTurno.Text = "";
                lblSupervisor.Text = "";
                lblArea.Text = "";
            }
            
        }

        private void num_person(string num) {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            codigo = txtCodigo.Text;
            //planta = txtPlanta.Text;
            codigoo = TextBox1.Text;

            if (codigoo.Length >= 3) {
                buscarCertificaciones(codigo, codigoo);
                num_person(codigo);
            }
            else if (codigo.Length >= 5)  {
                buscarCertificaciones(codigo.Substring(0, 5), "");
                num_person(codigo);
            }
            else {
                txtCodigo.Text = "";
                // txtPlanta.Text = "";
                TextBox1.Text = "";
            }
        }

        /*protected void Button1_Click(object sender, EventArgs e){
            codigo = txtCodigo.Text;
            palabra = TextBox1.Text;
            if (codigo.Length >= 5){
                mostrarlcplanta(palabra.Substring(0, 6), codigo.Substring(0, 5));
            }
            else{
                txtCodigo.Text = "";
                TextBox1.Text = "";
            }
        }*/

       /* protected void Button2_Click(object sender,EventArgs e) {
            codigo = txtCodigo.Text;
            palabra = TextBox1.Text;
            if (codigo.Length >= 5)
            {
                buscarCertificaciones(codigo);
                //asd = 1;
            }
            else
            {
                txtCodigo.Text = "";
                TextBox1.Text = "";
            }
        }*/

        /*private void mostrarlcplanta(string numero,string numero2){
            try{
                var res = DbUtil.GetCursor("sp_S_cursoslc",
                        new System.Data.SqlClient.SqlParameter("@codigo", numero)
                        ,new System.Data.SqlClient.SqlParameter("@nombre", numero2)
                        );

                grdCertificaciones.DataSource = res;
                grdCertificaciones.DataBind();
                lblMensaje.Text = "";

                if (grdCertificaciones.Rows.Count == 0)
                {
                    lblMensaje.Text = "The CHALE Mistake";
                }
                txtCodigo.Text = "";
                txtCodigo.Focus();
            }
            catch (Exception e)
            {
                txtCodigo.Text = "";
                txtCodigo.Focus();

            }
        }*/

        protected void btnMenu(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("MenuKey.aspx");
        }
        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            String MyvarNum = Session["numero"].ToString();
            grdCertificaciones.PageIndex = e.NewPageIndex;
            buscarCertificaciones(codigo,codigoo);
           // mostrarlcplanta(codigo, palabra);

        }

    }

    
}


