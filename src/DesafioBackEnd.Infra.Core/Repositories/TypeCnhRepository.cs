using DesafioBackEnd.Application.PersistenceAbstractions;
using DesafioBackEnd.Domain.Core.Entities;
using DesafioBackEnd.Infra.Core.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Infra.Core.Repositories
{
    public class TypeCnhRepository : BaseRepository<CNHType>, ITypeCnhRepository
    {
        public TypeCnhRepository(DesafioContext context) : base(context){ }
    }
}
