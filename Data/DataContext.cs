using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }

        public DbSet<Cliente> Cliente{get;set;}
        public DbSet<Persona> Persona{get;set;}
        public DbSet<Cuenta> Cuenta{get;set;}
        public DbSet<Movimientos> Movimientos{get;set;}
    }
}