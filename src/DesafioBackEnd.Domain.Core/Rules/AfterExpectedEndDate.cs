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
    public class AfterExpectedEndDate : IRentEstimatedValueRule
    {
        public void ExecuteRuleToEstimatePrice(Rent rent, DateTime backIntentionDate, ref decimal estimatedPrice)
        {
            if(backIntentionDate.Date > rent.ExpectedEnd.Date)
            {
                var qtyDaysOverRent = (backIntentionDate.Date - rent.ExpectedEnd.Date).Days;
                estimatedPrice = (qtyDaysOverRent * rent.Plan.DelayFeePrice) + estimatedPrice;
            }
        }
    }
}
