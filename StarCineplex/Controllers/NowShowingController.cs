using StarCineplex.Classes;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Newtonsoft.Json;
using StarCineplex.Models;

namespace StarCineplex.Controllers
{

    public class NowShowingController : ApiController
    {
        public HttpResponseMessage GetMovies()
        {
            List<Movie>  movieList = NowShowing.GetData();
            var content = JsonConvert.SerializeObject(movieList);
            var response = new HttpResponseMessage() { Content = new StringContent(content) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }
    }
}
