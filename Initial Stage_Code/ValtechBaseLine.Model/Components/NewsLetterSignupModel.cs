using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore;
using ValtechBaseLine.Validation;
using ValtechBaseLine.Validation.Validations;

namespace ValtechBaseLine.Model.Components
{
    public class NewsLetterSignupModel
    {
        [ValtechRequired(ValidationConstants.ValidationKeys.EmailIdRequired)]
        [ValtechRegularExpression(ValidationConstants.ValidationKeys.InvalidEmailAddress,ValidationConstants.RegularExpressions.ValidEmailRegex)]
        public string EmailId { get; set; }

        [ValtechRequired(ValidationConstants.ValidationKeys.FirstNameIsRequired)]
        public string FirstName { get; set; }

        [ValtechRequired(ValidationConstants.ValidationKeys.LastNameIsRequired)]
        public string LastName { get; set; }
    }
}
