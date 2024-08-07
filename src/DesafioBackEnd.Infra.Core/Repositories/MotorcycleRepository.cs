using DesafioBackEnd.Application.PersistenceAbstractions;
using DesafioBackEnd.Domain.Core.Entities;
using DesafioBackEnd.Domain.Core.Models;
using DesafioBackEnd.Infra.Core.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Infra.Core.Repositories
{
    public class MotorcycleRepository : BaseRepository<Motorcycle>, IMotorcycleRepository
    {
        private readonly DesafioContext _context;
        public MotorcycleRepository(DesafioContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<OperationResult<Motorcycle>> GetByIdAsync(Guid id)
        {
            var result = await _context.Set<Motorcycle>().FirstOrDefaultAsync(x => x.Id == id);
            if (result is null)
                return OperationResult<Motorcycle>.Fail("Nenhum registro foi encontrado");
            else
                return OperationResult<Motorcycle>.Ok(result, "Operação executada com sucesso");
        }

        public override async Task<OperationResult<IEnumerable<Motorcycle>>> GetAllAsync()
        {
            var result = await _context.Set<Motorcycle>().ToListAsync();
            if (result is null)
                return OperationResult<IEnumerable<Motorcycle>>.Fail("Nenhum registro foi encontrado");
            else
                return OperationResult<IEnumerable<Motorcycle>>.Ok(result, "Operação executada com sucesso");
        }

        public async Task<OperationResult<Motorcycle>> GetByPlateAsync(string plate)
        {
            var result = await _context.Set<Motorcycle>().FirstOrDefaultAsync(x => x.Plate == plate);
            if (result is null)
                return OperationResult<Motorcycle>.Fail("Moto não encontrada");
            else
                return OperationResult<Motorcycle>.Ok(result, "Operação executada com sucesso");
        }
    }
}
