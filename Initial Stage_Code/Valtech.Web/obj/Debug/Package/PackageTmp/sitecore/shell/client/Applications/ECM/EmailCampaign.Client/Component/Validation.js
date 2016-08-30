define(["sitecore"], function () {

  ValidationMethods = {
    createErrorMessage: function (id, text) {
      return {
        id: id,
        text: text,
        actions: [],
        closable: true
      };
    },
    nameIsValid: function (name, messageBar, sitecore, validationExpression) {
      if (!messageBar) { return false; }

      var validateNameMessage = ValidationMethods.createErrorMessage("validateNameMessage", sitecore.Resources.Dictionary.translate("ECM.Pipeline.Validate.NameRequired"));
      var result = ValidationMethods.isRequired(name, validateNameMessage, messageBar);
      if (!result) { return result; }

      validateNameMessage = ValidationMethods.createErrorMessage("validateNameMessage", sitecore.Resources.Dictionary.translate("ECM.Pipeline.Validate.NameTrimRequired"));
      result = ValidationMethods.isTrimRequired(name, validateNameMessage, messageBar);
      if (!result) { return result; }

      validateNameMessage = ValidationMethods.createErrorMessage("validateNameMessage", sitecore.Resources.Dictionary.translate("ECM.Pipeline.Validate.IsNotValidName").replace(/\{0\}/gi, name));
      result = ValidationMethods.isItemNameValid(name, validationExpression, validateNameMessage, messageBar);
      if (!result) { return result; }

      var validateMaxNameLengthMessage = ValidationMethods.createErrorMessage("validateMaxNameLengthMessage", sitecore.Resources.Dictionary.translate("ECM.Pipeline.Validate.NameMaxLength"));
      return ValidationMethods.maxLengthIsValid(name, 100, validateMaxNameLengthMessage, messageBar);
    },

    nameIsValidDuplicateMessage: function(content, validationExpression, sitecore) {
      var result = { error: false, message: "" }
      if (content === "") {
        return result;
      }
      if (!content || content.trim() === "") {
        result.message = sitecore.Resources.Dictionary.translate("ECM.Pipeline.Validate.NameTrimRequired");
        result.error = true;
        return result;
      }
      var re = new RegExp(validationExpression);
      if (!re.test(content)) {
        result.message = sitecore.Resources.Dictionary.translate("ECM.Pipeline.Validate.IsNotValidName").replace(/\{0\}/gi, content);
        result.error = true;
        return result;
      }
      if (content.length > 100) {
        result.message = sitecore.Resources.Dictionary.translate("ECM.Pipeline.Validate.NameMaxLength");
        result.error = true;
      }
      return result;
    },

    fromEmailIsValid: function (email, fromName, messageBar, sitecore) {
      if (!messageBar) { return false; }
      var fromEmailMessage = ValidationMethods.createErrorMessage("fromEmailMessage", sitecore.Resources.Dictionary.translate("ECM.Pipeline.Validate.FromEmailRequired"));
      var result = ValidationMethods.isRequired(email, fromEmailMessage, messageBar);
      if (!result) { return result; }
      var fromEmailNotValidMessage = ValidationMethods.createErrorMessage("fromEmailNotValidMessage", sitecore.Resources.Dictionary.translate("ECM.Pipeline.Validate.FromEmailNotValid"));
      result = ValidationMethods.emailIsValid(email, fromEmailNotValidMessage, messageBar);
      if (!result) { return result; }
      return ValidationMethods.fromNameAndFromEmailLenghtIsValid(fromName, email, messageBar, sitecore);
    },

    fromNameIsValid: function (fromName, fromEmail, messageBar, sitecore) {
      if (!messageBar) { return false; }
      return ValidationMethods.fromNameAndFromEmailLenghtIsValid(fromName, fromEmail, messageBar, sitecore);
    },

    replyToTextBoxIsValid: function (text, messageBar, sitecore) {
      if (!messageBar) { return false; }
      var replyToNotValidMessage = ValidationMethods.createErrorMessage("replyToNotValidMessage", sitecore.Resources.Dictionary.translate("ECM.Pipeline.Validate.ReplyToEmailNotValid"));
      if (text == "") {
        messageBar.removeMessage(function (error) { return error.id === replyToNotValidMessage.id; });
        return true;
      }
      return ValidationMethods.emailIsValid(text, replyToNotValidMessage, messageBar);
    },

    isRequired: function (content, messagetoAdd, messageBar) {
      messageBar.removeMessage(function (error) { return error.id === messagetoAdd.id; });
      if (!content || content == "") {
        messageBar.addMessage("error", messagetoAdd);
        return false;
      }
      return true;
    },

    isTrimRequired: function (content, messagetoAdd, messageBar) {
      messageBar.removeMessage(function (error) { return error.id === messagetoAdd.id; });
      if (!content || content.trim() === "") {
        messageBar.addMessage("error", messagetoAdd);
        return false;
      }
      return true;
    },

    isItemNameValid: function (content, validationExpression, messagetoAdd, messageBar) {
      messageBar.removeMessage(function (error) { return error.id === messagetoAdd.id; });
      var re = new RegExp(validationExpression);
      if (!re.test(content)) {
        messageBar.addMessage("error", messagetoAdd);
        return false;
      }
      return true;
    },

    maxLengthIsValid: function (content, maxLength, messagetoAdd, messageBar) {
      messageBar.removeMessage(function (error) { return error.id === messagetoAdd.id; });
      if (content.length > maxLength) {
        messageBar.addMessage("error", messagetoAdd);
        return false;
      }
      return true;
    },

    emailIsValid: function (email, messagetoAdd, messageBar) {
      messageBar.removeMessage(function (error) { return error.id === messagetoAdd.id; });
      var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
      if (filter.test(email)) {
        return true;
      }
      messageBar.addMessage("error", messagetoAdd);
      return false;
    },

    urlIsValid: function (textval, messagetoAdd, messageBar) {
      messageBar.removeMessage(function (error) { return error.id === messagetoAdd.id; });
      var filter = /^(http|https){1}\:\/\/(([a-zA-Z0-9][a-zA-Z0-9\-]{0,61}[a-zA-Z0-9]?)\.)*([a-zA-Z0-9][A-Za-z0-9\-]{0,61}[A-Za-z0-9]?)(:\d{2,5})?(\/[a-zA-Z0-9][A-Za-z0-9\-]{0,61}[A-Za-z0-9]?)*(\/{1})?$/;
      if (filter.test(textval)) {
        return true;
      }
      messageBar.addMessage("error", messagetoAdd);
      return false;
    },

    fromNameAndFromEmailLenghtIsValid: function (fromName, fromEmail, messageBar, sitecore) {
      var toLongMessage = ValidationMethods.createErrorMessage("toLongMessage", sitecore.Resources.Dictionary.translate("ECM.Pipeline.Validate.FromNamePlusFromAddressIsToLong"));
      messageBar.removeMessage(function (error) { return error.id === toLongMessage.id; });
      if (fromName.length + fromEmail.length > 254) {
        messageBar.addMessage("error", toLongMessage);
        return false;
      }
      return true;
    },

    isModified: function (messageContext, messageBar) {
      if (messageBar.get("errors").length > 0) {
        messageContext.set("isModified", false);
      }
    },

    subjectIsEmpty: function (variants, messageBar, sitecore) {
      var emptySubjectErrorMessage = ValidationMethods.createErrorMessage("emptySubjectErrorMessage", sitecore.Resources.Dictionary.translate("ECM.Pages.Message.TheSubjectFieldIsEmpty"));
      messageBar.removeMessage(function (error) { return error.id === emptySubjectErrorMessage.id; });
      var isSubjectEmpty = false;
      _.each(variants, function (variant) {
        if (!variant.subject.trim()) {     
          isSubjectEmpty = true;
        }
      });
      if (isSubjectEmpty) {
        messageBar.addMessage("error", emptySubjectErrorMessage);
        return false;
      } 
      return true; 
    }
  }
  return ValidationMethods;
});