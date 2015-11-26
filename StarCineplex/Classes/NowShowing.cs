using HtmlAgilityPack;
using Newtonsoft.Json;
using StarCineplex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace StarCineplex.Classes
{
    public static class NowShowing
    {

        public static List<Movie> GetData()
        {
            List<Movie> MovieList = new List<Movie>();

            string cineplexUrl = Constants.NOW_SHOWING;
            var host = HttpContext.Current.Request.Url.Host;

            string htmlPage = "";
            using (var client = new WebClient())
            {
                Uri url = new Uri(cineplexUrl, UriKind.Absolute);
                htmlPage = client.DownloadString(url);
            }
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlPage);


            foreach (HtmlNode singleNode in doc.DocumentNode.SelectSingleNode(@"//*[@id='left_navigation']/div[1]/div[2]").ChildNodes)
            {
                try
                {
                    if (singleNode.HasAttributes)
                    {
                        string showType = PatternHelper.GetMovieType(singleNode.InnerText.Trim());
                        string movieName = PatternHelper.GetFilteredMovieName(singleNode.InnerText.Trim());
                        Movie movie = MovieHelper.getSingleMovie(movieName);
                        if (movie.Country == "India")
                        {
                            movie = MovieHelper.getFromMyDb(movieName);
                        }

                        movie.Showtype = showType;
                        if (!movie.Title.Equals(null) && !movie.Title.Equals("null"))
                        {
                            MovieList.Add(movie);
                        }
                    }
                }
                catch (Exception e)
                {

                }
            }

            MovieList = MovieList.OrderBy(p => p.Title).ToList();
            return MovieList;
        }
    }

   
}