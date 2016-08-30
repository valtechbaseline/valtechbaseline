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
    priority: 4,
    execute: function (context) {
      postServerRequest("EXM/MessageUrl", { value: context.data.messageId }, function (response) {
        if (response.error || !response.value) {
          var errorMessage = response.errorMessage || sitecore.Resources.Dictionary.translate("ECM.Pipeline.CreateMessage.CanNotOpenNewMessage");
          DialogService.show('alert', { text: errorMessage });
          context.aborted = true;
          return;
        }

        if (response.error) {
          
          return;
        }

        window.parent.location.replace(response.value);
      }, false);
    }
  };
});