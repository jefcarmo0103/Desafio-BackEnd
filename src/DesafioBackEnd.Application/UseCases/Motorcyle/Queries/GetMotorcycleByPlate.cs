using DesafioBackEnd.Application.Communication.Responses;
using DesafioBackEnd.Application.PersistenceAbstractions;
using DesafioBackEnd.Domain.Core.Models;
using MediatR;

namespace DesafioBackEnd.Application.UseCases.Moto.Queries
{
    public sealed record GetMotorcycleByPlateQuery(string Placa) : IRequest<OperationResult<MotoResponse>> { }

    public class GetMotorcycleByPlateQueryHandler : IRequestHandler<GetMotorcycleByPlateQuery, OperationResult<MotoResponse>>
    {
        private readonly IMotorcycleRepository _repository;
        public GetMotorcycleByPlateQueryHandler(IMotorcycleRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<MotoResponse>> Handle(GetMotorcycleByPlateQuery request, CancellationToken cancellationToken)
        {
            var moto = await _repository.GetByPlateAsync(request.Placa);
            if(moto is { Data: not null }) 
                return OperationResult<MotoResponse>.Ok(moto.Data.ToResponse(),"Operação executada com sucesso");
            return OperationResult<MotoResponse>.Fail(moto.Messages);
        }
    }
}
