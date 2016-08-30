define([
  "sitecore",
  "/-/speak/v1/ecm/DialogService.js",
  "/-/speak/v1/ecm/Validation.js"
], function (
  sitecore,
  DialogService,
  Validation
  ) {

  return {
    priority: 2,
    execute: function (context) {
      if (!context.confirmed) {
        context.aborted = true;

        DialogService.get('prompt')
          .done(function (dialog) {
            // TODO: Re-factor validation and remove onChange method from here
            var onChange = function () {
              var result = Validation.nameIsValidDuplicateMessage(
                dialog.PromptTextBox.get('text'),
                context.messageContext.get('itemNameValidation'),
                sitecore
                );
              if (result.error) {
                dialog.PromptTextBox.viewModel.$el.parent().addClass("has-error").css("height", "");
                dialog.PromptErrorText.set("text", result.message);
              } else {
                dialog.PromptErrorText.viewModel.$el.addClass("control-label");
                dialog.PromptErrorText.set("text", "");
                dialog.PromptTextBox.viewModel.$el.parent().removeClass("has-error");
              }
              dialog.Ok.set("isEnabled", !result.error && dialog.PromptTextBox.get("text").length !== 0);
            };
            dialog.PromptTextBox.off("change:text", onChange);
            dialog.PromptTextBox.on("change:text", onChange);

            dialog.showDialog({
              text: sitecore.Resources.Dictionary.translate("ECM.Pipeline.CopyToDraft.NewName"),
              watermark: sitecore.Resources.Dictionary.translate("ECM.Pipeline.CopyToDraft.NewNameWatermark"),
              on: {
                ok: function (text) {
                  sitecore.Pipelines.CopyToDraft.execute({
                    data: {
                      messageId: context.data.messageId,
                      messageName: dialog.PromptTextBox.get('text')
                    },
                    confirmed: true,
                    messageContext: context.messageContext
                  });
                }
              }
            });
          });
      }
    }
  };
});