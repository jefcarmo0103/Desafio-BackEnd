using DesafioBackEnd.Domain.Core.Entities;
using FluentValidation;

namespace DesafioBackEnd.Domain.Core.Validations
{
    public class MotorcycleValidator : AbstractValidator<Motorcycle>
    {
        private static MotorcycleValidator? RESULT;

        private MotorcycleValidator()
        {
            RuleFor(x => x.Id)
                .Must(x => x != Guid.Empty)
                .WithMessage("Identificador Moto Inválido");

            RuleFor(x => x.Plate)
                .NotEmpty().NotNull()
                    .WithMessage("Placa da moto não pode ser vazia")
                .Length(7)
                    .WithMessage("Placa da moto deve ter 7 caracteres");

            RuleFor(x => x.Model)
                .NotEmpty().NotNull();

            RuleFor(x => x.Year)
                .GreaterThan(2000);
        }

        public static MotorcycleValidator GetValidationResult() => RESULT ??= new MotorcycleValidator();
    }
}
