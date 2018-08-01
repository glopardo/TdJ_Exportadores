using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace Utils
{
    public class XmlFormatter
    {
        #region Constants
        protected const string LISTA_DOCUMENTOS = "DOCUMENTO_LIST";
        protected const string DOCUMENTO = "DOCUMENTO";
        protected const string HEADER = "ENCABEZADO";
        protected const string DETAIL = "DETALLE";
        protected const string PAYMENT = "PAGOS";
        protected const string VALUES = "VALORES";
        #endregion

        #region Document
        /// <summary>
        /// Imprime estructura del documento sin datos con tags de encabezado
        /// </summary>
        /// <param name="path">Ruta del documento a generar</param>
        /// <param name="doc">Objeto del XML</param>
        /// <param name="index">Número de documento a imprimir</param>
        public static void PrintDocument(string path, XDocument doc, int index)
        {
            var root = new XElement(DOCUMENTO + "-" + index);
            doc.Element(LISTA_DOCUMENTOS).Add(root);

            PrintHeader(doc, path, index);
            doc.Save(path);
        }
        public static void PrintHeaderElements(XDocument doc, string filePath, string tipoDoc, BoletaVentaHeader bveh, int index)
        {
            XElement headerElement;
            var cliente = "1";
            var document = DOCUMENTO + "-" + index;

            headerElement = new XElement("Empresa", "E22");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("TipoDocto", tipoDoc);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Correlativo", bveh.Correlativo);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("CtaCte");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Numero", bveh.InvNumber);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Fecha", bveh.Fecha);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Proveedor");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Cliente", cliente);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Bodega");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Bodega2");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Local");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Comprador");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Vendedor");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("CentroCosto");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("FechaVcto", bveh.Fecha);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("ListaPrecio");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Analisis", 1);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Zona", "SI");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("TipoCta");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Moneda", "$");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Paridad", 1);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("RefTipoDocto");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("RefCorrelativo", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("ReferenciaExterna", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Neto", bveh.Neto);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("SubTotal", bveh.Total);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Total", bveh.Total);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("NetoIngreso", bveh.Neto);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("SubTotalIngreso", bveh.Total);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("TotalIngreso", bveh.Total);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Centraliza");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Valoriza", "S");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Costeo");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Aprobacion", "S");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("TipoComprobante");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("NroComprobante", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("FechaComprobante", bveh.Fecha);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("PeriodoLibro", bveh.Periodo);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("FactorMonto", 1);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("FactorMontoProyectado", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("TipoCtaCte", "CLIENTE");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("IdCtaCte", 1);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Glosa", "B" + bveh.InvNumber);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Comentario1");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Comentario2");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Comentario3");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Comentario4");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Estado");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("FechaEstado", bveh.Fecha);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("NroMensaje", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Vigencia", "S");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Emitido", "N");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("PorcentajeAsignado", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Adjuntos", "N");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Direccion");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Ciudad");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Comuna");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("EstadoDir");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Pais");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Contacto");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("FechaModif", bveh.Fecha);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("FechaUModif", bveh.Fecha);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("UsuarioModif", bveh.Usuario);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("ComisionCantidad", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("ComisionLPrecio", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("ComisionMonto", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Contacto");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Hora", bveh.Hora);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Caja");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Pago", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Donacion", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("IdApertura", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("DrCondPago", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("PorcDr1", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("PorcDr2", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("PorcDr3", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("PorcDr4", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("Multipagina");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("NetoBimoneda", bveh.Neto);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("SubtotalBimoneda", bveh.Total);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("TotalBimoneda", bveh.Total);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("ParidadBimoneda", 1);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("ParidadAdic", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("AnalisisE1");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("AnalisisE2");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("AnalisisE3");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("AnalisisE4");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("UsuarioAprueba");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("ANALISISE5");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("ANALISISE6");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("ANALISISE7");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("ANALISISE8");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("ANALISISE9");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("ANALISISE10");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("ANALISISE11");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("ANALISISE12");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("ANALISISE13");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("ANALISISE14");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("ANALISISE15");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("ANALISISE16");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("ANALISISE17");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("ANALISISE18");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("ANALISISE19");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("ANALISISE20");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("IdFolioSucursal");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("SUPER_DR");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("usuariocierre");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("FechaCierre");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("AnalisisE21");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("AnalisisE22");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("AnalisisE23");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("AnalisisE24");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("AnalisisE25");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("AnalisisE26");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("AnalisisE27");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("AnalisisE28");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("AnalisisE29");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("AnalisisE30");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            headerElement = new XElement("FechaAprueba", "01-01-1900");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(HEADER).Add(headerElement);

            doc.Save(filePath);
        }
        public static void PrintDetailElements(XDocument doc, string filePath, string tipoDoc, BoletaVentaDetalle bved, int i, int j)
        {
            var detail = DETAIL + "-" + j;
            var document = DOCUMENTO + "-" + i;
            var cantidad = 1;

            var detailElement = new XElement("Empresa", "E22");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("TipoDocto", tipoDoc);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Correlativo", bved.Correlativo.ToString());
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Secuencia", j.ToString());
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Linea", j.ToString());
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Producto", bved.Articulo);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Cantidad", cantidad);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Precio", bved.Precio);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("PorcentajeDR");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("SubTotal", bved.Subtotal);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Impuesto", bved.Iva);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Neto", bved.Subtotal);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("DRGlobal", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Costo", 1);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Total", bved.Total);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("PrecioAjustado", bved.Total);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("UnidadIngreso", "UN");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("CantidadIngreso", bved.Cantidad);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("PrecioIngreso", bved.Precio);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("SubTotalIngreso", bved.Subtotal);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("ImpuestoIngreso", bved.Iva);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("NetoIngreso", bved.Subtotal);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("DRGlobalIngreso", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("TotalIngreso", bved.Total);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Serie");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Lote");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("FechaVcto", "01-01-1900");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("TipoDoctoOrigen", "");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("CorrelativoOrigen", bved.CorrelativoOrigen);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("SecuenciaOrigen", bved.Secuencia);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Bodega");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("CentroCosto");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Proceso");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("FactorInventario", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("FactorInvProyectado", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("FechaEntrega", bved.Fecha);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("CantidadAsignada", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Fecha", bved.Fecha);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Nivel", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("SecciaProceso", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Comentario");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Vigente", "S");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("FechaModif", bved.Fecha);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("AUX_VALOR1");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("AUX_VALOR2");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("AUX_VALOR3");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("AUX_VALOR4");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("AUX_VALOR5");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("AUX_VALOR6");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("AUX_VALOR7");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("AUX_VALOR8");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("AUX_VALOR9");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("AUX_VALOR10");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("AUX_VALOR11");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("AUX_VALOR12");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("AUX_VALOR13");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("AUX_VALOR14");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("AUX_VALOR15");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("AUX_VALOR16");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("AUX_VALOR17");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("AUX_VALOR18");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("AUX_VALOR19");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("AUX_VALOR20");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("VALOR1");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("VALOR2");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("VALOR3");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("VALOR4");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("VALOR5");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("VALOR6");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("VALOR7");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("VALOR8");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("VALOR9");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("VALOR10");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("VALOR11");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("VALOR12");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("VALOR13");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("VALOR14");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("VALOR15");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("VALOR16");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("VALOR17");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("VALOR18");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("VALOR19");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("VALOR20");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("CUP", 124652212809);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Ubicacion", "PRINCIPAL");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Ubicacion2", "PRINCIPAL");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Cuenta");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("RFGrupo1");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("RFGrupo2");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("RFGrupo3");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Estado_Prod");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Placa");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Transportista");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("TipoPallet");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("TipoCaja");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("FactorImpto", 0.84033613);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("SeriePrint");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("PrecioBimoneda", bved.Precio);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("SubtotalBimoneda", bved.Subtotal);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("ImpuestoBimoneda", bved.Iva);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("NetoBimoneda", bved.Subtotal);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("DrGlobalBimoneda", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("TotalBimoneda", bved.Total);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("PrecioListaP", 1);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis1");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis2");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis3");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis4");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis5");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis6");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis7");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis8");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis9");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis10");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis11");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis12");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis13");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis14");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis15");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis16");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis17");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis18");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis19");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis20");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("UniMedDynamic", 1);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("ProdAlias");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("FechaVigenciaLp", bved.Fecha);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("LoteDestino");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("SerieDestino");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("DoctoOrigenVal", "S");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("DRGlobal1", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("DRGlobal2", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("DRGlobal3", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("DRGlobal4", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("DRGlobal5", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("DRGlobal1Ingreso", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("DRGlobal2Ingreso", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("DRGlobal3Ingreso", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("DRGlobal4Ingreso", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("DRGlobal5Ingreso", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("DRGlobal1Bimoneda", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("DRGlobal2Bimoneda", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("DRGlobal3Bimoneda", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("DRGlobal4Bimoneda", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("DRGlobal5Bimoneda", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("PorcentajeDr2", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("PorcentajeDr3", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("PorcentajeDr4", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("PorcentajeDr5", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("ValPorcentajeDr1", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("ValPorcentajeDr2", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("ValPorcentajeDr3", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("ValPorcentajeDr4", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("ValPorcentajeDr5", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("ValPorcentajeDr1Ingreso", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("ValPorcentajeDr2Ingreso", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("ValPorcentajeDr3Ingreso", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("ValPorcentajeDr4Ingreso", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("ValPorcentajeDr5Ingreso", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("ValPorcentajeDr1Bimoneda", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("ValPorcentajeDr2Bimoneda", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("ValPorcentajeDr3Bimoneda", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("ValPorcentajeDr4Bimoneda", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("ValPorcentajeDr5Bimoneda", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("CostoBimoneda", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("CupBimoneda", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("MontoAsignado", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis21");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis22");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis23");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis24");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis25");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis26");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis27");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis28");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis29");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Analisis30");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            detailElement = new XElement("Receta");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(detail).Add(detailElement);

            doc.Save(filePath);
        }
        public static void PrintPaymentElements(XDocument doc, string filePath, string tipoDoc, BoletaVentaPago bvep, int i, int j, bool desdeOpera)
        {
            var payment = $"{PAYMENT}-{j}";
            var document = $"{DOCUMENTO}-{i}";
            string codigoPago;

            if (desdeOpera)
            {
                switch (bvep.TrxCode)
                {
                    case "9040":
                        codigoPago = "AMERICAN EXPRESS";
                        break;
                    case "9041":
                        codigoPago = "VISA";
                        break;
                    case "9042":
                        codigoPago = "MASTER CARD";
                        break;
                    case "9043":
                        codigoPago = "DINERAL CLUB";
                        break;
                    case "9050":
                        codigoPago = "REDCOMPRA";
                        break;
                    case "9060":
                        codigoPago = "ANTICIPO";
                        break;
                    case "9020":
                        codigoPago = "CRED. 30 DIAS";
                        break;
                    case "9000":
                        codigoPago = "EFECTIVO";
                        break;
                    case "9010":
                        codigoPago = "EFECTIVO";
                        break;
                    case "9011":
                        codigoPago = "EFECTIVO";
                        break;
                    default:
                        codigoPago = "OTROS";
                        break;
                }
            }
            else
            {
                switch (bvep.TrxCode)
                {
                    case "15":
                        codigoPago = "AMERICAN EXPRESS";
                        break;
                    case "18":
                        codigoPago = "VISA";
                        break;
                    case "16":
                        codigoPago = "MASTER CARD";
                        break;
                    case "17":
                        codigoPago = "DINERAL CLUB";
                        break;
                    case "21":
                        codigoPago = "REDCOMPRA";
                        break;
                    case "8":
                        codigoPago = "EFECTIVO";
                        break;
                    case "9":
                        codigoPago = "EFECTIVO USD";
                        break;
                    case "24":
                        codigoPago = "CARGO";
                        break;
                    case "26":
                        codigoPago = "CARGO";
                        break;
                    case "101":
                        codigoPago = "CARGO";
                        break;
                    case "10":
                        codigoPago = "CHEQUE";
                        break;
                    case "81":
                        codigoPago = "DESC.PERSONAL";
                        break;
                    case "103":
                        codigoPago = "DESC.PERSONAL";
                        break;
                    default:
                        codigoPago = "EFECTIVO";
                        break;
                }
            }

            var paymentElement = new XElement("Empresa", "E22");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("TipoDocto", tipoDoc);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("Correlativo", bvep.Correlativo);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("Linea", bvep.Linea);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("CodigoPago", codigoPago);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("TipoPago", "");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("FechaVcto", bvep.Fecha);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("Monto", bvep.Monto);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("MontoIngreso", bvep.Monto);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("TipoDoctoPago", "");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("NroDoctoPago", bvep.BillNo);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("Cuenta", "");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("MontoBimoneda", bvep.Monto);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("AjusteBimoneda", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("Entidad");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("NumAutoriza");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("CuentaPago");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            var fechaSeparada = bvep.Fecha.Split('/');
            paymentElement = new XElement("FechaVctoTarjeta", $"{fechaSeparada[1]}{fechaSeparada[2]}");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("PropietarioTarjeta");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("FechaVctoDocto", bvep.Fecha);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("RutComprador");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("RutGirador");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("MonedaPago", "$");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("MontoPago", bvep.Monto);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("ParidadPago", 1);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("ValorGenerico");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            paymentElement = new XElement("LineaTipo", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(payment).Add(paymentElement);

            doc.Save(filePath);
        }
        public static void PrintValueElements(XDocument doc, string filePath, string tipoDoc, BoletaVentaValores bvev, int i, int j)
        {
            var values = VALUES + "-" + j;
            var document = DOCUMENTO + "-" + i;

            var valueElement = new XElement("Empresa", "E22");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(values).Add(valueElement);

            valueElement = new XElement("TipoDocto", tipoDoc);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(values).Add(valueElement);

            valueElement = new XElement("Correlativo", i);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(values).Add(valueElement);

            valueElement = new XElement("Nombre", bvev.Nombre);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(values).Add(valueElement);

            valueElement = new XElement("Orden", bvev.Orden);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(values).Add(valueElement);

            valueElement = new XElement("Factor", bvev.Factor);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(values).Add(valueElement);

            valueElement = new XElement("Monto", bvev.Monto);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(values).Add(valueElement);

            valueElement = new XElement("MontoIngreso", bvev.Monto);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(values).Add(valueElement);

            valueElement = new XElement("Ajuste", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(values).Add(valueElement);

            valueElement = new XElement("AjusteIngreso", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(values).Add(valueElement);

            valueElement = new XElement("Texto");
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(values).Add(valueElement);

            valueElement = new XElement("Porcentaje", bvev.Porcentaje);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(values).Add(valueElement);

            valueElement = new XElement("MontoBimoneda", bvev.Monto);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(values).Add(valueElement);

            valueElement = new XElement("AjusteBimoneda", 0);
            doc.Element(LISTA_DOCUMENTOS).Element(document).Element(values).Add(valueElement);

            doc.Save(filePath);

        }
        #endregion

        #region Public methods
        public static XDocument OpenFile(string filePath)
        {
            if (!File.Exists(filePath))
                return new XDocument(new XElement(LISTA_DOCUMENTOS));

            File.Delete(filePath);
            return new XDocument(new XElement(LISTA_DOCUMENTOS));
        }
        public static void PrintDetail(XDocument doc, string filePath, int i, int j)
        {
            var detail = new XElement(DETAIL + "-" + j);
            doc.Element(LISTA_DOCUMENTOS).Element(DOCUMENTO + "-" + i).Add(detail);
            doc.Save(filePath);
        }
        public static void PrintPayment(XDocument doc, string filePath, int i, int j)
        {
            var payment = new XElement(PAYMENT + "-" + j);
            doc.Element(LISTA_DOCUMENTOS).Element(DOCUMENTO + "-" + i).Add(payment);
            doc.Save(filePath);
        }
        public static void PrintValues(XDocument doc, string filePath, int i, int j)
        {
            var value = new XElement(VALUES + "-" + j);
            doc.Element(LISTA_DOCUMENTOS).Element(DOCUMENTO + "-" + i).Add(value);
            doc.Save(filePath);
        }
        public static void RenameXmlNodes(XDocument doc, string filePath)
        {
            var log = new Logger();
            log.W("Renaming " + filePath);

            foreach(var element in doc.Descendants())
            {
                if (element.Name.LocalName.StartsWith("DOCUMENTO-"))
                    element.Name = DOCUMENTO;
                if (element.Name.LocalName.StartsWith("DETALLE-"))
                    element.Name = DETAIL;
                if (element.Name.LocalName.StartsWith("PAGOS-"))
                    element.Name = PAYMENT;
                if (element.Name.LocalName.StartsWith("VALORES-"))
                    element.Name = VALUES;
            }
            doc.Save(filePath);
        }
        #endregion

        #region Protected methods
        protected static void PrintHeader(XDocument doc, string filePath, int index)
        {
            var header = new XElement(HEADER);
            doc.Element(LISTA_DOCUMENTOS).Element(DOCUMENTO + "-" + index).Add(header);
            doc.Save(filePath);
        }
        #endregion
    }
}
