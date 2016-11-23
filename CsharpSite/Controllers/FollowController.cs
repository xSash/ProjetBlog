using CsharpSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CsharpSite.Controllers
{
    public class FollowController : BaseController
    {
        // GET: Follow
        public ActionResult Index()
        {
            User user = ((Auth)Session[Auth.AUTH_USER_SESSION_NAME])?.User;

            ViewBag.followers = user.Followers;
            ViewBag.following = user.Following;

            return View();
        }


        [HttpPost]
        [ActionName("Follow")]
        public ActionResult Follow(int userIdToFollow ) {
            Object json_string = new { state = "success", message = "user followed successfully" };
            User user = ((Auth)Session[Auth.AUTH_USER_SESSION_NAME])?.User;
            if(user == null) {
                json_string = new { state = "error", message = "Not Logged in" };
            }
            User[] fTemp = db.Users.Where( u => u.UserId == userIdToFollow )?.ToArray();
            User followed = fTemp.Count() == 0 ? null : fTemp[0];
            if(followed == null) {
                json_string = new { state = "error", message = "User to follow does not exist" };
            }

            db.Users.Attach( followed );
            user.Following.Add( followed );

            db.SaveChanges();

            return Json(json_string); 
        }

        [HttpPost]
        [ActionName( "Unfollow" )]
        public ActionResult Unfollow( int userIdToUnFollow ) {
            Object json_string = new { state = "success", message = "user unfollowed successfully" };
            User user = ((Auth)Session[Auth.AUTH_USER_SESSION_NAME])?.User;
            if (user == null) {
                json_string = new { state = "error", message = "Not Logged in" };
                return Json( json_string );
            }
            User[] fTemp = db.Users.Where( u => u.UserId == userIdToUnFollow )?.ToArray();
            User followed = fTemp.Count() == 0 ? null : fTemp[0];
            if (followed == null) {
                json_string = new { state = "error", message = "User to follow does not exist" };
                return Json( json_string );
            }

            db.Users.Attach( followed );
            user.Following.Remove(followed);

            db.SaveChanges();

            return Json( json_string );
        }

    }
}