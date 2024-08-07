using DesafioBackEnd.Application.PersistenceAbstractions;
using DesafioBackEnd.Domain.Core.Base;
using DesafioBackEnd.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Infra.Core.Base
{
    public abstract class BaseRepository<TModel> : IRepository<TModel> where TModel : BaseEntity
    {
        private readonly DesafioContext _context;
        private readonly DbSet<TModel> _dbSet;
        public BaseRepository(DesafioContext context)
        {
            _context = context;
            _dbSet = context.Set<TModel>();
        }

        public virtual async Task<OperationResult<TModel>> GetByIdAsync(Guid id)
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (result is null)
                return OperationResult<TModel>.Fail("Nenhum registro foi encontrado");
            else
                return OperationResult<TModel>.Ok(result, "Operação executada com sucesso");
        }

        public virtual async Task<OperationResult<IEnumerable<TModel>>> GetAllAsync()
        {
            var result = await _dbSet.ToListAsync();
            if (result is null)
                return OperationResult<IEnumerable<TModel>>.Fail("Nenhum registro foi encontrado");
            else
                return OperationResult<IEnumerable<TModel>>.Ok(result, "Operação executada com sucesso");
        }

        public async Task<OperationResult<Guid>> CreateAsync(TModel entity)
        {
            await _dbSet.AddAsync(entity);
            _context.SaveChanges();

            return OperationResult<Guid>.Ok(entity.Id, "Operação executada com sucesso");
        }

        public async Task<OperationResult<Guid>> UpdateAsync(TModel entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return OperationResult<Guid>.Ok(entity.Id, "Operação executada com sucesso");
        }

        public async Task<OperationResult> DeleteAsync(TModel entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return OperationResult.Ok("Operação executada com sucesso");
        }
    }
}
