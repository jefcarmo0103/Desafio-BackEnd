using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Domain.Core.Models
{
    public sealed record OperationResult(IEnumerable<SupportMessage> messages)
    {
        public static OperationResult Ok(IEnumerable<SupportMessage> messages)
        {
            return new OperationResult(messages);
        }

        public static OperationResult Ok(string message)
        {
            return new OperationResult(new SupportMessage[] { new(message, true) });
        }

        public static OperationResult Fail(string message)
        {
            return new OperationResult(new SupportMessage[] { new(message, false) });
        }

        public static OperationResult Fail(IEnumerable<SupportMessage> messages)
        {
            return new OperationResult(messages);
        }

        public static OperationResult Fail(ValidationResult validationResult)
        {
            return new OperationResult(validationResult.Errors.Select(x => new SupportMessage(x.ErrorMessage, validationResult.IsValid)));
        }
    }

    public sealed record OperationResult<TResult>(TResult? Data, IEnumerable<SupportMessage> Messages)
    {
        public static OperationResult<TResult> Ok(TResult data, IEnumerable<SupportMessage> messages)
        {
            return new OperationResult<TResult>(data, messages);
        }

        public static OperationResult<TResult> Ok(TResult data, string message)
        {
            return new OperationResult<TResult>(data, new SupportMessage[] { new(message, true) });
        }

        public static OperationResult<TResult> Fail(string message)
        {
            return new OperationResult<TResult>(default, new SupportMessage[] { new(message, false) });
        }

        public static OperationResult<TResult> Fail(IEnumerable<SupportMessage> messages)
        {
            return new OperationResult<TResult>(default, messages);
        }

        public static OperationResult<TResult> Fail(ValidationResult validationResult)
        {
            return new OperationResult<TResult>(default, validationResult.Errors.Select(x => new SupportMessage(x.ErrorMessage, validationResult.IsValid)));
        }
    }

    public sealed record SupportMessage(string message, bool sucess) { }
}
