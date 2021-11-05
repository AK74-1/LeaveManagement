using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace HR.LeaveManagement.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string> Errors { get; set; } = new();

        public ValidationException(ValidationResult result)
        {
            foreach (var error in result.Errors)
            {
                Errors.Add(error.ErrorMessage);
            }
        }
    }
}