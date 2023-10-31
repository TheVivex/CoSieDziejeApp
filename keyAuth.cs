using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WRWpr
{
    class keyAuth
    {

        #region Syncfusion KEY
        public static string SyncfusionKey = "";
        #endregion

        #region MySQL Auth
        private static string hostname = "";
        private static string username = "";
        private static string password = "";
        private static string database = "";

        public static string MySQLKey = string.Format(@"Server={0};User ID={1};Password={2};Database={3};SSL Mode=None", hostname, username, password, database);
        #endregion
    }
}
