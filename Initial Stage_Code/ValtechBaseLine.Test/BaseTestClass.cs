using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Globalization;
using Sitecore.SecurityModel;

namespace ValtechBaseLine.Test
{
    // In NUnit, this ensures it will run before any/all tests are executed
    [SetUpFixture]
   public class BaseTestClass
    {
        public static SecurityDisabler _disabler;


        [SetUp]
        public void SetupTest()
        {
            try
            {
                // This grounds Sitecore in the current directory so when 
                // Sitecore.IO.FileUtil.MapPath runs, it can find the files.                
                State.HttpRuntime.AppDomainAppPath = Directory.GetCurrentDirectory();

                // This static.  Allows it live, avoiding garbage collection
                _disabler = new SecurityDisabler();


                // If you need to run pipelines do it hear.
                //CorePipeline.Run("initialize", new PipelineArgs());

                // Set any properties you need in content
                Context.SetLanguage(Language.Parse("en"), false);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
