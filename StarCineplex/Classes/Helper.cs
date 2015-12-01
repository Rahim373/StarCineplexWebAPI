using HtmlAgilityPack;
using Newtonsoft.Json;
using StarCineplex.Models;
using System;
using System.Collections.Generic;
using System.Net;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace StarCineplex.Classes
{
    public class Helper
    {
        // Collect and control over movies
        public static MovieModel getSingleMovie(string movieName)
        {
            MovieModel movie = getSingleFromTMDB(movieName);
            if (movie == null) movie = getFromMyDb(movieName);
            return movie;
        }

        // This code is to get Movies from THE Movie Database (http://www.themoviedb.org)
        public static MovieModel getSingleFromTMDB(string title)
        {
            // Year
            int year = DateTime.Now.Year;

            MovieModel expectedMovie = new MovieModel();
            TMDbClient client = new TMDbClient("34c356b1eb1a362f5a3b958a9e94a113");
            SearchContainer<SearchMovie> results = client.SearchMovie(title);

            if(results.Results.Count> 0)
            {
                int id = results.Results[0].Id;
                TMDbLib.Objects.Movies.Movie movie = client.GetMovie(id);
                if (movie.OriginalLanguage.Equals("hi")) return null;
                List<Genre> genres = movie.Genres;
                string genre = "";
                foreach(var item in genres)
                {
                    genre = genre + item.Name + ", ";
                }
                string director = "";

                expectedMovie.Title = movie.Title;
                expectedMovie.Year = movie.ReleaseDate.Value.Year.ToString();
                expectedMovie.Rated = "N/A";
                expectedMovie.Released = movie.ReleaseDate.Value.Date.ToString();
                expectedMovie.Runtime = movie.Runtime.ToString() + " Minutes";
                expectedMovie.Genre = genre;
                expectedMovie.Director = director;
                expectedMovie.Writer = "N/A";
                expectedMovie.Actors = "";
                expectedMovie.Plot = movie.Overview;
                expectedMovie.Language = movie.OriginalLanguage;
                expectedMovie.Country = movie.ProductionCountries[0].Name;
                expectedMovie.Awards = "";
                expectedMovie.Poster = "https://image.tmdb.org/t/p/original/" + movie.PosterPath;
                expectedMovie.Metascore = "";
                expectedMovie.imdbRating = movie.VoteAverage.ToString();
                expectedMovie.imdbVotes = movie.VoteCount.ToString();
                expectedMovie.imdbID = movie.ImdbId.ToString();
                expectedMovie.Type = "Movie";
                expectedMovie.Response = "True";
                expectedMovie.Showtype = "2D";
            }
            else return null;
            return expectedMovie;
        }

        // This code is to get from my created database
        public static MovieModel getFromMyDb(string title)
        {
            string result;
            using (var client = new WebClient())
            {
                Uri url = new Uri(Constants.MY_DB + title, UriKind.Absolute);
                result = client.DownloadString(url);
            }
            var movie = JsonConvert.DeserializeObject<MovieModel>(result);
            if(movie.Response == "True")
            {
                return movie;
            }
            return new MovieModel { Response = "False" };
        }

        // Give html page of the cineplex
        public static string getHtmlPage() {

            string cineplexUrl = Constants.CINEPLEX_LINK;

            string htmlPage = "";
            using (var client = new WebClient())
            {
                Uri url = new Uri(cineplexUrl, UriKind.Absolute);
                htmlPage = client.DownloadString(url);
            }
            return htmlPage;
        }

    }
}