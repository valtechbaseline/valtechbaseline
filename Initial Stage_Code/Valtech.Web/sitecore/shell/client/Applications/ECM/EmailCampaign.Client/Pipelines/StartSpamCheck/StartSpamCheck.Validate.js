define([
  "sitecore",
  "/-/speak/v1/ecm/ServerRequest.js",
  "/-/speak/v1/ecm/MessageReviewHelper.js"
], function (
  _sc,
  ServerRequest,
  MessageReviewHelper
  ) {
  return {
    priority: 2,
    execute: function (context) {
      postServerRequest("EXM/ValidateSelectedClients", {
        messageId: context.currentContext.messageId,
        language: context.currentContext.language,
        clients: context.currentContext.clientIds,
        variants: context.currentContext.variantIds
      }, function (response) {
        context.currentContext.messageBar.removeMessage(function (error) { return error.id === "error.ecm.spamcheck.validate"; });

        if (response.error) {
          var messagetoAddError = {
            id: "error.ecm.spamcheck.validate",
            text: response.errorMessage,
            actions: [],
            closable: true
          };
          context.currentContext.messageBar.addMessage("error", messagetoAddError);
          context.currentContext.errorCount = 1;
          context.aborted = true;
          MessageReviewHelper.setSpamCheckCheckButtonViewLogic(context.app, false);
          return;
        }

      }, false);
    }
  };
});