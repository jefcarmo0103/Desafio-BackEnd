using DesafioBackEnd.Application.Abstractions;
using DesafioBackEnd.Application.PersistenceAbstractions;
using DesafioBackEnd.Domain.Core.Entities;
using DesafioBackEnd.Domain.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DesafioBackEnd.Application.UseCases.Moto.Commands
{
    public sealed record UpdatePhotoDeliverymanCommand(
        Guid Id,
        IFormFile ImageCNH) : IRequest<OperationResult<Guid>> { }

    public class UpdatePhotoDeliverymanCommandHandler : IRequestHandler<UpdatePhotoDeliverymanCommand, OperationResult<Guid>>
    {
        private readonly IDeliveryManRepository _repository;
        private readonly IManagerFileBus _fileBus;
        public UpdatePhotoDeliverymanCommandHandler(IDeliveryManRepository repository, IManagerFileBus fileBus)
        {
            _repository = repository;
            _fileBus = fileBus;
        }

        public async Task<OperationResult<Guid>> Handle(UpdatePhotoDeliverymanCommand request, CancellationToken cancellationToken)
        {
            OperationResult<DeliveryMan> searched = await _repository.GetByIdAsync(request.Id);
            if (searched is { Data: null })
                return OperationResult<Guid>.Fail(searched.Messages);

            OperationResult<string> imageCNH = await SavePhoto(request.ImageCNH);
            if (imageCNH is { Data: null })
                return OperationResult<Guid>.Fail(imageCNH.Messages);

            OperationResult<DeliveryMan> modelToCreate = DeliveryMan.Build(request.Id, searched.Data.Name, searched.Data.CNPJ, searched.Data.BirthdayDate, searched.Data.NumberCNH, imageCNH.Data, searched.Data.TypeCNHId);
            if (modelToCreate is { Data: not null })
            {
                modelToCreate.Data.TypeCNH = null;
                var result = await _repository.UpdateAsync(modelToCreate.Data);
                if (result.Data != Guid.Empty)
                    return OperationResult<Guid>.Ok(request.Id, result.Messages);
                return OperationResult<Guid>.Fail(result.Messages);
            }
            else
            {
                return OperationResult<Guid>.Fail(modelToCreate.Messages);
            }
        }

        public async Task<OperationResult<string>> SavePhoto(IFormFile photoFile)
        {
            return await _fileBus.UploadFile(photoFile.FileName, ".jpg", photoFile.OpenReadStream());
        }
    }
}
