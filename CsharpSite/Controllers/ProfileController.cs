using CsharpSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CsharpSite.Controllers
{
    public class ProfileController : BaseController
    {
        // GET: Profil
        public ActionResult Index(){
            User user = getAuthUser();
            List<Post> posts = user.Posts.ToList();
            
            posts.OrderByDescending( p => p.Publication_date );

            ViewBag.feed = posts;

            return View();
        }

        public ActionResult View(int userid) {
            User user = getAuthUser();
            if(user.UserId == userid) {
                RedirectToAction( "Index", "Profile" );
            }
            User target = db.Users.Single( u => u.UserId == userid );
            List<Post> posts = target.Posts.ToList();

            posts.OrderByDescending( p => p.Publication_date );

            ViewBag.feed = posts;
            ViewBag.targetUser = target;
            return View();
        }

        public ActionResult Edit() {
            return View();
        }

    }

    
}