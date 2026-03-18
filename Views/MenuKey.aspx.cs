using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIE_KEY_USER.model;

namespace SIE_KEY_USER.Views
{
    public partial class MenuKey : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                String MyVarNum = Session["numero"].ToString();
                String MyVarNom = Session["nombre"].ToString();

                nombre.Text = MyVarNom;
                String tipo = Session["tipo"].ToString();

                

                if (tipo == "1")
                {
                    LinkButton13.Enabled = false;
                    img12.Visible = false;
                    img16.Visible = true;
                    LinkButton14.Enabled = false;




                }
                if (tipo == "2")
                {
                    img4.Visible = false;
                    img4_4.Visible = true;
                    img6.Visible = false;
                    img6_6.Visible = true;
                    img8.Visible = false;
                    img8_8.Visible = true;
                    LinkButton4.Enabled = false;
                    LinkButton6.Enabled = false;
                    LinkButton8.Enabled = false;
                    LinkButton13.Enabled = false;
                    img12.Visible = false;
                    LinkButton14.Enabled = false;
                    img15.Visible = false;
                    LinkButton16.Enabled = false;
                    img16_16.Visible = true;


                }

                if (tipo == "3")
                {
                    img1.Visible = false;
                    img1_1.Visible = true;
                    img2.Visible = false;
                    img2_2.Visible = true;
                    img3.Visible = false;
                    img3_3.Visible = true;
                    img4.Visible = false;
                    img4_4.Visible = true;
                    img6.Visible = false;
                    img6_6.Visible = true;
                    img7.Visible = false;
                    img7_7.Visible = true;
                    img8.Visible = false;
                    img8_8.Visible = true;
                    LinkButton1.Enabled = false;
                    LinkButton2.Enabled = false;
                    LinkButton3.Enabled = false;
                    LinkButton4.Enabled = false;
                    LinkButton6.Enabled = false;
                    LinkButton7.Enabled = false;
                    LinkButton8.Enabled = false;
                    LinkButton13.Enabled = false;
                    img12.Visible = false;
                    LinkButton14.Enabled = false;
                    img15.Visible = false;
                    LinkButton16.Enabled = false;
                    img16_16.Visible = true;


                }
                if (tipo == "4")
                {
                    img1.Visible = false;
                    img1_1.Visible = true;
                    img2.Visible = false;
                    img2_2.Visible = true;
                    img3.Visible = false;
                    img3_3.Visible = true;
                    img5.Visible = false;
                    img5_5.Visible = true;
                    img6.Visible = false;
                    img6_6.Visible = true;
                    img7.Visible = false;
                    img7_7.Visible = true;
                    img8.Visible = false;
                    img8_8.Visible = true;
                    img9.Visible = false;
                    img9_9.Visible = true;
                    img10.Visible = false;
                    img10_10.Visible = true;
                    LinkButton1.Enabled = false;
                    LinkButton2.Enabled = false;
                    LinkButton3.Enabled = false;
                    LinkButton5.Enabled = false;
                    LinkButton6.Enabled = false;
                    LinkButton7.Enabled = false;
                    LinkButton8.Enabled = false;
                    LinkButton9.Enabled = false;
                    LinkButton10.Enabled = false;
                    LinkButton13.Enabled = false;
                    img12.Visible = false;
                    LinkButton16.Enabled = false;
                    img16_16.Visible = false;

                }
                if (tipo == "5")
                {
                    img1.Visible = true;
                    img1_1.Visible = false;
                    img2.Visible = false;
                    img2_2.Visible = false;
                    img3.Visible = false;
                    img3_3.Visible = false;
                    img4.Visible = false;
                    img4_4.Visible = false;
                    img5.Visible = false;
                    img5_5.Visible = false;
                    img6.Visible = false;
                    img6_6.Visible = false;
                    img7.Visible = false;
                    img7_7.Visible = false;
                    img8.Visible = false;
                    img8_8.Visible = false;
                    img9.Visible = false;
                    img9_9.Visible = false;
                    img10.Visible = false;
                    img10_10.Visible = false;
                    img11.Visible = false;
                    img11_11.Visible = false;
                    img12.Visible = false;
                    LinkButton1.Enabled = true;
                    LinkButton2.Enabled = false;
                    LinkButton3.Enabled = false;
                    LinkButton4.Enabled = false;
                    LinkButton5.Enabled = false;
                    LinkButton6.Enabled = false;
                    LinkButton7.Enabled = false;
                    LinkButton8.Enabled = false;
                    LinkButton9.Enabled = false;
                    LinkButton10.Enabled = false;
                    LinkButton11.Enabled = false;
                    LinkButton13.Enabled = false;
                    LinkButton14.Enabled = false;
                    img15.Visible = false;
                    LinkButton16.Enabled = true;
                    img16.Visible = true;

                }
                if (tipo == "6")
                {
                    img1.Visible = false;
                    img1_1.Visible = false;
                    img2.Visible = false;
                    img2_2.Visible = false;
                    img3.Visible = false;
                    img3_3.Visible = false;
                    img4.Visible = false;
                    img4_4.Visible = false;
                    img5.Visible = false;
                    img5_5.Visible = false;
                    img6.Visible = false;
                    img6_6.Visible = false;
                    img7.Visible = false;
                    img7_7.Visible = false;
                    img8.Visible = false;
                    img8_8.Visible = false;
                    img9.Visible = false;
                    img9_9.Visible = false;
                    img10.Visible = false;
                    img10_10.Visible = false;
                    img11.Visible = false;
                    img11_11.Visible = false;
                    LinkButton1.Enabled = false;
                    LinkButton2.Enabled = false;
                    LinkButton3.Enabled = false;
                    LinkButton4.Enabled = false;
                    LinkButton5.Enabled = false;
                    LinkButton6.Enabled = false;
                    LinkButton7.Enabled = false;
                    LinkButton8.Enabled = false;
                    LinkButton9.Enabled = false;
                    LinkButton10.Enabled = false;
                    LinkButton11.Enabled = false;
                    LinkButton14.Enabled = false;
                    img15.Visible = false;
                    LinkButton16.Enabled = false;


                }

            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Click.playSimpleSound();
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Default.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("DesbloqueoUsuarios.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("prestamos.aspx");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Actualizacion_datos.aspx");
        }
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Reembolso_escolar.aspx");
        }
        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("solicitud_vacaciones.aspx");
        }

        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Agregar_familiares.aspx");
        }

        protected void LinkButton8_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Opciones.aspx");
        }

        protected void LinkButton9_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Reimpresion.aspx");
        }

        protected void LinkButton10_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("modificar_cartas.aspx");
        }

        protected void LinkButton11_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Actualizacion_ubicacion.aspx");
        }

        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Mantenimiento_Catalogos.aspx");
        }

        protected void LinkButton13_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Certificaciones.aspx");
        }
        protected void LinkButton14_Click(object sender, EventArgs e)
        {

                System.Threading.Thread.Sleep(200);
                Response.Redirect("./Cursos/MenuCourses.aspx");
            
        }

       protected void LinkButton15_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
           Response.Redirect("solicitud_vacaciones.aspx");
        }

        protected void LinkButton16_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("escaneoSolicitudes.aspx");
        }

    }
}