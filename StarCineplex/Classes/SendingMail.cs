using StarCineplex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace StarCineplex.Classes
{
    public class SendingMail
    {
        private MailModel model;
        public SendingMail(MailModel model)
        {
            this.model = model;
        }
        public async Task<bool> sendEmail()
        {
            var success = false;
            var body = "<p>From: {0} ({1})</p></br><p>Date: {2}</p><p>Message: {3}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress("rahim.prsf@gmail.com"));  // replace with valid value 
            message.From = new MailAddress(model.Email);  // replace with valid value
            message.Subject = model.Subject;
            message.Body = string.Format(body, model.Name, model.Email, model.Date, model.Feed);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "rahim.prsf@gmail.com",  // replace with valid value
                    Password = "Me.rahim29"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
                success = true;
            }
            return success;
        }
    }
}