﻿define(["sitecore"], function (sitecore) {
  return sitecore.Definitions.App.extend({
    initialized: function () {
      this.on("alertdialog:hide", this.hideDialog, this);
    },
    showDialog: function (text) {
      this.AlertText.set("text", text.text);
      this.AlertDialogWindow.show();
    },
    hideDialog: function () {
      this.AlertDialogWindow.hide();
    }
  });
});