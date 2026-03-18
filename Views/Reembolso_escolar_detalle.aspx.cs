using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using MsBarco;
using System.IO;

namespace SIE_KEY_USER.Views
{
    public partial class Reembolso_escolar_detalle : System.Web.UI.Page
    {
        public string boleta;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["nombre"] as string) && !string.IsNullOrEmpty(HttpContext.Current.Session["numero"] as string))
            {
                if (!IsPostBack)
                {
                    String MyVarNom = Session["nombre"].ToString();
                    String MyVarNum = Session["numero"].ToString();

                    nombre.Text = MyVarNom;
                    getReembolso();
                    //mensaje.Text = a + ' ' + f;
                }

            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();

                Response.Redirect("Default.aspx");
            }
        }
        public void getReembolso()
        {
            String MyVarNum = Session["numero"].ToString();
            String a = Session["numreloj"].ToString();
            String f = Session["escu"].ToString();
            TextNumEmp.Text = a;
            TextEsc.Text = f;
            var res = DbUtil.ExecuteProc("sp_verReembolsoDetalle",
                new SqlParameter("@codigo", a),
                new SqlParameter("@escuela",f),
                MsBarco.DbUtil.NewSqlParam("@fecha", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@periodo", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@nomemp", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@boleta", null, SqlDbType.VarChar, ParameterDirection.Output, 32),
                MsBarco.DbUtil.NewSqlParam("@promedio", null, SqlDbType.VarChar, ParameterDirection.Output, 15)                
                );
            
            string periodo = res["@periodo"].ToString();
            boleta = res["@boleta"].ToString();
            string nomemp = res["@nomemp"].ToString();
            string fecha = res["@fecha"].ToString();
            string promedio = res["@promedio"].ToString();
            TextNom.Text = nomemp;
            TextFec.Text = fecha;
            TextProm.Text = promedio;
            var res1 = DbUtil.ExecuteProc("sp_verPorcentajeReembolso",
                new SqlParameter("@promedio",promedio),
                MsBarco.DbUtil.NewSqlParam("@porcen", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                );
            var porcentaje = res1["@porcen"];
            TextPorc.Text = porcentaje+"%";
            mensaje.Text = boleta;
            SqlConnection con;
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            DataSet ds = new DataSet();
            using (con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("sp_ver_reembolsos_periodo_escuela", con);
                cmd.Parameters.AddWithValue("@escuela", f);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                DropDP.Items.Clear();
                DropDP.DataSource = ds;
                DropDP.DataTextField = "periodo";
                DropDP.DataBind();
            }
            



           
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            Response.Redirect("Reembolso_escolar.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(200);
            try
            {
                getReembolso();
                string FileName = Path.Combine(Server.MapPath(@"~\Virtual\archivos\boletas\"),boleta);
                //System.Diagnostics.Process.Start(FileName);
                mensaje.Text = FileName;
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                Response.AddHeader("Content-Disposition", string.Format("attachment; filename = \"{0}\"", System.IO.Path.GetFileName(FileName)));
                response.TransmitFile(FileName);
                response.Flush();
                response.End();
                mensaje.Text = "No se encontró el archivo " + FileName;
            }
            catch(Exception a)
            {
                Console.WriteLine(a);
            }
        }

        protected void guardar_configurar_Click(object sender, EventArgs e)
        {

            System.Threading.Thread.Sleep(200);
            Session.Add("periodo",DropDP.Text);
            Session.Add("promedio",TextProm.Text);
            Session.Add("porcentaje",TextPorc.Text);

            Response.Redirect("Reembolso_aprobacion.aspx");
        }

        
        protected void aceptar_ac_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            if (area_txtAgreg.Text != "")
            {
                
                
                System.Threading.Thread.Sleep(200);
                String a = Session["numreloj"].ToString();
                String f = Session["escu"].ToString();
                 string motivo = area_txtAgreg.Text;
                var res = DbUtil.ExecuteProc("sp_UpdateReembolso",
                    new SqlParameter("@codigo", int.Parse(a)),
                    new SqlParameter("@escuela", f),
                    new SqlParameter("@status", 2),
                    new SqlParameter("@motivo",motivo)
                    //new SqlParameter("@periodo", DropDP.Text)
                    );
                getReembolso();
                Response.Redirect("Reembolso_escolar.aspx");

               
            }
            else
            {
                mensaje.Text = "Escribe un motivo de rechazo.";
            }
        }
    }
}