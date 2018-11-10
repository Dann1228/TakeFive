using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TabkeFiveWebApplication.DAL
{
    public class DBConnClass
    {

        public static string GetDBConn()
        {

            string connStr =
          //  @"Server=tcp:takefive.database.windows.net,1433;Initial Catalog=TakeFiveDB;Persist Security Info=False;User ID=tfadmin;Password=TFdb1228;";
            @"Data Source=.;Initial Catalog=TakeFiveDB;Integrated Security=True";
            return connStr;
        }
                     
    }
}