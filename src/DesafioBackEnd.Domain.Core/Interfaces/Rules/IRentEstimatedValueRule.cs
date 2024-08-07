using DesafioBackEnd.Domain.Core.Entities;
using DesafioBackEnd.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Domain.Core.Interfaces.Rules
{
    public interface IRentEstimatedValueRule
    {
        void ExecuteRuleToEstimatePrice(Rent rent, DateTime backIntentionDate, ref decimal estimatedPrice);
    }
}
