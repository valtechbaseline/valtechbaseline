using Sitecore.Data.Items;
using Sitecore.Pipelines;
using Sitecore.Pipelines.GetTranslation;
using Sitecore.Pipelines.RenderField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Web.UI.WebControls;

namespace ValtechBaseLine.Web.Helpers
{
    public class RenderEditable
    {
        public void Process(GetTranslationArgs args)
        {
            var item = args.CustomData["item"] as Item;
            if (item == null)
            {
                return;
            }

            args.Result = MakePhraseEditable(item);
        }

        private string MakePhraseEditable(Item item)
        {
            string val = string.Empty;
            var args = new RenderFieldArgs()
            {
                FieldName = "Phrase",
                Item = item
            };

            // CorePipeline.Run("renderField", args);
            if (item != null)
            {
                val = FieldRenderer.Render(item, "Phrase");
            }
            return val.ToString();
        }

    }
}