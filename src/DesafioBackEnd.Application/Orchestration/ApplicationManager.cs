using DesafioBackEnd.Application.Abstractions;
using DesafioBackEnd.Domain.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DesafioBackEnd.Application.Orchestration
{
    public class ApplicationManager
    {
        private ILogger<ApplicationManager> _logger;
        private readonly IApplicationOperationDispatcher _applicationOperationDispatcher;

        public ApplicationManager(IApplicationOperationDispatcher applicationOperationDispatcher,
            ILogger<ApplicationManager> logger)
        {
            _applicationOperationDispatcher = applicationOperationDispatcher;
            _logger = logger;
        }

        public async Task<OperationResult> ExecuteOperationAsync(IRequest<OperationResult> request, CancellationToken cancellationToken = default) 
        {
            OperationResult? result;

            try
            { 
                result = await _applicationOperationDispatcher.DispatchAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                result = OperationResult.Fail($"Ocorreu um erro durante o processamento da operação: {ex.Message}");

            }

            return result;
        }

        public async Task<OperationResult<TResult>> ExecuteOperationWithResultAsync<TResult>(IRequest<OperationResult<TResult>> request, CancellationToken cancellationToken = default)
        {
            OperationResult<TResult>? result;

            try
            {
                result = await _applicationOperationDispatcher.DispatchAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                result = OperationResult<TResult>.Fail($"Ocorreu um erro durante o processamento da operação: {ex.Message}");

            }

            return result;
        }
    }
}
