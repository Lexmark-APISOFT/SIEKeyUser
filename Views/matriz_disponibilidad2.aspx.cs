using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MsBarco;
using System.Data;
using System.Data.SqlClient;


namespace SIE_KEY_USER.Views
{
    public partial class matriz_disponibilidad2 : System.Web.UI.Page
    {
        public String seleccionTipo;
        public String tipo;
        public int cantidadDisp;
        public int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                if (!IsPostBack)
                {
                    String nombreEmpleado = Session["nombre"].ToString();
                    lblNombreEmpleado.Text = nombreEmpleado;
                    //getSolicitudesVac();
                }
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }
        }

        protected void getSolicitudesVac()
        {
            var res = DbUtil.GetCursor("sp_s_allDisponibilidad_vac");
            gv_CheckList.DataSource = res;
            gv_CheckList.DataBind();
        }

        protected void gv_CheckList_PreRender(object sender, EventArgs e)
        {

            getSolicitudesVac();
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

        protected void btnDisponibilidad_Click(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static string EditClick(string cantidadDisp, string valTurno, string valPlanta,
                                    string valDept, string valArea, string valPuesto, string valCbCodigo, string valClasifi)
        {
            var res = DbUtil.ExecuteProc("sp_u_cantidad_disp",
           new SqlParameter("@cantidad_disp", Int32.Parse(cantidadDisp) + 1),
           new SqlParameter("@turno", valTurno),
           new SqlParameter("@planta", valPlanta),
           new SqlParameter("@dept", valDept),
           new SqlParameter("@area", valArea),
           new SqlParameter("@puesto", valPuesto),
           new SqlParameter("@cb_codigo", ""),
           new SqlParameter("@cb_clasifi", ""),
          MsBarco.DbUtil.NewSqlParam("@res", null, SqlDbType.VarChar, ParameterDirection.Output, 10));

            int resultado = Int32.Parse(res["@res"].ToString());

            if (resultado == 1)
            {
                return "El proceso se completo correctamente.";
            }
            else
            {
                return "La disponibilidad insertada sobrepasa el límite de disponibilidad disponible.";
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("solicitud_vacaciones.aspx");
        }
        
        protected void gv_CheckList_PageIndexChanged(object sender, EventArgs e)
        {
            // Retrieve the pager row.
            GridViewRow pagerRow = gv_CheckList.BottomPagerRow;

            gv_CheckList.PageIndex = gv_CheckList.PageIndex+1;
            getSolicitudesVac();
        }
    }
}