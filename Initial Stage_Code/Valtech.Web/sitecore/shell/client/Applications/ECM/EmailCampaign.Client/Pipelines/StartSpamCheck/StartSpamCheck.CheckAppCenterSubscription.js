﻿define([
  "sitecore",
  "/-/speak/v1/ecm/ServerRequest.js",
  "/-/speak/v1/ecm/MessageReviewHelper.js"
], function (
  _sc,
  ServerRequest,
  MessageReviewHelper
  ) {
  return {
    priority: 1,
    execute: function (context) {
      postServerRequest("EXM/IsSpamCheckServicePurchased", {}, function (response) {
        context.currentContext.messageBar.removeMessage(function (error) { return error.id === "error.ecm.spamcheck.checkforpurchase"; });

        if (response.error) {
          var messagetoAddError = {
            id: "error.ecm.spamcheck.checkforpurchase",
            text: response.errorMessage,
            actions: [],
            closable: true
          };

          if (response.actions) {
            messagetoAddError.actions = [{ text: response.actions[0].Key, action: response.actions[0].Value }];
          }

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