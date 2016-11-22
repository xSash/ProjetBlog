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
        private static string[] restricted_controllers = new string[] {
            "Feed",
            "Post",
            "Profile",
            "Index",
            "Chat",
            "Follow"
        };
        private static string[] restricted_actions = new string[] {
            //"Controller/Action"
            
        };

        protected override void OnActionExecuting( ActionExecutingContext filterContext ) {
            User user = ((Auth)Session[Auth.AUTH_USER_SESSION_NAME])?.User;
            if( (restricted_controllers.Contains( filterContext.ActionDescriptor.ControllerDescriptor.ControllerName )
                || restricted_actions.Contains( filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "/" + filterContext.ActionDescriptor.ActionName ))
                && ( user == null 
                || !db.Users.Any(u => u.UserId == user.UserId 
                    && u.Username == user.Username )) ) {

                filterContext.Result = RedirectToAction("Login", "Session");
                return;

            }

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