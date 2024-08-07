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
    public class Rent : BaseEntity
    {
        public DateTime Begin { get; set; }
        public DateTime? End { get; set; }
        public DateTime ExpectedEnd { get; set; }

        public Guid DeliveryManId { get; set; }
        public DeliveryMan DeliveryMan { get; set; }

        public Guid PlanId { get; set; }
        public RentPlan Plan { get; set; }

        public Guid MotorcycleId { get; set; }
        public Motorcycle Motorcycle { get; set; }

        public decimal RentValue { get; set; }    

        public DateTime CreationDate { get; set; }

        public static OperationResult<Rent> Build(int qtyDays, Guid deliveryManId, Guid rentPlanId, Guid motorcycleId)
        {
            Rent model = new Rent
            {
                Id = Guid.NewGuid(),
                Begin = DateTime.Now.Date.AddDays(1).SetKindUtc(),
                ExpectedEnd = DateTime.Now.Date.AddDays(qtyDays).SetKindUtc(),
                DeliveryManId = deliveryManId,
                PlanId = rentPlanId,
                MotorcycleId = motorcycleId,
                CreationDate = DateTime.Now.SetKindUtc()
            };

            ValidationResult validationResult = GetValidationResult(model);
            if (validationResult.IsValid)
                return OperationResult<Rent>.Ok(model, "Operação executada com sucesso");
            return OperationResult<Rent>.Fail(validationResult);
        }


        public static OperationResult<Rent> Build(DateTime begin, DateTime end, DateTime previewEnd, Guid deliveryManId, Guid rentPlanId, Guid motorcycleId)
        {
            Rent model = new Rent
            {
                Id = Guid.NewGuid(),
                Begin = begin,
                End = end,
                ExpectedEnd = previewEnd,
                DeliveryManId = deliveryManId,
                PlanId = rentPlanId,
                MotorcycleId = motorcycleId
            };

            ValidationResult validationResult = GetValidationResult(model);
            if (validationResult.IsValid)
                return OperationResult<Rent>.Ok(model, "Operação executada com sucesso");
            return OperationResult<Rent>.Fail(validationResult);
        }

        public static OperationResult<Rent> Build(Guid id, DateTime begin, DateTime end, DateTime previewEnd, Guid deliveryManId, Guid rentPlanId, Guid motorcycleId)
        {
            Rent model = new Rent
            {
                Id = id,
                Begin = begin,
                End = end,
                ExpectedEnd = previewEnd,
                DeliveryManId = deliveryManId,
                PlanId = rentPlanId,
                MotorcycleId = motorcycleId
            };

            ValidationResult validationResult = GetValidationResult(model);
            if (validationResult.IsValid)
                return OperationResult<Rent>.Ok(model, "Operação executada com sucesso");
            return OperationResult<Rent>.Fail(validationResult);
        }

        public static ValidationResult GetValidationResult(Rent dataModel) => RentValidator.GetValidationResult().Validate(dataModel);

    }
}
