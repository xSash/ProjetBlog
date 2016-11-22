using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CsharpSite.Models;
using System.Data.Entity;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web.Security;

namespace CsharpSite.Controllers
{
    public class SessionController : Controller
    {
        private DB db = new DB();
        // GET: Session
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ActionName("Register")]
        public ActionResult RegisterGet() {

            return View("Register");
        }

        [HttpPost]
        [ActionName("Register")]
        public ActionResult RegisterPost() {
            ActionResult result = null;
            string uname = Request.Form["username"];
            string passw = Request.Form["passw"];
            string cpassw= Request.Form["cpassw"];
            string email = Request.Form["email"];

            User user = new User();
            user.Password = passw;
            user.Username = uname;
            user.Email = email;
            user.Registration_date = DateTimeOffset.Now;

            try {
                
                db.Users.Add( user );
                db.SaveChanges();
                result = Json( new { status = "success", message = "registered successfully" } );
            } catch (Exception e) {
                result = Json( new { status = "error", message = "registration failed: "+e.Message } );
            }
            
            return result;
        }


        [HttpGet]
        [ActionName("Login")]
        public ActionResult LoginGet( string username = "CodeCap_Jeremi", string password = "password" ) {
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        public ActionResult LoginPost(string username = "CodeCap_Jeremi", string password = "password") {
            if (new UserManager().IsValid( username, password )) {
                FormsAuthenticationTicket tkt;
                string cookiestr;
                HttpCookie ck;
                tkt = new FormsAuthenticationTicket( 1, username, DateTime.Now,
                     DateTime.Now.AddMinutes( 30 ), true, "your custom data" );
                cookiestr = FormsAuthentication.Encrypt( tkt );
                ck = new HttpCookie( FormsAuthentication.FormsCookieName, cookiestr );
                if (true)
                    ck.Expires = tkt.Expiration;
                ck.Path = FormsAuthentication.FormsCookiePath;
                Response.Cookies.Add( ck );

                //FormsAuthentication.SetAuthCookie( username, false );

            }
            // invalid username or password
            ModelState.AddModelError( "", "invalid username or password" );
            return View();
        }
    }

    public class UserManager {
        
        public bool IsValid( string username, string password ) {
            using (DB db = new Models.DB())
            {
                return db.Users.Any( u => u.Username == username
                     && u.Password == password );
            }
        }
    }
}