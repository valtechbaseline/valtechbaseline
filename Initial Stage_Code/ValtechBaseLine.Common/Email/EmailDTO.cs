using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ValtechBaseLine.Common
{
    public class EmailDTO
    {
      

        public int ID { get; set; }
        public Guid SCEventID { get; set; }
        public string From { get; set; }
        public IEnumerable<string> To { get; set; }
        public IEnumerable<string> Cc { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime? SentDate { get; set; }
        public bool IsSent { get; set; }
        public string FailureMessage { get; set; }
        public bool IsHtmlMessage { get; set; }
        //public List<HttpPostedFileBase> Attachment { get; set; }
        public string FileName { get; set; }
        public Stream FileStream { get; set; }
        //public string SMTPServer { get; set; }
    }
}
