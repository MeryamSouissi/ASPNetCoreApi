using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZoneFranche.Data;
using ZoneFranche.Models;

namespace ZoneFranche.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly SmsDbContext _dbContext;
        public LoginController(SmsDbContext context)
        {
            this._dbContext = context;
        }
        [HttpGet]
        public async Task<List<Login>> GetLogin()
        {
            return await _dbContext.Logins.ToListAsync();
        }

        [HttpGet("maxId")]
        public async Task<int> GetMaxLoginId()
        {
            var maxId = await _dbContext.Logins.MaxAsync(l => l.id);
            return maxId;
        }

        private bool loginExists(int id)
        {
            return _dbContext.Logins.Any(e => e.id == id);
        }


        [HttpPost]
        public async Task<IActionResult> PostLogin(Login log)
        {

            _dbContext.Entry(log).State = EntityState.Modified;
            try
            {
                _dbContext.Logins.Add(log);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (loginExists(log.id))
                {
                    return Conflict();
                }
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Login>>DeleteLogin(int id)
        {
            var oneLogin = await _dbContext.Logins.FindAsync(id);
            if (oneLogin == null)
            {
                return NotFound();
            }
            _dbContext.Logins.Remove(oneLogin);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogin(int id, Login login)
        {
            if (id != login.id)
            {
                return BadRequest();
            }

            _dbContext.Entry(login).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!loginExists(id))
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

        [HttpGet("{email}")]
        public async Task<int> GetLoginCountByEmail(string email)
        {
            var count = await _dbContext.Logins.CountAsync(l => l.email == email);
            return count;
        }
        [HttpGet("{email}/{password}")]
        public async Task<ActionResult<Login>> GetLoginCountAccount(string email, string password)
        {
            var user = await _dbContext.Logins.FirstOrDefaultAsync(l => l.email == email && l.motDePasse == password);
            return user;
        }
    }
}
