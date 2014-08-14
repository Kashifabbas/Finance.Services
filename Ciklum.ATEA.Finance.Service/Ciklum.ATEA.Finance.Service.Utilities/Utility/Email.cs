using System;
using System.Web;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Ciklum.ATEA.Finance.Service.Utilities.Utilities
{
    public class Email
    {
        #region " SendEmail Function "
        public static bool SendEmail(string to, string from, string subject, string body)
        {
            bool bSuccess = false;
            try
            {
                MailAddress fromEmail = new MailAddress(from);
                MailAddress toEmail = new MailAddress(to);
                MailMessage message = new MailMessage(fromEmail, toEmail);
                message.IsBodyHtml = true;
                message.Subject = subject;
                message.Body = body;
                if (WebConfigurationManager.AppSettings["SMTPAddress"] != null && WebConfigurationManager.AppSettings["SMTPAddress"].Trim().Length > 0)
                {
                    SmtpClient mailClient = new SmtpClient(WebConfigurationManager.AppSettings["SMTPAddress"].Trim());
                    mailClient.Send(message);
                    bSuccess = true;
                }
            }
            catch (Exception ex)
            {
                bSuccess = false;
            }
            return bSuccess;
        }

        #endregion

        #region " SendDoNotReplyEmail Function "
        public static bool SendDoNotReplyEmail(String to, String subject, String body)
        {
            Boolean sendEmail = false;
            bool bSuccess = false;
            if (WebConfigurationManager.AppSettings["SendEmail"] != null && WebConfigurationManager.AppSettings["SendEmail"].ToString() != String.Empty)
            {
                sendEmail = Convert.ToBoolean(WebConfigurationManager.AppSettings["SendEmail"].ToString());
            }
            if (!sendEmail)
            {
                return bSuccess;
            }
            
            try
            {
                MailAddress fromEmail = new MailAddress("do-not-reply@atea.dk");
                MailAddress toEmail = new MailAddress(to);
                MailMessage message = new MailMessage(fromEmail, toEmail);
                message.IsBodyHtml = true;
                message.Subject = subject;
                message.Body = body;
                if (WebConfigurationManager.AppSettings["SMTPAddress"] != null && WebConfigurationManager.AppSettings["SMTPAddress"].Trim().Length > 0)
                {
                    SmtpClient mailClient = new SmtpClient(WebConfigurationManager.AppSettings["SMTPAddress"].Trim());
                    mailClient.Send(message);
                    bSuccess = true;
                }
            }
            catch (Exception ex)
            {
                bSuccess = false;
            }
            return bSuccess;
        }

        #endregion
    }
}
