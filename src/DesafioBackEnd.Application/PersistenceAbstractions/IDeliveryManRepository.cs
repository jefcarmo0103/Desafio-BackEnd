using DesafioBackEnd.Domain.Core.Entities;
using DesafioBackEnd.Domain.Core.Models;

namespace DesafioBackEnd.Application.PersistenceAbstractions
{
    public interface IDeliveryManRepository : IRepository<DeliveryMan>
    {
        Task<OperationResult<DeliveryMan>> GetByCNPJorCNHAsync(string CNPJ, long numberCNH);

    }
}
