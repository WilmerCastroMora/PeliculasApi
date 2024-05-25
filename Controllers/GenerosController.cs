using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasApi.DTOs;
using PeliculasApi.Entidades;

namespace PeliculasApi.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GenerosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GeneroDTO>>> Get()
        {
            var generos = await context.Generos.ToListAsync();
            return mapper.Map<List<GeneroDTO>>(generos);
        }
        
        [HttpGet("{id:int}", Name = "obtenerGenero")]
        public async Task<ActionResult<GeneroDTO>> Get (int id)
        {
            var genero = await context.Generos.FirstOrDefaultAsync(x => x.Id == id);
            if(genero == null)
            {
                return NotFound();
            }
            return mapper.Map<GeneroDTO>(genero);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            var entidad = mapper.Map<Genero>(generoCreacionDTO);
            context.Add(entidad);
            await context.SaveChangesAsync();

            var generoDTO = mapper.Map<GeneroDTO>(entidad);
            return CreatedAtRoute("obtenerGenero", new { id = generoDTO.Id }, generoDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody]GeneroCreacionDTO generoCreacionDTO)
        {
            var genero = mapper.Map<Genero>(generoCreacionDTO);
            genero.Id = id;
            context.Entry(genero).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var genero= await context.Generos.AnyAsync(x => x.Id == id);
            if (!genero) {
                return NotFound();
            }
            context.Remove(new Genero() { Id = id });
            await context.SaveChangesAsync();
            return NoContent() ;
        }

    }
}
