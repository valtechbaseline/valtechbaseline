using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Configuration;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;

namespace ValtechBaseLine.CMS.Extension.Personalization
{
    public class CommonRuleContext<T> : StringOperatorCondition<T> where T : RuleContext 
    {
        public string QueryStringName { get; set; }
        public string QueryStringValue { get; set; }

        protected override bool Execute(T ruleContext)
        {

            if (String.IsNullOrEmpty(QueryStringName))
                return false;

            if (String.IsNullOrEmpty(QueryStringValue))
                return false;

            //if (HttpContext.Current.Request.Cookies[QueryStringName] == null)
            //    return false;

            if (HttpContext.Current.Request.QueryString[QueryStringName] == null
                && HttpContext.Current.Session["PersonalizedParam"] == null)
                return false;


            //var actualVal = HttpContext.Current.Request.Cookies[CookieName].Value;

            var actualVal = HttpContext.Current.Request.QueryString[QueryStringName];
            if (HttpContext.Current.Session["PersonalizedParam"] != null)
            {
                actualVal = HttpContext.Current.Session["PersonalizedParam"].ToString();
            }

            if (String.IsNullOrEmpty(actualVal))
                return false;

            bool result = Compare(QueryStringValue, actualVal);

            if (result)
            {
                HttpContext.Current.Session["PersonalizedParam"] = actualVal;
            }
            return result;
        }
    }
}
