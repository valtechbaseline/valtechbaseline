define(["sitecore", "/-/speak/v1/ExperienceEditor/ExperienceEditor.js"],
  function (sitecore, experienceEditor) {
  return sitecore.Definitions.App.extend({
    variantContextObj: null,

    initialized: function () {
      this.bindToWebEditRibbonLoaded();
      this.attachEventHandlers();
    },

    attachEventHandlers: function() {
      sitecore.on("message:variant:content:editor:dialog:show", this.showDialog, this);

      this.on({
        "message:variant:content:editor:dialog:ok": this.onDialogOk,
        "message:variant:content:editor:dialog:cancel": this.onDialogCancel,
        "experience:editor:context:loaded": function (experienceEditor) {
          this.experienceEditor = experienceEditor;
          this.experienceEditorContext = experienceEditor.Context;
          this.setExperienceEditorEditMode();
        }
      }, this);

      this.DialogWindow.viewModel.$el.on("hide", _.bind(function () {
        if (typeof this.hideCallback === "function") {
          this.hideCallback();
        }
      }, this));
    },

    showDialog: function (params) {
      this.experienceEditorContext = null;
      this.onDialogShow(params.variantContext, params.hide);
    },

    bindToWebEditRibbonLoaded: function() {
      this.Frame.viewModel.$el.off('load.messageVariantsContentEditorDialog');
      this.Frame.viewModel.$el.on('load.messageVariantsContentEditorDialog', _.bind(function () {
        var webRibbon = this.Frame.viewModel.$el.contents()
          .find('#scWebEditRibbon');

        if (webRibbon.length) {
          // Need to wait while Sitecore object will be initialized inside webRibbon iframe.
          // It doesn't trigger any events when initialized, so setInterval is used here.
          var loadRibbonInterval = setInterval(_.bind(function () {
            if (sitecore && sitecore.ExperienceEditor) {
              this.trigger("experience:editor:context:loaded", sitecore.ExperienceEditor);
              clearInterval(loadRibbonInterval);
            }
          }, this), 1000);
        }
      }, this));
    },

    disableEditMode: function () {
      this.Frame.set('sourceUrl', '');
      document.cookie = 'website#sc_mode=normal;path=/';
    },

    getExperienceEditor: function () {
      return window.top.ExperienceEditor;
    },

    getExperienceEditorContext: function () {
      return this.getExperienceEditor().getContext();
    },

    onDialogShow: function (variant, hideCallback) {
      if (!variant && variant.readonly && !this.saveMessage()) {
        return;
      }

      this.hideCallback = hideCallback;

      var urlToEdit = variant.urlToEdit;
      if (urlToEdit) {
        this.Frame.set("sourceUrl", urlToEdit);
        this.DialogWindow.show();
      }
    },

    onDialogOk: function () {
      this.experienceEditorContext = this.getExperienceEditorContext();
      if (this.experienceEditorContext && this.experienceEditorContext.isModified) {
        this.experienceEditorContext.instance.disableRedirection = true;
        this.experienceEditorContext.instance.executeCommand("Save");

        this.experienceEditor = this.getExperienceEditor();

        // wait for content saving
        this.experienceEditor.Common.addOneTimeEvent(_.bind(function () {
          return this.experienceEditorContext.isContentSaved;
        }, this), _.bind(function () {
          this.DialogWindow.hide();
          this.disableEditMode();
        }, this), 100, this);
      } else {
        this.DialogWindow.hide();
        this.disableEditMode();
      }
    },

    onDialogCancel: function () {
      this.experienceEditorContext = this.getExperienceEditorContext();
      if (this.experienceEditorContext) {
        this.getExperienceEditor().modifiedHandling(true, _.bind(function (isOkButtonPressed) {
          if (!isOkButtonPressed) {
            this.experienceEditorContext.isModified = false;
          }
          this.DialogWindow.hide();
          this.disableEditMode();
        }, this));
      }
    },

    //Todo: This is the workaround to set the Experience Editor to edit mode. Remove it in Sitecore 8.o update 1. 
    setExperienceEditorEditMode: function () {
      var setEditMode = setInterval(_.bind(function () {
        var contentWindow = this.Frame.viewModel.$el[0].contentWindow;
        if (contentWindow.Sitecore && contentWindow.Sitecore.PageModes && contentWindow.Sitecore.PageModes.PageEditor) {
          contentWindow.Sitecore.PageModes.PageEditor.changeCapability("edit", true);
          clearInterval(setEditMode);
        }
      }, this), 1000);
    },

    saveMessage: function() {
      var args = { Verified: false };
      sitecore.trigger("message:save", args);
      return args.Verified;
    }
  });
});