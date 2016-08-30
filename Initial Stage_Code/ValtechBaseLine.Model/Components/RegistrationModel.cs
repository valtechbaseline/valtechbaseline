using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Shell.Framework.Commands;
using ValtechBaseLine.Model.Common;
using System.ComponentModel.DataAnnotations;
using Glass.Mapper.Sc.Configuration.Attributes;
using System.Web.Mvc;
using ValtechBaseLine.Validation;
using ValtechBaseLine.Validation.Validations;

namespace ValtechBaseLine.Model.Components
{
    public class RegistrationModel
    {
        #region Form
        [ValtechRequiredAttribute(ValidationConstants.ValidationKeys.SalutationIsRequired)]
        public string Salutation { get; set; }

        public List<SelectListItem> SalutationOption { get; set; }

        [ValtechRequiredAttribute(ValidationConstants.ValidationKeys.FirstNameIsRequired)]
        public string FirstName { get; set; }

        [ValtechRequiredAttribute(ValidationConstants.ValidationKeys.LastNameIsRequired)]
        public string LastName { get; set; }

        [ValtechRequiredAttribute(ValidationConstants.ValidationKeys.PhoneNoIsRequired)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = ValidationConstants.ValidationKeys.PhoneNoIsInValid)]
        public string PhoneNumber { get; set; }

        [ValtechRequired(ValidationConstants.ValidationKeys.EmailIdRequired)]
        [ValtechRegularExpression(ValidationConstants.ValidationKeys.InvalidEmailAddress, @"[A-Za-z-0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string EmailId { get; set; }

        [ValtechRequiredAttribute(ValidationConstants.ValidationKeys.PasswordIsRequired)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [ValtechRequiredAttribute(ValidationConstants.ValidationKeys.ConfirmPasswordIsRequired)]
        [DataType(DataType.Password)]
        [ValtechCompare("Password", ValidationConstants.ValidationKeys.PasswordMismatch)]
        public string ConfirmPassword { get; set; }

        public string SelectedSalutation { get; set; }
        #endregion

        #region Sitecore Fields
        [SitecoreField("Salutation")]
        public string SalutationLabel { get; set; }

        [SitecoreField("First Name")]
        public string FirstNameLabel { get; set; }

        [SitecoreField("Last Name")]
        public string LastNameLabel { get; set; }

        [SitecoreField("Phone Number")]
        public string PhoneNumberLabel { get; set; }

        [SitecoreField("E Mail Id")]
        public string EMailIdLabel { get; set; }

        [SitecoreField("Password")]
        public string PasswordLabel { get; set; }

        [SitecoreField("Confirm Password")]
        public string ConfirmPasswordLabel { get; set; }

        [SitecoreField("Show Recaptcha")]
        public bool ShowRecaptcha { get; set; }

        [SitecoreField("Register Button")]
        public string RegisterButtonLabel { get; set; }

        public string Status { get; set; }
        public string Message { get; set; }
        public string url { get; set; }

        #endregion


    }
}
