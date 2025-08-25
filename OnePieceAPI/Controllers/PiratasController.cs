using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnePieceAPI.Models.Common;
using OnePieceAPI.Models.DTOs.Piratas;
using OnePieceAPI.Models.Entities;
using OnePieceAPI.Services.Interfaces;



namespace OnePieceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PiratasController : ControllerBase
    {
        private readonly IPirataService _pirataService;
        public PiratasController(IPirataService pirataService)
        {
            _pirataService = pirataService ?? throw new ArgumentNullException(nameof(pirataService));
        }

        /// <summary>
        /// Obtiene todos los piratas con paginación
        /// </summary>
        /// <param name="page">Número de página (minimo 1)</param>
        /// <param name="pageSize">Tamaño de pagina (minimo 1)</param>
        /// <returns>Una lista paginada de los piratas</returns>
        [ProducesResponseType(typeof(PagedResult<PirataDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<PagedResult<PirataDto>>> GetAllPiratas([FromQuery]PirataFiltrosDto filtros)
        {
            var piratas = await _pirataService.GetPirataConFiltrosAsync(filtros);
            return Ok(piratas);
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
            var pirata = await _pirataService.GetByIdAsync(pirataId);
            if (pirata == null)
            {
                return NotFound(new { error = "Pirata no encontrado." });
            }
            return Ok(pirata);

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
            var pirataNuevo = await _pirataService.CreateAsync(pirata);
            return CreatedAtAction(nameof(GetPirata), new { pirataId = pirataNuevo?.Id }, pirataNuevo);
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
            var pirataExistente = await _pirataService.UpdateAsync(pirataId, pirata);
            if (pirataExistente == null)
            {
                return NotFound(new { error = "Pirata no encontrado." });
            }
            return Ok(pirataExistente);
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
            var resultado = await _pirataService.DeleteAsync(pirataId);
            if (!resultado)
                return NotFound(new { error = "Pirata no encontrado, no se puede borrar." });

            return NoContent();
        }
    }
}
