using System.Collections.Generic;

namespace StarCineplex.Models
{

    public class SingleTime
    {
        public string Time { get; set; }
    }

    public class MovieTime
    {
        public string Title { get; set; }
        public List<SingleTime> Times { get; set; }
    }

    public class Venue
    {
        public string Hall { get; set; }
        public List<MovieTime> MoviesTime { get; set; }
    }

    public class ShowTime
    {
        public string Date { get; set; }
        public List<Venue> Venue { get; set; }

    }
}