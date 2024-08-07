using DesafioBackEnd.Domain.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using DesafioBackEnd.Domain.Core.Validations;
using DesafioBackEnd.Domain.Core.Models;

namespace DesafioBackEnd.Domain.Core.Entities
{
    public class Motorcycle : BaseEntity
    {
        public required string Plate { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }

        public static OperationResult<Motorcycle> Build(string plate, int year, string model)
        {
            Motorcycle motorcycle = new Motorcycle
            {
                Id = Guid.NewGuid(),
                Plate = plate,
                Year = year,
                Model = model
            };

            ValidationResult validationResult = GetValidationResult(motorcycle);
            if (validationResult.IsValid)
                return OperationResult<Motorcycle>.Ok(motorcycle, "Operação executada com sucesso");
            return OperationResult<Motorcycle>.Fail(validationResult);

        }

        public static OperationResult<Motorcycle> Build(Guid id, string plate, int year, string model)
        {
            Motorcycle motorcycle = new Motorcycle
            {
                Id = id,
                Plate = plate,
                Year = year,
                Model = model
            };

            ValidationResult validationResult = GetValidationResult(motorcycle);
            if (validationResult.IsValid)
                return OperationResult<Motorcycle>.Ok(motorcycle, "Operação executada com sucesso");
            return OperationResult<Motorcycle>.Fail(validationResult);

        }

        public static ValidationResult GetValidationResult(Motorcycle dataModel) => MotorcycleValidator.GetValidationResult().Validate(dataModel);
    }
}
