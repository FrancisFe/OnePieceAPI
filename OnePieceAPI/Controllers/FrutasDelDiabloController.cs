using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnePieceAPI.Models.DTOs.FrutasDelDiablo;
using OnePieceAPI.Models.Entities;
using OnePieceAPI.Services.Interfaces;
using System.ComponentModel.DataAnnotations;


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

        /// <summary>
        /// Obtiene todas las frutas del diablo con paginación
        /// </summary>
        /// <param name="page">Número de página (minimo 1)</param>
        /// <param name="pageSize">Tamaño de pagina (minimo 1)</param>
        /// <returns>Una lista paginada de frutas del diablo</returns>
        [ProducesResponseType(typeof(IEnumerable<FrutaDelDiabloDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FrutaDelDiabloDto>>> GetAllFrutasDelDiablo([Range(1, int.MaxValue)] int page = 1,
            [Range(1, 100)] int pageSize = 5)
        {
            var frutas = await _frutaDelDiabloRepository.GetAllAsync(page, pageSize);
            return Ok(_mapper.Map<IEnumerable<FrutaDelDiabloDto>>(frutas));
        }
        /// <summary>
        /// Obtiene una fruta del diablo en especifico por su ID
        /// </summary>
        /// <param name="frutaId">ID de la fruta del diablo</param>
        /// <returns>Datos de la fruta del diablo</returns>
        [ProducesResponseType(typeof(FrutaDelDiabloDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [HttpGet("{frutaId}")]
        public async Task<ActionResult<FrutaDelDiabloDto?>> GetFrutaDelDiabloById(int frutaId)
        {

            var fruta = await _frutaDelDiabloRepository.GetAsync(frutaId);
            if (fruta == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<FrutaDelDiabloDto>(fruta));
        }
        /// <summary>
        /// Crea una nueva fruta del diablo
        /// </summary>
        /// <param name="frutaDelDiablo">Datos de la fruta a crear</param>
        /// <returns>La fruta creada</returns>
        [ProducesResponseType(typeof(FrutaDelDiabloDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [HttpPost]
        public async Task<ActionResult<FrutaDelDiabloDto>> CreateFrutaDelDiablo([FromBody] CrearFrutaDelDiabloDto frutaDelDiablo)
        {
            var nuevaFruta = _mapper.Map<FrutaDelDiablo>(frutaDelDiablo);
            await _frutaDelDiabloRepository.CreateAsync(nuevaFruta);
            var frutaDto = _mapper.Map<FrutaDelDiabloDto>(nuevaFruta);
            return CreatedAtAction(nameof(GetFrutaDelDiabloById), new { frutaId = frutaDto.Id }, frutaDto);
        }


        /// <summary>
        /// Actualiza una fruta del diablo existente
        /// </summary>
        /// <param name="frutaId">ID de la fruta del diablo a actualizar</param>
        /// <param name="frutaDelDiablo">Datos actualizados de la fruta del diablo</param>
        /// <returns>Fruta del diablo actualizada</returns>
        [ProducesResponseType(typeof(FrutaDelDiabloDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [HttpPut("{frutaId}")]
        public async Task<ActionResult<FrutaDelDiablo?>> UpdateFrutaDelDiablo(int frutaId, ActualizarFrutaDelDiabloDto frutaDelDiablo)
        {
            if (frutaDelDiablo == null)
            {
                return BadRequest("Fruta del diablo no puede ser null");
            }
            var frutaActualizada = await _frutaDelDiabloRepository.UpdateAsync(frutaId, _mapper.Map<FrutaDelDiablo>(frutaDelDiablo));
            if (frutaActualizada == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<FrutaDelDiabloDto>(frutaActualizada));
        }

        /// <summary>
        /// Borra una fruta del diablo existente
        /// </summary>
        /// <param name="frutaId">Id de la fruta a borrar</param>
        /// <returns>No content si la fruta fue borrada</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [HttpDelete("{frutaId}")]
        public async Task<IActionResult> DeleteFrutaDelDiablo(int frutaId)
        {
            var frutaExistente = await _frutaDelDiabloRepository.DeleteAsync(frutaId);
            if (frutaExistente == false)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
