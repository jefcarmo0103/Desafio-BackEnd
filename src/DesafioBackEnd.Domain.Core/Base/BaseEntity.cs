using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Domain.Core.Base
{
    public abstract class BaseEntity
    {
        public required Guid Id { get; set; }
    }

    public interface IValidatorModel<TDataModel> where TDataModel : BaseEntity
    {
        ValidationResult GetValidationResult(TDataModel dataModel);
    }
}
