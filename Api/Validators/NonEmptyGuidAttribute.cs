using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Validators
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class NonEmptyGuidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((value is Guid) && Guid.Empty == (Guid)value)
            {
                return new ValidationResult("Guid cannot be empty.");
            }
            return null;
        }
    }
}