using DesafioBackEnd.Domain.Core.Entities;
using DesafioBackEnd.Domain.Core.Interfaces.Rules;
using DesafioBackEnd.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Domain.Core.Rules
{
    public class InDayExpectedEndDate : IRentEstimatedValueRule
    {
        public void ExecuteRuleToEstimatePrice(Rent rent, DateTime backIntentionDate,ref decimal estimatedPrice)
        {
            if(backIntentionDate.Date >= rent.ExpectedEnd.Date)
            {
                estimatedPrice = estimatedPrice + (rent.Plan.QtyDays * rent.Plan.Price);
            }
        }
    }
}
