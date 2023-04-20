using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AppDiv.CRVS.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException() : base("Validation Failure", "One or more validation errors occurred")
        {
            ErrorsDictionary = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            ErrorsDictionary = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public ValidationException(IEnumerable<IdentityError> errors) : this()
        {
            ErrorsDictionary = errors
                .GroupBy(e => e.Code, e => e.Description)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

       
        public ValidationException(IReadOnlyDictionary<string, string[]> errorsDictionary) : this()
        {
            ErrorsDictionary = errorsDictionary;
        }

        public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }
    }
}
