using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnePieceAPI.DTOs.Piratas;
using OnePieceAPI.Exceptions.Piratas;
using OnePieceAPI.Models;
using OnePieceAPI.Services.Interfaces;

namespace OnePieceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PiratasController : ControllerBase
    {
        private readonly IPirataRepository _pirataRepository;
        private readonly IMapper _mapper;
        public PiratasController(IPirataRepository pirataRepository, IMapper mapper)
        {
            _pirataRepository = pirataRepository ?? throw new ArgumentNullException(nameof(pirataRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]   
        public async Task<ActionResult<IEnumerable<PirataDto>>> GetAllPiratas(int page = 1 , int pageSize = 5)
        {
            if(page <= 0 || pageSize <= 0)
            {
                return BadRequest("Los parámetros de paginación deben ser mayores que cero.");
            }
            var piratas = await _pirataRepository.GetAllAsync(page,pageSize);
            return Ok(_mapper.Map<IEnumerable<PirataDto>>(piratas));
        }

        [HttpGet("{pirataId}")]
        public async Task<ActionResult<PirataDto?>> GetPirata(int pirataId)
        {
            try
            {
                var pirata = await _pirataRepository.GetAsync(pirataId);

                if (pirata!.FrutaDelDiablo != null)
                {
                    return Ok(_mapper.Map<PirataConFrutaDto>(pirata));
                }
                return Ok(_mapper.Map<PirataDto>(pirata));
            }
            catch(PirataNoEncontradoException ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpPost]
        public async Task<ActionResult<PirataDto>> CreatePirata([FromBody]CrearPirataDto pirata)
        {
            if(pirata == null || !ModelState.IsValid)
            {
                return BadRequest("Pirata no puede ser nulo.");
            }
            var nuevoPirata = _mapper.Map<Pirata>(pirata);
            await _pirataRepository.CreateAsync(nuevoPirata);
            var pirataDto = _mapper.Map<PirataDto>(nuevoPirata);
            return CreatedAtAction(nameof(GetPirata), new { pirataId = pirataDto.Id }, pirataDto);
        }
        [HttpPut("{pirataId}")]
        public async Task<ActionResult<PirataDto?>> UpdatePirata(int pirataId, [FromBody] ActualizarPirataDto pirata)
        {
            if(pirata == null || !ModelState.IsValid)
            {
                return BadRequest("Pirata no puede ser nulo.");
            }
            var pirataExistente = await _pirataRepository.UpdateAsync(pirataId, _mapper.Map<Pirata>(pirata));
            if (pirataExistente == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PirataDto>(pirataExistente));
        }
        [HttpDelete("{pirataId}")]
        public async Task<IActionResult> DeletePirata(int pirataId)
        {
            var resultado = await _pirataRepository.DeleteAsync(pirataId);
            if (!resultado)
            {
                return NotFound("Pirata no encontrado, no se puede borrar");
            }
            return NoContent();
        }
    }
}
