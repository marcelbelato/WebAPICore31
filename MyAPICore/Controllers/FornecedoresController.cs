using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPICore.Models;

namespace MyAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedoresController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public FornecedoresController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Fornecedores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FornecedorModel>>> GetFornecedores()
        {
            return await _context.Fornecedores.ToListAsync();
        }

        // GET: api/Fornecedores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FornecedorModel>> GetFornecedorModel(int id)
        {
            var fornecedorModel = await _context.Fornecedores.FindAsync(id);

            if (fornecedorModel == null)
            {
                return NotFound();
            }

            return fornecedorModel;
        }

        // PUT: api/Fornecedores/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFornecedorModel(int id, FornecedorModel fornecedorModel)
        {
            if (id != fornecedorModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(fornecedorModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FornecedorModelExists(id))
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

        // POST: api/Fornecedores
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FornecedorModel>> PostFornecedorModel(FornecedorModel fornecedorModel)
        {
            _context.Fornecedores.Add(fornecedorModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFornecedorModel", new { id = fornecedorModel.Id }, fornecedorModel);
        }

        // DELETE: api/Fornecedores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FornecedorModel>> DeleteFornecedorModel(int id)
        {
            var fornecedorModel = await _context.Fornecedores.FindAsync(id);
            if (fornecedorModel == null)
            {
                return NotFound();
            }

            _context.Fornecedores.Remove(fornecedorModel);
            await _context.SaveChangesAsync();

            return fornecedorModel;
        }

        private bool FornecedorModelExists(int id)
        {
            return _context.Fornecedores.Any(e => e.Id == id);
        }
    }
}
