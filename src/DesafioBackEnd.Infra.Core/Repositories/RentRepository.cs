using DesafioBackEnd.Domain.Core.Entities;
using DesafioBackEnd.Domain.Core.Models;
using DesafioBackEnd.Infra.Core.Base;
using DesafioBackEnd.Application.PersistenceAbstractions;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackEnd.Infra.Core.Repositories
{
    public class RentRepository : BaseRepository<Rent>, IRentRepository
    {
        private readonly DesafioContext _context;
        public RentRepository(DesafioContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<OperationResult<Rent>> GetByIdAsync(Guid id)
        {
            var queryable = _context.Set<Rent>()
                .Include(x => x.Plan)
                .Include(x => x.DeliveryMan)
                .Include(x => x.Motorcycle);

            var result = await queryable.FirstOrDefaultAsync(x => x.Id == id);

            if (result != null)
                return OperationResult<Rent>.Ok(result, "Operação executada com sucesso");
            
            return OperationResult<Rent>.Fail("Não há locação para esse identificador");
        }

        public async Task<OperationResult<IEnumerable<Rent>>> GetOpenRentsByMotorcycleIdAsync(Guid id)
        {
            var resultQueryable = _context.Set<Rent>().Where(x => x.MotorcycleId == id);

            if (resultQueryable.Any())
            {
                var result = await resultQueryable.ToListAsync();
                return OperationResult<IEnumerable<Rent>>.Ok(result, "Operação executada com sucesso");
            }

            return OperationResult<IEnumerable<Rent>>.Fail("Não há locações para esse modelo de moto");
        }
    }
}
