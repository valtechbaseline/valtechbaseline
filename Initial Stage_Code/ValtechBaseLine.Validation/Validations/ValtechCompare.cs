using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace ValtechBaseLine.Validation.Validations
{
    public class ValtechCompareAttribute : CompareAttribute//, IClientValidatable
    {
        public string OtherPropertyName { get; set; }
        private string Messagekey { get; set; }

        public ValtechCompareAttribute(string otherProperty, string messageKey)
            : base(otherProperty)
        {
            OtherPropertyName = otherProperty;
            Messagekey = messageKey;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            PropertyInfo otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherPropertyName);

            var otherPropertyStringValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null).ToString();

            if (Equals(value.ToString(), otherPropertyStringValue))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage ??
                                          ValidationHelper.GetDictionaryValueByKey(Messagekey));
        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new[] { new ModelClientValidationEqualToRule(ValidationHelper.GetDictionaryValueByKey(Messagekey), OtherPropertyName) };
        }
    }
}