using FormulaOneApp.Data;
using FormulaOneApp.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FormulaOneApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        public static AppDbContext _context;
        public TeamsController(AppDbContext context)
        {
            _context = context;
        }
        //private static List<Team> teams = new List<Team>
        //{
        //    new Team()
        //    {
        //        Country = "Germany",
        //        Id = 1,
        //        Name = "Mercedes AMG F1",
        //        TeamPrinciple = "Toto Wolf"
        //    },

        //    new Team()
        //    {
        //        Country = "Italy",
        //        Id = 2,
        //        Name = "Ferrari",
        //        TeamPrinciple = "Mattia Binotto"
        //    },


        //    new Team()
        //    {
        //        Country = "Swiss",
        //        Id = 3,
        //        Name = "Alpha Romeo",
        //        TeamPrinciple = "Federic Vasseur"
        //    }

        //};

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var teams = await _context.Teams.ToListAsync();
            return Ok(teams);
        }


        [HttpGet("{id:int}")]

        public async Task<IActionResult> Get(int id)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);

            if (team == null)
            {
                return BadRequest("Invalid Id");
            }

            return Ok(team);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Team team)
        {

            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();

            //return Ok(team);

            return CreatedAtAction("Get", team.Id, team);
        }

        // HttpPut - Is used to update the entire team list BUT
        // HttpPatch is used to update specific attributes within the team list

        [HttpPatch]
        public async Task<IActionResult> Patch(int id, string country)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);

            if (team == null)
            {
                return BadRequest("Invalid id");
            }

            team.Country = country;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);

            if (team == null)
            {
                return BadRequest("Invalid Id, try a different one");
            }

           _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
