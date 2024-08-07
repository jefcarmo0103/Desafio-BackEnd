using DesafioBackEnd.Application.Communication.Responses;
using DesafioBackEnd.Application.PersistenceAbstractions;
using DesafioBackEnd.Domain.Core.Interfaces.Engines;
using DesafioBackEnd.Domain.Core.Models;
using MediatR;
namespace DesafioBackEnd.Application.UseCases.Locacao.Queries
{
    public sealed record CheckReturnValueQuery(Guid LocacaoId, DateTime DataIntencaoDevolucao)
        : IRequest<OperationResult<CheckReturnValueResponse>>
    { }

    public class CheckReturnValueQueryHandler : IRequestHandler<CheckReturnValueQuery, OperationResult<CheckReturnValueResponse>>
    {
        private readonly IRentRepository _repository;
        private readonly ICalculatorEstimatedPriceEngine _calculatorEstimatedPriceEngine;
        public CheckReturnValueQueryHandler(IRentRepository rentRepository, ICalculatorEstimatedPriceEngine calculatorEstimatedPriceEngine)
        {
            _repository = rentRepository;
            _calculatorEstimatedPriceEngine = calculatorEstimatedPriceEngine;
        }

        public async Task<OperationResult<CheckReturnValueResponse>> Handle(CheckReturnValueQuery request, CancellationToken cancellationToken)
        {
            if (request.DataIntencaoDevolucao.Date < DateTime.Now.Date)
                return OperationResult<CheckReturnValueResponse>.Fail("A data de devolução deve ser maior ou igual a data de hoje");

            var rent = await _repository.GetByIdAsync(request.LocacaoId);
            if(rent is { Data: null })
                return OperationResult<CheckReturnValueResponse>.Fail(rent.Messages);

            var estimatedPriceResult = _calculatorEstimatedPriceEngine.CalculateEstimatedPrice(rent.Data, request.DataIntencaoDevolucao);

            if(estimatedPriceResult is { Data: 0})
                return OperationResult<CheckReturnValueResponse>.Fail(estimatedPriceResult.Messages);

            return OperationResult<CheckReturnValueResponse>.Ok(new CheckReturnValueResponse {
                Rent = rent.Data.ToResponse(),
                EstimatedReturnValue = estimatedPriceResult.Data
            },"Operação executada com sucesso");
        }
    }
}
