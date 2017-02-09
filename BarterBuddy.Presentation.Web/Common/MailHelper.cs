using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;

namespace BarterBuddy.Presentation.Web.Common
{
    public class MailHelper
    {
        public bool SslEnable { get; set; }

        public string Password { get; set; }

        public string UserId { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public string Sender { get; set; }

        public string Recipient { get; set; }

        public string RecipientCC { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string DisplayName { get; set; }

        public string AttachmentFile { get; set; }

        public MailHelper(string strrecepitant, string strsubject, string strbody)
        {
            UserId = ConfigurationManager.AppSettings["BarterBuddyEmailId"];
            Sender = ConfigurationManager.AppSettings["BarterBuddyEmailId"];
            Port = int.Parse(ConfigurationManager.AppSettings["SMTPServerPort"]);
            Password = ConfigurationManager.AppSettings["BarterBuddyEmailPassword"];
            Host = ConfigurationManager.AppSettings["SMTPServer"];
            SslEnable = true;
            Recipient = strrecepitant;
            Subject = strsubject;
            Body = strbody;
            DisplayName = ConfigurationManager.AppSettings["BargerBuddySenderDisplayName"];
        }

        public void Send()
        {
            Attachment att = null;
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            try
            {
                MailAddress address = new MailAddress(Sender, DisplayName);
                MailAddress to = new MailAddress(Recipient);
                message = new MailMessage(address, to);
                message.Body = Body;
                message.Subject = Subject;
                message.IsBodyHtml = true;

                if (!string.IsNullOrEmpty(RecipientCC))
                {
                    message.Bcc.Add(RecipientCC);
                }

                var inlineLogo = new LinkedResource(HostingEnvironment.MapPath("~/Images/logo.png"));
                inlineLogo.ContentId = Guid.NewGuid().ToString();

                string body = string.Format(@"<div border-bottom: #29c5ff solid 5px; text-align: left; color: #8b8d8d; padding: 14px 0><img src=""cid:{0}"" /></div>", inlineLogo.ContentId);
                Body = body + "<br/><br/>" + message.Body;
                var view = AlternateView.CreateAlternateViewFromString(Body, null, "text/html");
                view.LinkedResources.Add(inlineLogo);
                message.AlternateViews.Add(view);

                smtp = new SmtpClient(Host, 587);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(UserId, Password);
                smtp.EnableSsl = SslEnable;
                smtp.Send(message);
            }
            catch(Exception ex)
            {
                return;
            }
            finally
            {
                att?.Dispose();
                message.Dispose();
                smtp.Dispose();
            }
        }
    }
}