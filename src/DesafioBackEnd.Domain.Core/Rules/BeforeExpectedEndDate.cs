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
    public class BeforeExpectedEndDate : IRentEstimatedValueRule
    {
        public void ExecuteRuleToEstimatePrice(Rent rent, DateTime backIntentionDate, ref decimal estimatedPrice)
        {
            if(backIntentionDate.Date < rent.ExpectedEnd.Date)
            {
                if (rent.Plan.HasFeeForAnticipation)
                {
                    var qtyDaysRent = (backIntentionDate.Date - rent.Begin.Date).Days;
                    estimatedPrice = qtyDaysRent * rent.Plan.Price;
                    estimatedPrice = estimatedPrice * (1 + (rent.Plan.AnticipationFeePercentage / 100));

                }
            }
        }
    }
}
