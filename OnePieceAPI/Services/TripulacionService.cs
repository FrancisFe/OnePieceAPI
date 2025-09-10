using AutoMapper;
using OnePieceAPI.Exceptions.Piratas;
using OnePieceAPI.Models.DTOs.Tripulaciones;
using OnePieceAPI.Models.Entities;
using OnePieceAPI.Repositories.Interfaces;
using OnePieceAPI.Services.Interfaces;

namespace OnePieceAPI.Services
{
    public class TripulacionService : ITripulacionService , IRecompensaTotalUpdater
    {
        private readonly ITripulacionRepository _tripulacionRepository;
        private readonly IPirataRepository _pirataRepository;
        private readonly IMapper _mapper;
        public TripulacionService(ITripulacionRepository tripulacionRepository, IPirataRepository pirataRepository, IMapper mapper)
        {
            _tripulacionRepository = tripulacionRepository ?? throw new ArgumentNullException(nameof(tripulacionRepository));
            _pirataRepository = pirataRepository ?? throw new ArgumentNullException(nameof(pirataRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<TripulacionSimpleDto>> GetAllAsync()
        {
            var tripulaciones = await _tripulacionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TripulacionSimpleDto>>(tripulaciones);
        }

        public async Task<TripulacionDto?> GetByIdAsync(int tripulacionId)
        {
            var tripulacion = await _tripulacionRepository.GetByIdAsync(tripulacionId);
            return _mapper.Map<TripulacionDto>(tripulacion);
        }
        public async Task<TripulacionDto> CreateAsync(CrearTripulacionDto tripulacion)
        {
            var tripulacionNueva = _mapper.Map<Tripulacion>(tripulacion);
            await _tripulacionRepository.CreateAsync(tripulacionNueva);
            var tripulacionDto = _mapper.Map<TripulacionDto>(tripulacionNueva);
            return tripulacionDto;
        }
        public async Task<TripulacionDto?> UpdateAsync(int tripulacionId, ActualizarTripulacionDto tripulacion)
        {
            var tripulacionEntrante = _mapper.Map<Tripulacion>(tripulacion);
            var tripulacionExistente = await _tripulacionRepository.UpdateAsync(tripulacionId, tripulacionEntrante);
            var tripulacionDto = _mapper.Map<TripulacionDto>(tripulacionExistente);
            return tripulacionDto;
        }



        public async Task<bool> DeleteAsync(int tripulacionId)
        {
            return await _tripulacionRepository.DeleteAsync(tripulacionId);
        }
        public async Task<TripulacionDto?> AddPirataAsync(int tripulacionId, int pirataId)
        {
            var tripulacion = await _tripulacionRepository.GetByIdAsync(tripulacionId) ?? throw new Exception("Tripulacion no encontrada");
            var pirata = await _pirataRepository.GetAsync(pirataId) ?? throw new PirataNoEncontradoException();

            if (pirata.TripulacionId.HasValue && pirata.TripulacionId != tripulacionId)
                throw new Exception("El pirata ya pertenece a otra tripulación.");
            if (pirata.TripulacionId == tripulacionId)
                return _mapper.Map<TripulacionDto>(tripulacion);
            pirata.TripulacionId = tripulacionId;
            await _pirataRepository.UpdateAsync(pirata.Id, pirata);
            await UpdateRecompensaTotalAsync(tripulacionId);
            return _mapper.Map<TripulacionDto>(tripulacion);
        }

        public async Task<TripulacionDto?> RemovePirataAsync(int tripulacionId, int pirataId)
        {
            var tripulacion = await _tripulacionRepository.GetByIdAsync(tripulacionId) ?? throw new Exception("Tripulacion no encontrada");
            var pirata = await _pirataRepository.GetAsync(pirataId) ?? throw new PirataNoEncontradoException();
            if(pirata.TripulacionId.HasValue && pirata.TripulacionId != tripulacion.Id)
            {
                throw new Exception("Ese pirata no pertenece a esa tripulacion");
            }
            pirata.TripulacionId = null;
            await _pirataRepository.UpdateAsync(pirata.Id, pirata);
            await UpdateRecompensaTotalAsync(tripulacionId);
            
            return _mapper.Map<TripulacionDto>(tripulacion);
        }


        public async Task UpdateRecompensaTotalAsync(int tripulacionId)
        {
            var tripulacion = await _tripulacionRepository.GetByIdAsync(tripulacionId) ?? throw new Exception("Tripulacion no encontrada");
            tripulacion.RecompensaTotal = tripulacion.Miembros.Sum(r => r.Recompensa);
            await _tripulacionRepository.SaveChangesAsync();
        }
    }
}
