using API.ProcessExecutor.Interfaces;
using API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Validation
{
    public class StringIsTypeValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try {
                var service = (IProcessElementTypesService)validationContext
                             .GetService(typeof(IProcessElementTypesService));
                Dictionary<string, string> properties = service.GetProcessElementTypes();
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