using CsharpSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CsharpSite.Controllers
{
    public class BaseController : Controller
    {
        protected DB db = new DB();
        private static string[] admin_restricted_controllers = new string[] {
            "Admin"
        };
        private static string[] restricted_controllers = new string[] {
            "Feed",
            "Posts",
            "Profile",
            "Index",
            "Chat",
            "Follow",
            "Chat"
        };
        private static string[] restricted_actions = new string[] {
            //"Controller/Action"
            
        };

        protected User getAuthUser()
        {
            User user = ((Auth)Session[Auth.AUTH_USER_SESSION_NAME])?.User;
            if(user != null)
                user = db.Users.Single( u=> u.UserId == user.UserId ) ;
            return user;
        }
        protected void setAuthUser(User user) {
            Session[Auth.AUTH_USER_SESSION_NAME] = new Auth() { User = user };
        }

        protected override void OnActionExecuting( ActionExecutingContext filterContext ) {
            User user = getAuthUser();
            string controllername = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionname = filterContext.ActionDescriptor.ActionName;
            if ( (restricted_controllers.Contains( controllername )
                || restricted_actions.Contains( controllername + "/" + actionname )
                || admin_restricted_controllers.Contains( controllername ))
                && ( user == null 
                || !db.Users.Any(u => u.UserId == user.UserId 
                    && u.Username == user.Username ))
                 ) {

                filterContext.Result = RedirectToAction("Login", "Session");
                return;

            }else if (admin_restricted_controllers.Contains( controllername ) && !user.IsAdmin) {
                filterContext.Result = new HttpStatusCodeResult(404);
                return;
            }

            ViewBag.connUser = getAuthUser();
            ViewBag.reactionTypes = db.ReactionTypes.ToArray();
            ViewBag.Countries = db.Countries.ToArray();

        }

    }

    public class UserManager {

        public static bool IsValid(string username, string password) {
            using (DB db = new Models.DB()) {
                return db.Users.Any(u => u.Username == username
                    && u.Password == password);
            }
        }
    }

    public class Auth {
        public const string REMEMBER_CREDENTIALS_COOKIE_NAME = "crdrmmbr";
        public const string USRNAME_COOKIE_NAME = "usrNmC";
        public const string PW_COOKIE_NAME = "usrPwC";
        public const string AUTH_USER_SESSION_NAME = "AuthUser";
        public User User;
    }
}