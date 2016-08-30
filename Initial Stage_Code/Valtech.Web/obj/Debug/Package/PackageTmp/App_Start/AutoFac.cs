using Sitecore.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ValtechBaseLine.Bootstrapper;

namespace ValtechBaseLine.Web.App_Start
{
    public class AutoFac
    {
        public void Process(PipelineArgs args)
        {
            IoC.Initialize();
        }
    }
}