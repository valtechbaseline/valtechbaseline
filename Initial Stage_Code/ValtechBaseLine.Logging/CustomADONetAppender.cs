using log4net.Appender;
using log4net.spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ValtechBaseLine.Logging
{
    class CustomADONetAppender:ADONetAppender
    {
        protected override void Append(LoggingEvent loggingEvent)
        {
            var properties = loggingEvent.Properties;
            properties["addr"] = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();

            base.Append(loggingEvent);
        }
    }
}
