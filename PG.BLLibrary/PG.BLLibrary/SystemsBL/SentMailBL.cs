using PG.DBClass.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using MailMessage = System.Net.Mail.MailMessage;
namespace PG.BLLibrary.SystemsBL
{
    public class SentMailBL
    {
        public static string SendNewMailFromSystem(MailMaster mailInfo)
        {
            string msg = string.Empty;
            try
            {
                string fromMail = mailInfo.From;
                string fromMailPassWord = mailInfo.Password;
                mailInfo.To = mailInfo.To;
                using (MailMessage mm = new MailMessage(fromMail, mailInfo.To))
                {                 
                    mm.Subject = mailInfo.Subject;
                    mm.IsBodyHtml = true;
                    mm.Body = string.Format(mailInfo.Body);
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = mailInfo.HostMailAddress;
                    
                    smtp.EnableSsl = true;
                    
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    NetworkCredential NetworkCred = new NetworkCredential(fromMail,
                        fromMailPassWord);
                    //smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                   // smtp.p
                    //smtp.Port = 25;
                    smtp.Send(mm);

                }

            }
            catch
            {
                msg = "Mail Sent Failed For Mail Id:" + mailInfo.To;
              
            }
            return msg;

        }
    }
}
