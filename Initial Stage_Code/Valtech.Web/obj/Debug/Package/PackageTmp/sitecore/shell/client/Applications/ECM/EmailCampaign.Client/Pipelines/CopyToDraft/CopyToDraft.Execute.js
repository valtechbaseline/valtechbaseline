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
      if (!context.confirmed) {
        context.aborted = true;
        return;
      }

      ServerRequest("EXM/CopyToDraft", {
        data: { sourceMessageId: context.data.messageId, messageName: _.escape(context.data.messageName) },
        success: function (response) {
          if (response.error) {
            DialogService.show('alert', { text: response.errorMessage });
            context.aborted = true;
            return;
          }

          context.data.messageId = response.value;
        },
        async: false
      });

    }
  };
});