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
    priority: 1,
    execute: function (context) {
      ServerRequest("EXM/CanDeleteFolder", {
        data: { value: context.currentContext.folderId },
        success: function (response) {
          if (response.error || !response.value) {
            var errorMessage = response.errorMessage || sitecore.Resources.Dictionary.translate("ECM.Pipeline.DeleteFolder.CanNotDelete");

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