using System;
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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.RemoveAll();
                Session.Abandon();
            }

            txtUsername.Focus();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.top.close()", true);
        }

        protected void entrar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);

            int n;
            bool isnumeric = int.TryParse(txtUsername.Text, out n);

            if (isnumeric == false)
            {
                string username = txtUsername.Text;
                string pass = txtPassword.Text;
                string adPath = "LDAP://na.ds.lexmark.com"; // txtDomain.Text

                LdapAuthentication adAuth = new LdapAuthentication(adPath);

                if (true == adAuth.IsAuhenticated("na.ds.lexmark.com", txtUsername.Text, txtPassword.Text))
                {
                    Session.Add("nombre", adAuth._nom.ToString());
                    string IdBadge = ""; //03/31/2019 Get IdBadge from CommonDB instead of LDAP
                    var dtTemp = DbUtil.ExecuteQuery("Select top 1 CB_Codigo from CommonDb.dbo.ColaboraV2 (NoLock) where CB_G_TEX18='" + txtUsername.Text + "'");
                    if (dtTemp.Rows.Count > 0)
                    {
                        //Session.Add("numero", adAuth._num.ToString());
                        IdBadge = dtTemp.Rows[0]["CB_Codigo"].ToString();
                        Session.Add("numero", IdBadge);
                    }
                    //Session.Add("numero", adAuth._num.ToString());

                    var codigo = Session["numero"].ToString();

                    var res = DbUtil.ExecuteProc("sp_loginKeyUser",
                        new SqlParameter("@codigo", codigo),
                        MsBarco.DbUtil.NewSqlParam("@resultado", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                        MsBarco.DbUtil.NewSqlParam("@tipo", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                        );

                    if (res["@resultado"].ToString() == "1")
                    {
                        Session.Add("tipo", res["@tipo"].ToString());

                        //Click.playSimpleSound();
                        Response.Redirect("MenuKey.aspx");
                    }
                    else
                    {
                        mensaje.Text = "No estas registrado en el sistema.";
                    }
                }
                else
                {
                    mensaje.Text = "Autenticación no exitosa. Error al ingresar con la cuenta.";
                }
            }


            
            //Gambeta hardcodeada de emergencia
            //System.Threading.Thread.Sleep(200);

            //int n;
            //bool isnumeric = int.TryParse(txtUsername.Text, out n);

            //if (txtUsername.Text == "1111" && txtPassword.Text == "1111")
            //{
            //    string username = "";
            //    string pass = txtPassword.Text;
            //    string adPath = "LDAP://na.ds.lexmark.com"; // txtDomain.Text

            //    LdapAuthentication adAuth = new LdapAuthentication(adPath);

            //        Session.Add("nombre", "Key User temporal");
            //        Session.Add("numero", "47932");

            //        var codigo = Session["numero"].ToString();

            //        var res = DbUtil.ExecuteProc("sp_loginKeyUser",
            //            new SqlParameter("@codigo", codigo),
            //            MsBarco.DbUtil.NewSqlParam("@resultado", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
            //            MsBarco.DbUtil.NewSqlParam("@tipo", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
            //            );

            //        if (res["@resultado"].ToString() == "1")
            //        {
            //            Session.Add("tipo", res["@tipo"].ToString());

            //            //Click.playSimpleSound();
            //            Response.Redirect("MenuKey.aspx");
            //        }
            //        else
            //        {
            //            mensaje.Text = "No estás registrado en el sistema.";
            //        }
            //    }
            //    else
            //    {
            //        mensaje.Text = "Autenticación no exitosa. Error al ingresar con la cuenta.";
            //    }
            }
        }
    }
