using DesafioBackEnd.Domain.Core.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Domain.Core.Validations
{
    public class RentValidator : AbstractValidator<Rent>
    {
        private static RentValidator? INSTANCE;

        public RentValidator()
        {
            RuleFor(x => x.Id)
                .Must(x => x != Guid.Empty)
                    .WithMessage("Identificador Locação Inválido");

            RuleFor(x => x.Begin)
                .NotEqual(DateTime.MinValue)
                    .WithMessage("Data de início da locação inválida");

            RuleFor(x => x.End)
                .NotEqual(DateTime.MinValue)
                    .WithMessage("Data de final de locação inválida");

            RuleFor(x => x.ExpectedEnd)
                .NotEqual(DateTime.MinValue)
                    .WithMessage("Data de previsão de término inválida");

            RuleFor(x => x.DeliveryManId)
                .Must(x => x != Guid.Empty)
                    .WithMessage("Identificador Entregador Inválido");

            RuleFor(x => x.PlanId)
                .Must(x => x != Guid.Empty)
                    .WithMessage("Identificador Plano Locação Inválido");

            RuleFor(x => x.MotorcycleId)
                .Must(x => x != Guid.Empty)
                    .WithMessage("Identificador Moto Inválido");
        }

        public static RentValidator GetValidationResult() => INSTANCE ??= new RentValidator();
    }
}
