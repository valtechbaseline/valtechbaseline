define(["sitecore", "/-/speak/v1/ecm/Validation.js"], function (sitecore, Validation) {
return {
    priority: 1,
    execute: function (context) {
      if (!Validation.nameIsValid(unescape(context.currentContext.message.name), context.currentContext.messageBar, sitecore, context.currentContext.message.itemNameValidation)) {
        context.currentContext.errorCount = context.currentContext.errorCount + 1;
      }
    }
  };
});