using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityTimetable.Common.Validators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ValidateFileSizeAttribute : ValidationAttribute, IClientValidatable
    {
        private const string _DefaultErrorMessage = "Maximum allowed file size is {0} bytes";
        private int _ValidSize { get; set; }
        
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = "validatefilesize",
                ErrorMessage = ErrorMessageString
            };
            rule.ValidationParameters.Add("validsize", _ValidSize);
            yield return rule;
        }

        public ValidateFileSizeAttribute(int validSize)
        {
            _ValidSize = validSize;
            ErrorMessage = string.Format(_DefaultErrorMessage, _ValidSize);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            HttpPostedFileBase file = value as HttpPostedFileBase;
            if ((file != null && file.ContentLength < 1 * 2048 * 2048) || file == null)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessageString);
        }
    
    }
}