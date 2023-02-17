using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnica.Models
{
    public class Cuenta
    {
        [Key]
        public int IdCuenta {get;set;}
        public string NumeroCuenta {get;set;}=string.Empty;
        public string TipoCuenta {get;set;}=string.Empty;
        public double SaldoInicial {get;set;}
        public bool Estado {get;set;}
        public int ClienteId {get;set;}
        public Cliente Cliente {get;set;}= new Cliente();
    }
}