using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Utils
{
    public class DbConn9700
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
        public DbConn9700()
        {
            string connectionString = "Dsn=mcrspos;uid=microsdb;pws=microsdb";

            conn.ConnectionString = connectionString;
            conn.ConnectionTimeout = 600000;
            conn.Open();
        }
        #endregion

        #region Public Methods
        public bool ProcesarArchivo()
        {
            string stringOut;
            Logger log = new Logger();

            //Revisar query en serevr
            OdbcDataReader dataReader = ConectarADb("SELECT FCRINVNUMBER FROM MICROS.FCR_INVOICE_DATA");

            //Abrir archivo

            while (dataReader.Read())
            {
                stringOut = "";
                stringOut += dataReader[0].ToString() + ",";    //Primer campo
                stringOut += dataReader[1].ToString() + ",";    //Segundo campo
                                                                //Etc.
                                                                //Imprimir línea en el archivo    
                //GrabarArchivo.EscribirLinea(stringOut, @"c:\ruta\archivo.txt");
            }

            if (!dataReader.HasRows)
            {
                //implementar log
                //log.Write("No se encontraron registros para procesar detalles, se graba vacío");
                dataReader.Close();
                return false;
            }

            try
            {
                //cerrar archivo
            }
            catch (Exception)
            {
                //escribir log
                dataReader.Close();
                throw;
            }

            return true;
        }
        protected OdbcDataReader ConectarADb(string sqlQuery)
        {
            cmd.CommandText = sqlQuery;
            return cmd.ExecuteReader();
        }
        #endregion
    }
}
