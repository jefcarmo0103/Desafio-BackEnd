using DesafioBackEnd.Domain.Core.Entities;
using DesafioBackEnd.Domain.Core.Interfaces.Engines;
using DesafioBackEnd.Domain.Core.Interfaces.Rules;
using DesafioBackEnd.Domain.Core.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Domain.Core.Engines
{
    public class CalculatorEstimatedPriceEngine : ICalculatorEstimatedPriceEngine
    {
        private List<IRentEstimatedValueRule> _rules = new();
        public CalculatorEstimatedPriceEngine(IEnumerable<IRentEstimatedValueRule?> rules) 
        {
            _rules.AddRange(rules);
        }

        public OperationResult<decimal> CalculateEstimatedPrice(Rent rent, DateTime backIntentionDate)
        {
            decimal estimatedPrice = 0; 
            foreach (var rule in _rules)
                rule.ExecuteRuleToEstimatePrice(rent, backIntentionDate, ref estimatedPrice);

            return OperationResult<decimal>.Ok(estimatedPrice, "Operação executada com sucesso");
        }
    }
}
