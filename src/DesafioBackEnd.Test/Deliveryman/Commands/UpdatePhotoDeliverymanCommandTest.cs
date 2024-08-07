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
    public class UpdatePhotoDeliverymanCommandTest
    {
        private readonly Mock<IDeliveryManRepository> _mockDeliveryManRepository;
        private readonly Mock<IManagerFileBus> _mockManagerFileBus;

        public UpdatePhotoDeliverymanCommandTest()
        {
            _mockManagerFileBus = new();
            _mockDeliveryManRepository = new();
        }

        [Fact]
        public async Task Handle_Should_OperationFail_WhenTypeDeliverymanNotAreExists()
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

            var command = new UpdatePhotoDeliverymanCommand(Guid.NewGuid(), formFile);

            _mockDeliveryManRepository.Setup(x =>
                x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(OperationResult<DeliveryMan>.Fail(
                    It.IsAny<string>()
                ));

            var handler = new UpdatePhotoDeliverymanCommandHandler(_mockDeliveryManRepository.Object, _mockManagerFileBus.Object);

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

            var command = new UpdatePhotoDeliverymanCommand(Guid.NewGuid(), formFile);

            _mockDeliveryManRepository.Setup(x =>
                x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(OperationResult<DeliveryMan>.Ok(
                    new DeliveryMan { Id = It.IsAny<Guid>(), CNPJ = It.IsAny<string>() },
                    It.IsAny<string>()
                ));

            _mockManagerFileBus.Setup(x =>
               x.UploadFile(
                   It.IsAny<string>(),
                   It.IsAny<string>(),
                   It.IsAny<Stream>()
               )).ReturnsAsync(OperationResult<string>.Fail(It.IsAny<string>()));

            var handler = new UpdatePhotoDeliverymanCommandHandler(_mockDeliveryManRepository.Object, _mockManagerFileBus.Object);

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

            var command = new UpdatePhotoDeliverymanCommand(Guid.NewGuid(), formFile);

            _mockDeliveryManRepository.Setup(x =>
                x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(OperationResult<DeliveryMan>.Ok(
                    new DeliveryMan { Id = It.IsAny<Guid>(), CNPJ = It.IsAny<string>() },
                    It.IsAny<string>()
                ));

            _mockManagerFileBus.Setup(x =>
                x.UploadFile(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<Stream>()
                )).ReturnsAsync(OperationResult<string>.Ok(Guid.NewGuid().ToString(), It.IsAny<string>()));

            _mockDeliveryManRepository.Setup(x =>
                x.UpdateAsync(It.IsAny<DeliveryMan>()
                )).ReturnsAsync(OperationResult<Guid>.Ok(It.IsAny<Guid>(), It.IsAny<string>()));

            var handler = new UpdatePhotoDeliverymanCommandHandler(_mockDeliveryManRepository.Object, _mockManagerFileBus.Object);

            //Act
            OperationResult<Guid> result = await handler.Handle(command, default);

            //Assert
            result.Data.Should().Be(It.IsAny<Guid>());
        }
    }
}
