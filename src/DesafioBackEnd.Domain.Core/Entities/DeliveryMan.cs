using DesafioBackEnd.Domain.Core.Base;
using DesafioBackEnd.Domain.Core.Extensions;
using DesafioBackEnd.Domain.Core.Models;
using DesafioBackEnd.Domain.Core.Validations;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Domain.Core.Entities
{
    public class DeliveryMan : BaseEntity
    {
        public string Name { get; set; }
        public required string CNPJ { get; set; }
        public DateTime BirthdayDate { get; set; }
        public long NumberCNH { get; set; }
        public string ImageCNH { get; set; }
        public Guid TypeCNHId { get; set; }
        public CNHType TypeCNH { get; set; }
        public bool QualifiedToRentMotorcyle { get { return TypeCNH.QualifiedToRentMotorcyle; } }


        public static OperationResult<DeliveryMan> Build(string name, string cnpj, DateTime birthdayDate, long numberCNH, string imageCNH, Guid typeCNH) 
        {
            DeliveryMan model = new DeliveryMan {
                Id = Guid.NewGuid(),
                Name = name,
                BirthdayDate = birthdayDate.SetKindUtc(),
                NumberCNH = numberCNH,
                ImageCNH = imageCNH,
                CNPJ = cnpj,
                TypeCNHId = typeCNH
            };

            ValidationResult validationResult = GetValidationResult(model);
            if (validationResult.IsValid)
                return OperationResult<DeliveryMan>.Ok(model, "Operação executada com sucesso");
            return OperationResult<DeliveryMan>.Fail(validationResult);

        }


        public static OperationResult<DeliveryMan> Build(Guid id, string name, string cnpj, DateTime birthdayDate, long numberCNH, string imageCNH, Guid typeCNH)
        {
            DeliveryMan model = new DeliveryMan
            {
                Id = id,
                Name = name,
                BirthdayDate = birthdayDate.SetKindUtc(),
                NumberCNH = numberCNH,
                ImageCNH = imageCNH,
                CNPJ = cnpj,
                TypeCNHId = typeCNH
            };

            ValidationResult validationResult = GetValidationResult(model);
            if (validationResult.IsValid)
                return OperationResult<DeliveryMan>.Ok(model, "Operação executada com sucesso");
            return OperationResult<DeliveryMan>.Fail(validationResult);

        }

        public static ValidationResult GetValidationResult(DeliveryMan deliveryMan) => DeliveryManValidator.GetValidationResult().Validate(deliveryMan);
    }
}
