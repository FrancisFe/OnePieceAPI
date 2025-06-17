using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnePieceAPI.DTOs.FrutasDelDiablo;
using OnePieceAPI.Models;
using OnePieceAPI.Services.Interfaces;


namespace OnePieceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FrutasDelDiabloController : ControllerBase
    {
        private readonly IFrutaDelDiabloRepository _frutaDelDiabloRepository;
        private readonly IMapper _mapper;
        public FrutasDelDiabloController(IFrutaDelDiabloRepository frutaDelDiabloRepository, IMapper mapper)
        {
            _frutaDelDiabloRepository = frutaDelDiabloRepository ?? throw new ArgumentNullException(nameof(frutaDelDiabloRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FrutaDelDiabloDto>>> GetAllFrutasDelDiablo(int page = 1 , int pageSize = 5)
        {
            if(page <= 0 || pageSize <= 0)
            {
                return BadRequest("Los parámetros de paginación deben ser mayores que cero.");
            }
            var frutas = await _frutaDelDiabloRepository.GetAllFrutasDelDiabloAsync(page, pageSize);
            return Ok(_mapper.Map<IEnumerable<FrutaDelDiabloDto>>(frutas));
        }

        [HttpGet("{frutaId}")]
        public async Task<ActionResult<FrutaDelDiabloDto?>> GetFrutaDelDiablo(int frutaId)
        {

            var fruta = await _frutaDelDiabloRepository.GetFrutaDelDiabloAsync(frutaId);
            if (fruta == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<FrutaDelDiabloDto>(fruta));
        }

        [HttpPost]
        public async Task<ActionResult<FrutaDelDiablo>> CreateFrutaDelDiablo([FromBody] CrearFrutaDelDiabloDto frutaDelDiablo)
        {
            if(frutaDelDiablo == null)
            {
                return BadRequest("Fruta del daiblo no puede ser null");
            }
            var nuevaFruta = _mapper.Map<FrutaDelDiablo>(frutaDelDiablo);
            await _frutaDelDiabloRepository.CreateFrutaDelDiabloAsync(nuevaFruta);
            return CreatedAtAction(nameof(GetFrutaDelDiablo), new { frutaId = nuevaFruta.Id }, nuevaFruta);
        }
        [HttpPut("{frutaId}")]
        public async Task<ActionResult<FrutaDelDiablo?>> UpdateFrutaDelDiablo(int frutaId, ActualizarFrutaDelDiabloDto frutaDelDiablo)
        {
            if(frutaDelDiablo == null)
            {
                return BadRequest("Fruta del diablo no puede ser null");
            }
            var frutaActualizada = await _frutaDelDiabloRepository.UpdateFrutaDelDiabloAsync(frutaId, _mapper.Map<FrutaDelDiablo>(frutaDelDiablo));
            if (frutaActualizada == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<FrutaDelDiabloDto>(frutaActualizada));
        }
        [HttpDelete("{frutaId}")]
        public async Task<ActionResult> DeleteFrutaDelDiablo(int frutaId)
        {
            var frutaExistente = await _frutaDelDiabloRepository.DeleteFrutaDelDiabloAsync(frutaId);
            if(frutaExistente == false)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
