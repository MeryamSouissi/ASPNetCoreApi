using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZoneFranche.Data;
using ZoneFranche.Models;


namespace ZoneFranche.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntrepriseController : ControllerBase
    {
        private readonly SmsDbContext _dbContext;
        public EntrepriseController(SmsDbContext context)
        {
            this._dbContext = context;
        }

        [HttpGet]
        public async Task<List<Entreprise>> GetEntreprise()
        {
            return await _dbContext.Entreprises.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Entreprise>> GetEntreprise(string id)
        {
            var oneEntreprise = await _dbContext.Entreprises.FindAsync(id);

            if (oneEntreprise == null)
            {
                return NotFound();
            }

            return oneEntreprise;
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Entreprise>> DeleteEntreprise(string id)
        {
            var oneEntreprise = await _dbContext.Entreprises.FindAsync(id);
            if (oneEntreprise == null)
            {
                return NotFound();
            }

            _dbContext.Entreprises.Remove(oneEntreprise);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        private bool entrepExists(string id)
        {
            return _dbContext.Entreprises.Any(e => e.id == id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntreprise(string id, Entreprise entrep)
        {
            if (id != entrep.id)
            {
                return BadRequest();
            }

            _dbContext.Entry(entrep).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!entrepExists(id))
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
        [HttpPost]
        public async Task<IActionResult> PostEntreprise(Entreprise entrep)
        {

           _dbContext.Entry(entrep).State = EntityState.Modified;
            try
            {
                _dbContext.Entreprises.Add(entrep);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (entrepExists(entrep.id))
                {
                    return Conflict();
                }
            }
            return NoContent();
        }


    }
}
