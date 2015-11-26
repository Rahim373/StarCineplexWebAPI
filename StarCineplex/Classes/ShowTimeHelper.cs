using HtmlAgilityPack;
using StarCineplex.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace StarCineplex.Classes

{
    public class ShowTimeHelper
    {
        List<ShowTime> shows = new List<ShowTime>();

        public List<ShowTime> GetShowTime()
        {
            GetTable(LoadWebPage());
            return shows;
        }

        private string LoadWebPage()
        {
            string result = "";
            using (WebClient client = new WebClient())
            {
                result = client.DownloadString(Constants.SHOW_TIME);
            }
            return result.ToString();
        }


        private void GetTable(string webPage)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(webPage);

            try {
                // Getting all tables
                var nodes = doc.DocumentNode.SelectSingleNode(@"//*[@id='main_body']/div[2]/div[2]").ChildNodes;

                int flag = 1;
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (nodes[i].HasAttributes)
                    {
                        List<Venue> Venues = new List<Venue>();
                        ShowTime show = new ShowTime();
                        // Getting Dates
                        var totalNode = nodes[i].ChildNodes;
                        var dateNode = totalNode[i].SelectSingleNode("//div[" + flag + "]/table[1]");
                        string date = Regex.Replace(dateNode.InnerText.Trim().ToString(), @"\s\s+", "");
                        show.Date = date.ToString();


                        #region Public Hall Movies
                        // Getting Regular Hall movie
                        Venue venue = new Venue();
                        venue.Hall = "Hall#1 - Hall#4";
                        var regularNodes = totalNode[i].SelectSingleNode("//div[" + flag + "]/table[2]").ChildNodes;
                        List<MovieTime> moviesTime = new List<MovieTime>();
                        for (int j = 7; j < regularNodes.Count; j += 4)
                        {
                            MovieTime movieTime = new MovieTime();

                            var singleRow = regularNodes[j];
                            var row = singleRow.InnerText.Trim().ToString();
                            var spilledData = Regex.Split(row, @"\s\s+");
                            movieTime.Title = PatternHelper.GetFilteredMovieName(spilledData[0]);

                            List<SingleTime> TimeList = new List<SingleTime>();
                            for (int k = 1; k < spilledData.Length; k++)
                            {
                                string time = spilledData[k].ToString();
                                SingleTime singleTime = new SingleTime();
                                singleTime.Time = time;
                                TimeList.Add(singleTime);
                            }
                            movieTime.Times = TimeList;
                            moviesTime.Add(movieTime);
                        }
                        venue.MoviesTime = moviesTime;          // Regular Movies added to Venue
                        Venues.Add(venue);
                        #endregion

                        #region VIP HALL
                        // Getting VIP Hall movie
                        Venue venue1 = new Venue();
                        venue1.Hall = "Hall#5";
                        var regularNodes1 = totalNode[i].SelectSingleNode("//div[" + flag + "]/table[3]").ChildNodes;
                        List<MovieTime> moviesTime1 = new List<MovieTime>();
                        for (int j = 7; j < regularNodes1.Count; j += 4)
                        {
                            MovieTime movieTime = new MovieTime();

                            var singleRow = regularNodes1[j];
                            var row = singleRow.InnerText.Trim().ToString();
                            var spilledData = Regex.Split(row, @"\s\s+");
                            movieTime.Title = PatternHelper.GetFilteredMovieName(spilledData[0]);

                            List<SingleTime> TimeList = new List<SingleTime>();
                            for (int k = 1; k < spilledData.Length; k++)
                            {
                                string time = spilledData[k].ToString();
                                SingleTime singleTime = new SingleTime();
                                singleTime.Time = time;
                                TimeList.Add(singleTime);
                            }
                            movieTime.Times = TimeList;
                            moviesTime1.Add(movieTime);
                        }
                        venue1.MoviesTime = moviesTime1;          // Regular Movies added to Venue
                        Venues.Add(venue1);
                        #endregion

                        #region Premium Hall
                        // Getting Premium Hall movie
                        Venue venue2 = new Venue();
                        venue2.Hall = "Hall#6";
                        var regularNodes2 = totalNode[i].SelectSingleNode("//div[" + flag + "]/table[4]").ChildNodes;
                        List<MovieTime> moviesTime2 = new List<MovieTime>();
                        for (int j = 7; j < regularNodes2.Count; j += 4)
                        {
                            MovieTime movieTime = new MovieTime();

                            var singleRow = regularNodes2[j];
                            var row = singleRow.InnerText.Trim().ToString();
                            var spilledData = Regex.Split(row, @"\s\s+");
                            movieTime.Title = PatternHelper.GetFilteredMovieName(spilledData[0]);

                            List<SingleTime> TimeList = new List<SingleTime>();
                            for (int k = 1; k < spilledData.Length; k++)
                            {
                                string time = spilledData[k].ToString();
                                SingleTime singleTime = new SingleTime();
                                singleTime.Time = time;
                                TimeList.Add(singleTime);
                            }
                            movieTime.Times = TimeList;
                            moviesTime2.Add(movieTime);
                        }
                        venue2.MoviesTime = moviesTime2;          // Regular Movies added to Venue
                        Venues.Add(venue2);
                        #endregion

                        show.Venue = Venues;
                        flag++;
                        shows.Add(show);
                    }
                }
            } catch ( Exception ex)
            {

            }
        }

    }

   
}
