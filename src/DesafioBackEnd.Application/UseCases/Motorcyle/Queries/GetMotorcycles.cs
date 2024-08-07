using DesafioBackEnd.Application.Communication.Responses;
using DesafioBackEnd.Application.PersistenceAbstractions;
using DesafioBackEnd.Domain.Core.Models;
using MediatR;

namespace DesafioBackEnd.Application.UseCases.Moto.Queries
{
    public sealed record GetMotorcyclesQuery() : IRequest<OperationResult<IEnumerable<MotoResponse>>> { }

    public class GetMotorcyclesQueryHandler : IRequestHandler<GetMotorcyclesQuery, OperationResult<IEnumerable<MotoResponse>>>
    {
        private readonly IMotorcycleRepository _repository;
        public GetMotorcyclesQueryHandler(IMotorcycleRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<IEnumerable<MotoResponse>>> Handle(GetMotorcyclesQuery request, CancellationToken cancellationToken)
        {
            var motos = await _repository.GetAllAsync();
            if (motos is { Data: not null })
                return OperationResult<IEnumerable<MotoResponse>>.Ok(motos.Data.Select(x => x.ToResponse()), "Operação executada com sucesso");
            else
                return OperationResult<IEnumerable<MotoResponse>>.Fail(motos.Messages);
        }
    }
}
