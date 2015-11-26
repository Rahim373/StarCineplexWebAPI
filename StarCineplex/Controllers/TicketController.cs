using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using StarCineplex.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace StarCineplex.Controllers
{
    public class TicketController : ApiController
    {
        public HttpResponseMessage GetTicket()
        {
            List<Ticket> shows = getTicketPrice();


            var content = JsonConvert.SerializeObject(shows);
            var response = new HttpResponseMessage() { Content = new StringContent(content) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;

        }


        private List<Ticket> getTicketPrice()
        {
            List<Ticket> shows = new List<Ticket>();

            // Public 2D
            shows.Add(new Ticket { Slot = "Matinee", Showtype = "2D", Venue = "Hall#1 - Hall#4", Type = "Regular", Price = 200, Days = "All", Comment = "Shows starting before 3:00pm except holidays"});
            shows.Add(new Ticket { Slot = "Matinee", Showtype = "2D", Venue = "Hall#1 - Hall#4", Type = "Premium", Price = 250, Days = "All", Comment = "Shows starting before 3:00pm except holidays"});
            shows.Add(new Ticket { Slot = "Evening", Showtype = "2D", Venue = "Hall#1 - Hall#4", Type = "Regular", Price = 250, Days = "All", Comment = "Shows starting at or after 3:00pm except holidays" });
            shows.Add(new Ticket { Slot = "Evening", Showtype = "2D", Venue = "Hall#1 - Hall#4", Type = "Premium", Price = 300, Days = "All", Comment = "Shows starting at or after 3:00pm except holidays" });
            // Public 3d
            shows.Add(new Ticket { Slot = "No", Showtype = "3D", Venue = "Hall#1 - Hall#4", Type = "Regular", Price = 350, Days = "Friday, Saturday, Holiday", Comment = "N/A" });
            shows.Add(new Ticket { Slot = "No", Showtype = "3D", Venue = "Hall#1 - Hall#4", Type = "Premium", Price = 400, Days = "Friday, Saturday, Holiday", Comment = "N/A" });
            shows.Add(new Ticket { Slot = "Matinee", Showtype = "3D", Venue = "Hall#1 - Hall#4", Type = "Regular", Price = 300, Days = "Sunday - Thursday", Comment = "Shows starting before 3:00pm except holidays" });
            shows.Add(new Ticket { Slot = "Matinee", Showtype = "3D", Venue = "Hall#1 - Hall#4", Type = "Premium", Price = 350, Days = "Sunday - Thursday", Comment = "Shows starting before 3:00pm except holidays" });
            // VIP 
            shows.Add(new Ticket { Slot = "Matinee", Showtype = "2D", Venue = "Hall#5", Type = "VIP", Price = 500, Days = "All", Comment = "Shows starting before 3:00pm except holidays" });
            shows.Add(new Ticket { Slot = "Evening", Showtype = "2D", Venue = "Hall#5", Type = "VIP", Price = 700, Days = "All", Comment = "Shows starting at or after 3:00pm except holidays" });
            shows.Add(new Ticket { Slot = "Matinee", Showtype = "3D", Venue = "Hall#5", Type = "VIP", Price = 600, Days = "All", Comment = "Shows starting before 3:00pm except holidays" });
            shows.Add(new Ticket { Slot = "Evening", Showtype = "3D", Venue = "Hall#5", Type = "VIP", Price = 800, Days = "All", Comment = "Shows starting at or after 3:00pm except holidays" });
            // Premium 
            shows.Add(new Ticket { Slot = "Matinee", Showtype = "2D", Venue = "Hall#6", Type = "Premium", Price = 350, Days = "All", Comment = "Shows starting before 3:00pm except holidays" });
            shows.Add(new Ticket { Slot = "Evening", Showtype = "2D", Venue = "Hall#6", Type = "Premium", Price = 450, Days = "All", Comment = "Shows starting at or after 3:00pm except holidays" });
            shows.Add(new Ticket { Slot = "Matinee", Showtype = "3D", Venue = "Hall#6", Type = "Premium", Price = 400, Days = "All", Comment = "Shows starting before 3:00pm except holidays" });
            shows.Add(new Ticket { Slot = "Evening", Showtype = "2D", Venue = "Hall#6", Type = "Premium", Price = 500, Days = "All", Comment = "Shows starting at or after 3:00pm except holidays" });

            return shows;
        }
    }
}
