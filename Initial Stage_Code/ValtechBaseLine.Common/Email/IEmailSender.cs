using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValtechBaseLine.Common
{
    public interface IEmailSender
    {
         EmailDTO Send(EmailDTO emailDTO);
    }
}
