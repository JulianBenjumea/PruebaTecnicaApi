using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Models
{
    public class Persona
    {
        [Key]
        public int Id {get;set;}
        public string Nombre {get;set;}= string.Empty;
        public string Genero {get;set;}= string.Empty;
        public int Identificacion {get;set;}
        public string Direccion {get;set;}= string.Empty;
        public string Telefono {get;set;}=string.Empty;
        public int Edad {get;set;}
    }
}