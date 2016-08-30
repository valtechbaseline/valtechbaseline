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
    priority: 20,

    execute: function (context) {
      if (!context.confirmed) {
        context.aborted = true;
        return;
      }
      ServerRequest("EXM/RemoveBouncedContacts", {
        data: { recipientListId: context.recipientList.itemId, messageId: context.messageId },
        success: function (response) {
          if (response.error) {
            DialogService.show('alert', { text: response.errorMessage });
            context.aborted = true;
            return;
          }

          context.response = response.value;
        },
        async: false
      });
    }
  };
});