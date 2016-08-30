using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Analytics;
using Sitecore.Analytics.Data;
using Sitecore.Analytics.Core;
using Sitecore.Analytics.Tracking;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace ValtechBaseLine.Web
{
    public partial class TrackUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("<br/>ContactId:" + Tracker.Current.Contact.ContactId);
                int VisistCount = Tracker.Current.Contact.System.VisitCount;
                builder.Append("<br/>Visit:" + VisistCount);
                ltpath.Text = builder.ToString();
            }
        }


        protected void btnGetSummary_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            int VisistCount = Tracker.Current.Contact.System.VisitCount;
            builder.Append("ContactId:" + Tracker.Current.Contact.ContactId);
            if (VisistCount == 1)
            {
                builder.Append("<br/>Visit:" + VisistCount);
                builder.Append("<br/>Welcome New Users.<br/>");
            }
            else if (VisistCount > 1)
            {
                var items = GetItemOfContact(Tracker.Current.Contact.ContactId.ToString());
                builder.Append("<br/>Yours Previously Page Visited:<br/>");

                foreach (string s in items)
                {
                    builder.Append(s + "<br/>");
                }
                // SITECORE API TO FETCH DATA FROM ANALYTIC -- NOT WORKING
                //var contactGuid = Guid.Parse(Tracker.Current.Contact.ContactId.ToString());
                //var manager = Sitecore.Configuration.Factory.CreateObject("tracking/contactManager", true) as Sitecore.Analytics.Tracking.ContactManager;
                //var contact = manager.LoadContactReadOnly(contactGuid);
                //IEnumerable<Sitecore.Analytics.Tracking.IInteractionData> data = contact.LoadHistorycalData(1);
                builder.Append("<br/>Visit:" + VisistCount);

            }
            ltpath.Text = builder.ToString();
        }

        protected void btnSession_Click(object sender, EventArgs e)
        {
            var tracker = Sitecore.Analytics.Tracker.Current;
            var manager = Sitecore.Configuration.Factory.CreateObject("tracking/contactManager", true) as Sitecore.Analytics.Tracking.ContactManager;
            manager.FlushContactToXdb(tracker.Contact);
            manager.SaveAndReleaseContact(tracker.Contact);
            var ctxManager = Sitecore.Configuration.Factory.CreateObject("tracking/sessionContextManager", true) as Sitecore.Analytics.Data.SessionContextManagerBase;
            ctxManager.Submit(tracker.Session);

            Session.Abandon();
        }

        public List<string> GetItemOfContact(string contactId)
        {
            string analyticDB = ConfigurationManager.ConnectionStrings["reporting"].ToString();
            List<string> items = new List<string>();
            string contextDB = ConfigurationManager.ConnectionStrings["web"].ToString();
            SqlConnection context = new SqlConnection(contextDB);

            using (SqlConnection connection =
                new SqlConnection(analyticDB))
            {
                connection.Open();

                string cmd =
                    @"select [" + context.Database + "].dbo.Items.ID from [" + context.Database + "].dbo.Items where [" + context.Database + "].dbo.Items.ID " +
                    @"In(select distinct(ItemId) from [" + connection.Database + "].dbo.Fact_PageViews where UPPER(ContactId)='" + contactId + "'" +
                     @"And ItemId<>'00000000-0000-0000-0000-000000000000')";
                SqlCommand com = new SqlCommand(cmd); // Create a object of SqlCommand class
                SqlDataReader reader;
                com.Connection = connection;
                reader = com.ExecuteReader();

                while (reader.Read())
                {

                    Sitecore.Data.Items.Item itm = Sitecore.Context.Database.GetItem(reader["ID"].ToString());
                    items.Add(Sitecore.Links.LinkManager.GetItemUrl(itm));
                }
                connection.Close();
            }
            return items;



        }
    }
}