using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI_DotNet8.Data;
using SuperHeroAPI_DotNet8.Entities;

namespace SuperHeroAPI_DotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
        {
            var heroes = await _context.SuperHeroes.ToListAsync();

            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);

            if (hero is null)
                return NotFound("Hero not found.");

            return Ok(hero);
        }

        [HttpPost]

        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]

        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero updatedHero)
        {
            var DbHero = await _context.SuperHeroes.FindAsync(updatedHero.Id);

            if (DbHero is null)
                return NotFound("Hero not found.");

            DbHero.Name = updatedHero.Name;
            DbHero.FirstName = updatedHero.FirstName;
            DbHero.LastName = updatedHero.LastName;
            DbHero.Place = updatedHero.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete]

        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var DeletedHero = await _context.SuperHeroes.FindAsync(id);

            if (DeletedHero is null)
                return NotFound("Hero not found.");

            _context.SuperHeroes.Remove(DeletedHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }



    }
}
