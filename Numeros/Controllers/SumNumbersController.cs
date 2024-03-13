using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Numeros.Context;
using Numeros.Migrations;
using Numeros.Models;

namespace Numeros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SumNumbersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SumNumbersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SumNumbers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SumNumber>>> GetNumbers()
        {
            return await _context.Numbers.ToListAsync();
        }

        // GET: api/SumNumbers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SumNumber>> GetSumNumber(int id)
        {
            var sumNumber = await _context.Numbers.FindAsync(id);

            if (sumNumber == null)
            {
                return NotFound();
            }

            return sumNumber;
        }

        // PUT: api/SumNumbers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSumNumber(int id, SumNumber sumNumber)
        {
            if (id != sumNumber.IdNumber)
            {
                return BadRequest();
            }

            _context.Entry(sumNumber).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SumNumberExists(id))
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

        // POST: api/SumNumbers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SumNumber>> PostSumNumber(SumNumber sumNumber)
        {
            // Calcular el resultado de la suma de los números menores
            //int resultado = sumNumber.numero == 5 ? 10 : SumarNumerosMenores(sumNumber.numero);
            int resultado = SumarNumerosMenores(sumNumber.numero);

            // Guardar el resultado y el número en el historial
            sumNumber.resultado = resultado;
            _context.Numbers.Add(sumNumber);
            await _context.SaveChangesAsync();

            // Crear un mensaje de respuesta con el resultado
            string mensaje = $"Si el usuario proporciona el número {sumNumber.numero}, la suma sería ";
            mensaje += string.Join("+", Enumerable.Range(1, sumNumber.numero));
            mensaje += $" = {resultado}";
           

            return CreatedAtAction("GetSumNumber", new { id = sumNumber.IdNumber }, mensaje);
        }

        // DELETE: api/SumNumbers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSumNumber(int id)
        {
            var sumNumber = await _context.Numbers.FindAsync(id);
            if (sumNumber == null)
            {
                return NotFound();
            }

            _context.Numbers.Remove(sumNumber);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SumNumberExists(int id)
        {
            return _context.Numbers.Any(e => e.IdNumber == id);
        }

        // GET: api/SumNumbers/historial
        [HttpGet("historial")]
        public async Task<ActionResult<IEnumerable<SumNumber>>> GetHistorial()
        {
            return await _context.Numbers.ToListAsync();
        }

        // Función para sumar los números menores que el número proporcionado
        private int SumarNumerosMenores(int numero)
        {
            int suma = 0;
            for (int i = numero - 1; i > 0; i--)
            {
                suma += i;
            }
            return suma;
        }
    }
}