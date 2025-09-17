using AutoMapper;
using Moq;
using OnePieceAPI.Models.DTOs.Piratas;
using OnePieceAPI.Repositories.Interfaces;
using OnePieceAPI.Services;
using OnePieceAPI.Services.Interfaces;
using OnePieceAPI.Models.Entities;

namespace OnePieceAPI.Tests.Services;

public class PirataServiceTests
{
    private readonly Mock<IPirataRepository> _mockPirataRepository;
    private readonly Mock<IRecompensaTotalUpdater> _mockRecompensaTotalUpdater;
    private readonly Mock<IMapper> _mockMapper;
    private readonly PirataService _pirataService;

    public PirataServiceTests()
    {
        _mockPirataRepository = new Mock<IPirataRepository>();
        _mockRecompensaTotalUpdater = new Mock<IRecompensaTotalUpdater>();
        _mockMapper = new Mock<IMapper>();
        _pirataService = new PirataService(
            _mockPirataRepository.Object,
            _mockMapper.Object,
            _mockRecompensaTotalUpdater.Object
            );
    }

    [Fact]
    public async Task UpdateAsync_PirataNoExiste_DeberiaRetornarNull()
    {
        //Arrange
        var pirataId = 999;
        var actualizarDto = new ActualizarPirataDto{Nombre = "Test"};
        _mockPirataRepository.Setup(x => x.GetAsync(pirataId)).ReturnsAsync((Pirata?)null);
        //Act
        var result = await _pirataService.UpdateAsync(pirataId, actualizarDto);
        //Assert
        Assert.Null(result);
        _mockPirataRepository.Verify(x => x.GetAsync(pirataId), Times.Once);

        _mockPirataRepository.Verify(x => x.UpdateAsync(It.IsAny<int>(),It.IsAny<Pirata>()), Times.Never);
        _mockRecompensaTotalUpdater.Verify(x => x.UpdateRecompensaTotalAsync(It.IsAny<int>()),Times.Never);
    }
}