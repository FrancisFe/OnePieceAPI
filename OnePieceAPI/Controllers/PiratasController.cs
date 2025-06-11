using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnePieceAPI.DTOs.Piratas;
using OnePieceAPI.Models;
using OnePieceAPI.Services.Interfaces;

namespace OnePieceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PiratasController : ControllerBase
    {
        private readonly IPirataService _pirataService;
        private readonly IMapper _mapper;
        public PiratasController(IPirataService pirataService, IMapper mapper)
        {
            _pirataService = pirataService ?? throw new ArgumentNullException(nameof(pirataService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PirataDto>>> GetAllPiratas(int page = 1 , int pageSize = 5)
        {
            if(page <= 0 || pageSize <= 0)
            {
                return BadRequest("Los parámetros de paginación deben ser mayores que cero.");
            }
            var piratas = await _pirataService.GetAllPiratasAsync(page,pageSize);
            return Ok(_mapper.Map<IEnumerable<PirataDto>>(piratas));
        }

        [HttpGet("{pirataId}")]
        public async Task<ActionResult<PirataDto?>> GetPirata(int pirataId)
        {
            var pirata = await _pirataService.GetPirataAsync(pirataId);
            if (pirata == null)
            {
                return NotFound();
            }
            if(pirata.FrutaDelDiablo != null)
            {
                return Ok(_mapper.Map<PirataConFrutaDto>(pirata));
            }
            return Ok(_mapper.Map<PirataDto>(pirata));
        }
        [HttpPost]
        public async Task<ActionResult<Pirata>> CreatePirata([FromBody]CrearPirataDto pirata)
        {
            if(pirata == null)
            {
                return BadRequest("Pirata no puede ser nulo.");
            }
            var nuevoPirata = _mapper.Map<Pirata>(pirata);
            await _pirataService.CreatePirataAsync(nuevoPirata);
            return CreatedAtAction(nameof(GetPirata), new { pirataId = nuevoPirata.Id }, nuevoPirata);
        }
        [HttpPut("{pirataId}")]
        public async Task<ActionResult<Pirata?>> UpdatePirata(int pirataId, [FromBody] ActualizarPirataDto pirata)
        {
            if(pirata == null)
            {
                return BadRequest("Pirata no puede ser nulo.");
            }
            var pirataExistente = await _pirataService.UpdatePirataAsync(pirataId, _mapper.Map<Pirata>(pirata));
            if (pirataExistente == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PirataDto>(pirataExistente));
        }
        [HttpDelete("{pirataId}")]
        public async Task<IActionResult> DeletePirata(int pirataId)
        {
            var resultado = await _pirataService.DeletePirataAsync(pirataId);
            if (!resultado)
            {
                return NotFound("Pirata no encontrado, no se puede borrar");
            }
            return NoContent();
        }
    }
}
