namespace ValtechBaseLine.Validation
{
    public class ValidationConstants
    {
        public const string TaxonomySourcePath = "/sitecore/content/Global Settings/Taxonomy";
        public const string TaxonomyTemplateID = "{E3C58E84-8E0D-4CF0-A25A-005E3ED9C6BC}";
        public static class ValidationKeys
        {
            #region Registration
            public const string SalutationIsRequired = "SalutationIsRequired";
            public const string FirstNameIsRequired = "FirstNameIsRequired";
            public const string LastNameIsRequired = "LastNameIsRequired";
            public const string PhoneNoIsRequired = "PhoneNoIsRequired";
            public const string EmailIdRequired = "EmailIdRequired";
            public const string InvalidEmailAddress = "InvalidEmailAddress";
            public const string PhoneNoIsInValid = "PhoneNoIsInValid";
            public const string PasswordIsRequired = "PasswordIsRequired";
            public const string ConfirmPasswordIsRequired = "ConfirmPasswordIsRequired";
            public const string PasswordMismatch = "PasswordMismatch";
            public const string DuplicateEmailAddress = "DuplicateEmailAddress";
            public const string FormSubmittedSuccessfully = "FormSubmittedSuccessfully";
            public const string TryAgain = "TryAgain";
            public const string InvalidReCaptcha = "InvalidReCaptcha";
            #endregion

            #region ContactUs
            public const string FirstNameRequired = "FirstNameRequired";
            public const string LastNameRequired = "LastNameRequired";
            public const string EmailRequired = "EmailRequired";
            public const string InvalidEmail = "InvalidEmail";


            #endregion
        }
        public static class RegularExpressions
        {
            public const string ValidEmailRegex = @"[A-Za-z-0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}";
        }
    }
}
