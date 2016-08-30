using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.IO;
using Sitecore.Shell.Framework;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.WebControls;
using Text = Sitecore.Shell.Web.UI.WebControls.Text;

namespace ValtechBaseLine.CMS.Extension.SheerUI
{
    public class EmailSettings : DialogForm
    {
        protected DataContext DialogFolderDataContext;
        protected TreeviewEx DialogContentItems;
        protected Listbox ListboxExample;
        // Fields  
        protected Edit TextEdit;

        protected override void OnLoad(EventArgs e)
        {
            Assert.ArgumentNotNull(e, "e");
            base.OnLoad(e);

            if (!Context.ClientPage.IsEvent)
            {
                Inialize();
            }
        }

        private void Inialize()
        {
            SetMode();
            SetDialogFolderDataContextFromQueryString();
        }

        private void SetMode()
        {
            Mode = WebUtil.GetQueryString("mo");
        }

        private void SetDialogFolderDataContextFromQueryString()
        {
            DialogFolderDataContext.GetFromQueryString();
        }

        protected override void OnCancel(object sender, EventArgs args)
        {
            Assert.ArgumentNotNull(sender, "sender");
            Assert.ArgumentNotNull(args, "args");
            if (this.Mode == "webedit")
            {
                base.OnCancel(sender, args);
            }
            else
            {
                SheerResponse.Eval("scCancel()");
            }
        }

        protected void ItemSelected()
        {
            Item item = DialogContentItems.GetSelectionItem();
            if (item != null)
            {
                string fieldValue = item["Key"] == null ? "" : item["Key"];
                if (!string.IsNullOrEmpty(fieldValue))
                {
                    ListItem listItem = new ListItem();
                    ListItem control = new ListItem();
                    control.Value = fieldValue;
                    control.Header = fieldValue;

                    ListboxExample.Controls.AddAt(ListboxExample.Items.Count(), control);

                    SheerResponse.Refresh((Sitecore.Web.UI.HtmlControls.Control)ListboxExample);
                    SetModified();
                }
            }

        }

        protected void OnSelectedItem()
        {
            Item item = DialogContentItems.GetSelectionItem();
            if (item != null)
            {
                string fieldValue = item["TokenValue"] == null ? "" : item["TokenValue"];
                if (!string.IsNullOrEmpty(fieldValue))
                {
                    ListItem listItem = new ListItem();
                    ListItem control = new ListItem();
                    control.Value = fieldValue;
                    control.Header = fieldValue;

                    ListboxExample.Controls.AddAt(ListboxExample.Items.Count(), control);

                    SheerResponse.Refresh((Sitecore.Web.UI.HtmlControls.Control)ListboxExample);
                    SetModified();
                }
            }
        }

        protected static void SetModified()
        {
            Sitecore.Context.ClientPage.Modified = true;
        }

        protected override void OnOK(object sender, EventArgs args)
        {
            Assert.ArgumentNotNull(sender, "sender");
            Assert.ArgumentNotNull(args, "args");
            StringBuilder builder = new StringBuilder();
            if (ListboxExample.Items.Count() == 0)
            {
                SheerResponse.Alert("Please select some tokens");
            }
            foreach (var itm in ListboxExample.Items)
            {
                builder.Append(itm.Header);
            }
            if (this.Mode == "webedit")
            {
                //SheerResponse.SetDialogValue(StringUtil.EscapeJavascriptString(mediaUrl));  
                base.OnOK(sender, args);
            }
            else
            {
                SheerResponse.Eval("scClose('" + builder.ToString() + "')");
            }
        }

        // Properties  
        protected string Mode
        {
            get
            {
                string str = StringUtil.GetString(base.ServerProperties["Mode"]);
                if (!string.IsNullOrEmpty(str))
                {
                    return str;
                }
                return "shell";
            }
            set
            {
                Assert.ArgumentNotNull(value, "value");
                base.ServerProperties["Mode"] = value;
            }
        }

    }
}