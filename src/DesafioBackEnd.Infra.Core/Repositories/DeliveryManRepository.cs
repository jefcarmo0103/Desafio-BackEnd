using DesafioBackEnd.Application.PersistenceAbstractions;
using DesafioBackEnd.Domain.Core.Entities;
using DesafioBackEnd.Domain.Core.Models;
using DesafioBackEnd.Infra.Core.Base;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackEnd.Infra.Core.Repositories
{
    public class DeliveryManRepository : BaseRepository<DeliveryMan>, IDeliveryManRepository
    {
        private readonly DesafioContext _context;
        public DeliveryManRepository(DesafioContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<OperationResult<DeliveryMan>> GetByIdAsync(Guid id)
        {
            var result = await _context.Set<DeliveryMan>()
                    .Include(x => x.TypeCNH)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (result is null)
                return OperationResult<DeliveryMan>.Fail("Entregador não encontrado");
            else
                return OperationResult<DeliveryMan>.Ok(result, "Operação executada com sucesso");
        }

        public async Task<OperationResult<DeliveryMan>> GetByCNPJorCNHAsync(string CNPJ, long numberCNH)
        {
            var result = await _context.Set<DeliveryMan>()
                .Where(x => x.CNPJ == CNPJ || x.NumberCNH == numberCNH)
                .FirstOrDefaultAsync();

            if (result is null)
                return OperationResult<DeliveryMan>.Fail("Entregador não encontrado");
            else
                return OperationResult<DeliveryMan>.Ok(result, "Operação executada com sucesso");
        }
    }
}
