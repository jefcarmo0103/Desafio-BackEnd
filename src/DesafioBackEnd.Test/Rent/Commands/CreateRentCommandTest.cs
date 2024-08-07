using DesafioBackEnd.Application.PersistenceAbstractions;
using DesafioBackEnd.Application.UseCases.Locacao.Commands;
using DesafioBackEnd.Domain.Core.Entities;
using DesafioBackEnd.Domain.Core.Models;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Test.Rent.Commands
{
    public class CreateRentCommandTest
    {
        private readonly Mock<IMotorcycleRepository> _motorcycleMockRepository;
        private readonly Mock<IRentPlanRepository> _rentPlanMockRepository;
        private readonly Mock<IDeliveryManRepository> _deliveryManMockRepository;
        private readonly Mock<IRentRepository> _rentRepositoryMock;

        public CreateRentCommandTest()
        {
            _motorcycleMockRepository = new Mock<IMotorcycleRepository>();
            _rentPlanMockRepository = new Mock<IRentPlanRepository>();
            _deliveryManMockRepository = new Mock<IDeliveryManRepository>();
            _rentRepositoryMock = new Mock<IRentRepository>();
        }

        [Fact]
        public async Task Handle_Should_OperationFail_WhenPlanRentNotExists()
        {
            //Arrange
            var deliverymanId = Guid.NewGuid();
            var planId = Guid.NewGuid();
            var motorcycleId = Guid.NewGuid();

            var command = new CreateRentCommand(deliverymanId, planId, motorcycleId);
            var commandHandler = new CreateRentCommandHandler(_rentRepositoryMock.Object, _rentPlanMockRepository.Object, _deliveryManMockRepository.Object, _motorcycleMockRepository.Object);

            _rentPlanMockRepository.Setup(x =>
                x.GetByIdAsync(planId)
            ).ReturnsAsync(OperationResult<RentPlan>.Fail(It.IsAny<string>()));

            //Act
            var result = await commandHandler.Handle(command, default);

            //Assert
            result.Messages.ToList().Exists(x => !x.sucess).Should().BeTrue();
        }

        [Fact]
        public async Task Handle_Should_OperationFail_WhenDeliverymanNotFunded()
        {
            //Arrange
            var deliverymanId = Guid.NewGuid();
            var planId = Guid.NewGuid();
            var motorcycleId = Guid.NewGuid();

            var command = new CreateRentCommand(deliverymanId, planId, motorcycleId);
            var commandHandler = new CreateRentCommandHandler(_rentRepositoryMock.Object, _rentPlanMockRepository.Object, _deliveryManMockRepository.Object, _motorcycleMockRepository.Object);

            _rentPlanMockRepository.Setup(x =>
                x.GetByIdAsync(planId)
            ).ReturnsAsync(OperationResult<RentPlan>.Ok(
                It.IsAny<RentPlan>(),
                It.IsAny<string>()
            ));

            //Act
            var result = await commandHandler.Handle(command, default);

            //Assert
            result.Messages.ToList().Exists(x => !x.sucess).Should().BeTrue();
        }

        [Fact]
        public async Task Handle_Should_OperationFail_WhenMotorcycleNotFunded()
        {
            //Arrange
            var deliverymanId = Guid.NewGuid();
            var planId = Guid.NewGuid();
            var motorcycleId = Guid.NewGuid();

            var command = new CreateRentCommand(deliverymanId, planId, motorcycleId);
            var commandHandler = new CreateRentCommandHandler(_rentRepositoryMock.Object, _rentPlanMockRepository.Object, _deliveryManMockRepository.Object, _motorcycleMockRepository.Object);

            _rentPlanMockRepository.Setup(x =>
                x.GetByIdAsync(planId)
            ).ReturnsAsync(OperationResult<RentPlan>.Ok(
                It.IsAny<RentPlan>(),
                It.IsAny<string>()
            ));

            _deliveryManMockRepository.Setup(x =>
                x.GetByIdAsync(deliverymanId)
            ).ReturnsAsync(OperationResult<DeliveryMan>.Ok(
                It.IsAny<DeliveryMan>(),
                It.IsAny<string>()
            ));

            //Act
            var result = await commandHandler.Handle(command, default);

            //Assert
            result.Messages.ToList().Exists(x => !x.sucess).Should().BeTrue();
        }

        [Fact]
        public async Task Handle_Should_OperationFail_WhenPersistenceFail()
        {
            //Arrange
            var deliverymanId = Guid.NewGuid();
            var planId = Guid.NewGuid();
            var motorcycleId = Guid.NewGuid();

            var command = new CreateRentCommand(deliverymanId, planId, motorcycleId);
            var commandHandler = new CreateRentCommandHandler(_rentRepositoryMock.Object, _rentPlanMockRepository.Object, _deliveryManMockRepository.Object, _motorcycleMockRepository.Object);

            _rentPlanMockRepository.Setup(x =>
                x.GetByIdAsync(planId)
            ).ReturnsAsync(OperationResult<RentPlan>.Ok(
                It.IsAny<RentPlan>(),
                It.IsAny<string>()
            ));

            _deliveryManMockRepository.Setup(x =>
                x.GetByIdAsync(deliverymanId)
            ).ReturnsAsync(OperationResult<DeliveryMan>.Ok(
                It.IsAny<DeliveryMan>(),
                It.IsAny<string>()
            ));

            _motorcycleMockRepository.Setup(x =>
                x.GetByIdAsync(motorcycleId)
            ).ReturnsAsync(OperationResult<Domain.Core.Entities.Motorcycle>.Ok(
                It.IsAny<Domain.Core.Entities.Motorcycle>(),
                It.IsAny<string>()
            ));

            //Act
            var result = await commandHandler.Handle(command, default);

            //Assert
            result.Messages.ToList().Exists(x => !x.sucess).Should().BeTrue();
        }

        [Fact]
        public async Task Handle_Should_OperationOk_WhenAllAreOk()
        {
            //Arrange
            var deliverymanId = Guid.NewGuid();
            var planId = Guid.NewGuid();
            var motorcycleId = Guid.NewGuid();

            var command = new CreateRentCommand(deliverymanId, planId, motorcycleId);
            var commandHandler = new CreateRentCommandHandler(_rentRepositoryMock.Object, _rentPlanMockRepository.Object, _deliveryManMockRepository.Object, _motorcycleMockRepository.Object);

            _rentPlanMockRepository.Setup(x =>
                x.GetByIdAsync(planId)
            ).ReturnsAsync(OperationResult<RentPlan>.Ok(
                It.IsAny<RentPlan>(),
                It.IsAny<string>()
            ));

            _deliveryManMockRepository.Setup(x =>
                x.GetByIdAsync(deliverymanId)
            ).ReturnsAsync(OperationResult<DeliveryMan>.Ok(
                It.IsAny<DeliveryMan>(),
                It.IsAny<string>()
            ));

            _motorcycleMockRepository.Setup(x =>
                x.GetByIdAsync(motorcycleId)
            ).ReturnsAsync(OperationResult<Domain.Core.Entities.Motorcycle>.Ok(
                It.IsAny<Domain.Core.Entities.Motorcycle>(),
                It.IsAny<string>()
            ));

            _rentRepositoryMock.Setup(x =>
                x.CreateAsync(It.IsAny<Domain.Core.Entities.Rent>())
            ).ReturnsAsync(OperationResult<Guid>.Ok(
                It.IsAny<Guid>(),
                It.IsAny<string>()
            ));

            //Act
            var result = await commandHandler.Handle(command, default);

            //Assert
            result.Data.Should().Be(It.IsAny<Guid>());
        }

    }
}
