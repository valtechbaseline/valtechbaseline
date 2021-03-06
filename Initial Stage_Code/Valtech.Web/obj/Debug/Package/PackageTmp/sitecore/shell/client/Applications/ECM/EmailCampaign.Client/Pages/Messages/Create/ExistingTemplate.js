﻿define(["sitecore", "/-/speak/v1/ecm/MessageHelper.js"], function (sitecore, MessageHelper) {
  return sitecore.Definitions.App.extend({
    initialized: function() {
      var contextApp = this;

      contextApp.MessageBar.removeMessage(function(error) { return error.id === "absenceOfEnoughInfomation"; });
      if (!sessionStorage.createMessageParameters) {
        var messagetoAdd = { id: "absenceOfEnoughInfomation", text: sitecore.Resources.Dictionary.translate("ECM.Pipeline.CreateMessage.AbsenceOfEnoughInfomation"), actions: [], closable: false };
        contextApp.MessageBar.addMessage("error", messagetoAdd);
        return;
      }

      var parameters = JSON.parse(sessionStorage.createMessageParameters);
      contextApp.setHeaderTitle(parameters.messageTemplateId, parameters.messageTypeTemplateId, contextApp);
      contextApp.setBylineText(parameters.messageTypeTemplateId, contextApp);

      var nameTextBoxViewModel = contextApp.NameTextBox.viewModel;
      nameTextBoxViewModel.focus();
      sessionStorage.removeItem("createMessageName");

      nameTextBoxViewModel.$el.on("keyup", function (e) {
        $(this).change();
        var createButtonViewModel = contextApp.CreateButton.viewModel;
        var value = $(this).val(); 
        if (!value) {
          createButtonViewModel.disable();
        } else {         
          createButtonViewModel.enable();
          if (e.keyCode === 13) {
            createButtonViewModel.disable();
            if (MessageHelper.isCreateMessageAlreadyClicked(value)) { return; }
            contextApp.trigger("createmessage:create");
          }
        }
      });
      
      // back
      contextApp.on("createmessage:back", function() {
        history.back();
      });

      contextApp.on("createmessage:create", function () {
        contextApp.MessageBar.removeMessages();
        var name = _.escape(contextApp.NameTextBox.get("text"));
        if (!name) {
          var createMessageEmptyName = { id: "createMessageEmptyName", text: sitecore.Resources.Dictionary.translate("ECM.Pipeline.CreateMessage.CreateMessageEmptyName"), actions: [], closable: false };
          contextApp.MessageBar.addMessage("error", createMessageEmptyName);
          contextApp.NameTextBox.viewModel.focus();
          return;
        }

        var rootId = contextApp.EmailManagerRoot.get("managerRootId");
        var templateId = parameters.messageTemplateId;
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
      window.onbeforeunload = function() {
        //sessionStorage.removeItem("createMessageParameters");       
      };
    },

    setHeaderTitle: function (messageTemplateId, messageTypeTemplateId, contextApp) {
      switch (messageTemplateId) {
      case "{F112BDEF-8D86-4CEA-9B9A-8477A582926C}":
        contextApp.HeaderTitle.set("text", sitecore.Resources.Dictionary.translate("ECM.ExistingTemplatePage.NewsletterHeaderTitle"));
        break;
      case "{9E170B31-F8AA-4E1F-B605-2FC0BD4FE120}":
        contextApp.HeaderTitle.set("text", sitecore.Resources.Dictionary.translate("ECM.ExistingTemplatePage.TwoColumnHeaderTitle"));
        break;
      case "{6FE51EB4-1D30-4E6B-8BA0-0EBB1405D283}":
        contextApp.HeaderTitle.set("text", sitecore.Resources.Dictionary.translate("ECM.ExistingTemplatePage.OneColumnHeaderTitle"));
        break;
      case "{ECF0A9C8-FE56-45D8-B750-6BDE3A34A532}":
        contextApp.HeaderTitle.set("text", sitecore.Resources.Dictionary.translate("ECM.ExistingTemplatePage.SimpleHTMLHeaderTitle"));
        break;
      case "{1AFE38A7-9461-4278-ADAF-D807F27F36E4}":
        contextApp.HeaderTitle.set("text", sitecore.Resources.Dictionary.translate("ECM.ExistingTemplatePage.PlainTextMessageHeaderTitle"));
        break;
      default:
        if (messageTypeTemplateId === "{7BE25E20-281C-43DD-AACC-156102617D66}") {
            contextApp.HeaderTitle.set("text", sitecore.Resources.Dictionary.translate("ECM.ExistingTemplatePage.SubscriptionMessageHeaderTitle"));
        } else {
            contextApp.HeaderTitle.set("text", sitecore.Resources.Dictionary.translate("ECM.ExistingTemplatePage.DefaultHeaderTitle"));
        }
      }
    },

    setBylineText: function (messageTypeTemplateId, contextApp) {
    	switch (messageTypeTemplateId) {
    		case "{54E7880D-2621-49EA-A417-982FCD372903}":
    			contextApp.MessageNameBylineText.set("text", sitecore.Resources.Dictionary.translate("ECM.BylineTexts.OneTimeMessageNameByline"));
    			break;
    		case "{7BE25E20-281C-43DD-AACC-156102617D66}":
    			contextApp.MessageNameBylineText.set("text", sitecore.Resources.Dictionary.translate("ECM.BylineTexts.SubscriptionMessageNameByline"));
    			break;
    		case "{9795C562-98D9-4A9E-BC9C-C6B7A079FDF4}":
    			contextApp.MessageNameBylineText.set("text", sitecore.Resources.Dictionary.translate("ECM.BylineTexts.TriggeredMessageNameByline"));
    			break;
    		default:
    			break;
    	}
    	contextApp.MessageNameBylineText.viewModel.$el.css("font-style", "italic");
    }
  });
});
