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
    public class RentPlanRepository : BaseRepository<RentPlan>, IRentPlanRepository
    {
        public RentPlanRepository(DesafioContext context) : base(context)
        {
        }
    }
}
