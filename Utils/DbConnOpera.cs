using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Utils
{
    public class DbConnOpera
    {
        #region Properties
        private OdbcCommand cmd;
        public OdbcCommand Cmd
        {
            get { return cmd; }
            set { cmd = value; }
        }

        private OdbcConnection conn;
        public OdbcConnection Conn
        {
            get { return conn; }
            set { conn = value; }
        }
        #endregion

        #region Constructor
        public DbConnOpera()
        {
            string connectionString = "Dsn=opera;uid=opera;pws=opera";

            conn.ConnectionString = connectionString;
            conn.ConnectionTimeout = 600000;
            conn.Open();
        }
        #endregion
    }
}
