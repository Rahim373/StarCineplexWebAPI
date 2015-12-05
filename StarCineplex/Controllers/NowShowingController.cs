using StarCineplex.Classes;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Newtonsoft.Json;
using StarCineplex.Models;
using System.Web.Mvc;
using System.Web.UI;
using System.Text;

namespace StarCineplex.Controllers
{

    public class NowShowingController : ApiController
    {
        public HttpResponseMessage GetMovies()
        {
            Movies movies = new Movies(Constants.NOW_SHOWING_PATTERN);
            List<MovieModel> movieList = movies.getMovieList();
            var content = JsonConvert.SerializeObject(movieList);
            var response = new HttpResponseMessage() { Content = new StringContent(content)};
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
           
            return response;
        }
    }
}
