define(["sitecore", "/-/speak/v1/ecm/Validation.js"], function (sitecore, Validation) {
return {
    priority: 4,
    execute: function (context) {
      if (!Validation.fromNameIsValid(unescape(context.currentContext.message.sender.name), context.currentContext.message.sender.email, context.currentContext.messageBar, sitecore)) {
        context.currentContext.errorCount = context.currentContext.errorCount + 1;
      }
    }
  };
});