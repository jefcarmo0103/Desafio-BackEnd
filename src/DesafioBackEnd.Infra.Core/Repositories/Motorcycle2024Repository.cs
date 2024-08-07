using DesafioBackEnd.Application.PersistenceAbstractions;
using DesafioBackEnd.Domain.Core.Entities;
using DesafioBackEnd.Domain.Core.Models;
using DesafioBackEnd.Infra.Core.Base;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackEnd.Infra.Core.Repositories
{
    public class Motorcycle2024Repository : BaseRepository<Motorcycle2024>, IMotorcycle2024Repository
    {
        private readonly DesafioContext _context;
        public Motorcycle2024Repository(DesafioContext context) : base(context)
        {
            _context = context;
        }

        public async Task<OperationResult<Motorcycle2024>> GetByMotorcycleIdAsync(Guid id)
        {
            var result = await _context.Set<Motorcycle2024>().FirstOrDefaultAsync(x => x.MotorcycleId == id);
            if (result is null)
                return OperationResult<Motorcycle2024>.Fail("Nenhum registro foi encontrado");
            else
                return OperationResult<Motorcycle2024>.Ok(result, "Operação executada com sucesso");
        }
    }
}
