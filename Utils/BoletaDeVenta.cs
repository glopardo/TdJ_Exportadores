using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    public class BoletaDeVenta
    {
        public BoletaDeVentaHeader Header { get; set; }
        public BoletaDeVentaDetalle Detail { get; set; }
    }
    public class BoletaDeVentaHeader
    {
        public int Correlativo { get; set; }
        public int InvNumber { get; set; }
        public string Fecha { get; set; }
        /// <summary>
        /// Periodo: yyyyMM
        /// </summary>
        public string Periodo { get; set; }
        public int Neto { get; set; }
        public int Subtotal { get; set; }
        public int Total { get; set; }
        public string Usuario { get; set; }
    }
    public class BoletaDeVentaValor
    {
        public string Valor { get; set; }
        public int Monto { get; set; }
    }
    public class BoletaDeVentaPago
    {
        public int Correlativo { get; set; }
        public string CodigoPago { get; set; }
        public string TipoPago { get; set; }
        public string FechaPago { get; set; }
        public int Monto { get; set; }
        public string TipoDoctoPago { get; set; }
        public string NroDoctoPago { get; set; }
        public string Cuenta { get; set; }
    }
    public class BoletaDeVentaDetalle
    {
    }
}
