using DesafioBackEnd.Application.Abstractions;
using DesafioBackEnd.Application.PersistenceAbstractions;
using DesafioBackEnd.Application.UseCases.Moto.Commands;
using DesafioBackEnd.Domain.Core.Entities;
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
    public class CreateMotorcycleCommandTest
    {
        private readonly Mock<IMotorcycleRepository> _motorcycleMockRepository;
        private readonly Mock<IEventBus> _eventBus;

        public CreateMotorcycleCommandTest()
        {
            _motorcycleMockRepository = new();
            _eventBus = new();
        }

        [Fact]
        public async Task Handle_Should_OperataionFail_WhenAreExistsMotorcycleWithSamePlate()
        {
            //Arrange
            var command = new CreateMotorcycleCommand(2024, "Bizinha", "KLO9087");
            var commandHandler = new CreateMotorcycleCommandHandler(_motorcycleMockRepository.Object, _eventBus.Object);

            _motorcycleMockRepository.Setup(x =>
                x.GetByPlateAsync(It.IsAny<string>())
            ).ReturnsAsync(OperationResult<Domain.Core.Entities.Motorcycle>.Ok(
                    new Domain.Core.Entities.Motorcycle
                    {
                        Id = It.IsAny<Guid>(),
                        Plate = It.IsAny<string>()
                    }, It.IsAny<string>()
                ));

            //Act
            var result = await commandHandler.Handle(command, default);

            //Assert
            result.Messages.ToList().Exists(x => !x.sucess).Should().BeTrue();
        }

        [Fact]
        public async Task Handle_Should_OperataionFail_WhenThereIsErrorInPersistence()
        {
            //Arrange
            var command = new CreateMotorcycleCommand(2024, "Bizinha", "KLO9087");
            var commandHandler = new CreateMotorcycleCommandHandler(_motorcycleMockRepository.Object, _eventBus.Object);

            _motorcycleMockRepository.Setup(x =>
                x.GetByPlateAsync(It.IsAny<string>())
            ).ReturnsAsync(OperationResult<Domain.Core.Entities.Motorcycle>.Fail(It.IsAny<string>()));

            _motorcycleMockRepository.Setup(x =>
                x.CreateAsync(It.IsAny<Domain.Core.Entities.Motorcycle>())
            ).ReturnsAsync(OperationResult<Guid>.Fail(It.IsAny<string>()));

            //Act
            var result = await commandHandler.Handle(command, default);

            //Assert
            result.Messages.ToList().Exists(x => !x.sucess).Should().BeTrue();
        }

        [Fact]
        public async Task Handle_Should_OperataionOk_WhenAllAreOk()
        {
            //Arrange
            var command = new CreateMotorcycleCommand(2024, "Bizinha", "KLO9087");
            var commandHandler = new CreateMotorcycleCommandHandler(_motorcycleMockRepository.Object, _eventBus.Object);

            _motorcycleMockRepository.Setup(x =>
                x.GetByPlateAsync(It.IsAny<string>())
            ).ReturnsAsync(OperationResult<Domain.Core.Entities.Motorcycle>.Fail(It.IsAny<string>()));

            _motorcycleMockRepository.Setup(x =>
                x.CreateAsync(It.IsAny<Domain.Core.Entities.Motorcycle>())
            ).ReturnsAsync(OperationResult<Guid>.Ok(It.IsAny<Guid>(), It.IsAny<string>()));

            //Act
            var result = await commandHandler.Handle(command, default);

            //Assert
            result.Data.Should().Be(It.IsAny<Guid>());
        }
    }
}
