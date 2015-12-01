using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarCineplex.Classes
{
    public class Constants
    {
        public static string CINEPLEX_LINK = @"http://www.cineplexbd.com/";
        public static string SHOW_TIME = @"http://www.cineplexbd.com/index.php?visit=schedule/schedules";

        public static string MY_DB = @"http://movies.turn-bd.com/api.php?name=";
        public static string NOW_SHOWING_PATTERN = @"//*[@id='left_navigation']/div[1]/div[2]";
        public static string COMING_SOON_PATTERN = @"//*[@id='left_navigation']/div[2]/div[2]";
    }
}