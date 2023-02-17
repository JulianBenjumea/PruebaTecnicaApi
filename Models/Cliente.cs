using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnica.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente {get;set;}
        public string Contraseña {get;set;}=string.Empty;
        public bool? Estado {get;set;}
        public int PersonaId {get;set;}
        public Persona Persona{get;set;}= new Persona();
    }
}