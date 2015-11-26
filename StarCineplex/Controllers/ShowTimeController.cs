using Newtonsoft.Json;
using StarCineplex.Classes;
using StarCineplex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace StarCineplex.Controllers
{
    public class ShowTimeController : ApiController
    {
        public HttpResponseMessage GetShowTime()
        {
            var showTimeHelper = new ShowTimeHelper();
            List<ShowTime> movieList = showTimeHelper.GetShowTime();
            var content = JsonConvert.SerializeObject(movieList);
            var response = new HttpResponseMessage() { Content = new StringContent(content) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;

        }
    }
}
