define([
  "sitecore",
  "/-/speak/v1/ecm/ServerRequest.js",
  "/-/speak/v1/ecm/DialogService.js"
], function (
  sitecore,
  ServerRequest,
  DialogService
  ) {
  return {
    priority: 3,
    execute: function (context) {
      if (!context.currentContext.confirmed) {
        context.aborted = true;
        return;
      }

      ServerRequest("EXM/DeleteMessage", {
        data: { value: context.currentContext.messageId },
        success: function (response) {
          var errorMessage;
          context.aborted = true;
          if (response.error) {
            errorMessage = response.errorMessage;
          } else if (!response.type) {
            errorMessage = sitecore.Resources.Dictionary.translate("ECM.Pipeline.DeleteMessage.DeleteFailed");
          } else if (response.type === "error") {
            errorMessage = sitecore.Resources.Dictionary.translate("ECM.Pipeline.DeleteMessage.CouldNotBeFound")
              .split("{0}").join(context.currentContext.messageName);
            context.aborted = false;
          } else if (response.type === "notification") {
            errorMessage = sitecore.Resources.Dictionary.translate("ECM.Pipeline.DeleteMessage.HasBeenDeleted")
              .split("{0}").join(context.currentContext.messageName);
            context.aborted = false;
          }

          if (errorMessage) {
            DialogService.show('alert', { text: errorMessage });
          }

        },
        async: false
      });
    }
  };
});