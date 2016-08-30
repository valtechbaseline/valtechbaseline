using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Glass.Mapper.Sc;
using ValtechBaseLine.Common;
using ValtechBaseLine.RepositoryContract.Common;

namespace ValtechBaseLine.Web.Controllers
{
    public class UnSubscribeNewsLetterController : Controller
    {
        private readonly SitecoreContext _sitecoreContext;
        private readonly IAnalyticsContract _analyticsContract;

        public UnSubscribeNewsLetterController(IAnalyticsContract analyticsContract)
        {
            _sitecoreContext = new SitecoreContext();
            _analyticsContract = analyticsContract;
        }

        public ActionResult UnsbcribeNewsLetter(string ec_contact_id, string ec_message_id)
        {
            _analyticsContract.RemovingContactsFromList(Constants.list.NewsLetterSubscriptionList, ec_contact_id, ec_message_id);

            return View("~/Views/Component/UnSubscribeNewsLetterView.cshtml");

        }
    }
}