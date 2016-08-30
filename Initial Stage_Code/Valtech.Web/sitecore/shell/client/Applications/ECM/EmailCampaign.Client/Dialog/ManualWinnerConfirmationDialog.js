define(["sitecore"], function (sitecore) {
  return sitecore.Definitions.App.extend({
    initialized: function () {
      sitecore.on("dispatch:manualwinner:confirmation:dialog:show", this.showDialog, this);
      this.on("dispatch:manualwinner:confirmation:dialog:no", this.hideDialog, this);
      this.on("dispatch:manualwinner:confirmation:dialog:yes", this.selectWinner, this);
    },
    callBack: null,
    showDialog: function (params) {
      this.callBack = params.ok;

      // TODO remake for a dynamic height
      var modalBodyArray = this.ManualWinnerConfirmationDialog.viewModel.$el.find(".sc-dialogWindow-body");
      Array.prototype.filter.call(modalBodyArray, function (modalBodyElement) {
        $(modalBodyElement).css('overflow-y', 'auto');
        $(modalBodyElement).css('max-height', '140px');
      });

      if (params.messageType == "Triggered") {
        this.WhenYouSelectAWinnerTextTriggered.set("isVisible", true);
      }
      else {
        this.WhenYouSelectAWinnerTextUsual.set("isVisible", true);
      }

      this.ManualWinnerConfirmationDialog.show();
    },
    hideDialog: function () {
      this.ManualWinnerConfirmationDialog.hide();
    },
    selectWinner: function () {
      this.callBack();

      this.hideDialog();
    }
  });
});
