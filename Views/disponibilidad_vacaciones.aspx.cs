using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using MsBarco;
using System.Text;

namespace SIE_KEY_USER.Views
{
    public partial class disponibilidad_vacaciones : System.Web.UI.Page
    {
        public String seleccionTipo;
        public String tipo;
        public String ddlTipo;
        public int cantidadDisp;
        public int id;
        public bool ddlState = false;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                if (!IsPostBack)
                {
                    Page.MaintainScrollPositionOnPostBack = true;
                    String nombreEmpleado = Session["nombre"].ToString();
                    lblNombreEmpleado.Text = nombreEmpleado;
                    String MyvarNum = Session["numero"].ToString();
                    getSolicitudesVac();
                    tipo = "General";
                    getTipoDisponibilidad();
                    getGridViewRow();



                }
            }
        }

        protected void getSolicitudesVac()
        {
            gv_CheckList.Columns[4].Visible = false;
            gv_CheckList.Columns[5].Visible = false;
            var res = DbUtil.GetCursor("sp_s_disponibilidad_vac",
              new SqlParameter("@tipo_disponibilidad", '1'));
            gv_CheckList.DataSource = res;
            gv_CheckList.DataBind();
        }

        protected void getTipoDisponibilidad()
        {
            tipo = "General";
            var res = DbUtil.GetCursor("sp_get_tipo_disp_vac",
                new SqlParameter("@tipo_disponibilidad", tipo));
            ddlTipoDisp.DataSource = res;
            ddlTipoDisp.DataTextField = "desc_tipo";
            ddlTipoDisp.DataValueField = "desc_tipo";
            ddlTipoDisp.DataBind();

            foreach (GridViewRow rows in gv_CheckList.Rows)
            {
                DropDownList ddlSeleccionTipo = (DropDownList)rows.FindControl("ddlSeleccionTipo");
                ddlSeleccionTipo.DataSource = res;
                ddlSeleccionTipo.DataTextField = "desc_tipo";
                ddlSeleccionTipo.DataValueField = "desc_tipo";
                ddlSeleccionTipo.DataBind();
            }
        }

        public void getGridViewRow()
        {
            foreach (GridViewRow row in gv_CheckList.Rows)
            {
                DropDownList ddlSeleccion = (DropDownList)row.FindControl("ddlSeleccionTipo") as DropDownList;
                ddlSeleccion.Enabled = false;
            }
        }
        

        public void ddlTipoDisp_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DropDownList dropDownList = sender as DropDownList;
            ddlTipo = dropDownList.SelectedItem.Text.ToString();

            var res2 = DbUtil.GetCursor("sp_get_tipo_disp_vac",
                   new SqlParameter("@tipo_disponibilidad", ddlTipo));

            if (ddlTipo == "General")
            {
                tipo = ddlTipo;
                getTipoDisponibilidad();
                gv_CheckList.Columns[4].Visible = false;
                gv_CheckList.Columns[5].Visible = false;
                var res = DbUtil.GetCursor("sp_s_disponibilidad_vac",
                    new SqlParameter("@tipo_disponibilidad", '1'));
                gv_CheckList.DataSource = res;
                gv_CheckList.DataBind();
                cargarTipo();

            }
            else if (ddlTipo == "Particular")
            {
                gv_CheckList.Columns[4].Visible = true;
                gv_CheckList.Columns[5].Visible = true;
                var res = DbUtil.GetCursor("sp_s_disponibilidad_vac",
                   new SqlParameter("@tipo_disponibilidad", '2'));
                gv_CheckList.DataSource = res;
                gv_CheckList.DataBind();

                cargarTipo();
            }

        }

        public void ddlTipo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as Control).Parent.Parent;
            DropDownList dropDownList = sender as DropDownList;
            seleccionTipo = dropDownList.SelectedItem.Text.ToString();
            gv_CheckList.EditIndex = -1;
        }

        protected void gv_CheckList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            GridViewRow row = gv_CheckList.Rows[e.RowIndex];
            DropDownList ddlSeleccionTipo = (DropDownList)row.FindControl("ddlSeleccionTipo");
            TextBox tbDisponibilidad = (TextBox)row.FindControl("tbDisponibilidad");
            Label lblIdDisponibilidad = (Label)row.FindControl("lblIdDisp");
            ddlSeleccionTipo.Enabled = false;
            cantidadDisp = Convert.ToInt32(tbDisponibilidad.Text.ToString());
            seleccionTipo = ddlSeleccionTipo.SelectedItem.Text.ToString();
            id = Convert.ToInt32(lblIdDisponibilidad.Text.ToString());


            ddlTipo = ddlTipoDisp.SelectedItem.Text.ToString();
            gv_CheckList.EditIndex = -1;

            if (ddlTipo == "General")
            {
                
                DbUtil.ExecuteProc("sp_u_tipo_disp",
                new SqlParameter("@cantidad_disp", cantidadDisp),
                new SqlParameter("@tipo_disponibilidad", seleccionTipo),
                new SqlParameter("@id_disp", id));
                tipo = ddlTipo;
                getTipoDisponibilidad();
                gv_CheckList.Columns[4].Visible = false;
                gv_CheckList.Columns[5].Visible = false;
               
                var res = DbUtil.GetCursor("sp_s_disponibilidad_vac",
                    new SqlParameter("@tipo_disponibilidad", '1'));
                gv_CheckList.DataSource = res;
                gv_CheckList.DataBind();

                cargarTipo();
            }
             if (ddlTipo == "Particular")
            {
                
                DbUtil.ExecuteProc("sp_u_tipo_disp",
                new SqlParameter("@cantidad_disp", cantidadDisp),
                new SqlParameter("@tipo_disponibilidad", seleccionTipo),
                new SqlParameter("@id_disp", id));

                gv_CheckList.Columns[4].Visible = true;
                gv_CheckList.Columns[5].Visible = true;
                var res = DbUtil.GetCursor("sp_s_disponibilidad_vac",
                    new SqlParameter("@tipo_disponibilidad", '2'));
                gv_CheckList.DataSource = res;
                gv_CheckList.DataBind();

                cargarTipo();

            }

        }

        protected void gv_CheckList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewRow row = gv_CheckList.Rows[e.RowIndex];
            DropDownList ddlSeleccionTipo = (DropDownList)row.FindControl("ddlSeleccionTipo");
            ddlTipo = ddlTipoDisp.SelectedItem.Text.ToString();
            gv_CheckList.EditIndex = -1;

            var res2 = DbUtil.GetCursor("sp_get_tipo_disp_vac",
                   new SqlParameter("@tipo_disponibilidad", ddlTipo));

            if (ddlTipo == "General")
            {
                tipo = ddlTipo;
                getTipoDisponibilidad();
                gv_CheckList.Columns[4].Visible = false;
                gv_CheckList.Columns[5].Visible = false;
                var res = DbUtil.GetCursor("sp_s_disponibilidad_vac",
                    new SqlParameter("@tipo_disponibilidad", '1'));
                gv_CheckList.DataSource = res;
                gv_CheckList.DataBind();
                cargarTipo();
              

            }
            else if (ddlTipo == "Particular")
            {
                gv_CheckList.Columns[4].Visible = true;
                gv_CheckList.Columns[5].Visible = true;
                var res = DbUtil.GetCursor("sp_s_disponibilidad_vac",
                   new SqlParameter("@tipo_disponibilidad", '2'));
                gv_CheckList.DataSource = res;
                gv_CheckList.DataBind();
                cargarTipo();
              
            }
        }

        protected void gv_CheckList_RowEditing(object sender, GridViewEditEventArgs e)
        {
        
            gv_CheckList.EditIndex = e.NewEditIndex;
            GridViewRow row = gv_CheckList.Rows[e.NewEditIndex];
            DropDownList ddlSeleccionTipo = (DropDownList)gv_CheckList.Rows[e.NewEditIndex].FindControl("ddlSeleccionTipo");
            //DropDownList ddlSeleccionTipo = gv_CheckList.Rows[].FindControl("ddlSeleccionTipo") as DropDownList;
            ddlTipo = ddlTipoDisp.SelectedItem.Text.ToString();
            ddlSeleccionTipo.Enabled = true;


            if (ddlTipo == "General")
            {
                tipo = ddlTipo;
                getTipoDisponibilidad();
                gv_CheckList.Columns[4].Visible = false;
                gv_CheckList.Columns[5].Visible = false;
                var res = DbUtil.GetCursor("sp_s_disponibilidad_vac",
                    new SqlParameter("@tipo_disponibilidad", '1'));
                gv_CheckList.DataSource = res;
                gv_CheckList.DataBind();
                cargarTipo();

            }
            else if (ddlTipo == "Particular")
            {
                gv_CheckList.Columns[4].Visible = true;
                gv_CheckList.Columns[5].Visible = true;
                var res = DbUtil.GetCursor("sp_s_disponibilidad_vac",
                   new SqlParameter("@tipo_disponibilidad", '2'));
                gv_CheckList.DataSource = res;
                gv_CheckList.DataBind();
                cargarTipo();

            }
            
        }

        public void cargarTipo()
        {
            var res2 = DbUtil.GetCursor("sp_get_tipo_disp_vac",
                   new SqlParameter("@tipo_disponibilidad", ddlTipo));

            foreach (GridViewRow row in gv_CheckList.Rows)
            {
                DropDownList ddlSeleccionTipo = (DropDownList)row.FindControl("ddlSeleccionTipo");
                ddlSeleccionTipo.DataSource = res2;
                ddlSeleccionTipo.DataTextField = "desc_tipo";
                ddlSeleccionTipo.DataValueField = "desc_tipo";
                ddlSeleccionTipo.DataBind();

            }
            
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("matriz_disponibilidad.aspx");
        }

        protected void gv_CheckList_PreRender(object sender, EventArgs e)
        {
            var myGrid = sender as GridView;
            if (myGrid.Rows.Count > 0)
            {
                myGrid.UseAccessibleHeader = true;

                // This will add the <thead> and <tbody> elements
                myGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    } 
}