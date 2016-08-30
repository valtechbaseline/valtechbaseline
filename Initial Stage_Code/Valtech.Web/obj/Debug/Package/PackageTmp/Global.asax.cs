using System;
using System.Web.Routing;
using Castle.Windsor;
using Sitecore.ContentSearch.SolrProvider.CastleWindsorIntegration;

namespace ValtechBaseLine.Web
{
    public class Global : Sitecore.Web.Application
    {
        public IWindsorContainer Container { get; set; }

        protected void Application_Start(object sender, EventArgs e)
        {
            Container = new WindsorContainer();
            new WindsorSolrStartUp(Container).Initialize();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

        }


    }
}