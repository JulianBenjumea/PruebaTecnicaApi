using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PruebaTecnica.Data;
using PruebaTecnica.Models;

namespace PruebaTecnica.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovimientosController : Controller
    {
       private readonly DataContext _context;

        public MovimientosController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Consular")]
        public async Task<IActionResult> GetAsync()
        {
            var cuentas = await _context.Movimientos.ToListAsync();
            return Ok(cuentas);
        }
        [HttpGet]
        [Route("ConsularEstadoCuenta/{Id:int}")]
        public async Task<IActionResult> GetClienteIdAsync(int Id)
        {
            var cuentas = await _context.Cuenta.Where(f=>f.ClienteId == Id && f.Estado == true).ToListAsync();
            var consolidado = new List<ReporteMovimientos>();
            if(cuentas?.Count > 0)
            {
                foreach(var cuenta in cuentas)
                {
                    var movimientos = await _context.Movimientos.Where(f=>f.CuentaId == cuenta.IdCuenta).ToListAsync();
                    if(movimientos?.Count > 0)
                    {
                        foreach(var mov in movimientos)
                        {
                            var clienteCuenta = await _context.Cliente.FindAsync(Id);
                            if(clienteCuenta?.PersonaId > 0)
                            {
                                clienteCuenta.Persona = await _context.Persona.FindAsync(clienteCuenta.PersonaId);
                            }
                            consolidado.Add(new ReporteMovimientos{
                                Cliente = clienteCuenta.Persona?.Nombre,
                                Estado = cuenta.Estado,
                                Fecha = mov.Fecha.Date,
                                NumeroCuenta = cuenta.NumeroCuenta,
                                SaldoDisponible = mov.saldo,
                                SaldoInicial = cuenta.SaldoInicial,
                                Movimiento = mov.valor,
                                Tipo = cuenta.TipoCuenta
                            });
                        }                        
                    }
                }                
            }
            return Ok(consolidado);           
        }
        [HttpGet]
        [Route("ConsularEstadoCuentaF/{fecha1:DateTime}/{fecha2:DateTime}/{Id:int}")]
        public async Task<IActionResult> GetClienteIdAsync(DateTime fecha1, DateTime fecha2, int Id)
        {

            var cuentas = await _context.Cuenta.Where(f=>f.ClienteId == Id && f.Estado == true).ToListAsync();
            var consolidado = new List<ReporteMovimientos>();
            if(cuentas?.Count > 0)
            {
                foreach(var cuenta in cuentas)
                {
                    var movimientos = await _context.Movimientos.Where(f=>f.CuentaId == cuenta.IdCuenta && f.Fecha >= fecha1 && f.Fecha <= fecha2).ToListAsync();
                    if(movimientos?.Count > 0)
                    {
                        foreach(var mov in movimientos)
                        {
                            var clienteCuenta = await _context.Cliente.FindAsync(Id);
                            if(clienteCuenta?.PersonaId > 0)
                            {
                                clienteCuenta.Persona = await _context.Persona.FindAsync(clienteCuenta.PersonaId);
                            }
                            consolidado.Add(new ReporteMovimientos{
                                Cliente = clienteCuenta.Persona?.Nombre,
                                Estado = cuenta.Estado,
                                Fecha = mov.Fecha.Date,
                                NumeroCuenta = cuenta.NumeroCuenta,
                                SaldoDisponible = mov.saldo,
                                SaldoInicial = cuenta.SaldoInicial,
                                Movimiento = mov.valor,
                                Tipo = cuenta.TipoCuenta
                            });
                        }                        
                    }
                }                
            }
            return Ok(consolidado);           
        }
        [Route("Guardar")]
        [HttpPost]
        public async Task<IActionResult> PosAsync(Movimientos cuenta)
        {
            _context.Movimientos.Add(cuenta);
            await _context.SaveChangesAsync();
            return Ok(cuenta);
        }
                [Route("Actualizar")]
        [HttpPut]
        public async Task<IActionResult> PutAsync(Movimientos MovimientoUpdate)
        {
             try{
                _context.Movimientos.Update(MovimientoUpdate);
                await _context.SaveChangesAsync();
                return NoContent();
             }
            catch (Exception ex)
            {
                throw new Exception("Error: "+ex.ToString());
            }
        }
        [Route("Eliminar/{id:int}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int Id)
        {
            try{
                
                var mov = await _context.Movimientos.FindAsync(Id);
                if(mov == null) {
                    return NotFound();
                }
               
                _context.Movimientos.Remove(mov);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception("Error: "+ex.ToString());
            }
        } 
    }
}