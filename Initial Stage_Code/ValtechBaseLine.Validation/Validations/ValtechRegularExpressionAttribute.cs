using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using ModelMetadata = System.Web.ModelBinding.ModelMetadata;

namespace ValtechBaseLine.Validation.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
    public class ValtechRegularExpressionAttribute : ValidationAttribute//, IClientValidatable
    {
        private string Messagekey { get; set; }
        private string Pattern { get; set; }

        public ValtechRegularExpressionAttribute(string key, string pattern)
        {
            Messagekey = key;
            Pattern = pattern;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (new Regex(Pattern).IsMatch(value.ToString()))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ??
                                        ValidationHelper.GetDictionaryValueByKey(Messagekey));
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var regexRule = new ModelClientValidationRegexRule(ValidationHelper.GetDictionaryValueByKey(Messagekey), Pattern);

            return new[] { regexRule };
        }
    }
}