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
    public partial class configurar_prestamo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                if (!IsPostBack)
                {
                    String MyVarNom = Session["nombre"].ToString();
                    String MyVarNUm = Session["numero"].ToString();

                    nombre.Text = MyVarNom;

                    var res = DbUtil.ExecuteProc("sp_getPretamo",
                        MsBarco.DbUtil.NewSqlParam("@fec_ini", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                        MsBarco.DbUtil.NewSqlParam("@fec_fin", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                        MsBarco.DbUtil.NewSqlParam("@num_max", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                        MsBarco.DbUtil.NewSqlParam("@num_min", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                        MsBarco.DbUtil.NewSqlParam("@tasa", null, SqlDbType.VarChar, ParameterDirection.Output, 40)
                        );

                    TextBox1.Text = res["@fec_ini"].ToString();
                    TextBox2.Text = res["@fec_fin"].ToString();
                    TextBox3.Text = res["@num_max"].ToString();
                    TextBox4.Text = res["@num_min"].ToString();
                    TextBox5.Text = res["@tasa"].ToString();
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
            System.Threading.Thread.Sleep(200);

            if (TextBox1.Text != "" && TextBox2.Text != "" && TextBox3.Text != "" && TextBox4.Text != "" && TextBox5.Text != "")
            {
                float n;
                bool isNumeric = float.TryParse(TextBox5.Text, out n);

                int a = int.Parse(hdnField.Value);
                int a1 = int.Parse(TextBox4.Text);

                if (isNumeric == true)
                {
                    if (a >= 1 && a <= 52 && a1 >= 1 && a1 <= 52)
                    {
                        if (TextBox1.Text != TextBox2.Text)
                        {
                            if (a1 < a)
                            {
                                var res = DbUtil.ExecuteProc("sp_Configurarprestamo",
                                    new SqlParameter("@fec_ini", TextBox1.Text),
                                    new SqlParameter("@fec_fin", TextBox2.Text),
                                    new SqlParameter("@num_max", TextBox3.Text),
                                    new SqlParameter("@num_min", TextBox4.Text),
                                    new SqlParameter("@tasa_int", TextBox5.Text)
                                    );

                                //Click.playSimpleSound();
                                Response.Redirect("confirmar_configuracion.aspx");
                            }
                            else
                            {
                                mensaje.Text = "El número de semanas mínimo no puede ser mayor al número de semanas máximo";
                            }
                        }
                        else
                        {
                            mensaje.Text = "No puedes seleccionar la misma fecha.";
                        }
                    }
                    else
                    {
                        mensaje.Text = "No puedes poner numeros mayor a 52 ó menor a 1.";
                    }
                }
                else
                {
                    mensaje.Text = "No puedes introducir letras.";
                }
            }
            else
            {
                mensaje.Text = "Por favor completa todos los campos.";
            }
        }

        protected void cancelar_configurar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("prestamos.aspx");
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}