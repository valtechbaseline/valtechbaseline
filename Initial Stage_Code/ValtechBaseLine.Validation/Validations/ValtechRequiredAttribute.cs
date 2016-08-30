using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ValtechBaseLine.Validation.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class ValtechRequiredAttribute : RequiredAttribute, IClientValidatable
    {
        private string Messagekey { get; set; }

        public ValtechRequiredAttribute(string key)
        {
            Messagekey = key;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
            {
                if (value.GetType() == typeof(List<System.Web.HttpPostedFileBase>))
                {
                    if (((List<HttpPostedFileBase>)value).First() == null)
                    {
                        return new ValidationResult(ErrorMessage ?? ValidationHelper.GetDictionaryValueByKey(Messagekey));
                                       
                    }
                }
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage ??
                                        Sitecore.Globalization.Translate.Text(Messagekey));
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRequiredRule(ValidationHelper.GetDictionaryValueByKey(Messagekey));
            return new[] { rule };
        }
    }
}