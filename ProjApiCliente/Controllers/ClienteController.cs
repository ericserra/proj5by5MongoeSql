using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjApiCliente.Dal;
using ProjApiCliente.Models;

namespace ProjApiCliente.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
       private readonly ClienteContext _context;
       public ClienteController(ClienteContext context)
       {
           _context = context;
       }

       [HttpGet]
       public async Task<ActionResult<IEnumerable<Cliente>>> GetCliente()
       {
           return await _context.Cliente.Include(c => c.Endereco).ToListAsync();
       }

       [HttpGet("{id}")]
       public async Task<ActionResult<Cliente>> GetCliente(int id)
       {
           var cliente = await _context.Cliente.Include(c => c.Endereco).FirstOrDefaultAsync(c => c.Id == id);

           if (cliente == null)
           {
               return NotFound();
           }

           return cliente;
       }

       [HttpPost]
       public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
       {
           _context.Cliente.Add(cliente);
           await _context.SaveChangesAsync();
           return CreatedAtAction("GetCliente", new {id = cliente.Id}, cliente);
       }

       [HttpPut("{id}")]
       public async Task<IActionResult> PutCliente(int id, Cliente cliente)
       {
           if (id != cliente.Id)
           {
               return BadRequest();
           }
             _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
       }
       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(c => c.Id == id);
        }
    }
}