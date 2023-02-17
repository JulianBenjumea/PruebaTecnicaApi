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
    public class CuentaController : ControllerBase
    {
       private readonly DataContext _context;

        public CuentaController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Consular")]
        public async Task<IActionResult> GetAsync()
        {
            var cuentas = await _context.Cuenta.ToListAsync();
            return Ok(cuentas);
        }
        [HttpGet]
        [Route("ConsularCuentaClienteId/{Id:int}")]
        public async Task<IActionResult> GetClienteIdAsync(int Id)
        {
            var cuentas = await _context.Cuenta.Where(f=>f.ClienteId == Id && f.Estado == true).ToListAsync();
            return Ok(cuentas);            
        }
        [Route("Guardar")]
        [HttpPost]
        public async Task<IActionResult> PostAsync(Cuenta cuenta)
        {
            _context.Cuenta.Add(cuenta);
            await _context.SaveChangesAsync();
            return Ok(cuenta);
        }
        [Route("Actualizar")]
        [HttpPut]
        public async Task<IActionResult> PutAsync(Cuenta cuentaUpdate)
        {
             try{
                _context.Cuenta.Update(cuentaUpdate);
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
                
                var cuenta = await _context.Cuenta.FindAsync(Id);
                if(cuenta == null) {
                    return NotFound();
                }
               
                _context.Cuenta.Remove(cuenta);
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