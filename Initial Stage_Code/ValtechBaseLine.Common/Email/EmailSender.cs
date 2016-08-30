using System.Linq;
using System;
using System.Net.Mail;

namespace ValtechBaseLine.Common
{
    public class EmailSender : IEmailSender
    {
       
        public EmailSender()
        {
            
        }
     

        public EmailDTO Send(EmailDTO emailDTO)
        {
            if (string.IsNullOrEmpty(emailDTO.From) || emailDTO.To == null || string.IsNullOrEmpty(emailDTO.Subject) ||
                string.IsNullOrEmpty(emailDTO.Message))
                return emailDTO;

            var msg = GetMailMessage(emailDTO);

            try
            {
                using (var smtpClient = new SmtpClient{DeliveryMethod = SmtpDeliveryMethod.Network})
                {
                    smtpClient.Send(msg);
                    emailDTO.IsSent = true;
                    emailDTO.SentDate = DateTime.Now.ToUniversalTime();
                }
            }
            catch (Exception ex)
            {
                emailDTO.IsSent = false;
                emailDTO.FailureMessage = ex.InnerException.Message;
            }
            finally
            {
               

            }
            return emailDTO;
        }

        private MailMessage GetMailMessage(EmailDTO emailDTO)
        {
            var msg = new MailMessage();
            foreach (var email in emailDTO.To)
            {
                msg.To.Add(new MailAddress(email));
            }

            if (emailDTO.Cc != null)
            {
                foreach (var email in emailDTO.Cc)
                {
                    msg.CC.Add(new MailAddress(email));
                }
            }

            msg.Subject = emailDTO.Subject;
            msg.From = new MailAddress(emailDTO.From);
            msg.Body = emailDTO.Message;
            msg.IsBodyHtml = emailDTO.IsHtmlMessage;
            if (!string.IsNullOrEmpty(emailDTO.FileName))
            {
                msg.Attachments.Add(new Attachment(emailDTO.FileStream, emailDTO.FileName));
            }
            //if (emailDTO.Attachment != null && emailDTO.Attachment.Count > 0)
            //{
            //    foreach (
            //        var attachment in
            //            emailDTO.Attachment.Select(file => new Attachment(file.InputStream, file.FileName)))
            //        msg.Attachments.Add(attachment);
            //}
            return msg;
        }

       
    }
}
