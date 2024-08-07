using DesafioBackEnd.Application.Abstractions;
using DesafioBackEnd.Domain.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Application.Orchestration.Dispatchers
{
    public class ApplicationOperationDispatcher : IApplicationOperationDispatcher
    {
        private readonly IMediator _mediator;

        public ApplicationOperationDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> DispatchAsync(IRequest<OperationResult> request, CancellationToken cancellation)
        {
            return await _mediator.Send(request, cancellation);
        }

        public async Task<OperationResult<TData>> DispatchAsync<TData>(IRequest<OperationResult<TData>> request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(request);
        }
    }
}
