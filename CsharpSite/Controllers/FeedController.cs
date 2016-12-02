using CsharpSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CsharpSite.Controllers
{
    public class FeedController : BaseController
    {
        // GET: Feed
        public ActionResult Index()
        {
            User user = getAuthUser();
            List<Post> posts = user.Posts.ToList();
            foreach(var followed in user.Following) {
                posts.AddRange( followed.Posts );
            }
            posts.OrderByDescending( p => p.Publication_date );

            ViewBag.feed = posts;

            return View();
        }
        

    }
}