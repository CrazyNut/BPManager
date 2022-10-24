using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DynamicLinq;
namespace API.Validation
{
     [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Unique : ValidationAttribute 
    {
        public Type TargetModelType { get; set; }
        public string TargetPropertyName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return (TargetModelType != null && !string.IsNullOrEmpty(TargetPropertyName)) ? DirectlyValid(value, validationContext) : ValidationResult.Success;
        }


        private ValidationResult DirectlyValid(object value, ValidationContext validationContext)
        {
           ApplicationContext applicationContext = (ApplicationContext)validationContext
                         .GetService(typeof(ApplicationContext));

                // var ttt=validationContext.ObjectInstance.GetType().GetProperties();
                // foreach (var item in validationContext.ObjectInstance.GetType().GetProperties())
                // {
                //    foreach (var item1 in item.Att)
                //    {
                    
                //     var t = item1;
                //     var t2 = t.AttributeType;
                //    }
                // }
                PropertyInfo IdProp = validationContext.ObjectInstance.GetType().GetProperties().FirstOrDefault(x => x.Name.ToLower().Contains("id") || x.CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute)));

                int? Id = IdProp == null ? null : (int)IdProp.GetValue(validationContext.ObjectInstance, null);
                
                Type entityType = validationContext.ObjectType;
                bool result = (bool)typeof(Unique)
                    .GetMethod("CheckUniqueParam")
                    .MakeGenericMethod(TargetModelType)
                    .Invoke(this, new object[] { Id, TargetPropertyName, value, applicationContext });
                
                if (!result)
                    return ValidationResult.Success;
                else
                    return new ValidationResult(ErrorMessageString);

        }
        public bool CheckUniqueParam<T>(int? modelId, string paramName, string paramValue, ApplicationContext applicationContext) where T : BaseEntity
        {
            var query = applicationContext.Set<T>().Where(paramName + "==@0", paramValue);
            if(modelId != null)
                query = query.Where(m => m.Id != modelId);
            return query.Any();
        }
    }
}