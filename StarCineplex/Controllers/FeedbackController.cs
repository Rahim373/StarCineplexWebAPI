using StarCineplex.Classes;
using StarCineplex.Models;
using System.Web.Http;

namespace StarCineplex.Controllers
{
    public class FeedbackController : ApiController
    {

        public async void postFeedback(MailModel model)
        {
            MailModel mail = new MailModel();
            mail.Name = model.Name;
            mail.Email = model.Email;
            mail.Feed = model.Feed;
            mail.Subject = model.Subject;
            mail.Date = model.Date;

            SendingMail sendingMail = new SendingMail(mail);
            await sendingMail.sendEmail();
        }
    }
}
