using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZoneFranche.Data;
using ZoneFranche.Models;

namespace ZoneFranche.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemandeController : Controller
    {
       private readonly SmsDbContext _dbContext;
            public DemandeController(SmsDbContext context)
            {
                this._dbContext = context;
            }

            [HttpGet]
            public async Task<List<Demande>> GetDemande()
            {
            return await _dbContext.Demandes
                .Include(d => d.Visiteur)  
                    .ThenInclude(v => v.Login)  
                .ToListAsync();            
        }

            [HttpGet("{id}")]
            public async Task<ActionResult<Demande>> GetDemandeById(int id)
            {
            var oneDemande = await _dbContext.Demandes
                .Include(d => d.Visiteur)  // Include Visiteur
                    .ThenInclude(v => v.Login)  // Include Login within Visiteur
                .FirstOrDefaultAsync(d => d.id == id);

            // var oneDemande = await _dbContext.Demandes.FindAsync(id);

            if (oneDemande == null)
                {
                    return NotFound();
                }

                return oneDemande;
            }
        private bool DemExists(int id)
        {
            return _dbContext.Demandes.Any(e => e.id == id);
        }

        [HttpPost]
        public async Task<IActionResult> PostDemande(Demande dem)
        {

            _dbContext.Entry(dem).State = EntityState.Modified;
            try
            {
                _dbContext.Demandes.Add(dem);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (DemExists(dem.id))
                {
                    return Conflict();
                }
            }
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDemande(int id, Demande dem)
        {
            if (id != dem.id)
            {
                return BadRequest();
            }

            _dbContext.Entry(dem).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DemExists(id))
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
        public async Task<ActionResult<Demande>> DeleteDemande(int id)
        {
            var oneDemande = await _dbContext.Demandes.FindAsync(id);
            if (oneDemande == null)
            {
                return NotFound();
            }

            _dbContext.Demandes.Remove(oneDemande);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
