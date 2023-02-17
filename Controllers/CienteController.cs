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
    public class CienteController : ControllerBase
    {
        private readonly DataContext _context;

        public CienteController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Consular")]
        public async Task<IActionResult> GetAsync()
        {
            var clientes = await _context.Cliente.ToListAsync();
            return Ok(clientes);
        }
        [HttpGet]
        [Route("ConsularId/{Id:int}")]
        public async Task<IActionResult> GetClienteIdAsync(int Id)
        {
            var cliente = await _context.Cliente.FindAsync(Id);
            if(cliente?.PersonaId > 0)
            {
                cliente.Persona = await _context.Persona.FindAsync(cliente.PersonaId);
            }

            return Ok(cliente);            
        }
        [Route("Guardar")]
        [HttpPost]
        public async Task<IActionResult> PosAsync(Cliente cliente)
        {
            _context.Persona.Add(cliente.Persona);
            await _context.SaveChangesAsync();
            cliente.PersonaId = cliente.Persona.Id;
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();
            return Ok(cliente);
        }
        [Route("Actualizar")]
        [HttpPut]
        public async Task<IActionResult> PutAsync(Cliente clienteUpdate)
        {
             try{
                _context.Persona.Update(clienteUpdate.Persona);
                await _context.SaveChangesAsync();
                _context.Cliente.Update(clienteUpdate);
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
                
                var cliente = await _context.Cliente.FindAsync(Id);
                if(cliente == null) {
                    return NotFound();
                }
                if(cliente?.PersonaId > 0)
                {
                    cliente.Persona = await _context.Persona.FindAsync(cliente.PersonaId);
                    _context.Persona.Remove(cliente.Persona);
                    await _context.SaveChangesAsync();
                }
                _context.Cliente.Remove(cliente);
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