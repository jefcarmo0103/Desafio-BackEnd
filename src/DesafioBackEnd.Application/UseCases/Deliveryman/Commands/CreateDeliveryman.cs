using DesafioBackEnd.Application.Abstractions;
using DesafioBackEnd.Application.PersistenceAbstractions;
using DesafioBackEnd.Domain.Core.Entities;
using DesafioBackEnd.Domain.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DesafioBackEnd.Application.UseCases.Moto.Commands
{
    public sealed record CreateDeliverymanCommand(
        string Name,
        string CNPJ,
        DateTime BirthdayDate,
        long NumberCNH,
        Guid TypeCNH,
        IFormFile ImageCNH
        ) : IRequest<OperationResult<Guid>>
    { }

    public class CreateDeliverymanCommandHandler : IRequestHandler<CreateDeliverymanCommand, OperationResult<Guid>>
    {
        private readonly IDeliveryManRepository _repository;
        private readonly IManagerFileBus _managerFileBus;
        private readonly ITypeCnhRepository _typeCNHRepository;

        public CreateDeliverymanCommandHandler(IDeliveryManRepository repository, IManagerFileBus managerFileBus, ITypeCnhRepository typeCNHRepository)
        {
            _repository = repository;
            _managerFileBus = managerFileBus;
            _typeCNHRepository = typeCNHRepository;
        }

        public async Task<OperationResult<Guid>> Handle(CreateDeliverymanCommand request, CancellationToken cancellationToken)
        {
            var cnpjFormatted = request.CNPJ.Replace(".", "").Replace("-", "").Replace("/", "");

            OperationResult<DeliveryMan> searched = await _repository.GetByCNPJorCNHAsync(cnpjFormatted, request.NumberCNH);
            if (searched is { Data: not null })
                return OperationResult<Guid>.Fail("Já existe um entregador registrado com esse CNPJ ou Número da CNH");

            OperationResult<CNHType> typeSearched = await _typeCNHRepository.GetByIdAsync(request.TypeCNH);
            if(typeSearched is { Data: null })
                return OperationResult<Guid>.Fail("Tipo de CNH não encontrado");

            OperationResult<string> imageCNH = await SavePhoto(request.ImageCNH);
            if (imageCNH is { Data: null })
                return OperationResult<Guid>.Fail(imageCNH.Messages);

            OperationResult<DeliveryMan> modelToCreate = DeliveryMan.Build(request.Name, cnpjFormatted, request.BirthdayDate, request.NumberCNH, imageCNH.Data, request.TypeCNH);
            if(modelToCreate is { Data: not null })
            {
                var result = await _repository.CreateAsync(modelToCreate.Data);
                if (result.Data != Guid.Empty)
                    return OperationResult<Guid>.Ok(modelToCreate.Data.Id, result.Messages);
                
                return OperationResult<Guid>.Fail(result.Messages);
            }
            return OperationResult<Guid>.Fail(modelToCreate.Messages);
        }

        public async Task<OperationResult<string>> SavePhoto(IFormFile photoFile)
        {
            return await _managerFileBus.UploadFile(photoFile.FileName, ".jpg", photoFile.OpenReadStream());
        }
    }
}
