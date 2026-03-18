using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using MsBarco;

namespace SIE_KEY_USER.model
{
    public class fecha_cartas
    {
        public static void GetFechaCartas()
        {
            var res1 = DbUtil.ExecuteProc("sp_fechaCartas",
                    MsBarco.DbUtil.NewSqlParam("@fecha", null, SqlDbType.VarChar, ParameterDirection.Output, 40)
                    );

            HttpContext.Current.Session.Add("fecha", res1["@fecha"].ToString());
        }
    }
}