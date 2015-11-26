using HtmlAgilityPack;
using System.Text.RegularExpressions;

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
            if(Regex.IsMatch(movieName, pattern1, RegexOptions.IgnoreCase))
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


        public static string FindRatingFromSearchResult(string searchResult)
        {
            string pattern1 = @"value\W>(\d{1}|\d\.\d)<";
            string pattern2 = @"\d\.\d|\d{1}";
            if (Regex.IsMatch(searchResult, pattern1))
            {
                var result = Regex.Matches(searchResult, pattern1, RegexOptions.IgnoreCase);
                var finalResult = Regex.Matches(result[0].ToString(), pattern2, RegexOptions.IgnoreCase);
                return finalResult[0].ToString();
            }
            else
            {
                return "0";
            }

        }


        public static string GetMovieImage(string htmlPage)
        {
            string pattern1 = @"alt.*Poster.*\s*.*\s*.*";
            string pattern2 = @"http.*jpg";
            string PosterLink = "";
            var firstFilter = Regex.Matches(htmlPage, pattern1);
            var secondFilter = Regex.Matches(firstFilter[0].ToString(), pattern2);
            PosterLink = secondFilter[0].ToString();
            return PosterLink; 
        }

        public static string getMovieNameFromGoogle(string htmlData)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlData);
            string movieName = "";

            string patterm1 = @"height\S*\salt\S*";
            var filterd1 = Regex.IsMatch(htmlData, patterm1, RegexOptions.IgnoreCase);
            if (filterd1)
            {
                string data = Regex.Matches(htmlData, patterm1, RegexOptions.IgnoreCase)[0].ToString();
                string filteranothertime = Regex.Matches(data, @"[A-Z].*")[0].ToString();
                movieName = filteranothertime.Replace("\"", ""); 
            }

            else
            {
                movieName = "Bangla";
            }

            return movieName;

        }

    }
}