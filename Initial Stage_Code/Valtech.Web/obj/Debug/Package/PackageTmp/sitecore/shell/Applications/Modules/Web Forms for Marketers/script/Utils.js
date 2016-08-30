window.Sitecore = window.Sitecore || {};
window.Sitecore.Wfm = window.Sitecore.Wfm || {};

window.Sitecore.Wfm.Utils = new function() {
  this.IsFF = function() {
    var agt = navigator.userAgent.toLowerCase();
    return (agt.indexOf("firefox") != -1);
  };

  this.IsIE = function() {
    var agt = navigator.userAgent.toLowerCase();
    return ((agt.indexOf("msie") != -1) && (agt.indexOf("opera") == -1));
  };

  this.zoom = function(ctr) {
    var x = (document.body.scrollWidth - 40) / ctr.clientWidth;

    ctr.style.zoom = x + "";

    $(ctr).select('input').each(function(element) {
      element.disabled = true;
    });
  };

  this.select = function(obj, id) {
    if (event.srcElement != null && event.srcElement.tagName == "A") {
      var ctr = $(id);
      if (ctr.checked) {
        ctr.checked = false;
      } else {
        ctr.checked = true;
      }

      event.cancelBubble = true;
    }

    return true;
  };

  this.updateDisabled = function(checkboxControl, area, forbid) {
    if (!forbid) {
      $(area).disabled = !(checkboxControl.checked);

      $$('#' + area + ' input').each(function(element) {
        element.disabled = $(area).disabled;
      });
    }
  };

  this.updateChecked = function(checkboxControl, value) {
    if (!value) {
      checkboxControl.checked = value;
    }
  };

  /* Obsolete */
  window.IsFF = this.IsFF;
  window.IsIE = this.IsIE;
  window.zoom = this.zoom;
  window.select = this.select;
  window.updateDisabled = this.updateDisabled;
  window.updateChecked = this.updateChecked;

  /* Obsolete */
  window.alignButtons = function(buttons) {
    var list = $A(buttons);
    var maxWidth = 0;
    list.each(function(item) {
      if (item != null) {
        item.style.width = '75px';
      }
    });

    list.each(function(item) {
      if (item != null) {
        var width = item.offsetWidth;
        maxWidth = maxWidth < width ? width : maxWidth;
      }
    });

    list.each(function(item) {
      if (item != null) {
        item.style.width = 'auto';
        var width = item.offsetWidth;
        maxWidth = maxWidth < width ? width : maxWidth;
      }
    });

    if (maxWidth <= 75) {
      list.each(function(item) {
        if (item != null) {
          item.style.width = maxWidth + 'px';
        }
      });
    }
  };

  /* Obsolete */
  window.IsChrome = function() {

    var agt = navigator.userAgent.toLowerCase();
    return (agt.indexOf("chrome") != -1);
  };

  /* Obsolete */
  window.patchStyle = function(e) {
    var element = $(e);

    element.up().style.width = "75px";
    element.up().style.height = "25px";

    element.style.width = "75px";

    element.down().align = "absmiddle";
    element.down().next().style.verticalAlign = "";
  };

  /* Obsolete */
  window.IsCursorInElement = function(element, cursorX, cursorY) {
    var parent = element.parentElement;
    var offset = 0;

    while (parent != null) {
      offset += parent.offsetTop;
      parent = parent.parentElement;
    }

    return (element.clientTop + offset < cursorY && (element.clientTop + offset + element.clientHeight) > cursorY &&
      element.clientLeft < cursorX && (element.clientLeft + element.clientWidth) > cursorX);
  };

  /* Obsolete */
  window.getOnlyValue = function(object) {
    for (var attribute in object) {
      if (typeof object[attribute] != 'string') {
        object[attribute] = "";
      }
    }

    return object;
  };

  /* Obsolete */
  window.disableIsEmpty = function(sender, obj) {
    if (sender.getValue() == null || sender.getValue() == "") {
      $(obj).disabled = true;
    } else {
      $(obj).disabled = false;
    }
  };

  /* Obsolete */
  window.clear = function(value, oldChar, newChar) {
    var _clear = "";
    for (var i = 0; i < value.length; ++i) {
      if (value.charAt(i) == oldChar) {
        _clear += newChar;
      } else {
        _clear += value.charAt(i);
      }
    }
    return _clear;
  };

  /* Obsolete */
  window.OpenFormsEditorFromWebEdit = function(id) {
    var frame = $("scWebEditRibbon");
    var url = "http://" + location.hostname + "/default.aspx";
    var attribute = "?" + location.search + "&xmlcontrol=Forms.FormDesigner&formid=" + id;
    frame.src = url + attribute;
  };

  /* Obsolete */
  window.unescapeArray = function(value, separator) {
    var array = value.split(separator);
    array.each(function(element) {
      element = unescape(element);
    });

    return array.join(separator);
  };

  /* Obsolete */
  window.escapeArray = function(value, separator) {
    var array = value.split(separator);
    array.each(function(element) {
      element = escape(element);
    });

    return array.join(separator);
  };

  /* Obsolete */
  window.onExpand = function(ctrl, target) {
    var element = ($(target).select("table"))[0];
    if (!$(element).visible()) {
      $(element).show();
      ctrl.firstChild.src = "/sitecore/shell/Applications/Modules/Web Forms for Marketers/images/up.png";
    } else {
      $(element).hide();
      ctrl.firstChild.src = "/sitecore/shell/Applications/Modules/Web Forms for Marketers/images/down.png";
    }
  };

  /* Obsolete */
  window.updateVisibility = function(id, hidden) {
    if (hidden) {
      $(id).style.visibility = "hidden";
    } else {
      $(id).style.visibility = "visible";
    }
  };

  /* Obsolete */
  window.updateDisplay = function(selectControl, id) {
    if (selectControl.selectedIndex == 0) {
      $(id).style.display = "none";
    } else {
      $(id).style.display = "block";
    }
  };

  /* Obsolete */
  window.__nonMSDOMBrowser = (window.navigator.appName.toLowerCase().indexOf('explorer') == -1);
  window.FireDefaultButton = function(event, target) {
    if (event.keyCode == 13 && !(event.srcElement && (event.srcElement.tagName.toLowerCase() == "textarea"))) {
      var defaultButton;
      if (window.__nonMSDOMBrowser) {
        defaultButton = document.getElementById(target);
      } else {
        defaultButton = document.all[target];
      }

      if (defaultButton && typeof (defaultButton.click) != "undefined") {
        defaultButton.click();
        event.cancelBubble = true;
        if (event.stopPropagation) {
          event.stopPropagation();
        }

        return false;
      }
    }

    return true;
  };
};