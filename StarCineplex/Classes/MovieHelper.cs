using Newtonsoft.Json;
using StarCineplex.Models;
using System;
using System.Net;

namespace StarCineplex.Classes
{
    public class MovieHelper
    {
        public static Movie getSingleMovie(string movieName)
        {
            int year = DateTime.Now.Year;
            var singleMovie = JsonConvert.DeserializeObject<Movie>(getResult(movieName, year));

            if(singleMovie.Response.ToLower() == "true")
            {
                return singleMovie;
            }

            else
            {
                // Try last five years
                for (int i = 0; i < 5; i++)
                {
                    year--;
                    var singleMovie1 = JsonConvert.DeserializeObject<Movie>(getResult(movieName, year));
                    if (singleMovie1.Response.ToLower() == "true")
                    {
                       return singleMovie1;
                    }
                }
                return  getFromMyDb(movieName);
            }
        }



        // Get only from OMDB database
        public static string getResult(string movieName, int year)
        {
            string result;
            using (var client = new WebClient())
            {
                Uri url = new Uri("http://www.omdbapi.com/?t=" + movieName + "&type=movie&y=" + year + "&plot=short&r=json", UriKind.Absolute);
                result = client.DownloadString(url);
            }
            return result;
        }


        // This code is to get from my created database
        public static Movie getFromMyDb(string title)
        {
            string result;
            using (var client = new WebClient())
            {
                Uri url = new Uri(Constants.MY_DB + title, UriKind.Absolute);
                result = client.DownloadString(url);
            }
            var movie = JsonConvert.DeserializeObject<Movie>(result);
            if(movie.Response == "True")
            {
                return movie;
            }
            return new Movie { Response = "False" };
        }


    }
}