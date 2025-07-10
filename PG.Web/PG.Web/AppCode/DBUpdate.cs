using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
//using System.Linq.Dynamic;
using PG.Core.DBBase;
using PG.Core.Extentions;
using PG.Core.DBFilters;

namespace PG.Web
{
    public class DBUpdate
    {

        public static string GetSQLFromFile(string pFilePath)
        {
            string data = string.Empty;
            data = File.ReadAllText(pFilePath);
            return data;
        }


        public static bool ExecuteSQL(string strSQL)
        {
            DBContext dc = DBContextManager.CreateDBContext();

            dc.InitConnection();


            SqlCommand cmd = (SqlCommand)dc.CreateDbCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL;

            dc.ExecuteNonQuery(cmd);

            dc.CloseConnection();


            AppCache.Clear();


            //server = new Server(new ServerConnection(conn));
            //server.ConnectionContext.ExecuteNonQuery(script);


            return true;

        }
    }
}
