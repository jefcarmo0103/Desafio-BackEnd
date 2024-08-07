using DesafioBackEnd.Application.Abstractions;
using DesafioBackEnd.Application.PersistenceAbstractions;
using DesafioBackEnd.Application.UseCases.Moto.Commands;
using DesafioBackEnd.Domain.Core.Entities;
using DesafioBackEnd.Domain.Core.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Test.Deliveryman.Commands
{
    public class CreateDeliverymanCommandTest
    {
        private readonly Mock<IDeliveryManRepository> _mockDeliveryManRepository;
        private readonly Mock<IManagerFileBus> _mockManagerFileBus;
        private readonly Mock<ITypeCnhRepository> _mockCNHTypeRepository;

        public CreateDeliverymanCommandTest()
        {
            _mockManagerFileBus = new();
            _mockCNHTypeRepository = new();
            _mockDeliveryManRepository = new();
        }

        [Fact]
        public async Task Handle_Should_OperationFail_WhenTypeDeliverymanAreExists()
        {
            //Arrange

            var content = "Fake File";
            var fileName = "test.pdf";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            IFormFile formFile = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            var command = new CreateDeliverymanCommand("Jefferson", "35230630000124", DateTime.Now, 65967099537, Guid.NewGuid(), formFile);

            _mockDeliveryManRepository.Setup(x =>
                x.GetByCNPJorCNHAsync(
                        It.IsAny<string>(),
                        It.IsAny<long>()
                    ))
                .ReturnsAsync(OperationResult<DeliveryMan>.Ok(
                    new DeliveryMan { Id = It.IsAny<Guid>(), CNPJ = It.IsAny<string>() },
                    It.IsAny<string>()
                ));

            var handler = new CreateDeliverymanCommandHandler(_mockDeliveryManRepository.Object, _mockManagerFileBus.Object, _mockCNHTypeRepository.Object);

            //Act
            OperationResult<Guid> result = await handler.Handle(command, default);

            //Assert
            result.Messages.ToList().Exists(x => !x.sucess).Should().BeTrue();
        }

        [Fact]
        public async Task Handle_Should_OperationFail_WhenTypeCNHNotExists()
        {
            //Arrange

            var content = "Fake File";
            var fileName = "test.pdf";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            IFormFile formFile = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            var command = new CreateDeliverymanCommand("Jefferson", "35230630000124", DateTime.Now, 65967099537, Guid.NewGuid(), formFile);

            _mockCNHTypeRepository.Setup(x =>
                x.GetByIdAsync(
                    It.IsAny<Guid>()
                )).ReturnsAsync(OperationResult<CNHType>.Fail(It.IsAny<string>()));


            var handler = new CreateDeliverymanCommandHandler(_mockDeliveryManRepository.Object, _mockManagerFileBus.Object, _mockCNHTypeRepository.Object);

            //Act
            OperationResult<Guid> result = await handler.Handle(command, default);

            //Assert
            result.Messages.ToList().Exists(x => !x.sucess).Should().BeTrue();
        }

        [Fact]
        public async Task Handle_Should_OperationFail_WhenUploadFileFail()
        {
            //Arrange

            var content = "Fake File";
            var fileName = "test.pdf";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            IFormFile formFile = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            var command = new CreateDeliverymanCommand("Jefferson", "35230630000124", DateTime.Now, 65967099537, Guid.NewGuid(), formFile);

            _mockCNHTypeRepository.Setup(x =>
                x.GetByIdAsync(
                    It.IsAny<Guid>()
                )).ReturnsAsync(OperationResult<CNHType>.Ok(new CNHType { Id = It.IsAny<Guid>() }, It.IsAny<string>()));

            _mockManagerFileBus.Setup(x =>
               x.UploadFile(
                   It.IsAny<string>(),
                   It.IsAny<string>(),
                   It.IsAny<Stream>()
               )).ReturnsAsync(OperationResult<string>.Fail(It.IsAny<string>()));

            var handler = new CreateDeliverymanCommandHandler(_mockDeliveryManRepository.Object, _mockManagerFileBus.Object, _mockCNHTypeRepository.Object);

            //Act
            OperationResult<Guid> result = await handler.Handle(command, default);

            //Assert
            result.Messages.ToList().Exists(x => !x.sucess).Should().BeTrue();
        }

        [Fact]
        public async Task Handle_Should_OperationOk_WhenAllAreOk()
        {
            //Arrange

            var content = "Fake File";
            var fileName = "test.pdf";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            IFormFile formFile = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            var command = new CreateDeliverymanCommand("Jefferson", "35230630000124", DateTime.Now, 65967099537, Guid.NewGuid(), formFile);

            _mockCNHTypeRepository.Setup(x =>
                x.GetByIdAsync(
                    It.IsAny<Guid>()
                )).ReturnsAsync(OperationResult<CNHType>.Ok(new CNHType { Id = It.IsAny<Guid>() }, It.IsAny<string>()));

            _mockManagerFileBus.Setup(x =>
                x.UploadFile(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<Stream>()
                )).ReturnsAsync(OperationResult<string>.Ok(Guid.NewGuid().ToString(), It.IsAny<string>()));

            _mockDeliveryManRepository.Setup(x =>
                x.CreateAsync(It.IsAny<DeliveryMan>()
                )).ReturnsAsync(OperationResult<Guid>.Ok(It.IsAny<Guid>(), It.IsAny<string>()));

            var handler = new CreateDeliverymanCommandHandler(_mockDeliveryManRepository.Object, _mockManagerFileBus.Object, _mockCNHTypeRepository.Object);

            //Act
            OperationResult<Guid> result = await handler.Handle(command, default);

            //Assert
            result.Data.Should().Be(It.IsAny<Guid>());
        }
    }
}
