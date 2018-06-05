using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    public class BoletaVentaHeader
    {
        public int Correlativo { get; set; }
        public int InvNumber { get; set; }
        /// <summary>
        /// dd/MM/yyyy
        /// </summary>
        public string Fecha { get; set; }
        /// <summary>
        /// hh:mm:ss
        /// </summary>
        public string Hora { get; set; }
        public string Cliente { get; set; }
        /// <summary>
        /// Periodo: yyyyMM
        /// </summary>
        public string Periodo { get; set; }
        public decimal Neto { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string Usuario { get; set; }
    }
    public class BoletaVentaDetalle
    {
        /// <summary>
        /// Número de documento en la lista
        /// </summary>
        public int Correlativo { get; set; }
        /// <summary>
        /// Micros check number
        /// </summary>
        public int CorrelativoOrigen { get; set; }
        /// <summary>
        /// Número de línea en el check
        /// </summary>
        public int Secuencia { get; set; }
        public string Articulo { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public string Fecha { get; set; }
    }
    public class BoletaVentaPago
    {
        public int Correlativo { get; set; }
        public int Linea { get; set; }
        public string TrxCode { get; set; }
        public string Fecha { get; set; }
        public decimal Monto { get; set; }
        public int BillNo { get; set; }
    }
    public class BoletaVentaValores
    {
        public int Correlativo { get; set; }
        public string Nombre { get; set; }
        public int Orden { get; set; }
        public int Factor { get; set; }
        public decimal Monto { get; set; }
        public decimal Porcentaje { get; set; }
    }
}
