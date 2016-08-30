define([
  "sitecore",
  "/-/speak/v1/ecm/ServerRequest.js",
  "/-/speak/v1/ecm/DialogService.js"
], function (
  sitecore,
  ServerRequest,
  DialogService) {
  return {
    priority: 1,
    execute: function (context) {
      ServerRequest("EXM/CheckPermissions", {
        data: { value: context.data.messageId },
        success: function (response) {
          if (response.error || !response.value) {
            var errorMessage = response.errorMessage || sitecore.Resources.Dictionary.translate("ECM.Pipeline.CopyToDraft.DoNotHavePermissions");
            DialogService.show('alert', { text: errorMessage });
            context.aborted = true;
            return;
          }
        },
        async: false
      });
    }
  };
});