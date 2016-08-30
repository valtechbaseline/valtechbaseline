define(["sitecore", "/-/speak/v1/ecm/Validation.js"], function (sitecore, Validation) {
  return {
    priority: 5,
    execute: function (context) {
      if (!Validation.fromEmailIsValid(context.currentContext.message.sender.email, context.currentContext.message.sender.name, context.currentContext.messageBar, sitecore)) {
        context.currentContext.errorCount = context.currentContext.errorCount + 1;
      }
      if (!Validation.replyToTextBoxIsValid(context.currentContext.message.sender.replyTo, context.currentContext.messageBar, sitecore)) {
        context.currentContext.errorCount = context.currentContext.errorCount + 1;
      }
    }
  };
});