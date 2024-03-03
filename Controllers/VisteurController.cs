using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZoneFranche.Data;
using ZoneFranche.Models;

namespace ZoneFranche.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class VisiteurController : ControllerBase
        {
            private readonly SmsDbContext _dbContext;
            public VisiteurController(SmsDbContext context)
            {
                this._dbContext = context;
            }
    
        [HttpGet]
        public async Task<List<Visiteur>> GetVisiteurs()
        {
            return await _dbContext.Visiteurs.Include(d => d.Login).ToListAsync();

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Visiteur>> GetVisiteurById(int id)
        {
            var oneVisiteur = await _dbContext.Visiteurs.Include(d => d.Login).FirstOrDefaultAsync(d => d.id == id);

            if (oneVisiteur == null)
            {
                return NotFound();
            }

            return oneVisiteur;
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Visiteur>> DeleteVisiteurs(int id)
        {
            var oneVisiteur = await _dbContext.Visiteurs.FindAsync(id);
            if (oneVisiteur == null)
            {
                return NotFound();
            }

            _dbContext.Visiteurs.Remove(oneVisiteur);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        private bool VisitExists(int id)
        {
            return _dbContext.Visiteurs.Any(e => e.id == id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisiteur(int id, Visiteur visit)
        {
            if (id != visit.id)
            {
                return BadRequest();
            }

            _dbContext.Entry(visit).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitExists(id))
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
        public async Task<IActionResult> PostVisiteur(Visiteur visit)
        {

            _dbContext.Entry(visit).State = EntityState.Modified;
            try
            {
                _dbContext.Visiteurs.Add(visit);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (VisitExists(visit.id))
                {
                    return Conflict();
                }
            }
            return NoContent();
        }
        [HttpGet("bylogin/{loginId}")]
        public async Task<ActionResult<int>> GetVisiteurIdByLoginId(int loginId)
        {
            var visiteur = await _dbContext.Visiteurs.FirstOrDefaultAsync(v => v.idLogin == loginId);

            if (visiteur == null)
            {
                return NotFound();
            }
            return visiteur.id;
        }
    }
}