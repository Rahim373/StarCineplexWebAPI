using StarCineplex.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StarCineplex.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            ViewBag.NowShowing = Movies.getMovieCountCount(Constants.NOW_SHOWING_PATTERN);
            ViewBag.ComingSoon = Movies.getMovieCountCount(Constants.COMING_SOON_PATTERN);
            return View();
        }
    }
}