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
    execute: function(context) {
      if (!context.confirmed) {
        context.aborted = true;
        return;
      }

      var itemIds = [];
      for (var i = 0; i < context.recipientLists.length; i++) {
        itemIds[i] = context.recipientLists[i].itemId;
      }

      ServerRequest("EXM/RemoveUnsubscribedContacts", {
        data: { recipientListIds: itemIds, messageId: context.messageId },
        success: function (response) {
          if (response.error) {
            DialogService.show('alert', { text: response.errorMessage });
            context.aborted = true;
            return;
          }

          context.response = response.results;
        },
        async: true
      });
    }
  };
});