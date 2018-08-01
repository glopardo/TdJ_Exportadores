using Oracle.DataAccess.Client;
using System;
using System.Windows.Forms;
using Utils;
using System.Collections.Generic;

namespace Exportador9700
{
    public partial class frmMain : Form
    {
        Logger log = new Logger();
        ConfigurationReader _configReader = new ConfigurationReader();
        //MapeoItemsReader _mapeoItemsReader = new MapeoItemsReader();
        Configuration _configuration;
        //Dictionary<string, string> _mapeoItems = new Dictionary<string, string>();

        public frmMain()
        {
            InitializeComponent();
        }

        #region Event methods
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            using (var connection = ConnDb.GetDBConnection(_configuration))
            {
                GenerarArchivo(connection);
            }
            Cursor.Current = Cursors.Default;
            dtpDesde.Enabled = true;
            btnGenerar.Enabled = true;
            btnSalir.Enabled = true;
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Private methods
        private void GenerarArchivo(OracleConnection connection)
        {
            var tipoDocumento = "BOLETA DE VENTA";
            log.W("Inicia procesamiento " + tipoDocumento + " - " + dtpDesde.Value.ToString("dd/MM/yyyy"));

            if (connection != null)
            {
                dtpDesde.Enabled = false;
                btnGenerar.Enabled = false;
                btnSalir.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    var subCommand = connection.CreateCommand();

                    var query = "SELECT TO_CHAR(FCRBSNZDATE, 'dd/MM/yyyy') FECHA, TO_CHAR(FCRBSNZDATE, 'yyyyMMdd') FECHA2, TO_CHAR(FCRBSNZDATE, 'yyyyMM') PERIODO, " +
                                "TO_CHAR(FCRBSNZDATE, 'dd/MM/yyyy') HORA, FCRINVNUMBER FCRINVNUMBER, FCRINVNUMBER INVNUM, " +
                                "MICROSCHKNUM CHECKNUM, ROUND(SUBTOTAL8/1.19) NETO, SUBTOTAL2 DESCUENTOS, SUBTOTAL3 SERVICIOS, SUBTOTAL5 EXENTO, " +
                                "SUBTOTAL8 TOTAL, SUBTOTAL9 PROPINA, ROUND(SUBTOTAL8/1.19 * 0.19) IVA, PCWSID TERMINAL " +
                                "FROM FCR_INVOICE_DATA WHERE TO_CHAR(FCRBSNZDATE, 'yyyyMMdd') = '" + dtpDesde.Value.ToString("yyyyMMdd") + "'" +
                                "ORDER BY FCRBSNZDATE DESC";

                    log.W($"query: {query}");

                    command.CommandText = query;
                    var reader = command.ExecuteReader();

                    var path = dtpDesde.Value.ToString("yyyyMMdd") + "_BoletaDeVenta.xml";
                    var xDoc = XmlFormatter.OpenFile(path);

                    var i = 0;
                    while (reader.Read())
                    {
                        i++;

                        var bveh = new BoletaVentaHeader()
                        {
                            Correlativo = i,
                            InvNumber = Convert.ToInt32(reader["INVNUM"]),
                            Fecha = reader["FECHA"].ToString(),
                            Hora = reader["HORA"].ToString(),
                            Usuario = reader["TERMINAL"].ToString(),
                            Periodo = reader["PERIODO"].ToString(),
                            Neto = decimal.Round(Convert.ToDecimal(reader["NETO"])),
                            Subtotal = Convert.ToDecimal(reader["NETO"]),
                            Total = Convert.ToDecimal(reader["TOTAL"])
                        };

                        XmlFormatter.PrintDocument(path, xDoc, i);
                        XmlFormatter.PrintHeaderElements(xDoc, path, tipoDocumento, bveh, i);

                        //DETALLE ---------------------------------------------------------------------
                        var sqlChecks = "SELECT CHECKID, CHECKNUMBER, EMPLOYEEID, CASHIERID, WORKSTATIONID, CHECKOPEN, " +
                                        "TABLEOPEN, CHECKCLOSE, SPLITFROMCHECKNUM, SUBTOTAL, TAX, OTHER, PAYMENT, DUE FROM CHECKS " +
                                        "WHERE TO_CHAR(CHECKPOSTINGTIME, 'YYYYMMDD') = '" + reader["FECHA2"] + "' and " +
                                        "CHECKNUMBER = " + reader["CHECKNUM"];

                        command = connection.CreateCommand();
                        command.CommandText = sqlChecks;
                        var subReaderChecks = command.ExecuteReader();

                        while (subReaderChecks.Read())
                        {
                            //ITEMS ---------------------------------------------------------------------
                            var sqlCheckDetail = "SELECT CHECK_DETAIL.CHECKDETAILID, CHECK_DETAIL.CHECKID, CHECK_DETAIL.DETAILINDEX, " +
                                                 "CHECK_DETAIL.DETAILTYPE, CHECK_DETAIL.TOTAL, CHECK_DETAIL.SALESCOUNT, STRING_TABLE.STRINGTEXT, MENU_ITEM_DEFINITION.MENUITEMDEFID MENUITEMDEFID, " +
                                                 "MENU_ITEM_MASTER.OBJECTNUMBER OBJECTNUMBER FROM CHECK_DETAIL INNER JOIN MENU_ITEM_DETAIL ON CHECK_DETAIL.CHECKDETAILID = " +
                                                 "MENU_ITEM_DETAIL.CHECKDETAILID INNER JOIN MENU_ITEM_DEFINITION ON MENU_ITEM_DEFINITION.MENUITEMDEFID = " +
                                                 "MENU_ITEM_DETAIL.MENUITEMDEFID INNER JOIN STRING_TABLE ON STRING_TABLE.STRINGNUMBERID = " +
                                                 "MENU_ITEM_DEFINITION.NAME1ID INNER JOIN MENU_ITEM_MASTER ON MENU_ITEM_MASTER.MENUITEMMASTERID = MENU_ITEM_DEFINITION.MENUITEMMASTERID " +
                                                 "WHERE CHECKID = " + subReaderChecks["CHECKID"] + " AND TOTAL <> 0 AND MENU_ITEM_DEFINITION.MENUITEMCLASSID <> 1341 " + 
                                                 "ORDER BY CHECK_DETAIL.DETAILINDEX";
                            //log.W(sqlCheckDetail);
                            command = connection.CreateCommand();
                            command.CommandText = sqlCheckDetail;
                            var subReaderDetail = command.ExecuteReader();
                            log.W("Procesado sqlCheckDetail");
                            var k = 0;
                            string articulo;
                            while (subReaderDetail.Read())
                            {
                                try
                                {
                                    k++;
                                    var subtotal = decimal.Round(Convert.ToDecimal(subReaderDetail["TOTAL"]) / 1.19m);

                                    articulo = subReaderDetail["OBJECTNUMBER"].ToString();

                                    var bved = new BoletaVentaDetalle()
                                    {
                                        Correlativo = i,
                                        CorrelativoOrigen = Convert.ToInt32(subReaderChecks["CHECKNUMBER"]),
                                        Secuencia = k,
                                        Articulo = articulo,
                                        Cantidad = Convert.ToInt32(subReaderDetail["SALESCOUNT"]),
                                        Precio = Convert.ToInt32(subReaderDetail["TOTAL"]) / Convert.ToInt32(subReaderDetail["SALESCOUNT"]),
                                        Subtotal = subtotal,
                                        Iva = decimal.Round(subtotal * 0.19m),
                                        Fecha = reader["FECHA"].ToString(),
                                        Total = Convert.ToInt32(subReaderDetail["TOTAL"])
                                    };

                                    XmlFormatter.PrintDetail(xDoc, path, i, k);
                                    XmlFormatter.PrintDetailElements(xDoc, path, tipoDocumento, bved, i, k);
                                }
                                catch (Exception ex)
                                {
                                    log.W($"sqlCheckDetail: {ex.Message}");
                                    throw;
                                }
                            }
                            //PAGOS ---------------------------------------------------------------------
                            var sqlCheckPayment = "SELECT CHECK_DETAIL.CHECKDETAILID, CHECK_DETAIL.CHECKID, CHECK_DETAIL.DETAILINDEX, " +
                                                  "CHECK_DETAIL.DETAILTYPE, CHECK_DETAIL.TOTAL, CHECK_DETAIL.SALESCOUNT, TENDER_MEDIA_DETAIL.TENDMEDID " +
                                                  "FROM CHECK_DETAIL INNER JOIN TENDER_MEDIA_DETAIL ON CHECK_DETAIL.CHECKDETAILID = TENDER_MEDIA_DETAIL.CHECKDETAILID " +
                                                  "WHERE CHECKID = " + subReaderChecks["CHECKID"] + " AND TOTAL > 0 ORDER BY CHECK_DETAIL.DETAILINDEX";

                            decimal transbank = 0;
                            command = connection.CreateCommand();
                            command.CommandText = sqlCheckPayment;

                            var subReaderPayment = command.ExecuteReader();
                            var l = 0;

                            //log.W($"sqlCheckPayment: {sqlCheckPayment}");

                            while (subReaderPayment.Read())
                            {
                                try
                                {
                                    l++;
                                    if (subReaderPayment["TENDMEDID"].ToString() == "21")
                                        transbank += Convert.ToDecimal(subReaderPayment["TOTAL"]);

                                    articulo = subReaderPayment["TENDMEDID"].ToString();

                                    var bvep = new BoletaVentaPago()
                                    {
                                        Correlativo = i,
                                        BillNo = Convert.ToInt32(reader["INVNUM"]),
                                        Linea = l,
                                        Monto = Convert.ToDecimal(subReaderPayment["TOTAL"]),
                                        TrxCode = articulo,
                                        Fecha = reader["FECHA"].ToString()
                                    };

                                    XmlFormatter.PrintPayment(xDoc, path, i, l);
                                    XmlFormatter.PrintPaymentElements(xDoc, path, tipoDocumento, bvep, i, l, false);
                                }
                                catch (Exception ex)
                                {
                                    log.W($"sqlCheckPayment: {ex.Message}");
                                    throw;
                                }
                            }

                            //VALORES ---------------------------------------------------------------------
                            //DESCUENTO -------------------------------------------------------------------
                            var sqlCheckDiscount = "SELECT ABS(CHECK_DETAIL.TOTAL) TOTAL " +
                                                   "FROM CHECK_DETAIL WHERE CHECKID = " + subReaderChecks["CHECKID"] +
                                                   "AND DETAILTYPE = 2 ORDER BY CHECK_DETAIL.DETAILINDEX";

                            command = connection.CreateCommand();
                            command.CommandText = sqlCheckDiscount;
                            var subReaderDiscount = command.ExecuteReader();
                            var descQty = 0;

                            while (subReaderDiscount.Read())
                            {
                                descQty++;
                                var bvev = new BoletaVentaValores()
                                {
                                    Correlativo = i,
                                    Nombre = "DESCUENTO",
                                    Factor = -1,
                                    Orden = 2,
                                    Monto = Convert.ToDecimal(subReaderDiscount["TOTAL"]),
                                    Porcentaje = 0
                                };

                                XmlFormatter.PrintValues(xDoc, path, i, 1);
                                XmlFormatter.PrintValueElements(xDoc, path, tipoDocumento, bvev, i, 1);
                            }

                            //Si no hubo descuentos no agrego el nodo
                            if(descQty == 0)
                            {
                                var bvev = new BoletaVentaValores()
                                {
                                    Correlativo = i,
                                    Nombre = "DESCUENTO",
                                    Factor = -1,
                                    Orden = 2,
                                    Monto = 0,
                                    Porcentaje = 0
                                };
                                XmlFormatter.PrintValues(xDoc, path, i, 1);
                                XmlFormatter.PrintValueElements(xDoc, path, tipoDocumento, bvev, i, 1);
                            }

                            //VALORES ------------------------------------------------------------------
                            //EXENTO -------------------------------------------------------------------
                            var bvev2 = new BoletaVentaValores()
                            {
                                Correlativo = i,
                                Nombre = "EXENTO",
                                Factor = 1,
                                Orden = 3,
                                Monto = 0,
                                Porcentaje = 0
                            };

                            //Por ahora no agrego el nodo correspondiente a EXENTO ya que no se manejan montos exentos
                            //XmlFormatter.PrintValues(xDoc, path, i, 2);
                            //XmlFormatter.PrintValueElements(xDoc, path, tipoDocumento, bvev2, i, 2);

                            //VALORES ---------------------------------------------------------------
                            //IVA -------------------------------------------------------------------
                            var iva = "0";

                            if (reader["IVA"].ToString() != string.Empty)
                                iva = reader["IVA"].ToString();

                            bvev2 = new BoletaVentaValores()
                            {
                                Correlativo = i,
                                Nombre = "IVA",
                                Factor = 1,
                                Orden = 3,
                                Monto = decimal.Round(Convert.ToDecimal(iva)),
                                Porcentaje = 0
                            };

                            XmlFormatter.PrintValues(xDoc, path, i, 3);
                            XmlFormatter.PrintValueElements(xDoc, path, tipoDocumento, bvev2, i, 3);

                            //VALORES ---------------------------------------------------------------
                            //NBASE -----------------------------------------------------------------
                            bvev2 = new BoletaVentaValores()
                            {
                                Correlativo = i,
                                Nombre = "NBASE",
                                Factor = 0,
                                Orden = 6,
                                Monto = Convert.ToDecimal(reader["NETO"]),
                                Porcentaje = 0
                            };
                            XmlFormatter.PrintValues(xDoc, path, i, 4);
                            XmlFormatter.PrintValueElements(xDoc, path, tipoDocumento, bvev2, i, 4);
                            //VALORES ---------------------------------------------------------------
                            //NETO ------------------------------------------------------------------
                            bvev2 = new BoletaVentaValores()
                            {
                                Correlativo = i,
                                Nombre = "NETO",
                                Factor = 0,
                                Orden = 6,
                                Monto = Convert.ToDecimal(reader["NETO"]),
                                Porcentaje = 0
                            };
                            XmlFormatter.PrintValues(xDoc, path, i, 5);
                            XmlFormatter.PrintValueElements(xDoc, path, tipoDocumento, bvev2, i, 5);
                            //VALORES ---------------------------------------------------------------
                            //PROPINA ---------------------------------------------------------------
                            var propina = "0";

                            if (reader["PROPINA"].ToString() != string.Empty)
                                propina = reader["PROPINA"].ToString();

                            bvev2 = new BoletaVentaValores()
                            {
                                Correlativo = i,
                                Nombre = "PROPINA",
                                Factor = 1,
                                Orden = 1,
                                Monto = Convert.ToDecimal(propina),
                                Porcentaje = 0
                            };
                            XmlFormatter.PrintValues(xDoc, path, i, 6);
                            XmlFormatter.PrintValueElements(xDoc, path, tipoDocumento, bvev2, i, 6);
                            //VALORES ---------------------------------------------------------------
                            //SPA -------------------------------------------------------------------
                            bvev2 = new BoletaVentaValores()
                            {
                                Correlativo = i,
                                Nombre = "SPA",
                                Factor = 1,
                                Orden = 7,
                                Monto = 0,
                                Porcentaje = 0
                            };
                            XmlFormatter.PrintValues(xDoc, path, i, 7);
                            XmlFormatter.PrintValueElements(xDoc, path, tipoDocumento, bvev2, i, 7);
                            //VALORES ---------------------------------------------------------------
                            //TOTAL -----------------------------------------------------------------
                            bvev2 = new BoletaVentaValores()
                            {
                                Correlativo = i,
                                Nombre = "TOTAL",
                                Factor = 0,
                                Orden = 6,
                                Monto = Convert.ToDecimal(reader["TOTAL"]),
                                Porcentaje = 0
                            };
                            XmlFormatter.PrintValues(xDoc, path, i, 8);
                            XmlFormatter.PrintValueElements(xDoc, path, tipoDocumento, bvev2, i, 8);
                            //VALORES ---------------------------------------------------------------
                            //TRANSBANK -------------------------------------------------------------
                            bvev2 = new BoletaVentaValores()
                            {
                                Correlativo = i,
                                Nombre = "TRANSBANK",
                                Factor = 0,
                                Orden = 6,
                                Monto = transbank,
                                Porcentaje = 0
                            };
                            XmlFormatter.PrintValues(xDoc, path, i, 9);
                            XmlFormatter.PrintValueElements(xDoc, path, tipoDocumento, bvev2, i, 9);
                            //VALORES ---------------------------------------------------------------
                            //TRANSIVA --------------------------------------------------------------
                            bvev2 = new BoletaVentaValores()
                            {
                                Correlativo = i,
                                Nombre = "TRANSIVA",
                                Factor = 0,
                                Orden = 6,
                                Monto = transbank - decimal.Round(transbank / 1.19m),
                                Porcentaje = 0
                            };
                            XmlFormatter.PrintValues(xDoc, path, i, 10);
                            XmlFormatter.PrintValueElements(xDoc, path, tipoDocumento, bvev2, i, 10);
                            //VALORES ---------------------------------------------------------------
                            //TRANSNETO -------------------------------------------------------------
                            bvev2 = new BoletaVentaValores()
                            {
                                Correlativo = i,
                                Nombre = "TRANSNETO",
                                Factor = 0,
                                Orden = 6,
                                Monto = decimal.Round(transbank / 1.19m),
                                Porcentaje = 0
                            };
                            XmlFormatter.PrintValues(xDoc, path, i, 11);
                            XmlFormatter.PrintValueElements(xDoc, path, tipoDocumento, bvev2, i, 11);
                        }
                    }
                    XmlFormatter.RenameXmlNodes(xDoc, path);
                    log.W("Finaliza procesamiento " + tipoDocumento + " - " + dtpDesde.Value.ToString("dd/MM/yyyy"));
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
        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                _configuration = _configReader.Read("Config.ini");
                //_mapeoItems = _mapeoItemsReader.Read("MapeoItems.ini");
                log.W("Inicia exportador 9700");
            }
            catch (Exception ex)
            {
                log.W(ex.Message);
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
        }
        #endregion
    }
}
