using DesafioBackEnd.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Application.PersistenceAbstractions
{
    public interface IRepository<TModel>
    {
        Task<OperationResult<TModel>> GetByIdAsync(Guid id);
        Task<OperationResult<IEnumerable<TModel>>> GetAllAsync();
        Task<OperationResult<Guid>> CreateAsync(TModel entity);
        Task<OperationResult<Guid>> UpdateAsync(TModel entity);
        Task<OperationResult> DeleteAsync(TModel entity);
    }
}
