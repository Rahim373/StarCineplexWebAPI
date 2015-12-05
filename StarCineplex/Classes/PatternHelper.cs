using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System;

namespace StarCineplex.Classes
{
    public static class PatternHelper
    {
        public static string GetFilteredMovieName(string movieName)
        {
            string pattern1 = @"\(\d{1}d\)\s*";
            string Pattern2 = @"\s$";

            movieName = Regex.Replace(movieName, pattern1, "", RegexOptions.IgnoreCase);
            movieName = Regex.Replace(movieName, Pattern2, "", RegexOptions.IgnoreCase);

            return movieName;
        }

        public static string GetMovieType(string movieName)
        {
            string pattern1 = @"\(\d{1}d\)\s*";
            string Pattern2 = @"\dd";
            if (Regex.IsMatch(movieName, pattern1, RegexOptions.IgnoreCase))
            {
                var matches1 = Regex.Matches(movieName, pattern1, RegexOptions.IgnoreCase);
                var matches2 = Regex.Matches(matches1[0].ToString(), Pattern2, RegexOptions.IgnoreCase);
                return matches2[0].ToString().ToUpper();
            }
            else
            {
                return "2D";
            }
        }

        internal static string GetFilterMovieUrl(string innerHtml)
        {
            var url = Regex.Matches(innerHtml, @"http.*id=\d*");
            var link = url[0].ToString();
            return link;
        }

        public static string getPosterUrlFromCineplex(string htmlPage)
        {
            var filtered = Regex.Matches(htmlPage, @"test.*http.*(jpg|png|bnp)");
            var poster = filtered[0].ToString();
            var filtered2 = Regex.Matches(poster, @"http:.*", RegexOptions.IgnoreCase);
            return filtered2[0].ToString();
        }

        public static string getPlotFromCineplex(string htmlPage)
        {
            var match1 = Regex.Matches(htmlPage, @"Plot.:\s\s*.*\.", RegexOptions.IgnoreCase);
            if (match1.Count > 0)
            {
                try
                {
                    string filtered = match1[0].ToString();
                    filtered = Regex.Replace(filtered, @"plot.*\s", "", RegexOptions.IgnoreCase);
                    filtered = Regex.Replace(filtered, @"\s\s+", " ", RegexOptions.IgnoreCase);
                    filtered = Regex.Replace(filtered, @"\.\.+\s.*>", ".", RegexOptions.IgnoreCase);
                    return filtered;
                }
                catch (Exception e)
                {
                    return "";
                }
            }
            return "";
        }

        public static string getDirectorFromCineplex(string htmlPage)
        {
            var match1 = Regex.Matches(htmlPage, @"Director.*\s\s*..*", RegexOptions.IgnoreCase);
            if (match1.Count > 0)
            {
                try
                {
                    string filtered = match1[0].ToString();
                    filtered = Regex.Replace(filtered, @"director", "", RegexOptions.IgnoreCase);
                    var match2 = Regex.Matches(filtered, @"\;.*");
                    var filtered2 = match2[0].ToString();
                    filtered2 = Regex.Replace(filtered2, @"\;", "", RegexOptions.IgnoreCase);
                    filtered2 = Regex.Replace(filtered2, @"\s*</td>", "", RegexOptions.IgnoreCase);
                    return filtered2;
                }
                catch (Exception e)
                {
                    return "";
                }
            }
            return "";
        }

        public static string getGenreFromCineplex(string htmlPage)
        {
            var match1 = Regex.Matches(htmlPage, @"Genre.*\s\s*..*", RegexOptions.IgnoreCase);
            if (match1.Count > 0)
            {
                try
                {
                    string filtered = match1[0].ToString();
                    filtered = Regex.Replace(filtered, @"Genre", "", RegexOptions.IgnoreCase);
                    var match2 = Regex.Matches(filtered, @"\;.*");
                    var filtered2 = match2[0].ToString();
                    filtered2 = Regex.Replace(filtered2, @"\s*</td>", "", RegexOptions.IgnoreCase);
                    filtered2 = Regex.Replace(filtered2, @"\;", "", RegexOptions.IgnoreCase);
                    return filtered2;
                }
                catch (Exception e)
                {
                    return "";
                }
            }
            return "";
        }
    }
}