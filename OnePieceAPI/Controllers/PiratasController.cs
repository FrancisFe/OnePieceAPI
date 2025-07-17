using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnePieceAPI.DTOs.Piratas;
using OnePieceAPI.Exceptions.Piratas;
using OnePieceAPI.Models;
using OnePieceAPI.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

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

        /// <summary>
        /// Obtiene todos los piratas con paginación
        /// </summary>
        /// <param name="page">Número de página (minimo 1)</param>
        /// <param name="pageSize">Tamaño de pagina (minimo 1)</param>
        /// <returns>Una lista paginada de los piratas</returns>
        [ProducesResponseType(typeof(IEnumerable<PirataDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PirataDto>>> GetAllPiratas([Range(1, int.MaxValue)] int page = 1,
            [Range(1, 100)] int pageSize = 5)
        {
            var piratas = await _pirataRepository.GetAllAsync(page, pageSize);
            return Ok(_mapper.Map<IEnumerable<PirataDto>>(piratas));
        }
        /// <summary>
        /// Obtiene un pirata en especifico por su ID
        /// </summary>
        /// <param name="pirataId">ID del pirata</param>
        /// <returns>Datos del pirata</returns>
        [ProducesResponseType(typeof(PirataDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [HttpGet("{pirataId}")]
        public async Task<ActionResult<PirataDto>> GetPirata(int pirataId)
        {
            var pirata = await _pirataRepository.GetAsync(pirataId);
            if (pirata == null)
            {
                return NotFound(new { error = "Pirata no encontrado." });
            }
            return Ok(_mapper.Map<PirataDto>(pirata));

        }

        /// <summary>
        /// Crea un nuevo pirata
        /// </summary>
        /// <param name="pirata">Datos del pirata a crear</param>
        /// <returns>El pirata creado</returns>
        [ProducesResponseType(typeof(PirataDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [HttpPost]
        public async Task<ActionResult<PirataDto>> CreatePirata([FromBody] CrearPirataDto pirata)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var nuevoPirata = _mapper.Map<Pirata>(pirata);
            await _pirataRepository.CreateAsync(nuevoPirata);
            var pirataDto = _mapper.Map<PirataDto>(nuevoPirata);
            return CreatedAtAction(nameof(GetPirata), new { pirataId = pirataDto.Id }, pirataDto);
        }
        /// <summary>
        /// Actualiza un pirata existente
        /// </summary>
        /// <param name="pirataId">ID del pirata a actualizar</param>
        /// <param name="pirata">Datos actualizados del pirata</param>
        /// <returns>El pirata actualizado</returns>
        [ProducesResponseType(typeof(PirataDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [HttpPut("{pirataId}")]
        public async Task<ActionResult<PirataDto?>> UpdatePirata(int pirataId, [FromBody] ActualizarPirataDto pirata)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var pirataExistente = await _pirataRepository.UpdateAsync(pirataId, _mapper.Map<Pirata>(pirata));
            if (pirataExistente == null)
            {
                return NotFound(new { error = "Pirata no encontrado." });
            }
            return Ok(_mapper.Map<PirataDto>(pirataExistente));
        }

        /// <summary>
        /// Borra un pirata por su ID
        /// </summary>
        /// <param name="pirataId">ID del pirata a borrar</param>
        /// <returns>No content si el pirata fue borrado</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [HttpDelete("{pirataId}")]
        public async Task<IActionResult> DeletePirata(int pirataId)
        {
            var resultado = await _pirataRepository.DeleteAsync(pirataId);
            if (!resultado)
                return NotFound(new { error = "Pirata no encontrado, no se puede borrar." });

            return NoContent();
        }
    }
}
