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
    public partial class Porcentajes_promedios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                String MyVarNom = Session["nombre"].ToString();
                String MyVarNum = Session["numero"].ToString();

                nombre.Text = MyVarNom;
                getPorcProm();

            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }
        }
        public void getPorcProm()
        {
            String MyVarNum = Session["numero"].ToString();

        
            var res = DbUtil.GetCursor("sp_verPorcProm");

            Grid_PorcentProm.DataSource = res;
            Grid_PorcentProm.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Reembolso_escolar.aspx");
        }

        protected void Grid_PorcentProm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           // try
            //{
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;
                mensaje.Text = "";

                string PMI = Grid_PorcentProm.Rows[rowIndex].Cells[0].Text;
                string PMA = Grid_PorcentProm.Rows[rowIndex].Cells[1].Text;
                string PCN =  Grid_PorcentProm.Rows[rowIndex].Cells[2].Text;
                
                //mensaje.Text = PMI + "<br/> " + PMA + "<br/> " + PCN;
               // mensaje.Text = actan + "<br /> " + actam + "<br/>" + hr + "<br/>" + e.CommandName;
                if (e.CommandName == "mod")
                {
                    promMin.Text = PMI;
                    promMax.Text = PMA;
                    porcent.Text = PCN;
                    Session.Add("PMIDef", PMI);
                    Session.Add("PMADef", PMA);
                    Session.Add("PCNDef", PCN);
                    promMin.Enabled = true;
                    promMax.Enabled = true;
                    porcent.Enabled = true;
                    Guardar.Enabled = true;
                    Cancelar.Enabled = true;
                }
                else if (e.CommandName == "del")
                {
                    
                    var res = DbUtil.ExecuteProc("sp_insertupdatePorcprom",
                        new SqlParameter("@prommin", float.Parse(PMI)),
                        new SqlParameter("@prommax", float.Parse(PMA)),
                        new SqlParameter("@porcen", PCN),
                        new SqlParameter("@del", "2"),
                        new SqlParameter("@PMIDef", float.Parse(PMI)),
                        new SqlParameter("@PMADef", float.Parse(PMA)),
                        new SqlParameter("@PCNDef", PCN)
                        );
                    getPorcProm();
                    mensaje.Text = "Eliminado exitosamente";
                    
                }
               
            //}
            /*catch
            {

            }*/
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            promMin.Text = "";
            promMax.Text = "";
            porcent.Text = "";
            promMin.Enabled = false;
            promMax.Enabled = false;
            porcent.Enabled = false;
            Guardar.Enabled = false;
            Cancelar.Enabled = false;
            mensaje.Text = "";
            getPorcProm();
        }

        protected void ModifProm_Click(object sender, EventArgs e)
        {
            promMinReem.Enabled = true;
            GuardarProm.Enabled = true;
            CancelarProm.Enabled = true;
            mensaje.Text = "";
            getPorcProm();
        }

        protected void CancelarProm_Click(object sender, EventArgs e)
        {
            promMinReem.Enabled = false;
            GuardarProm.Enabled = false;
            CancelarProm.Enabled = false;
            mensaje.Text = "";
            getPorcProm();
        }

        protected void Guardar_Click(object sender, EventArgs e)
        {
            var PMI = promMin.Text;
            var PMA = promMax.Text;
            var PCNS = porcent.Text;
            if (PMI != "" && PMA != "" && PCNS != "")
            {
                int PCN = int.Parse(PCNS);
                if (PCN <= 100 && PCN > 0)
                {
                    if ((float.Parse(PMI) <= 10 && float.Parse(PMI) >= 0) && (float.Parse(PMA) <= 10 && float.Parse(PMA) >= 0))
                    {
                        if (float.Parse(PMI) < float.Parse(PMA))
                        {
                            string PMIDef = Session["PMIDef"].ToString();
                            string PMADef = Session["PMADef"].ToString();
                            string PCNDef = Session["PCNDef"].ToString();
                            mensaje.Text = "Guardado exitosamente";
                            if (PMIDef != "" && PMADef != "" && PCNDef != "")
                            {
                                var res = DbUtil.ExecuteProc("sp_insertupdatePorcprom",
                                    new SqlParameter("@prommin", float.Parse(PMI)),
                                    new SqlParameter("@prommax", float.Parse(PMA)),
                                    new SqlParameter("@porcen", PCNS),
                                    new SqlParameter("@del", "1"),
                                    new SqlParameter("@PMIDef", float.Parse(PMIDef)),
                                    new SqlParameter("@PMADef", float.Parse(PMADef)),
                                    new SqlParameter("@PCNDef", PCNDef)
                                    );
                                getPorcProm();
                                promMin.Text = "";
                                promMax.Text = "";
                                porcent.Text = "";
                                promMin.Enabled = false;
                                promMax.Enabled = false;
                                porcent.Enabled = false;
                                Guardar.Enabled = false;
                                Cancelar.Enabled = false;
                                mensaje.Text = "";
                                getPorcProm();
                            }
                            else
                            {
                                var res = DbUtil.ExecuteProc("sp_insertupdatePorcprom",
                                    new SqlParameter("@prommin", float.Parse(PMI)),
                                    new SqlParameter("@prommax", float.Parse(PMA)),
                                    new SqlParameter("@porcen", PCNS),
                                    new SqlParameter("@del", "1"),
                                    new SqlParameter("@PMIDef", float.Parse(PMI)),
                                    new SqlParameter("@PMADef", float.Parse(PMA)),
                                    new SqlParameter("@PCNDef", PCNS)
                                    );
                                getPorcProm();
                                promMin.Text = "";
                                promMax.Text = "";
                                porcent.Text = "";
                                promMin.Enabled = false;
                                promMax.Enabled = false;
                                porcent.Enabled = false;
                                Guardar.Enabled = false;
                                Cancelar.Enabled = false;
                                mensaje.Text = "";
                                getPorcProm();
                            }
                        }
                        else
                        {
                            mensaje.Text = "El promedio mínimo no debe ser mayor al promedio máaximo";
                        }
                    }
                    else
                    {
                        mensaje.Text = "El promedio debe estar en un rango entre 0 y 10";
                    }
                }
                else
                {
                    mensaje.Text = "El porcentaje no debe ser mayor a 100 ni menor a 0";
                }
            }
            else
            {
                mensaje.Text = "Favor de llenar todos los campos";
            }
        }

        protected void agregar_Click(object sender, EventArgs e)
        {
            Session.Add("PMIDef", "");
            Session.Add("PMADef", "");
            Session.Add("PCNDef", "");
            promMin.Text = "";
            promMax.Text = "";
            porcent.Text = "";
            promMin.Enabled = true;
            promMax.Enabled = true;
            porcent.Enabled = true;
            Guardar.Enabled = true;
            Cancelar.Enabled = true;
            mensaje.Text = "";
        }

        protected void GuardarProm_Click(object sender, EventArgs e)
        {
            var PMR = promMinReem.Text;
            if (float.Parse(PMR) <= 10 && float.Parse(PMR) >= 0)
            {
                mensaje.Text = "Guardado exitosamente";
                var res = DbUtil.ExecuteProc("sp_updatePrommin",
                    new SqlParameter("@prommin", float.Parse(PMR))
                    );
            }
            else
            {
                mensaje.Text = "El promedio mínimo debe estar en un rango entre 0 y 10";
            }
        }
    }
}