using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Utils;
using System.Collections.Generic;

namespace ExportadorOpera
{
    public partial class frmMain : Form
    {
        Logger log = new Logger();
        ConfigurationReader configReader = new ConfigurationReader();
        Configuration configuration;
        MapeoItemsReader mapeoItemsReader = new MapeoItemsReader();
        Dictionary<string, string> mapeoItems;
        
        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                configuration = configReader.Read("Config.ini");
                mapeoItems = mapeoItemsReader.Read("MapeoItems.ini");
                log.W("Inicia exportador Opera");
            }
            catch (Exception ex)
            {
                log.W(ex.Message);
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            string tipoDocumento = "BOLETA VENTA (EL)";
            log.W("Inicia procesamiento " + tipoDocumento + " - " + dtpDesde.Value.ToString("dd/MM/yyyy"));

            dtpDesde.Enabled = false;
            btnGenerar.Enabled = false;
            btnSalir.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;

            using (OracleConnection connection = ConnDb.GetDBConnection(configuration))
            {
                Logger log = new Logger();

                if (connection != null)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    try
                    {
                        connection.Open();
                        OracleCommand command = connection.CreateCommand();
                        OracleCommand subCommand = connection.CreateCommand();

                        string sql, sqlTrx, sqlPagos, sqlTransbank, sqlIva, sqlPaidouts, sqlPropina, sqlDescuentos, sqlSpa;

                        sql = "SELECT BILL_NO, TO_CHAR(BUSINESS_DATE, 'dd/MM/yyyy') BUSINESSDATE, TO_CHAR(BUSINESS_DATE, 'yyyyMM') PERIODO, RESV_NAME_ID, ROOM, TOT_REV_TAXABLE, TOT_NONREV_TAXABLE, TOT_REV_NONTAXABLE, TOT_NONREV_NONTAXABLE, TOTAL_NET, TOTAL_GROSS, TAX1_AMT, NET1_AMT, FOLIO_TYPE, FOLIO_NO, CASHIER_ID FROM FOLIO$_TAX " +
                              "WHERE TO_CHAR(BUSINESS_DATE, 'YYYYmmDD') = '" + dtpDesde.Value.ToString("yyyyMMdd") + "' AND TOTAL_NET <> 0";

                        command.CommandText = sql;
                        OracleDataReader reader = command.ExecuteReader();

                        string path = dtpDesde.Value.ToString("yyyyMMdd") + "_BoletaDeVentaEL.xml";
                        var xDoc = XmlFormatter.OpenFile(path);
                        
                        int i = 0;
                        while (reader.Read())
                        {
                            i++;

                            var bveh = new BoletaVentaHeader()
                            {
                                Correlativo = i,
                                InvNumber = Convert.ToInt32(reader["BILL_NO"]),
                                Fecha = reader["BUSINESSDATE"].ToString(),
                                Usuario = reader["CASHIER_ID"].ToString(),
                                Periodo = reader["PERIODO"].ToString(),
                                Neto = Convert.ToDecimal(reader["TOTAL_NET"]),
                                Subtotal = Convert.ToDecimal(reader["TOTAL_NET"]),
                                Total = Convert.ToDecimal(reader["TOTAL_GROSS"])
                            };

                            XmlFormatter.PrintDocument(path, xDoc, i);
                            XmlFormatter.PrintHeaderElements(xDoc, path, tipoDocumento, bveh, i);

                            //Transacciones
                            sqlTrx = "SELECT FT.TAX_INCLUSIVE_YN, FT.NET_AMOUNT, FT.GROSS_AMOUNT, FT.CHEQUE_NUMBER," +
                                    "FT.TRX_NO, TC.DESCRIPTION, FT.TC_GROUP, FT.TRX_CODE, FT.QUANTITY, TO_CHAR(FT.TRX_DATE, 'dd/MM/yyyy') TRX_DATE, FT.ROOM, FT.RESV_NAME_ID," + 
                                    "FT.PRICE_PER_UNIT, FT.TRX_AMOUNT, FT.POSTED_AMOUNT, FT.REVENUE_AMT, FT.BILL_NO " +
                                    "FROM FINANCIAL_TRANSACTIONS FT INNER JOIN TRX$_CODES TC ON TC.TRX_CODE = FT.TRX_CODE " +
                                    "WHERE RESV_NAME_ID = " + reader["RESV_NAME_ID"] + " and FT.TC_GROUP not in (70, 80, 90) AND NET_AMOUNT > 0 and BILL_NO = " + reader["BILL_NO"].ToString();

                            sqlIva = "SELECT NVL(SUM(FT.TRX_AMOUNT), 0) AMOUNT " +
                                    "FROM FINANCIAL_TRANSACTIONS FT INNER JOIN TRX$_CODES TC ON TC.TRX_CODE = FT.TRX_CODE " +
                                    "WHERE RESV_NAME_ID = " + reader["RESV_NAME_ID"] + " and FT.TC_GROUP = 80 AND NET_AMOUNT <> 0 and BILL_NO = " + reader["BILL_NO"].ToString();

                            sqlPagos = "SELECT TO_CHAR(FT.BUSINESS_DATE, 'dd/MM/yyyy') BUSINESS_DATE, FT.TAX_INCLUSIVE_YN, FT.NET_AMOUNT, FT.GROSS_AMOUNT, FT.CHEQUE_NUMBER," +
                                    "FT.TRX_NO, TC.DESCRIPTION, FT.TC_GROUP, FT.TRX_CODE, FT.QUANTITY, TO_CHAR(FT.TRX_DATE, 'dd/MM/yyyy') TRX_DATE, FT.ROOM, FT.RESV_NAME_ID," +
                                    "FT.PRICE_PER_UNIT, FT.TRX_AMOUNT, FT.POSTED_AMOUNT, FT.REVENUE_AMT, FT.BILL_NO " +
                                    "FROM FINANCIAL_TRANSACTIONS FT INNER JOIN TRX$_CODES TC ON TC.TRX_CODE = FT.TRX_CODE " +
                                    "WHERE RESV_NAME_ID = " + reader["RESV_NAME_ID"] + " and FT.TC_GROUP = 90 AND BILL_NO = " + reader["BILL_NO"].ToString();

                            sqlTransbank = "SELECT NVL(SUM(FT.TRX_AMOUNT), 0) AMOUNT " +
                                    "FROM FINANCIAL_TRANSACTIONS FT INNER JOIN TRX$_CODES TC ON TC.TRX_CODE = FT.TRX_CODE " +
                                    "WHERE RESV_NAME_ID = " + reader["RESV_NAME_ID"] + " and FT.TRX_CODE = 9050 AND BILL_NO = " + reader["BILL_NO"].ToString();

                            sqlPropina = "SELECT NVL(SUM(FT.TRX_AMOUNT), 0) AMOUNT " +
                                    "FROM FINANCIAL_TRANSACTIONS FT INNER JOIN TRX$_CODES TC ON TC.TRX_CODE = FT.TRX_CODE " +
                                    "WHERE RESV_NAME_ID = " + reader["RESV_NAME_ID"] + " and FT.TC_GROUP = 70 AND NET_AMOUNT <> 0 and BILL_NO = " + reader["BILL_NO"].ToString();

                            sqlPaidouts = "SELECT NVL(SUM(FT.TRX_AMOUNT), 0) AMOUNT " +
                                    "FROM FINANCIAL_TRANSACTIONS FT INNER JOIN TRX$_CODES TC ON TC.TRX_CODE = FT.TRX_CODE " +
                                    "WHERE RESV_NAME_ID = " + reader["RESV_NAME_ID"] + " and FT.TC_GROUP = 75 AND NET_AMOUNT <> 0 and BILL_NO = " + reader["BILL_NO"].ToString();

                            sqlSpa = "SELECT NVL(SUM(FT.TRX_AMOUNT), 0) AMOUNT " +
                                     "FROM FINANCIAL_TRANSACTIONS FT INNER JOIN TRX$_CODES TC ON TC.TRX_CODE = FT.TRX_CODE " +
                                     "WHERE RESV_NAME_ID = " + reader["RESV_NAME_ID"] + " and FT.TC_GROUP = 40 AND NET_AMOUNT <> 0 and BILL_NO = " + reader["BILL_NO"].ToString();

                            sqlDescuentos = "SELECT NVL(SUM(FT.TRX_AMOUNT), 0) AMOUNT " +
                                    "FROM FINANCIAL_TRANSACTIONS FT INNER JOIN TRX$_CODES TC ON TC.TRX_CODE = FT.TRX_CODE " +
                                    "WHERE RESV_NAME_ID = " + reader["RESV_NAME_ID"] + " and FT.TC_GROUP NOT IN (70, 80, 90) AND NET_AMOUNT < 0 and BILL_NO = " + reader["BILL_NO"].ToString();

                            command = connection.CreateCommand();
                            command.CommandText = sqlTrx;
                            OracleDataReader subReader = command.ExecuteReader();

                            int j = 0;
                            string articulo;
                            while (subReader.Read())
                            {
                                j++;
                                try
                                {
                                    articulo = mapeoItems[subReader["TRX_CODE"].ToString()];
                                }
                                catch
                                {
                                    articulo = subReader["TRX_CODE"].ToString();
                                }
                                var bved = new BoletaVentaDetalle()
                                {
                                    Correlativo = i,
                                    Secuencia = j,
                                    Articulo = articulo,
                                    Cantidad = Convert.ToInt32(subReader["QUANTITY"]),
                                    Precio = decimal.Round(Convert.ToDecimal(subReader["PRICE_PER_UNIT"]), 2),
                                    Subtotal = decimal.Round(Convert.ToDecimal(subReader["NET_AMOUNT"]) / 1.19m, 2),
                                    Iva = decimal.Round(Convert.ToDecimal(subReader["NET_AMOUNT"]) * 0.19m, 2),
                                    Fecha = subReader["TRX_DATE"].ToString(),
                                    Total = decimal.Round(Convert.ToDecimal(subReader["GROSS_AMOUNT"]), 2)
                                };
                                XmlFormatter.PrintDetail(xDoc, path, i, j);
                                XmlFormatter.PrintDetailElements(xDoc, path, tipoDocumento, bved, i, j);
                            }

                            //PRINT PAGOS
                            command = connection.CreateCommand();
                            command.CommandText = sqlPagos;
                            subReader = command.ExecuteReader();

                            j = 0;
                            while (subReader.Read())
                            {
                                j++;
                                try
                                {
                                    articulo = mapeoItems[subReader["TRX_CODE"].ToString()];
                                }
                                catch
                                {
                                    articulo = subReader["TRX_CODE"].ToString();
                                }
                                var bvep = new BoletaVentaPago()
                                {
                                    Correlativo = i,
                                    Linea = j,
                                    TrxCode = articulo,
                                    BillNo = Convert.ToInt32(subReader["BILL_NO"]),
                                    Fecha = subReader["BUSINESS_DATE"].ToString(),
                                    Monto = Convert.ToDecimal(subReader["TRX_AMOUNT"])
                                };
                                XmlFormatter.PrintPayment(xDoc, path, i, j);
                                XmlFormatter.PrintPaymentElements(xDoc, path, tipoDocumento, bvep, i, j, true);
                            }

                            //Print VALORES(DESCUENTO)
                            command = connection.CreateCommand();
                            command.CommandText = sqlDescuentos;
                            subReader = command.ExecuteReader();
                            subReader.Read();

                            var bvev = new BoletaVentaValores()
                            {
                                Correlativo = i,
                                Factor = -1,
                                Monto = Convert.ToDecimal(subReader["AMOUNT"]),
                                Nombre = "DESCUENTO",
                                Orden = 2,
                                Porcentaje = 0
                            };
                            XmlFormatter.PrintValues(xDoc, path, i, 1);
                            XmlFormatter.PrintValueElements(xDoc, path, tipoDocumento, bvev, i, 1);

                            //PRINT VALORES(EXENTO - PAIDOUTS)
                            command = connection.CreateCommand();
                            command.CommandText = sqlPaidouts;
                            subReader = command.ExecuteReader();
                            subReader.Read();

                            bvev = new BoletaVentaValores()
                            {
                                Correlativo = i,
                                Factor = 1,
                                Monto = Convert.ToDecimal(subReader["AMOUNT"]),
                                Nombre = "EXENTO",
                                Orden = 3,
                                Porcentaje = 0
                            };
                            XmlFormatter.PrintValues(xDoc, path, i, 2);
                            XmlFormatter.PrintValueElements(xDoc, path, tipoDocumento, bvev, i, 2);

                            //PRINT VALORES(IVA)
                            command.Dispose();
                            command.CommandText = sqlIva;
                            subReader = command.ExecuteReader();
                            subReader.Read();

                            bvev = new BoletaVentaValores()
                            {
                                Correlativo = i,
                                Factor = 0,
                                Monto = Convert.ToDecimal(subReader["AMOUNT"]),
                                Nombre = "IVA",
                                Orden = 5,
                                Porcentaje = 19m
                            };
                            XmlFormatter.PrintValues(xDoc, path, i, 3);
                            XmlFormatter.PrintValueElements(xDoc, path, tipoDocumento, bvev, i, 3);

                            //PRINT VALORES(NBASE)
                            bvev = new BoletaVentaValores()
                            {
                                Correlativo = i,
                                Factor = 0,
                                Monto = Convert.ToDecimal(reader["TOTAL_GROSS"]),
                                Nombre = "NBASE",
                                Orden = 6,
                                Porcentaje = 0
                            };

                            XmlFormatter.PrintValues(xDoc, path, i, 4);
                            XmlFormatter.PrintValueElements(xDoc, path, tipoDocumento, bvev, i, 4);

                            //PRINT VALORES(NETO)
                            bvev = new BoletaVentaValores()
                            {
                                Correlativo = i,
                                Factor = 0,
                                Monto = Convert.ToDecimal(reader["TOTAL_NET"]),
                                Nombre = "NETO",
                                Orden = 4,
                                Porcentaje = 0
                            };
                            XmlFormatter.PrintValues(xDoc, path, i, 5);
                            XmlFormatter.PrintValueElements(xDoc, path, tipoDocumento, bvev, i, 5);

                            //PRINT VALORES(PROPINA)
                            command.Dispose();
                            command.CommandText = sqlPropina;
                            subReader = command.ExecuteReader();
                            subReader.Read();

                            bvev = new BoletaVentaValores()
                            {
                                Correlativo = i,
                                Factor = 1,
                                Monto = Convert.ToDecimal(subReader["AMOUNT"]),
                                Nombre = "PROPINA",
                                Orden = 1,
                                Porcentaje = 0
                            };
                            XmlFormatter.PrintValues(xDoc, path, i, 6);
                            XmlFormatter.PrintValueElements(xDoc, path, tipoDocumento, bvev, i, 6);

                            //PRINT VALORES(SPA)
                            command.Dispose();
                            command.CommandText = sqlSpa;
                            subReader = command.ExecuteReader();
                            subReader.Read();

                            bvev = new BoletaVentaValores()
                            {
                                Correlativo = i,
                                Factor = 1,
                                Monto = Convert.ToDecimal(subReader["AMOUNT"]),
                                Nombre = "SPA",
                                Orden = 7,
                                Porcentaje = 0
                            };
                            XmlFormatter.PrintValues(xDoc, path, i, 7);
                            XmlFormatter.PrintValueElements(xDoc, path, tipoDocumento, bvev, i, 7);

                            //PRINT VALORES(TOTAL)
                            bvev = new BoletaVentaValores()
                            {
                                Correlativo = i,
                                Factor = 0,
                                Monto = Convert.ToDecimal(reader["TOTAL_NET"]),
                                Nombre = "TOTAL",
                                Orden = 8,
                                Porcentaje = 0
                            };
                            XmlFormatter.PrintValues(xDoc, path, i, 8);
                            XmlFormatter.PrintValueElements(xDoc, path, tipoDocumento, bvev, i, 8);

                            //PRINT VALORES(TRANSBANK)
                            command.Dispose();
                            command.CommandText = sqlTransbank;
                            subReader = command.ExecuteReader();
                            subReader.Read();

                            bvev = new BoletaVentaValores()
                            {
                                Correlativo = i,
                                Factor = 0,
                                Monto = Convert.ToDecimal(subReader["AMOUNT"]),
                                Nombre = "TRANSBANK",
                                Orden = 9,
                                Porcentaje = 0
                            };
                            XmlFormatter.PrintValues(xDoc, path, i, 9);
                            XmlFormatter.PrintValueElements(xDoc, path, tipoDocumento, bvev, i, 9);

                            //PRINT VALORES(TRANSIVA)
                            bvev = new BoletaVentaValores()
                            {
                                Correlativo = i,
                                Factor = 0,
                                Monto = 0,
                                Nombre = "TRANSIVA",
                                Orden = 11,
                                Porcentaje = 0
                            };
                            XmlFormatter.PrintValues(xDoc, path, i, 10);
                            XmlFormatter.PrintValueElements(xDoc, path, tipoDocumento, bvev, i, 10);

                            //PRINT VALORES(TRANSNETO)
                            bvev = new BoletaVentaValores()
                            {
                                Correlativo = i,
                                Factor = 0,
                                Monto = 0,
                                Nombre = "TRANSNETO",
                                Orden = 10,
                                Porcentaje = 0
                            };
                            XmlFormatter.PrintValues(xDoc, path, i, 11);
                            XmlFormatter.PrintValueElements(xDoc, path, tipoDocumento, bvev, i, 11);
                        }
                        XmlFormatter.RenameXmlNodes(xDoc, path);
                        log.W("Finaliza procesamiento " + tipoDocumento + " - " + dtpDesde.Value.ToString("dd/MM/yyyy"));

                        dtpDesde.Enabled = true;
                        btnGenerar.Enabled = true;
                        btnSalir.Enabled = true;
                        Cursor.Current = Cursors.Default;
                    }
                    catch (Exception ex)
                    {
                        log.W(ex.Message + "|" + ex.InnerException.Message);
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Archivo de configuración no encontrado, contacte a su proveedor.");
                    log.W("Archivo de configuración no encontrado");
                }
            }
            Cursor.Current = Cursors.Default;
        }
    }
}
