using DesafioBackEnd.Application.Abstractions;
using DesafioBackEnd.Application.PersistenceAbstractions;
using DesafioBackEnd.Application.UseCases.Moto.Commands;
using DesafioBackEnd.Domain.Core.Entities;
using DesafioBackEnd.Domain.Core.Models;
using FluentAssertions;
using Moq;

namespace DesafioBackEnd.Test.Motorcycle.Commands
{
    public class DeleteMotorcycleCommandTest
    {
        private readonly Mock<IMotorcycleRepository> _motorcycleMockRepository;
        private readonly Mock<IMotorcycle2024Repository> _motorcycle2024MockRepository;
        private readonly Mock<IRentRepository> _rentMockRepository;

        public DeleteMotorcycleCommandTest()
        {
            _motorcycleMockRepository = new();
            _motorcycle2024MockRepository = new();
            _rentMockRepository = new();
        }

        [Fact]
        public async Task Handle_Should_OperationFail_WhenAreNotExistsMotorcycleSearchedById()
        {
            //Arrange
            var mockId = Guid.NewGuid();
            var command = new DeleteMotorcyleCommand(mockId);
            var commandHandler = new DeleteMotorcyleCommandHandler(_motorcycleMockRepository.Object, _rentMockRepository.Object, _motorcycle2024MockRepository.Object);

            _motorcycleMockRepository.Setup(x =>
                x.GetByIdAsync(mockId)
            ).ReturnsAsync(OperationResult<Domain.Core.Entities.Motorcycle>.Fail(It.IsAny<string>()));

            //Act
            var result = await commandHandler.Handle(command, default);

            //Assert
            result.Messages.ToList().Exists(x => !x.sucess).Should().BeTrue();
        }

        [Fact]
        public async Task Handle_Should_OperationFail_WhenAreExistsOpenRents()
        {
            //Arrange
            var mockId = Guid.NewGuid();

            var command = new DeleteMotorcyleCommand(mockId);
            var commandHandler = new DeleteMotorcyleCommandHandler(_motorcycleMockRepository.Object, _rentMockRepository.Object, _motorcycle2024MockRepository.Object);

            _motorcycleMockRepository.Setup(x =>
                x.GetByIdAsync(mockId)
            ).ReturnsAsync(OperationResult<Domain.Core.Entities.Motorcycle>.Ok(
                    new Domain.Core.Entities.Motorcycle
                    {
                        Id = It.IsAny<Guid>(),
                        Plate = It.IsAny<string>()
                    }, It.IsAny<string>()
                ));

            _rentMockRepository.Setup(x =>
                x.GetOpenRentsByMotorcycleIdAsync(mockId)
            ).ReturnsAsync(OperationResult<IEnumerable<Domain.Core.Entities.Rent>>.Ok(
                    It.IsAny<IEnumerable<Domain.Core.Entities.Rent>>(), It.IsAny<string>()
                ));

            //Act
            var result = await commandHandler.Handle(command, default);

            //Assert
            result.Messages.ToList().Exists(x => !x.sucess).Should().BeTrue();
        }
    }
}
