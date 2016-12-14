using CsharpSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        [HttpPost]
        public ActionResult GetFeed() {
            User user = getAuthUser();
            if (Request["UserId"] != null)
                user = db.Users.Find(int.Parse(Request["UserId"]));
            List<object> serializedfeed = new List<object>();
            List<Post> posts = user.Posts.ToList();

            foreach (var followed in user.Following) {
                posts.AddRange( followed.Posts );
            }
            foreach(Post p in posts) {
                serializedfeed.Add( p.Serialize() );
            }

            object json = new {
                data = serializedfeed
            };

            return Json( json );
        }

        


    }
}