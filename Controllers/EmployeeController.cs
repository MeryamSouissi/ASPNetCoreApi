using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZoneFranche.Data;
using ZoneFranche.Models;

namespace ZoneFranche.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly SmsDbContext _dbContext;
        public EmployeeController(SmsDbContext context)
        {
            this._dbContext = context;
        }

        [HttpGet]
        public async Task<List<Employee>> GetEmployees()
        {
            return await _dbContext.Employees.Include(d => d.Login).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var oneEmployee = await _dbContext.Employees.Include(d => d.Login).FirstOrDefaultAsync(d => d.id == id);

            if (oneEmployee == null)
            {
                return NotFound();
            }

            return oneEmployee;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var oneEmployee = await _dbContext.Employees.FindAsync(id);
            if (oneEmployee == null)
            {
                return NotFound();
            }

            _dbContext.Employees.Remove(oneEmployee);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }


        private bool empExists(int id)
        {
            return _dbContext.Employees.Any(e => e.id == id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee emp)
        {
            if (id != emp.id)
            {
                return BadRequest();
            }

            _dbContext.Entry(emp).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!empExists(id))
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
        public async Task<IActionResult> PostEmployee(Employee emp)
        {

            _dbContext.Entry(emp).State = EntityState.Modified;
            try
            {
                _dbContext.Employees.Add(emp);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return NoContent();
        }


    }
}
