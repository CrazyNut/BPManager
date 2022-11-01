using API.ProcessExecutor.Interfaces;
using API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Services;

namespace API.Validation
{
    public class StringIsTypeValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try {
                Dictionary<string, string> properties = ProcessElementTypesService.GetTypes();
                // Since NoneSuch does not exist in this assembly, GetType throws a TypeLoadException.
                Type myType2 = Type.GetType(properties[(string)value], true);
             return ValidationResult.Success;
            }
            catch(TypeLoadException) {
                return new ValidationResult(ErrorMessageString);
            }
        }

         
    }
}