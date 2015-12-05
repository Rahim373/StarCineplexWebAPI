using HtmlAgilityPack;
using StarCineplex.Models;
using System;
using System.Collections.Generic;

namespace StarCineplex.Classes
{
    public class Movies
    {
        private string pattern;

        public Movies(string pattern)
        {
            this.pattern = pattern;
        }

        public List<MovieModel> getMovieList()
        {
            List<MovieModel> MovieList = new List<MovieModel>();
            string htmlPage = Helper.getHtmlPage();
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlPage);

            foreach (HtmlNode singleNode in doc.DocumentNode.SelectSingleNode(pattern).ChildNodes)
            {
                try
                {
                    if (singleNode.HasAttributes)
                    {
                        string showType = PatternHelper.GetMovieType(singleNode.InnerText.Trim());
                        string movieName = PatternHelper.GetFilteredMovieName(singleNode.InnerText.Trim());
                        string movieUrl = PatternHelper.GetFilterMovieUrl(singleNode.InnerHtml);
                        MovieModel movie = Helper.getSingleMovie(movieName, movieUrl);
                        movie.Showtype = showType;
                        if (movie.Title != null)
                        {
                            MovieList.Add(movie);
                        }
                    }
                }
                catch (Exception e)  { }
            }
            return MovieList;
        }

        public static int getMovieCountCount(string pattern)
        {
            string htmlPage = Helper.getHtmlPage();
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlPage);
            int flag = 0;

            foreach (HtmlNode singleNode in doc.DocumentNode.SelectSingleNode(pattern).ChildNodes)
            {
                if (singleNode.HasAttributes) flag++;
            }
            return flag;
        }
    }

   
}