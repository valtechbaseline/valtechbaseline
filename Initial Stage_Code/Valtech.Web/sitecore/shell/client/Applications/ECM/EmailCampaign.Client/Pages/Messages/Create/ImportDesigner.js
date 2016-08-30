define([
  "sitecore",
  "/-/speak/v1/ecm/MessageHelper.js",
  "/-/speak/v1/ecm/ServerRequest.js",
  "/-/speak/v1/ecm/DialogService.js"
], function (
  sitecore,
  MessageHelper,
  ServerRequest,
  DialogService
  ) {
  return sitecore.Definitions.App.extend({
    initialized: function () {
      var contextApp = this;

      // Set the notification text style to italic.
      contextApp.NotificationText.viewModel.$el.css("font-style", "italic");

      // Set the cursor of DesignImporterItemTextBox to "auto" instead of "not-allowed".
      contextApp.DesignImporterItemTextBox.viewModel.$el.css("cursor", "auto");

      contextApp.MessageBar.removeMessage(function (error) { return error.id === "absenceOfEnoughInfomation"; });
      if (!sessionStorage.createMessageParameters) {
        var absenceOfEnoughInfomation = { id: "absenceOfEnoughInfomation", text: sitecore.Resources.Dictionary.translate("ECM.Pipeline.CreateMessage.AbsenceOfEnoughInfomation"), actions: [], closable: false };
        contextApp.MessageBar.addMessage("error", absenceOfEnoughInfomation);
        return;
      }

      sessionStorage.removeItem("createMessageName");

      contextApp.NameTextBox.viewModel.$el.on("keyup", function (e) {
        $(this).change();
        var createButtonViewModel = contextApp.CreateButton.viewModel;
        var designName = contextApp.DesignImporterItemTextBox.get("text");
        var value = $(this).val();
        if (!value) {
          createButtonViewModel.disable();
        } else if (designName) {
          createButtonViewModel.enable();
          if (e.keyCode === 13) {
            createButtonViewModel.disable();
            if (MessageHelper.isCreateMessageAlreadyClicked(value)) { return; }
            contextApp.trigger("createmessage:create");
          }
        }
      });

      contextApp.DesignImporterItemTextBox.viewModel.$el.on("keyup", function (e) {
        contextApp.DesignImporterItemTextBox.viewModel.$el.change();
        var name = contextApp.NameTextBox.viewModel.$el.val();
        var designName = contextApp.DesignImporterItemTextBox.get("text");
        if (!designName) {
          contextApp.CreateButton.viewModel.disable();
        } else if (name) {
          contextApp.CreateButton.viewModel.enable();
          if (e.keyCode == 13) {
            contextApp.trigger("createmessage:create");
          }
        }
      });

      contextApp.NameTextBox.viewModel.focus();

      // back
      var numberOfEntriesWhenPageLoaded = history.length;
      contextApp.on("createmessage:back", function () {
        var updatednumberOfEntries = history.length;
        var pageToGoBack = (updatednumberOfEntries - numberOfEntriesWhenPageLoaded + 1) * -1;
        history.go(pageToGoBack);
      });

      // TODO: check is this deprecated
      contextApp.on("desingimporter:import", function() {
        var managerRootId = contextApp.EmailManagerRoot.get("managerRootId");
        var parameters = JSON.parse(sessionStorage.createMessageParameters);
        DialogService.show('designImporter', {
          designImporterItemTextBox: contextApp.DesignImporterItemTextBox,
          managerRootId: managerRootId,
          messageTypeTemplateId: parameters.messageTypeTemplateId,
          createButton: contextApp.CreateButton,
          nameTextBox: contextApp.NameTextBox
        });
      });

      contextApp.on("createmessage:create", function () {
        contextApp.MessageBar.removeMessages();
        var hasErrors = false;

        var name = _.escape(contextApp.NameTextBox.get("text"));
        if (!name) {
          var createMessageEmptyName = { id: "createMessageEmptyName", text: sitecore.Resources.Dictionary.translate("ECM.Pipeline.CreateMessage.CreateMessageEmptyName"), actions: [], closable: false };
          contextApp.MessageBar.addMessage("error", createMessageEmptyName);
          contextApp.NameTextBox.viewModel.focus();
          hasErrors = true;
        }

        var designName = contextApp.DesignImporterItemTextBox.get("text");
        if (!designName || !sessionStorage.newMessageTemplateId) {
          this.MessageBar.removeMessage(function (error) { return error.id === "notImportDesign"; });
          var notImportDesign = { id: "notImportDesign", text: sitecore.Resources.Dictionary.translate("ECM.Pipeline.ImportNewDesign.OneImportDesign"), actions: [], closable: false };
          this.MessageBar.addMessage("error", notImportDesign);
          hasErrors = true;
        }

        if (hasErrors) {
          return;
        }

        var parameters = JSON.parse(sessionStorage.createMessageParameters);
        var rootId = contextApp.EmailManagerRoot.get("managerRootId");
        var templateId = sessionStorage.newMessageTemplateId;
        var messageTypeTemplateId = parameters.messageTypeTemplateId;

        contextApp.currentContext = {
          messageTemplateId: templateId,
          managerRootId: rootId,
          messageName: name,
          messageTypeTemplateId: messageTypeTemplateId
        };
        var context = _.clone(contextApp.currentContext);

        MessageHelper.createNewMessage(context, contextApp.MessageBar, null, contextApp, sitecore);
      });

      window.onbeforeunload = function () {
        //sessionStorage.removeItem("createMessageParameters");
        //sessionStorage.removeItem("newMessageTemplateId");

        var importFolderId = $(".sc-DesignImporter").data("importfolderid");
        if (!importFolderId) {
          return;
        }

        contextApp.RemoveImportFolder(importFolderId);
      };
    },

    RemoveImportFolder: function (importFolderId) {
      if (!importFolderId) {
        return;
      }

      var contextApp = this;
      contextApp.currentContext = {
        folderId: importFolderId
      };

      var context = _.clone(contextApp.currentContext);
      sitecore.Pipelines.DeleteFolder.execute({ app: contextApp, currentContext: context });
    }
  });
});