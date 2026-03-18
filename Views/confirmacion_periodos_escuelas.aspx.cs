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
    public partial class confirmacion_periodos_escuelas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                
                string proceso = Session["Proceso"].ToString();
                if (proceso == "mod")
                {
                    aviso_confirmar.Text = "¿Seguro que deseas modificar este campo?";
                }
                if (proceso == "del")
                {
                    aviso_confirmar.Text = "¿Seguro que deseas eliminar este campo?";
                }
                if (proceso == "add")
                {
                    aviso_confirmar.Text = "¿Seguro que deseas agregar este campo?";
                }
                    
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("Default.aspx");
            }
        }

        protected void guardar_configurar_Click(object sender, EventArgs e)
        {
            try
            {
                var x = Session["X"].ToString();
                string proceso = Session["Proceso"].ToString();

                if (x == "1")
                {
                    string tipo = Session["TipoP"].ToString();
                    string tipoDef = Session["tipoDef"].ToString();
                    Periodo(proceso, tipo, tipoDef);

                }
                if (x == "2")
                {
                    string DDtipo = Session["DDtipo"].ToString();
                    string Delmnt = Session["Delmnt"].ToString();
                    string DDtipoDef = Session["DDtipoDef"].ToString();
                    string DelmntDef = Session["DelmntDef"].ToString();
                    Eperiodo(proceso, DDtipo, Delmnt, DDtipoDef, DelmntDef);

                }
                if (x == "3")
                {
                    string DDtipo = Session["DDtipo"].ToString();
                    string NomEsc = Session["NomEsc"].ToString();
                    string DDtipoDef = Session["DDtipoDef"].ToString();
                    string NomEscDef = Session["NomEscDef"].ToString();
                    Nescuela(proceso, DDtipo, NomEsc, DDtipoDef, NomEscDef);

                }
                System.Threading.Thread.Sleep(5000);
                Response.Redirect("MenuKey.aspx");
            }
            catch
            {
            }
                //modificar();               
        }
        public void Periodo(string proceso, string tipo,string tipoDef)
        {
            var res = DbUtil.ExecuteProc("sp_PeriodoMDA",
                new SqlParameter("@proceso", proceso),
                new SqlParameter("@tipo",tipo),
                new SqlParameter ("@tipoDef",tipoDef)
            );
            lblErrMsg.Text = "cambios guardados exitosamente";
        }
        public void Eperiodo(string proceso, string DDtipo, string Delmnt, string DDtipoDef, string DelmntDef)
        {
            var res=DbUtil.ExecuteProc("sp_EperiodoMDA",
                new SqlParameter("@proceso", proceso),
                new SqlParameter("@DDtipo", DDtipo),
                new SqlParameter("@DDtipoDef", DDtipoDef),
                new SqlParameter("@Delmnt",Delmnt),
                new SqlParameter("@DelmntDef",DelmntDef)
            );
            lblErrMsg.Text = "cambios guardados exitosamente";
        }
        public void Nescuela(string proceso, string DDtipo, string NomEsc, string DDtipoDef, string NomEscDef)
        {
            var res = DbUtil.ExecuteProc("sp_NescuelaMDA",
                new SqlParameter("@proceso", proceso),
                new SqlParameter("@DDtipo", DDtipo),
                new SqlParameter("@DDtipoDef", DDtipoDef),
                new SqlParameter("@NomEsc", NomEsc),
                new SqlParameter("@NomEscDef", NomEscDef)
            );
            lblErrMsg.Text = "cambios guardados exitosamente";
        }

        protected void NoPeriodos_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Periodos_escuelas.aspx");
        }
    }
}