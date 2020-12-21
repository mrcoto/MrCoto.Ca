using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using MrCoto.Ca.Domain.Common.Exceptions;

namespace MrCoto.Ca.Application.Common.Exceptions
{
    public class ValidationException : BusinessException
    {
        public const string Code = "SYS:VALIDATION";
        
        public ValidationException()
            : base(Code, "Se han encontrado uno o más errores de validación")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}