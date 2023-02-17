using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnica.Models
{
    public class Movimientos
    {
        [Key]
        public int IdMovimientos {get;set;}
        public DateTime Fecha {get;set;}
        public string TipoMovimiento{get;set;}=string.Empty;
        public double valor {get;set;}
        public double saldo {get;set;}
        public int CuentaId {get;set;}
        public  Cuenta Cuenta {get;set;}=new Cuenta();
    }
}