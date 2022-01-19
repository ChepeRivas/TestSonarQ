using PerfectWorld.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWorld.Data.Helpers
{
    public class SendMailClass : ISendMail
    {
        private readonly MailAddress fromAddress;
        private readonly string fromPassword = "fromPassword";
        private readonly string subject = "Subject";
        private readonly string body = "Body";
        public SendMailClass()
        {

        }

        public SendMailClass(string Mail, string Password, string BuildBody, string Subject)
        {
            this.fromAddress = new MailAddress(Mail, "GM Hera");

            this.fromPassword = Password;
            this.body = BuildBody;
            this.subject = Subject;
        }


        public async Task<int> Send(string To)
        {

            return await InvoqueMail(To);
        }
        public async Task<int> Send(string To, string Addbody)
        {

            return await InvoqueMail(To, Addbody);
        }
        public async Task<int> Send(string To, string Addbody, string AddSubject)
        {

            return await InvoqueMail(To, Addbody, AddSubject);
        }


        private async Task<int> InvoqueMail(string To, string Addbody = "", string AddSubject = "")
        {
            try
            {
                var toAddress = new MailAddress(To, To);

                var smtp = new SmtpClient
                {
                    Host = "smtp.office365.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    TargetName = "STARTTLS/smtp.office365.com",
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address.Trim(), fromPassword.Trim())
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = true,
                    Subject = AddSubject == "" ? subject : AddSubject,
                    Body = Addbody == "" ? body : body + Addbody
                })
                {
                    smtp.Send(message);
                }
                return 1;
            }
            catch (Exception ex)
            {

                return 0;
            }

        }
    }
}
