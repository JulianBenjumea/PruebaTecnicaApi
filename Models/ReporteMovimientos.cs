using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnica.Models
{
    public class ReporteMovimientos
    {
        public DateTime Fecha { get;set; }
        public string Cliente {get;set;}=string.Empty;
        public string NumeroCuenta {get;set;}=string.Empty;
        public string Tipo {get;set;}=string.Empty;
        public double SaldoInicial {get;set;}
        public bool Estado {get;set;}
        public double Movimiento {get;set;}
        public double SaldoDisponible {get;set;}
    }
}