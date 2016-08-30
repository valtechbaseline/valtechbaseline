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
    priority: 3,
    execute: function (context) {
      postServerRequest("EXM/ExecuteEmailPreview", {
        messageId: context.currentContext.messageId,
        language: context.currentContext.language,
        clients: context.currentContext.clientIds,
        variants: context.currentContext.variantIds
      }, function (response) {
        context.currentContext.messageBar.removeMessage(function (error) { return error.id === "error.ecm.emailpreview.execute"; });
        
        if (response.error) {
          context.currentContext.messageBar.addMessage("error", response.errorMessage);
          context.currentContext.errorCount = 1;
          context.aborted = true;
          MessageReviewHelper.setEmailPreviewCheckButtonViewLogic(context.app, false);
          return;
        }

        context.response = response;
      }, false);
    }
  };
});