define(["sitecore", "/-/speak/v1/ecm/DialogBase.js"], function (sitecore, DialogBase) {
  return DialogBase.extend({
    initialized: function () {
      this._super();
      this.defaults.text = this.Text.get("text");
      this.defaults.showIcon = true;
    },
    
    update: function() {
      this.Text.set("text", this.options.text);
      this.Icon.set("isVisible", this.options.showIcon);
      this._super();
    },

    resetDefaults: function () {
      this.Text.set("text", this.defaults.text);
      this.Icon.set("isVisible", this.defaults.showIcon);
      this._super();
    }
  });
});