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
        public ActionResult Index(){
            User user = ((Auth)Session[Auth.AUTH_USER_SESSION_NAME])?.User;
            ViewBag.followers = user.Followers;
            ViewBag.following = user.Following;

            return View();
        }

        [HttpPost]
        public ActionResult SearchUser() {
            string searchstring = Request.Form["search"];
            List<User> match = db.Users.Where( u => u.Username.Contains( searchstring ) || u.Email.Contains( searchstring ) ).ToList();
            List<object> json = new List<object>();
            foreach(var u in match) {
                json.Add( u.Serialize() );
            }

            return Json(new { searchString = searchstring, data = json }  );
        }

        [HttpPost]
        public ActionResult GetListJson() {
            User user = ((Auth)Session[Auth.AUTH_USER_SESSION_NAME])?.User;
            List<object> serializedFollowing = new List<object>();
            List<object> serializedFollower = new List<object>();
            foreach (var followed in user.Following) {
                serializedFollowing.Add( followed.Serialize() );
            }
            foreach (var follower in user.Followers) {
                serializedFollower.Add( follower.Serialize() );
            }
            return Json(new { following = serializedFollowing.ToArray(), followers = serializedFollower.ToArray() });
        }


        [HttpPost]
        [ActionName("Follow")]
        public ActionResult Follow(int userIdToFollow ) {
            Object json_string = new { state = "success", message = "user followed successfully" };
            int uid = getAuthUser()?.UserId ?? -1;
            User user = db.Users.First( u => u.UserId == uid );
            if (user == null) {
                json_string = new { state = "error", message = "Not Logged in" };
            } else {
                User[] fTemp = db.Users.Where( u => u.UserId == userIdToFollow )?.ToArray();
                User followed = fTemp.Count() == 0 ? null : fTemp[0];
                if (followed == null) {
                    json_string = new { state = "error", message = "User to follow does not exist" };
                }else if (user.UserId == followed.UserId) {
                    json_string = new { state = "error", message = "cant follow yourself, dumbass..." };
                }else {
                    user.Following.Add( followed );
                    followed.Following.Add( user );

                    db.SaveChanges();
                    setAuthUser( user );
                }
            }
           
            return Json(json_string); 
        }

        [HttpPost]
        [ActionName( "Unfollow" )]
        public ActionResult Unfollow( int userIdToUnFollow ) {
            Object json_string = new { state = "success", message = "user unfollowed successfully" };
            int uid = getAuthUser()?.UserId ?? -1;
            User user = db.Users.First( u => u.UserId == uid );
            if (user == null) {
                json_string = new { state = "error", message = "Not Logged in" };
                return Json( json_string );
            }else {
                User[] fTemp = db.Users.Where( u => u.UserId == userIdToUnFollow )?.ToArray();
                User followed = fTemp.Count() == 0 ? null : fTemp[0];
                if (followed == null) {
                    json_string = new { state = "error", message = "User to follow does not exist" };
                    return Json( json_string );
                } else if (user.UserId == followed.UserId) {
                    json_string = new { state = "error", message = "cant unfollow yourself, dumbass..." };
                } else {
                    user.Following.Remove( followed );
                    followed.Following.Remove( user );

                    db.SaveChanges();
                    setAuthUser( user );
                }
            }
            
            return Json( json_string );
        }

    }
}