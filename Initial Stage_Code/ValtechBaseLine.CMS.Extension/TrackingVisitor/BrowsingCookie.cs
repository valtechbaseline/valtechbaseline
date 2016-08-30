using System.Web;
using Sitecore.Data.Items;
using Sitecore.Pipelines.HttpRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using ValtechBaseLine.Model.Common;
using ValtechBaseLine.Common;

namespace ValtechBaseLine.CMS.Extension.TrackingVisitor
{
    public class BrowsingCookie : IDisposable
    {
        private bool disposed = false;

        Dictionary<string, string> _dictionary = new Dictionary<string, string>();
        public const string ValtechUserBrowserInfoCookie = Constants.ValtechUserBrowserInfoCookie;// "valtechuserbroswerinfo";


        // Use C# destructor syntax for finalization code.
        ~BrowsingCookie()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

        public string GetBrowserCookie()
        {
            return ValtechUserBrowserInfoCookie;
        }

        public void SavedDataIntoCookie(string itemId)
        {

            var dic = GetMultipleUsingSingleKeyCookies(ValtechUserBrowserInfoCookie);
            DeleteCookie();
            HttpCookie cookie = new HttpCookie(ValtechUserBrowserInfoCookie);
            if ((_dictionary != null && cookie != null) && !_dictionary.ContainsKey(System.DateTime.Now.ToString()))
            {
                _dictionary.Add(System.DateTime.Now.ToString(), itemId);
                //This adds multiple cookie in the same key.
                foreach (KeyValuePair<string, string> val in dic)
                {
                    cookie[val.Key] = val.Value;
                }
                cookie.Expires = DateTime.Now.Add(TimeSpan.FromHours(20000));
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        //Implement IDisposable.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                }
                // Free your own state (unmanaged objects).
                // Set large fields to null.
                disposed = true;
            }
        }

        public void DeleteCookie()
        {
            HttpCookie cookie = new HttpCookie(ValtechUserBrowserInfoCookie);
            cookie.Expires = DateTime.Now.AddDays(-1d);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        //Getting multiple values from single cookie
        public Dictionary<string, string> GetMultipleUsingSingleKeyCookies(string cookieName)
        {

            //creating dic to return as collection.
            Dictionary<string, string> dicVal = new Dictionary<string, string>();
            //Check whether the cookie available or not.
            if (HttpContext.Current.Request.Cookies[cookieName] != null)
            {

                //Creating a cookie.
                HttpCookie cok = HttpContext.Current.Request.Cookies[cookieName];

                if (cok != null)
                {
                    //Getting multiple values from single cookie.
                    NameValueCollection nameValueCollection = cok.Values;

                    //Iterate the unique keys.
                    foreach (string key in nameValueCollection.AllKeys)
                    {
                        _dictionary.Add(key, cok[key]);
                    }
                }
            }
            return _dictionary;
        }



        public TrackUserModelCollection GetBrowserCookieValue()
        {
            var trackingcollection = new TrackUserModelCollection();
            using (var browsingCookie = new BrowsingCookie())
            {
                var nlsit =
                    browsingCookie.GetMultipleUsingSingleKeyCookies(browsingCookie.GetBrowserCookie())
                        .GroupBy(x => x.Value)
                        .Select(y => y.First());

                foreach (var s in nlsit)
                {
                    Sitecore.Data.Items.Item item = Sitecore.Context.Database.GetItem(s.Value);
                    var userPageViewHistory = new TrackUserModel();
                    if (item != null)
                    {
                        var imageField = ((Sitecore.Data.Fields.FileField)item.Fields["Image"]);
                        var backgroundimgField = ((Sitecore.Data.Fields.FileField)item.Fields["Background Image"]);
                        if (imageField != null && imageField.MediaItem != null)
                        {
                            userPageViewHistory.PageImageUrl = Sitecore.Resources.Media.MediaManager.GetMediaUrl(imageField.MediaItem);
                        }
                        else if (backgroundimgField != null && backgroundimgField.MediaItem != null)
                        {
                            userPageViewHistory.PageImageUrl = Sitecore.Resources.Media.MediaManager.GetMediaUrl(backgroundimgField.MediaItem);
                        }
                        else
                        {
                            var defaultImage = Sitecore.Context.Database.GetItem(Constants.DefaultImage);
                            var mediaItem = new MediaItem(defaultImage);
                            userPageViewHistory.PageImageUrl = Sitecore.Resources.Media.MediaManager.GetMediaUrl(mediaItem);
                        }
                        string url = Sitecore.Links.LinkManager.GetItemUrl(item);
                        userPageViewHistory.ItemUrl = url;
                        userPageViewHistory.Headline = item.Fields["Headline"] != null ? item.Fields["Headline"].Value : "";
                        userPageViewHistory.ShortDescription = item.Fields["Short Description"] != null ? item.Fields["Short Description"].Value : "";
                        trackingcollection.TrackUserList.Add(userPageViewHistory);


                    }
                }

            }

            return trackingcollection;
        }

    }
}
