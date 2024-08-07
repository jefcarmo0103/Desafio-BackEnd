using DesafioBackEnd.Domain.Core.Entities;
using FluentValidation;

namespace DesafioBackEnd.Domain.Core.Validations
{
    public class DeliveryManValidator : AbstractValidator<DeliveryMan>
    {
        private static DeliveryManValidator? RESULT;

        private DeliveryManValidator()
        {
            RuleFor(x => x.Id)
                .Must(x => x != Guid.Empty)
                    .WithMessage("Identificador Entregador Inválido");

            RuleFor(x => x.BirthdayDate)
                .NotEqual(DateTime.MinValue)
                    .WithMessage("Data de nascimento do entregador inválida");

            RuleFor(x => x.Name)
                .NotEmpty().NotNull()
                    .WithMessage("O nome do entregador é obrigatório");

            RuleFor(x => x.CNPJ)
                .Length(14)
                    .WithMessage("O CNPJ deve ter 14 caracteres");
        }

        public static DeliveryManValidator GetValidationResult() => RESULT ??= new DeliveryManValidator();
    }
}
