using Newtonsoft.Json;
using StarCineplex.Classes;
using StarCineplex.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace StarCineplex.Controllers
{
    public class UpcomingController : ApiController
    {
        public HttpResponseMessage GetMovies()
        {
            Movies movies = new Movies(Constants.COMING_SOON_PATTERN);
            List<MovieModel> movieList = movies.getMovieList();
            var content = JsonConvert.SerializeObject(movieList);
            var response = new HttpResponseMessage() { Content = new StringContent(content) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }
    }
}
