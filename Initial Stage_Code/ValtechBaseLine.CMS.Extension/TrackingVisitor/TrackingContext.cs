using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Sitecore.Pipelines.HttpRequest;
using Sitecore;

namespace ValtechBaseLine.CMS.Extension.TrackingVisitor
{
    public class TrackingContext : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            // THIS WILL FAIL:
            var somevar = HttpContext.Current.Session;
            if (Sitecore.Context.Database != null && Sitecore.Context.Database.Name == "master" && args.Url.ItemPath.Length > 0 && Context.Item != null)
            {
                using(var bcookie = new BrowsingCookie())
                {
                    if(Context.Item!=null)
                    bcookie.SavedDataIntoCookie(Context.Item.ID.ToString());
                }
               
            }
        }
    }
}
