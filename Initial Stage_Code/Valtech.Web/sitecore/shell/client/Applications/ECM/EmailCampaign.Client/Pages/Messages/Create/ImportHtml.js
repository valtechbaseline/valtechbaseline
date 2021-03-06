﻿define(["sitecore", "/-/speak/v1/ecm/MessageHelper.js"], function (sitecore, MessageHelper) {
  return sitecore.Definitions.App.extend({
    initialized: function() {
      var contextApp = this;
      //Set the notification text style to italic.
      contextApp.UploadText.viewModel.$el.css("font-style", "italic");

      contextApp.MessageBar.removeMessage(function(error) { return error.id === "absenceOfEnoughInfomation"; });
      if (!sessionStorage.createMessageParameters) {
        var absenceOfEnoughInfomation = { id: "absenceOfEnoughInfomation", text: sitecore.Resources.Dictionary.translate("ECM.Pipeline.CreateMessage.AbsenceOfEnoughInfomation"), actions: [], closable: false };
        contextApp.MessageBar.addMessage("error", absenceOfEnoughInfomation);
        return;
      }

      var nameTextBoxViewModel = contextApp.NameTextBox.viewModel;
      nameTextBoxViewModel.focus();
      sessionStorage.removeItem("createMessageName");

      contextApp.on("upload-fileUploaded", contextApp.createNewMessage, contextApp);
      contextApp.on("upload-error", contextApp.handleUploadError, contextApp);

      nameTextBoxViewModel.$el.on("keyup", function (e) {
        $(this).change();
        var html = contextApp.Uploader.viewModel.totalFiles();
        var createButtonViewModel = contextApp.CreateButton.viewModel;
        var value = $(this).val();
        if (!value) {
          createButtonViewModel.disable();
        } else if (html > 0) {
          createButtonViewModel.enable();
          if (e.keyCode === 13) {
            createButtonViewModel.disable();
            if (MessageHelper.isCreateMessageAlreadyClicked(value)) { return; }
            contextApp.trigger("createmessage:create");
          }
        }
      });

      contextApp.Uploader.on("change:totalFiles", function() {
        var html = contextApp.Uploader.viewModel.totalFiles();
        var name = _.escape(contextApp.NameTextBox.get("text"));
        if (html == 0) {
          contextApp.CreateButton.viewModel.disable();
        } else if (name) {
          contextApp.CreateButton.viewModel.enable();
        }
      });

      // back
      contextApp.on("createmessage:back", function() {
        history.back();
      });

      contextApp.on("createmessage:create", contextApp.upload, contextApp);

      window.onbeforeunload = function() {
        //sessionStorage.removeItem("createMessageParameters");
      };
    },

    upload: function() {
      var contextApp = this;

      contextApp.MessageBar.removeMessages();
      var hasErrors = false;
      var name = contextApp.NameTextBox.get("text");
      if (!name) {
        var createMessageEmptyName = { id: "createMessageEmptyName", text: sitecore.Resources.Dictionary.translate("ECM.Pipeline.CreateMessage.CreateMessageEmptyName"), actions: [], closable: false };
        contextApp.MessageBar.addMessage("error", createMessageEmptyName);
        contextApp.NameTextBox.viewModel.focus();
        hasErrors = true;
      }

      if (contextApp.Uploader.viewModel.totalFiles() == 0) {
        var notImportHtml = { id: "notImportHtml", text: sitecore.Resources.Dictionary.translate("ECM.Pipeline.ImportHtmlLayout.NotImportHtml"), actions: [], closable: false };
        contextApp.MessageBar.addMessage("error", notImportHtml);
        hasErrors = true;
      }

      if (contextApp.Uploader.viewModel.totalFiles() > 1) {
        var oneHtmlOnly = { id: "oneHtmlOnly", text: sitecore.Resources.Dictionary.translate("ECM.Pipeline.ImportHtmlLayout.OneHtmlOnly"), actions: [], closable: false };
        contextApp.MessageBar.addMessage("error", oneHtmlOnly);
        hasErrors = true;
      }

      if (contextApp.UploaderInfo.viewModel.files()[0].type() !== 'text/html') {
        var notHtmlFile = { id: "notHtmlFile", text: sitecore.Resources.Dictionary.translate("ECM.Pipeline.ImportHtmlLayout.NotHtmlFile"), actions: [], closable: false };
        contextApp.MessageBar.addMessage("error", notHtmlFile);
        hasErrors = true;
      }
      if (hasErrors) {
        return;
      }
      contextApp.Uploader.viewModel.upload();
    },

    handleUploadError: function(errorObject) {
      _.each(errorObject.errors, function(err) {
        this.MessageBar.addMessage("error", err.Message);
      }, this);
    },

    createNewMessage: function(file) {
      var contextApp = this;
      var parameters = JSON.parse(sessionStorage.createMessageParameters);

      var name = contextApp.NameTextBox.get("text");
      var rootId = contextApp.EmailManagerRoot.get("managerRootId");
      var templateId = parameters.messageTemplateId;
      var messageTypeTemplateId = parameters.messageTypeTemplateId;

      contextApp.currentContext = {
        messageTemplateId: templateId,
        managerRootId: rootId,
        messageName: name,
        messageTypeTemplateId: messageTypeTemplateId,
        fileItemId: file.itemId,
        fileName: file.data.name,
        database: contextApp.Uploader.viewModel.$el.data("sc-databasename")
    };
      var context = _.clone(contextApp.currentContext);

      var result = MessageHelper.createNewMessageFromImportHtml(context, contextApp.MessageBar, null, contextApp, sitecore);
      if (!result) {
        contextApp.UploaderInfo.viewModel.$el.find("div.sc-uploaderInfo-row").parent().remove();
      }
    },
  });
  
});