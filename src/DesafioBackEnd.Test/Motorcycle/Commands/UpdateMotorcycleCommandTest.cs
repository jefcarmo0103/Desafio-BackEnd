using DesafioBackEnd.Application.PersistenceAbstractions;
using DesafioBackEnd.Application.UseCases.Moto.Commands;
using DesafioBackEnd.Domain.Core.Models;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Test.Motorcycle.Commands
{
    public class UpdateMotorcycleCommandTest
    {
        private readonly Mock<IMotorcycleRepository> _motorcycleMockRepository;
        public UpdateMotorcycleCommandTest()
        {
            _motorcycleMockRepository = new();
        }

        [Fact]
        public async Task Handle_Should_OperationFail_WhenAreNotFunded()
        {
            //Arrange
            var mockId = Guid.NewGuid();
            var mockPlate = "KLO9087";

            var command = new UpdateMotorcycleCommand(mockId, mockPlate);
            var commandHandler = new UpdateMotorcycleCommandHandler(_motorcycleMockRepository.Object);

            _motorcycleMockRepository.Setup(x =>
                x.GetByIdAsync(mockId)
            ).ReturnsAsync(OperationResult<Domain.Core.Entities.Motorcycle>.Fail(It.IsAny<string>()));

            //Act
            var result = await commandHandler.Handle(command, default);

            //Assert
            result.Messages.ToList().Exists(x => !x.sucess).Should().BeTrue();
        }

        [Fact]
        public async Task Handle_Should_OperationFail_WhenUpdateFail()
        {
            //Arrange
            var mockId = Guid.NewGuid();
            var mockPlate = "KLO9087";

            var command = new UpdateMotorcycleCommand(mockId, mockPlate);
            var commandHandler = new UpdateMotorcycleCommandHandler(_motorcycleMockRepository.Object);

            _motorcycleMockRepository.Setup(x =>
                x.GetByIdAsync(mockId)
            ).ReturnsAsync(OperationResult<Domain.Core.Entities.Motorcycle>.Ok(
                    new Domain.Core.Entities.Motorcycle
                    {
                        Id = mockId,
                        Plate = mockPlate
                    }, It.IsAny<string>()
                ));

            _motorcycleMockRepository.Setup(x =>
                x.UpdateAsync(It.IsAny<Domain.Core.Entities.Motorcycle>())
            ).ReturnsAsync(OperationResult<Guid>.Fail(It.IsAny<string>()));

            //Act
            var result = await commandHandler.Handle(command, default);

            //Assert
            result.Messages.ToList().Exists(x => !x.sucess).Should().BeTrue();
        }
    }
}
