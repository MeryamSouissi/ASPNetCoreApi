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
        
    }
}
