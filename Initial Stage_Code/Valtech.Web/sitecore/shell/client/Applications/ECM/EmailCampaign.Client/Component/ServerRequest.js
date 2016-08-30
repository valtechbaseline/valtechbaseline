define([
  "sitecore"
], function (
  sitecore
  ) {
  $(document)
    .ajaxError(function(e, jqXHR) {
      switch (jqXHR.status) {
        case 500:
          sitecore.trigger('ajax:error', jqXHR);
          break;
        case 403:
          console.error("Not logged in, will reload page");
          window.top.location.reload(true);
          break;
      }
    });

  var ajaxDefaults = {
      type: "POST",
      async: true
    },
    ajaxUrlPrefix = '/sitecore/api/ssc/';

  function serverRequest(name, params) {
    if ($.type(name) === 'object') {
      params = name;
      name = null;
    }
    params = _.extend(_.clone(ajaxDefaults), params && $.type(params) === "object" ? params : {});

    if (name) {
      params.url = ajaxUrlPrefix + name;
    }
    
    $.ajax(params);
  }

  // TODO: re-factor all usages of postServerRequest to use serverRequest instead, and remove postServerRequest
  function postServerRequest(name, context, handler, async) {
    serverRequest(name, {
      data: context,
      success: handler,
      async: async != undefined ? async : true
    });
  }

  // Still need to make postServerRequest backward compatible
  window.postServerRequest = postServerRequest;
  return serverRequest;
});
