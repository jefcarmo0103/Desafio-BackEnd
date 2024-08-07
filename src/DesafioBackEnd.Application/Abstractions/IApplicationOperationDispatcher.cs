using DesafioBackEnd.Domain.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Application.Abstractions
{
    public interface IApplicationOperationDispatcher
    {
        Task<OperationResult> DispatchAsync(IRequest<OperationResult> request, CancellationToken cancellation);

        Task<OperationResult<TData>> DispatchAsync<TData>(IRequest<OperationResult<TData>> request, CancellationToken cancellationToken);
    }
}
