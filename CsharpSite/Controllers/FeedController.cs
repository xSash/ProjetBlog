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
        private DB db = new DB();
        // GET: Feed
        public ActionResult Index()
        {
            Session.Add( "ConnUsr", db.Users.ToArray()[0] );

            User user = (User)Session["ConnUsr"];
            List<Post> posts = new List<Post>();
            foreach(var followed in user.Following) {
                posts.AddRange( followed.Posts );
            }
            posts.OrderByDescending( p => p.Publication_date );

            ViewBag.feed = posts;

            return View();
        }
        

    }
}