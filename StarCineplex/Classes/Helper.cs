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
using TMDbLib.Objects.Credit;

namespace StarCineplex.Classes
{
    public class Helper
    {
        // Collect and control over movies
        public static MovieModel getSingleMovie(string movieName, string movieUrl)
        {
            MovieModel movie = getSingleFromTMDB(movieName);
            if (movie == null) movie = getFromCineplexUrl(movieName, movieUrl);
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
                try
                {
                    int id = results.Results[0].Id;
                    Movie movie = client.GetMovie(id);
                    if (movie.OriginalLanguage.Equals("hi")) return null;
                    List<Genre> genres = movie.Genres;
                    var credits = client.GetMovieCredits(id);
                    List<string> credit = getStringFromCrewList(credits);

                    expectedMovie.Title = title;
                    expectedMovie.Year = movie.ReleaseDate.Value.Year.ToString();
                    expectedMovie.Released = movie.ReleaseDate.Value.Date.ToString();
                    expectedMovie.Runtime = movie.Runtime.ToString() + " Minutes";
                    expectedMovie.Genre = getStringFromGenereList(movie.Genres);

                    expectedMovie.Actors = credit[0].ToString();

                    expectedMovie.Director = credit[1].ToString();

                    expectedMovie.Writer = credit[2].ToString();

                    expectedMovie.Plot = movie.Overview;
                    expectedMovie.Language = movie.OriginalLanguage;
                    if(movie.ProductionCountries.Count>0) expectedMovie.Country = movie.ProductionCountries[0].Name;
                    expectedMovie.Poster = Constants.POSTER_LINK_HOST_PATH + movie.PosterPath;
                    expectedMovie.imdbRating = movie.VoteAverage.ToString();
                    expectedMovie.imdbVotes = movie.VoteCount.ToString();
                    expectedMovie.imdbID = movie.ImdbId.ToString();
                    expectedMovie.Showtype = "2D";
                    return expectedMovie;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            else return null;
        }

        private static string getStringFromGenereList(List<Genre> genreList)
        {
            string data = "";
            foreach(Genre item in genreList)
            {
                data = data + item.Name.ToString() + ", ";
            }
            return getSimpleString(data);
        }

        private static List<string> getStringFromCrewList(Credits credits)
        {
            List<string> credit = new List<string>();

            // Actors
            string cast = "";
            if (credits.Cast.Count > 0)
            {
                for (int i = 0; i < 4 && i<credits.Cast.Count; i++)
                {
                    cast = cast + credits.Cast[i].Name.ToString() + ", ";
                }
            }
            credit.Add(getSimpleString(cast));



            // Director
            string director = "";
            if (credits.Crew.Count > 0)
            {
                for (int i = 0; i < credits.Crew.Count; i++)
                {
                    if (credits.Crew[i].Job.Equals("Director"))
                    {
                        director = director + credits.Crew[i].Name.ToString() + ", ";
                    }
                }
            }
            credit.Add(getSimpleString(director));

            // Writer
            string writer = "";
            if (credits.Crew.Count > 0)
            {
                for (int i = 0; i < credits.Crew.Count; i++)
                {
                    if (credits.Crew[i].Department.Equals("Writing"))
                    {
                        writer = writer + credits.Crew[i].Name.ToString() + ", ";
                    }
                }
            }
            credit.Add(getSimpleString(writer));

            return credit;
        }

        private static string getSimpleString(string data)
        {
            try
            {
                int flag = data.LastIndexOf(", ");
                data = data.Remove(flag, 2);
            }catch(Exception e) {}
            return data;
        }

        // This code is to get from my created database
        public static MovieModel getFromCineplexUrl(string title, string movieUrl)
        {
            string htmlPage = "";
            using (var client = new WebClient())
            {
                Uri url = new Uri(movieUrl, UriKind.Absolute);
                htmlPage = client.DownloadString(url);
            }
            MovieModel movie = new MovieModel();

            movie.Title = title;
            movie.Poster = PatternHelper.getPosterUrlFromCineplex(htmlPage);
            movie.Plot = PatternHelper.getPlotFromCineplex(htmlPage);
            movie.Director = PatternHelper.getDirectorFromCineplex(htmlPage);
            movie.Genre = PatternHelper.getGenreFromCineplex(htmlPage);


            return movie;
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