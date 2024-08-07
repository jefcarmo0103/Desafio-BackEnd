using DesafioBackEnd.Domain.Core.Entities;
using DesafioBackEnd.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Application.PersistenceAbstractions
{
    public interface IRentRepository : IRepository<Rent>
    {
        Task<OperationResult<IEnumerable<Rent>>> GetOpenRentsByMotorcycleIdAsync(Guid id);
    }
}
