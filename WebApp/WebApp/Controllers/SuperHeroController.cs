using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public SuperHeroController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _dataContext.SuperHeros.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Get(int id)
        {
            var hero = await _dataContext.SuperHeros.FindAsync(id);
            if(hero == null) return BadRequest("Herói não encontrado!");
            else return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _dataContext.SuperHeros.Add(hero);
            await _dataContext.SaveChangesAsync(); //save the changes
            return Ok(await _dataContext.SuperHeros.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbHero = await _dataContext.SuperHeros.FindAsync(request.Id);
            if (dbHero == null) return BadRequest("Herói não encontrado!");

            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;

            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.SuperHeros.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var dbHero = await _dataContext.SuperHeros.FindAsync(id);
            if (dbHero == null) return BadRequest("Herói não encontrado!");

            _dataContext.SuperHeros.Remove(dbHero);

            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.SuperHeros.ToListAsync());
        }
    }
}
