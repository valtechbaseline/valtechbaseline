using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ValtechBaseLine.Test
{
    [TestFixture]
    public class SimpleTest
    {
        [Test]
        public void ShouldFindTheHomeNode()
        {
            // Get getfishtank is declared under:
            // /App_Config/Include/SiteDefinition.getfishtank.ca.config
            Sitecore.Context.SetActiveSite("website");

            // Pull the start path of the site
            string startPath = Sitecore.Context.Site.StartPath;

            // Pull the database name
            string databaseName = Sitecore.Context.Site.Database.Name;

            // Load the web database, and get item
            var db = Sitecore.Data.Database.GetDatabase("web");
            var item = db.GetItem("/sitecore/content/home");


            // Found the home node
            Assert.That(item, Is.Not.Null);

            // Paths of the home items match
            Assert.That(startPath.ToLower(), Is.EqualTo(item.Paths.FullPath.ToLower()));

            // Database name pulled from context matches too 
            Assert.That(databaseName, Is.EqualTo(db.Name));
        }
    }
}
