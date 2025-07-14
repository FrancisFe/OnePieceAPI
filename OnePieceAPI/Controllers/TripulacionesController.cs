using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnePieceAPI.DTOs.Tripulaciones;
using OnePieceAPI.Models;
using OnePieceAPI.Repositories.Interfaces;

namespace OnePieceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripulacionesController : ControllerBase
    {
        private readonly ITripulacionRepository _tripulacionRepository;
        private readonly IMapper _mapper;
        public TripulacionesController(ITripulacionRepository tripulacionRepository, IMapper mapper)
        {
            _tripulacionRepository = tripulacionRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TripulacionDto?>>> GetAllTripulaciones()
        {
            var tripulaciones = await _tripulacionRepository.GetAllTripulacionesAsync();
            return Ok(_mapper.Map<IEnumerable<TripulacionDto>>(tripulaciones));
        }

        [HttpGet("{tripulacionId}")]
        public async Task<ActionResult<TripulacionDto?>> GetTripulacion(int tripulacionId)
        {
            var tripulacion = await _tripulacionRepository.GetTripulacionByIdAsync(tripulacionId);
            if(tripulacion == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<TripulacionDto>(tripulacion));
        }

        [HttpPost]
        public async Task<ActionResult<TripulacionDto>> CreateTripulacion(CrearTripulacionDto tripulacion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var tripulacionNueva = _mapper.Map<Tripulacion>(tripulacion);
            await _tripulacionRepository.CreateTripulacionAsync(tripulacionNueva);
            var tripulacionDto = _mapper.Map<TripulacionDto>(tripulacionNueva);
            return CreatedAtAction(nameof(GetTripulacion), new { tripulacionId = tripulacionDto.Id }, tripulacionDto);
        }

        [HttpPut("{tripulacionId}")]
        public async Task<ActionResult<Tripulacion?>> UpdateTripulacion(int tripulacionId, ActualizarTripulacionDto tripulacion)
        {
            if(tripulacion == null || !ModelState.IsValid)
            {
                return BadRequest("Tripulacion no puede ser nula");
            }
            var tripulacionExistente = await _tripulacionRepository.UpdateTripulacionAsync(tripulacionId, _mapper.Map<Tripulacion>(tripulacion));
            if(tripulacionExistente == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<TripulacionDto>(tripulacionExistente));
        }

        [HttpDelete("{tripulacionId}")]
        public async Task<ActionResult<bool>> DeleteTripulacion(int tripulacionId)
        {
            var resultado = await _tripulacionRepository.DeleteTripulacionAsync(tripulacionId);
            if(!resultado)
            {
                return NotFound("Tripulacion no encontrada, no se puede borrar");
            }
            return NoContent();
        }
    }
}
